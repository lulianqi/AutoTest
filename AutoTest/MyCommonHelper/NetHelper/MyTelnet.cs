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
        const byte IAC = 255;
        /// <summary>        
        /// 表示一方要求另一方使用，或者确认你希望另一方使用指定的选项。
        /// </summary>        
        const byte DO = 253;
        /// <summary>
        /// 表示一方要求另一方停止使用，或者确认你不再希望另一方使用指定的选项。       
        /// </summary>       
        const byte DONT = 254;
        /// <summary>
        /// 表示希望开始使用或者确认所使用的是指定的选项。
        /// </summary>
        const byte WILL = 251;
        /// <summary>
        /// 表示拒绝使用或者继续使用指定的选项。
        /// </summary>
        const byte WONT = 252;
        /// <summary>
        /// 表示后面所跟的是对需要的选项的子谈判
        /// </summary>
        const byte SB = 250;
        /// <summary>
        /// 子谈判参数的结束
        /// </summary>
        const byte SE = 240;

        //Assigned Number

        /// <summary>
        /// 回显
        /// </summary>
        const byte SHOWBACK = 1;
        /// <summary>
        /// 抑制继续进行
        /// </summary>
        const byte RESTRAIN = 3;
        /// <summary>
        /// 终端类型
        /// </summary>
        const byte TERMINAL = 24;


        //字选项协商

        // some constants
        const byte ESC = 27;
        const byte CR = 13;
        const byte LF = 10;
        const String F1 = "\033OP"; // function key
        const String F2 = "\033OQ";
        const String F3 = "\033OR";
        const String F4 = "\033OS";
        const String F5 = "\033[15~";
        const String F6 = "\033[17~";
        const String F7 = "\033[18~";
        const String F8 = "\033[19~";
        const String F9 = "\033[20~";
        const String F10 = "\033[21~";
        const String F11 = "\033[23~";
        const String F12 = "\033[24~";

        const string ENDOFLINE = "\r\n"; // CR LF
        /// <summary> 
        /// 流
        /// /// </summary>
        byte[] telnetReceiveBuff = new byte[1024*128];
        /// <summary>
        /// 收到的控制信息
        /// </summary>
        private ArrayList optionsList = new ArrayList();
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

        private string nowShowData = "";     
        private StringBuilder allShowData = new StringBuilder();


        public string WorkingData
        {
            get { return nowShowData; }
        }


        public string SessionLog
        {
            get
            {
                return allShowData.ToString();
            }
        }

        /// <summary>
        /// 获取最近的错误信息
        /// </summary>
        public string NowErrorMes
        {
            get { return nowErrorMes; }
        }

        private void ReportErrorMes(string errorMes)
        {

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

                    byte[] tempShowByte = DealRawBytes(tempByte);
                    if (tempShowByte.Length>0)
                    {
                        nowShowData = encoding.GetString(tempShowByte);
                        allShowData.Append(nowShowData);
                    }
                    DealOptions();
                }
                catch (Exception ex)
                {
                    throw new Exception("控制选项错误 " + ex.Message);
                }
            }
            else// 如果没有接收到任何数据， 关闭连接           
            {          
                so.Shutdown(SocketShutdown.Both);
                so.Close();
            }
           
        }
        /// <summary>        
        ///  发送数据的函数       
        /// </summary>        
        private void DealOptions()
        {
            if (optionsList.Count>0)
            {
                byte[] nowResponseOption=null;
                foreach(byte[] tempOption in optionsList)
                {
                    nowResponseOption = GetResponseOption(tempOption);
                    if(nowResponseOption!=null)
                    {
                        DispatchMessage(nowResponseOption);
                    }
                }
                optionsList.Clear();
            }
        }
        
        /// <summary>        
        /// 处理原始报文，提取可显示数据，及控制命令   
        ///</summary>       
        ///<param name="yourRawBytes">原始数据</param>     
        /// <returns>可显示数据</returns>     
        private byte[] DealRawBytes(byte[] yourRawBytes)
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
                            optionsList.Add(tempOptionCmd);
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
                            optionsList.Add(tempSBOptionCmd);
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

        /// <summary>
        /// 获取协商答复
        /// </summary>
        /// <param name="optionBytes">协商</param>
        /// <returns>答复（无法答复或错误返回null）</returns>
        private byte[] GetResponseOption(byte[] optionBytes)
        {

            byte[] responseOption=new byte[3];
            responseOption[0]=IAC;
            //协商选项命令为3字节，附加选项超过3个             
            if (optionBytes.Length < 3)
            {
                ReportErrorMes(string.Format("error option by errer length with :{0}",MyBytes.ByteToHexString(optionBytes,HexaDecimal.hex16,ShowHexMode.space)));
                return null;
            }
            if(optionBytes[0]==IAC)
            {
                switch(optionBytes[1])
                {
                    //WILL： 发送方本身将激活( e n a b l e )选项
                    case WILL:
                        if(optionBytes[2]==SHOWBACK||optionBytes[2]==SHOWBACK)
                        {
                            responseOption[1]=optionBytes[1];
                            responseOption[2]=DO;
                        }
                        else if(optionBytes[2]==TERMINAL)
                        {
                            responseOption[1]=optionBytes[1];
                            responseOption[2]=WONT;
                        }
                        else
                        {
                            ReportErrorMes(string.Format("unknow Assigned Number with :{0}",MyBytes.ByteToHexString(optionBytes,HexaDecimal.hex16,ShowHexMode.space)));
                            return null;
                        }
                        break;
                    //DO ：发送方想叫接收端激活选项。
                    case DO:
                        if(optionBytes[2]==SHOWBACK||optionBytes[2]==SHOWBACK)
                        {
                            responseOption[1]=optionBytes[1];
                            responseOption[2]=WILL;
                        }
                        else if(optionBytes[2]==TERMINAL)
                        {
                            responseOption[1]=optionBytes[1];
                            responseOption[2]=WONT;
                        }
                        else
                        {
                            ReportErrorMes(string.Format("unknow Assigned Number with :{0}",MyBytes.ByteToHexString(optionBytes,HexaDecimal.hex16,ShowHexMode.space)));
                            return null;
                        }
                        break;
                    //WONT ：发送方本身想禁止选项。
                    case WONT:
                        if(optionBytes[2]==SHOWBACK||optionBytes[2]==SHOWBACK)
                        {
                            responseOption[1]=optionBytes[1];
                            responseOption[2]=DONT;
                        }
                        else if(optionBytes[2]==TERMINAL)
                        {
                            responseOption[1]=optionBytes[1];
                            responseOption[2]=DONT;
                        }
                        else
                        {
                            ReportErrorMes(string.Format("unknow Assigned Number with :{0}",MyBytes.ByteToHexString(optionBytes,HexaDecimal.hex16,ShowHexMode.space)));
                            return null;
                        }
                        break;
                    //DON’T：发送方想让接收端去禁止选项。
                    case DONT:
                        if(optionBytes[2]==SHOWBACK||optionBytes[2]==SHOWBACK)
                        {
                            responseOption[1]=optionBytes[1];
                            responseOption[2]=WONT;
                        }
                        else if(optionBytes[2]==TERMINAL)
                        {
                            responseOption[1]=optionBytes[1];
                            responseOption[2]=WONT;
                        }
                        else
                        {
                            ReportErrorMes(string.Format("unknow Assigned Number with :{0}",MyBytes.ByteToHexString(optionBytes,HexaDecimal.hex16,ShowHexMode.space)));
                            return null;
                        }
                        break;
                    //子选项协商 (暂不处理)
                    case SB:
                        ReportErrorMes(string.Format("unsuport SB/SE option with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)));
                        return null;
                    default:
                        ReportErrorMes(string.Format("unknow option with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)));
                        return null;
                }

            }
            else
            {
                ReportErrorMes(string.Format("error option by no IAC with :{0}",MyBytes.ByteToHexString(optionBytes,HexaDecimal.hex16,ShowHexMode.space)));
                return null;
            }
            return responseOption;
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

            while (nowShowData.ToLower().IndexOf(DataToWaitFor.ToLower()) == -1)
            {
                lngCurTime = DateTime.Now.Ticks;
                if (lngCurTime > lngStart)
                {
                    throw new Exception("Timed Out waiting for : " + DataToWaitFor);
                }
                Thread.Sleep(1);
            }
            nowShowData = "";
            return 0;
        }

        public void Send(string message)
        {
            DispatchMessage(message);
            //因为每发送一行都没有发送回车,故在此处补上         
            DispatchMessage("\r\n");
        }
        #endregion

       

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