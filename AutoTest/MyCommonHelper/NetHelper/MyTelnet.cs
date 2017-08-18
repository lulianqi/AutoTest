#define INTEST

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace MyCommonHelper.NetHelper
{
    public class MyTelnet
    {
        #region telnet的数据定义
        /// <summary>        
        /// 标志符,代表是一个TELNET 指令        
        /// </summary>        
        readonly byte IAC = 255;
        /// <summary>        
        /// 表示一方要求另一方使用，或者确认你希望另一方使用指定的选项。
        /// </summary>        
        readonly byte DO = 253;
        /// <summary>
        /// 表示一方要求另一方停止使用，或者确认你不再希望另一方使用指定的选项。       
        /// </summary>       
        readonly byte DONT = 254;
        /// <summary>
        /// 表示希望开始使用或者确认所使用的是指定的选项。
        /// </summary>
        readonly byte WILL = 251;
        /// <summary>
        /// 表示拒绝使用或者继续使用指定的选项。
        /// </summary>
        readonly byte WONT = 252;
        /// <summary>
        /// 表示后面所跟的是对需要的选项的子谈判
        /// </summary>
        readonly byte SB = 250;
        /// <summary>
        /// 子谈判参数的结束
        /// </summary>
        readonly byte SE = 240;
        const byte IS = 0;
        const byte SEND = 1;
        const byte INFO = 2;
        const byte VAR = 0;
        const byte VALUE = 1;
        const byte ESC = 2;
        const byte USERVAR = 3;
        /// <summary> 
        /// 流
        /// /// </summary>
        byte[] telnetReceiveBuff = new byte[1024*128];
        /// <summary>
        /// 收到的控制信息
        /// </summary>
        private ArrayList m_ListOptions = new ArrayList();
        /// <summary>
        /// 存储准备发送的信息
        /// </summary>
        string m_strResp;
        /// <summary>
        /// 一个Socket套接字
        /// </summary>
        private Socket mySocket;
        #endregion

        private IPEndPoint iep;
        private int timeout;
        private Encoding encoding = Encoding.ASCII;

        private string nowErrorMes;

        private string strWorkingData = "";     // 保存从服务器端接收到的数据
        private string strFullLog = "";
        //====================================================           

        private string strWorkingDataX = "";
        //用于获取当前工作的数据内容

        public string WorkingData
        {
            get { return strWorkingDataX; }
        }
        
        /// <summary>
        /// 获取最近的错误信息
        /// </summary>
        public string NowErrorMes
        {
            get { return nowErrorMes; }
        }

        /// <summary>
        /// 可能会引发异常
        /// </summary>
        /// <param name="Address">主机ip地址 (可以使用Dns.GetHostEntry(host)获取使用主机名的ip)</param>
        /// <param name="Port">端口</param>
        /// <param name="CommandTimeout">超时</param>
        public MyTelnet(string Address, int Port, int CommandTimeout)
        {
            iep = new IPEndPoint(IPAddress.Parse(Address), Port);
            timeout = CommandTimeout;
        }

        public MyTelnet(IPEndPoint yourEp, int CommandTimeout)
        {
            iep = yourEp;
            timeout = CommandTimeout;
        }

        /// <summary>        
        /// 连接telnet     
        /// </summary>       
        public bool Connect()
        {
            //启动socket 进行telnet操作   
            try
            {
                // Try a blocking connection to the server
                mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mySocket.Connect(iep);

                //异步回调
                AsyncCallback recieveData = new AsyncCallback(OnRecievedData);
                mySocket.BeginReceive(telnetReceiveBuff, 0, telnetReceiveBuff.Length, SocketFlags.None, recieveData, mySocket);

                return true;
            }
            catch (Exception ex)
            {
                nowErrorMes = ex.Message;
                return false;
            }
        }

        /// <summary>        
        /// 当接收完成后,执行的方法(供委托使用)       
        /// </summary>      
        /// <param name="ar"></param>       
        private void OnRecievedData(IAsyncResult ar)
        {
            try
            {
                //从参数中获得给的socket 对象           
                Socket so = (Socket)ar.AsyncState;

                //EndReceive方法为结束挂起的异步读取         
                int recLen = so.EndReceive(ar);
                //如果有接收到数据的话            
                if (recLen > 0)
                {
#if INTEST
                    byte[] tempByte = new byte[recLen];
                    Array.Copy(telnetReceiveBuff, 0, tempByte, 0, recLen);
                    System.Diagnostics.Debug.WriteLine("-------------------------------------");
                    System.Diagnostics.Debug.WriteLine(MyBytes.ByteToHexString(tempByte, HexaDecimal.hex16, ShowHexMode.space));
                    System.Diagnostics.Debug.WriteLine(MyBytes.ByteToHexString(tempByte, HexaDecimal.hex10, ShowHexMode.space));
                    System.Diagnostics.Debug.WriteLine(Encoding.ASCII.GetString(tempByte));
                    System.Diagnostics.Debug.WriteLine(Encoding.UTF8.GetString(tempByte));
#endif
                   
                    try
                    {

                        byte[] tempShowByte = DealOptions(tempByte);
                        if (tempShowByte.Length>0)
                        {
                            strWorkingData = encoding.GetString(tempShowByte);
                            strFullLog += mOutText;
                        }
                        RespondToOptions();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("接收数据的时候出错了! " + ex.Message);
                    }
                }
                else// 如果没有接收到任何数据的话           
                {
                    // 关闭连接            
                    // 关闭socket               
                    so.Shutdown(SocketShutdown.Both);
                    so.Close();
                }
            }
            catch { }
        }
        /// <summary>        
        ///  发送数据的函数       
        /// </summary>        
        private void RespondToOptions()
        {
            try
            {
                //声明一个字符串,来存储 接收到的参数            
                string strOption;
                /*               
                 * 此处的控制信息参数,是之前接受到信息之后保存的              
                 * 例如 255   253   23   等等                                
                 */
                for (int i = 0; i < m_ListOptions.Count; i++)
                {
                    //获得一个控制信息参数                   
                    strOption = (string)m_ListOptions[i];
                    //根据这个参数,进行处理                   
                    ArrangeReply(strOption);
                }
                DispatchMessage(m_strResp);
                m_strResp = "";
                m_ListOptions.Clear();
            }
            catch (Exception ers)
            {
                Console.WriteLine("出错了,在回发数据的时候 " + ers.Message);

            }
        }
        
        /// <summary>        
        /// 处理原始报文，提取可显示数据，及控制命令   
        ///</summary>       
        ///<param name="yourRawBytes">原始数据</param>     
        /// <returns>可显示数据</returns>     
        private byte[] DealOptions(byte[] yourRawBytes)
        {

            List<byte> showByteList = new List<byte>();

            for (int i = 0; i < yourRawBytes.Length;i++ )
            {
                if(yourRawBytes[i]==IAC)
                {
                    if ((i + 1) >= yourRawBytes.Length)
                    {
                        throw new Exception("find error IAC data , no data after IAC");
                    }
                    byte nextByte = yourRawBytes[i + 1];
                    if (nextByte == DO || nextByte == DONT || nextByte == WILL || nextByte == WONT)
                    {
                        if((i + 2)<yourRawBytes.Length)
                        {
                            byte[] tempOptionCmd = new byte[] { yourRawBytes[i], yourRawBytes[i + 1], yourRawBytes[i + 2] };
                            m_ListOptions.Add(tempOptionCmd);
                        }
                        else
                        {
                            throw new Exception("find error IAC data ,it is less the 3 byte");
                        }
                        i = i + 2;
                    }
                    //如果IAC后面又跟了个IAC (255)  
                    else if (nextByte == IAC)
                    {
                        showByteList.Add(yourRawBytes[i]);
                        i = i + 1;
                    }
                    //如果IAC后面跟的是SB(250)       
                    else if (nextByte == SB)
                    {
                        int sbEndIndex = yourRawBytes.MyIndexOf(SE, i + 1);
                        if (sbEndIndex>0)
                        {
                            byte[] tempSBOptionCmd = new byte[sbEndIndex-i];
                            Array.Copy(yourRawBytes, i, tempSBOptionCmd, 0, sbEndIndex - i);
                            m_ListOptions.Add(tempSBOptionCmd);
                        }
                        else
                        {
                            throw new Exception("find error SB data ,can not find SE");
                        }
                    }
                    else
                    {
                        throw new Exception("find error IAC data ,the next byte is error");
                    }
                }
                else
                {
                    showByteList.Add(yourRawBytes[i]);
                }
            }
            return showByteList.ToArray();
        }
        
       
        #region magic Function
        //解析传过来的参数,生成回发的数据到m_strResp   
        private void ArrangeReply(string strOption)
        {
            try
            {
                Char Verb;
                Char Option;
                Char Modifier;
                Char ch;
                bool bDefined = false;
                //排错选项,无啥意义              
                if (strOption.Length < 3) return;
                //获得命令码               
                Verb = strOption[1];
                //获得选项码             
                Option = strOption[2];
                //如果选项码为 回显(1) 或者是抑制继续进行(3)
                if (Option == 1 || Option == 3)
                {
                    bDefined = true;
                }
                // 设置回发消息,首先为标志位255         
                m_strResp += IAC;
                //如果选项码为 回显(1) 或者是抑制继续进行(3) ==true   
                if (bDefined == true)
                {
                    #region 继续判断
                    //如果命令码为253 (DO)             
                    if (Verb == DO)
                    {
                        //我设置我应答的命令码为 251(WILL) 即为支持 回显或抑制继续进行     
                        ch = WILL;
                        m_strResp += ch;
                        m_strResp += Option;
                    }
                    //如果命令码为 254(DONT)     
                    if (Verb == DONT)
                    {
                        //我设置我应答的命令码为 252(WONT) 即为我也会"拒绝启动" 回显或抑制继续进行
                        ch = WONT;
                        m_strResp += ch;
                        m_strResp += Option;
                    }
                    //如果命令码为251(WILL)   
                    if (Verb == WILL)
                    {
                        //我设置我应答的命令码为 253(DO) 即为我认可你使用回显或抑制继续进行 
                        ch = DO;
                        m_strResp += ch;
                        m_strResp += Option;
                        //break;               
                    }
                    //如果接受到的命令码为251(WONT)         
                    if (Verb == WONT)
                    {
                        //应答  我也拒绝选项请求回显或抑制继续进行       
                        ch = DONT;
                        m_strResp += ch;
                        m_strResp += Option;
                        //break;            
                    }
                    //如果接受到250(sb,标志子选项开始)            
                    if (Verb == SB)
                    {
                        /*                  
                         * 因为启动了子标志位,命令长度扩展到了4字节,                    
                         * 取最后一个标志字节为选项码                     
                         * 如果这个选项码字节为1(send)                    
                         * 则回发为 250(SB子选项开始) + 获取的第二个字节 + 0(is) + 255(标志位IAC) + 240(SE子选项结束)               
                         */
                        Modifier = strOption[3];
                        if (Modifier == SEND)
                        {
                            ch = SB;
                            m_strResp += ch;
                            m_strResp += Option;
                            m_strResp += IS;
                            m_strResp += IAC;
                            m_strResp += SE;
                        }
                    }
                    #endregion
                }
                else //如果选项码不是1 或者3 
                {
                    #region 底下一系列代表,无论你发那种请求,我都不干
                    if (Verb == DO)
                    {
                        ch = WONT;
                        m_strResp += ch;
                        m_strResp += Option;
                    }
                    if (Verb == DONT)
                    {
                        ch = WONT;
                        m_strResp += ch;
                        m_strResp += Option;
                    }
                    if (Verb == WILL)
                    {
                        ch = DONT;
                        m_strResp += ch;
                        m_strResp += Option;
                    }
                    if (Verb == WONT)
                    {
                        ch = DONT;
                        m_strResp += ch;
                        m_strResp += Option;
                    }
                    #endregion
                }
            }
            catch (Exception eeeee)
            {
                throw new Exception("解析参数时出错:" + eeeee.Message);
            }
        }
        /// <summary>     
        /// 将信息转化成charp[] 流的形式,使用socket 进行发出   
        /// 发出结束之后,使用一个匿名委托,进行接收,  
        /// 之后这个委托里,又有个委托,意思是接受完了之后执行OnRecieveData 方法 
        ///       
        /// </summary>       
        /// <param name="strText"></param>  
        void DispatchMessage(string strText)
        {
            try
            {
                //申请一个与字符串相当长度的char流      
                Byte[] smk = new Byte[strText.Length];
                for (int i = 0; i < strText.Length; i++)
                {
                    //解析字符串,将其存储到char流中去   
                    Byte ss = Convert.ToByte(strText[i]);
                    smk[i] = ss;
                }
                //发送char流,之后发送完毕后执行委托中的方法(此处为匿名委托)    
                IAsyncResult ar2 = mySocket.BeginSend(smk, 0, smk.Length, SocketFlags.None, delegate(IAsyncResult ar)
                {
                    //当执行完"发送数据" 这个动作后                  
                    // 获取Socket对象,对象从beginsend 中的最后个参数上获得          
                    Socket sock1 = (Socket)ar.AsyncState;
                    if (sock1.Connected)//如果连接还是有效                    
                    {
                        //这里建立一个委托      
                        AsyncCallback recieveData = new AsyncCallback(OnRecievedData);

                        sock1.BeginReceive(telnetReceiveBuff, 0, telnetReceiveBuff.Length, SocketFlags.None, recieveData, sock1);
                      
                    }
                }, mySocket);

                mySocket.EndSend(ar2);
            }
            catch (Exception ers)
            {
                Console.WriteLine("出错了,在回发数据的时候:" + ers.Message);
            }
        }

        /// <summary>
        /// 等待指定的字符串返回
        /// </summary>
        /// <param name="DataToWaitFor">等待的字符串</param>
        /// <returns>返回0</returns>
        public int WaitFor(string DataToWaitFor)
        {
            long lngStart = DateTime.Now.AddSeconds(this.timeout).Ticks;
            long lngCurTime = 0;

            while (strWorkingData.ToLower().IndexOf(DataToWaitFor.ToLower()) == -1)
            {
                lngCurTime = DateTime.Now.Ticks;
                if (lngCurTime > lngStart)
                {
                    throw new Exception("Timed Out waiting for : " + DataToWaitFor);
                }
                Thread.Sleep(1);
            }
            strWorkingData = "";
            return 0;
        }

        public void Send(string message)
        {
            DispatchMessage(message);
            //因为每发送一行都没有发送回车,故在此处补上         
            DispatchMessage("\r\n");
        }
        #endregion

        /// <summary>
        /// 取完整日志
        /// </summary>
        public string SessionLog
        {
            get
            {
                return strFullLog;
            }
        }

        //======================================================================================
        /// <summary>
        /// 字符串编码转换，解决汉字显示乱码问题。

        /// 原始字符串中的汉字存储的是汉字内码，此代码实质是将汉字内码转换为GB2312编码。（夏春涛20110531）
        /// </summary>
        /// <param name="str_origin">需要转换的字符串</param>
        /// <returns>转换后的字符串</returns>
        private string ConvertToGB2312(string str_origin)
        {
            char[] chars = str_origin.ToCharArray();
            byte[] bytes = new byte[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                int c = (int)chars[i];
                bytes[i] = (byte)c;
            }
            Encoding Encoding_GB2312 = Encoding.GetEncoding("GB2312");
            string str_converted = Encoding_GB2312.GetString(bytes);
            return str_converted;
        }
        //======================================================================================
    }
}