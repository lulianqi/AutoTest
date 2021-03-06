﻿#define INTEST_

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;


/*******************************************************************************
* Copyright (c) 2017 lijie
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   20170827           创建人: 李杰 15158155511
* 描    述: 创建
*******************************************************************************/

namespace MyCommonHelper.NetHelper
{
    public enum TelnetMessageType
    {
        Error,
        ShowData,
        Message,
        Warning
    }

    public class MyTelnet
    {
        #region telnet的数据定义
        /// <summary>
        /// 表示希望开始使用或者确认所使用的是指定的选项。
        /// </summary>
        const byte WILL = 251;
        /// <summary>
        /// 表示拒绝使用或者继续使用指定的选项。
        /// </summary>
        const byte WONT = 252;
        /// <summary>        
        /// 表示一方要求另一方使用，或者确认你希望另一方使用指定的选项。
        /// </summary>        
        const byte DO = 253;
        /// <summary>
        /// 表示一方要求另一方停止使用，或者确认你不再希望另一方使用指定的选项。       
        /// </summary>       
        const byte DONT = 254;
        /// <summary>        
        /// 标志符,代表是一个TELNET 指令        
        /// </summary>        
        const byte IAC = 255;

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
        #endregion

        
        private  AutoResetEvent sendDone = new AutoResetEvent(true); //false 非终止状态
        private  AutoResetEvent receiveDone = new AutoResetEvent(true);
        private  readonly object nowShowDataLock = new object();


        byte[] telnetReceiveBuff = new byte[1024*128];

        private ArrayList optionsList = new ArrayList();

        private Socket mySocket;
        private AsyncCallback recieveData;

        private IPEndPoint iep;
        private int timeout;
        private Encoding encoding = Encoding.UTF8;
        private string defaultExpectPattern = null;

        private string nowErrorMes;
        private int maxSaveData=10000000;

        private StringBuilder nowShowData = new StringBuilder();
        private void AddNowShowData(string yourData)
        {
            lock (nowShowDataLock)
            {
                if ((nowShowData.Length + yourData.Length) > maxSaveData)
                {
                    nowShowData.Clear();
                }
                nowShowData.Append(yourData);
            }
        }

        private StringBuilder allShowData = new StringBuilder();

        private void AddAllShowData(StringBuilder yourData)
        {
            if ((allShowData.Length + yourData.Length) > maxSaveData)
            {
                allShowData.Clear();
            }
            allShowData.Append(yourData.ToString());
        }

        public delegate void delegateDataOut(string mesStr, TelnetMessageType mesType);
        /// <summary>
        /// telnet接收到新消息后返回（请区分TelnetMessageType）
        /// </summary>
        public event delegateDataOut OnMesageReport;


        

        /// <summary>
        /// 获取当前显示数据（递增）
        /// </summary>
        public string NowShowData
        {
            get 
            {
                return nowShowData.ToString();
            }
        }


        /// <summary>
        /// 获取整个输出（但超过最大长度后，会清除前面的内容）
        /// </summary>
        public string AllLogData
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

        /// <summary>
        /// 获取或设置当前缓存返回打印的打最大长度
        /// </summary>
        public int MaxSaveData
        {
            get { return maxSaveData; }
            set { maxSaveData = value; }
        }

        /// <summary>
        /// 获取或设置查找打印时的最大超时WaitExpectPattern WaitStr 时使用（单位为秒）
        /// </summary>
        public int WaitTimeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        /// <summary>
        /// 获取或设置当前终端使用的编码（默认为UTF8）
        /// </summary>
        public Encoding Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }

        /// <summary>
        /// 获取或设置ExpectPattern（用于时标shell命令结算）
        /// </summary>
        public string ExpectPattern
        {
            get { return defaultExpectPattern; }
            set { defaultExpectPattern = value; }
        }

        private void ReportMes(string mesInfo, TelnetMessageType mesType)
        {
#if INTEST
            System.Diagnostics.Debug.WriteLine("-------------------------------------");
            System.Diagnostics.Debug.WriteLine(mesType.ToString() + mesInfo);
#endif
            if(OnMesageReport!=null)
            {
                OnMesageReport(mesInfo, mesType);
            }
        }

        /// <summary>
        /// 可能会引发异常
        /// </summary>
        /// <param name="Address">主机ip地址 (可以使用Dns.GetHostEntry(host)获取使用主机名的ip)</param>
        /// <param name="Port">端口</param>
        /// <param name="CommandTimeout">查询字符串超时时间，单位秒（0，为不超时）</param>
        public MyTelnet(string Address, int Port, int CommandTimeout)
        {
            iep = new IPEndPoint(IPAddress.Parse(Address), Port);
            timeout = CommandTimeout;
            recieveData = new AsyncCallback(OnRecievedData);
        }

        public MyTelnet(IPEndPoint yourEp, int CommandTimeout)
        {
            iep = yourEp;
            timeout = CommandTimeout;
            recieveData = new AsyncCallback(OnRecievedData);
        }

        public MyTelnet(IPEndPoint yourEp) : this(yourEp, 5) { }


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

                receiveDone.Set();
                sendDone.Set();
                //接收数据
                mySocket.BeginReceive(telnetReceiveBuff, 0, telnetReceiveBuff.Length, SocketFlags.None, recieveData, mySocket);

                return true;
            }
            catch (Exception ex)
            {
                nowErrorMes = ex.Message;
                return false;
            }
        }

        #region MyFunction

        private void OnRecievedData(IAsyncResult ar)
        {
            receiveDone.WaitOne();
            Socket so = (Socket)ar.AsyncState;
            if (so==null)
            {
                receiveDone.Set();
                return;
            }
            if(!so.Connected)
            {
                receiveDone.Set();
                return;
            }
            //EndReceive方法为结束挂起的异步读取         
            int recLen = so.EndReceive(ar);

            //可以在OnRecievedData直接调用BeginReceive，因为有同步线程锁，msdn示例也是如此
            try
            {
                mySocket.BeginReceive(telnetReceiveBuff, 0, telnetReceiveBuff.Length, SocketFlags.None, recieveData, mySocket);
            }
            catch (Exception ex) //BeginReceive时Socket可能被异步关闭
            {
                ReportMes(ex.Message, TelnetMessageType.Error);
            }

            //如果有接收到数据的话            
            if (recLen > 0)
            {

                if (recLen > telnetReceiveBuff.Length)
                {
                    ReportMes(string.Format("ReceiveBuff is out of memory [{0}] [{1}]", telnetReceiveBuff.Length, recLen), TelnetMessageType.Error);
                    recLen = telnetReceiveBuff.Length;
                }

                byte[] tempByte = new byte[recLen];
                Array.Copy(telnetReceiveBuff, 0, tempByte, 0, recLen);
#if INTEST
                System.Diagnostics.Debug.WriteLine("-------------------------------------");
                //System.Diagnostics.Debug.WriteLine(MyBytes.ByteToHexString(tempByte, HexaDecimal.hex16, ShowHexMode.space));
                //System.Diagnostics.Debug.WriteLine(MyBytes.ByteToHexString(tempByte, HexaDecimal.hex10, ShowHexMode.space));
                //System.Diagnostics.Debug.WriteLine(Encoding.ASCII.GetString(tempByte));
                System.Diagnostics.Debug.WriteLine(Encoding.UTF8.GetString(tempByte));
#endif

                try
                {

                    byte[] tempShowByte = DealRawBytes(tempByte);
                    if (tempShowByte.Length > 0)
                    {
                        string tempNowStr = encoding.GetString(tempShowByte);
                        ReportMes(tempNowStr, TelnetMessageType.ShowData);
                        AddNowShowData(tempNowStr);

                    }
                    //超过接收缓存的数据也不可是选项数据，即不用考虑选项被截断的情况
                    DealOptions();
                }
                catch (Exception ex)
                {
                    throw new Exception("控制选项错误 " + ex.Message);
                }
                finally
                {
                    receiveDone.Set(); //要在数据处理完成后开锁，然后在接收完成后也会有可能出现数据错位
                }
            }
            else// 如果没有接收到任何数据， 关闭连接           
            {
                DisConnect();
                receiveDone.Set();
            }

        }


        /// <summary>        
        ///  发送数据的函数       
        /// </summary>        
        private void DealOptions()
        {
            if (optionsList.Count > 0)
            {
                byte[] sendResponseOption = null;
                byte[] nowResponseOption = null;
                foreach (byte[] tempOption in optionsList)
                {
                    nowResponseOption = GetResponseOption(tempOption);
                    if (nowResponseOption != null)
                    {
                        if (sendResponseOption == null)
                        {
                            sendResponseOption = nowResponseOption;
                        }
                        else
                        {
                            Array.Resize(ref sendResponseOption, sendResponseOption.Length + nowResponseOption.Length);
                            nowResponseOption.CopyTo(sendResponseOption, sendResponseOption.Length - nowResponseOption.Length);
                        }
                    }
                }
                if (sendResponseOption != null)
                {
                    WriteRawData(sendResponseOption);
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

            for (int i = 0; i < yourRawBytes.Length; i++)
            {
                if (yourRawBytes[i] == IAC)
                {
                    if ((i + 1) >= yourRawBytes.Length)
                    {
                        throw new Exception("find error IAC data , no data after IAC");
                    }
                    byte nextByte = yourRawBytes[i + 1];
                    if (nextByte == DO || nextByte == DONT || nextByte == WILL || nextByte == WONT)
                    {
                        if ((i + 2) < yourRawBytes.Length)
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
                        if (sbEndIndex > 0)
                        {
                            byte[] tempSBOptionCmd = new byte[sbEndIndex - i];
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



        /// <summary>
        /// 获取协商答复
        /// </summary>
        /// <param name="optionBytes">协商</param>
        /// <returns>答复（无法答复或错误返回null）</returns>
        private byte[] GetResponseOption(byte[] optionBytes)
        {

            byte[] responseOption = new byte[3];
            responseOption[0] = IAC;
            //协商选项命令为3字节，附加选项超过3个             
            if (optionBytes.Length < 3)
            {
                ReportMes(string.Format("error option by errer length with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)), TelnetMessageType.Error);
                return null;
            }
            if (optionBytes[0] == IAC)
            {
                switch (optionBytes[1])
                {
                    //WILL： 发送方本身将激活( e n a b l e )选项
                    case WILL:
                        if (optionBytes[2] == SHOWBACK || optionBytes[2] == RESTRAIN)
                        {
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = DO;
                        }
                        else if (optionBytes[2] == TERMINAL)
                        {
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = WONT;
                        }
                        else
                        {
                            ReportMes(string.Format("unknow Assigned Number with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)), TelnetMessageType.Warning);
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = WONT;
                        }
                        break;
                    //DO ：发送方想叫接收端激活选项。
                    case DO:
                        if (optionBytes[2] == SHOWBACK || optionBytes[2] == RESTRAIN)
                        {
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = WILL;
                        }
                        else if (optionBytes[2] == TERMINAL)
                        {
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = WONT;
                        }
                        else
                        {
                            ReportMes(string.Format("unknow Assigned Number with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)), TelnetMessageType.Warning);
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = WONT;
                        }
                        break;
                    //WONT ：发送方本身想禁止选项。
                    case WONT:
                        if (optionBytes[2] == SHOWBACK || optionBytes[2] == RESTRAIN)
                        {
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = DONT;
                        }
                        else if (optionBytes[2] == TERMINAL)
                        {
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = DONT;
                        }
                        else
                        {
                            ReportMes(string.Format("unknow Assigned Number with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)), TelnetMessageType.Warning);
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = WONT;
                        }
                        break;
                    //DON’T：发送方想让接收端去禁止选项。
                    case DONT:
                        if (optionBytes[2] == SHOWBACK || optionBytes[2] == RESTRAIN)
                        {
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = WONT;
                        }
                        else if (optionBytes[2] == TERMINAL)
                        {
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = WONT;
                        }
                        else
                        {
                            ReportMes(string.Format("unknow Assigned Number with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)), TelnetMessageType.Warning);
                            responseOption[2] = optionBytes[2];
                            responseOption[1] = WONT;
                        }
                        break;
                    //子选项协商 (暂不处理)
                    case SB:
                        ReportMes(string.Format("unsuport SB/SE option with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)), TelnetMessageType.Warning);
                        return null;
                    default:
                        ReportMes(string.Format("unknow option with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)), TelnetMessageType.Warning);
                        responseOption[2] = optionBytes[2];
                        responseOption[1] = WONT;
                        break;
                }

            }
            else
            {
                ReportMes(string.Format("error option by no IAC with :{0}", MyBytes.ByteToHexString(optionBytes, HexaDecimal.hex16, ShowHexMode.space)), TelnetMessageType.Warning);
                return null;
            }
            return responseOption;
        }


        private bool WriteRawData(byte[] yourData)
        {
            if(!mySocket.Connected)
            {
                return false;
            }
            sendDone.WaitOne();
            mySocket.BeginSend(yourData, 0, yourData.Length, SocketFlags.None, new AsyncCallback((IAsyncResult ar) =>
            {
                try
                {
                    Socket client = (Socket)ar.AsyncState;
                    int bytesSent = client.EndSend(ar);


#if INTEST
                    System.Diagnostics.Debug.WriteLine("-------------------------------------");
                    System.Diagnostics.Debug.WriteLine(string.Format("Sent {0} bytes to server.", bytesSent));
                    System.Diagnostics.Debug.WriteLine(MyBytes.ByteToHexString(yourData, HexaDecimal.hex16, ShowHexMode.space));
                    System.Diagnostics.Debug.WriteLine(MyBytes.ByteToHexString(yourData, HexaDecimal.hex10, ShowHexMode.space));
                    System.Diagnostics.Debug.WriteLine(Encoding.ASCII.GetString(yourData));
                    System.Diagnostics.Debug.WriteLine(Encoding.UTF8.GetString(yourData));
#endif
                }
                catch (Exception ex)
                {
                    ReportMes(string.Format("error in send data with :{0}", ex.Message), TelnetMessageType.Error);
                }
                finally
                {
                    sendDone.Set();
                }

            }), mySocket);
            return true;
        }
        
        #endregion
        
        /// <summary>
        /// 指定时间内等待指定的字符串 （若期望获取较高性能应尽量避免使用wait）
        /// </summary>
        /// <param name="waitStr">等待字符串</param>
        /// <returns>查询到返回true，否则为false</returns>
        public bool WaitStr(string waitStr)
        {
            if(timeout>0)
            {
                long endTicks = DateTime.Now.AddSeconds(timeout).Ticks;
                while (((nowShowData.ToString()).ToLower()).IndexOf(waitStr.ToLower()) == -1)
                {
                    if (DateTime.Now.Ticks > endTicks)
                    {
                        return false;
                    }
                    Thread.Sleep(100);
                }
                return true;
            }
            else
            {
                return ((nowShowData.ToString()).ToLower().Contains(waitStr.ToLower()));
            }
        }

        public bool WaitExpectPattern(string expectPattern)
        {
            bool isFind = false; ;
            if (timeout > 0)
            {
                long endTicks = DateTime.Now.AddSeconds(timeout).Ticks;
                while (!isFind)
                {
                    lock (nowShowDataLock)
                    {
                        if (nowShowData.Length > expectPattern.Length)
                        {
                            isFind = nowShowData.ToString(nowShowData.Length-expectPattern.Length,expectPattern.Length) == expectPattern;
                        }
                        else
                        {
                            isFind = false;
                        }

                    }
                    if (DateTime.Now.Ticks > endTicks)
                    {
                        return false;
                    }
                    Thread.Sleep(20);
                }
                return true;
            }
            else
            {
                lock (nowShowDataLock)
                {
                    if (nowShowData.Length > expectPattern.Length)
                    {
                        isFind = nowShowData.ToString(nowShowData.Length - expectPattern.Length, expectPattern.Length) == expectPattern;
                    }
                    else
                    {
                        isFind = false;
                    }
                }
                return (isFind);
            }
        }

        public bool Write(string message)
        {
            return WriteRawData(encoding.GetBytes(message));
        }

        public bool Write(byte[] bytes)
        {
            return WriteRawData(bytes);
        }

        public bool WriteLine(string message)
        {
            return WriteRawData(encoding.GetBytes(message + ENDOFLINE));
        }

        /// <summary>
        /// 获取当前显示数据（获取之后即从该缓存中移除）
        /// </summary>
        /// <returns></returns>
        public string GetAndMoveShowData()
        {
            AddAllShowData(nowShowData);
            string tempOutStr= nowShowData.ToString();
            lock(nowShowDataLock)
            {
                nowShowData.Clear();
            }
            return tempOutStr;
        }

        /// <summary>
        /// 清除当前显示缓存，并将其移至全局缓存
        /// </summary>
        public void ClearShowData()
        {
            if(nowShowData.Length>0)
            {
                AddAllShowData(nowShowData);
                lock(nowShowDataLock)
                {
                    nowShowData.Clear();
                }
            }
        }


        /// <summary>
        /// 发起一个命令并以阻塞的形式获取返回（获取指定查找字符串时返回）
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="waitStr">指定查找字符串</param>
        /// <returns>命令返回</returns>
        public string DoRequestWithWaitStr(string cmd, string waitStr)
        {
            ClearShowData();
            WriteLine(cmd);
            WaitStr(waitStr);
            return GetAndMoveShowData();
        }

        /// <summary>
        /// 发起一个命令并以阻塞的形式获取返回（获取expectPattern时返回）
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="expectPattern">expectPattern（如#$等）</param>
        /// <returns>命令返回</returns>
        public string DoRequest(string cmd, string expectPattern)
        {
            ClearShowData();
            WriteLine(cmd);
            WaitExpectPattern(expectPattern);
            return GetAndMoveShowData();
        }


        /// <summary>
        /// 发起一个命令并以阻塞的形式获取返回（指定延时时间到达时时返回）
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="waitTime">指定延时时间毫秒为单位</param>
        /// <returns>命令返回</returns>
        public string DoRequest(string cmd, int waitTime)
        {
            ClearShowData();
            WriteLine(cmd);
            Thread.Sleep(waitTime);
            return GetAndMoveShowData();
        }

        /// <summary>
        /// 发起一个命令并以阻塞的形式获取返回（如果设置过ExpectPattern，则使用ExpectPattern作为达时时返回，如果没有则使用2000毫秒的延时作为达到时返回）
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <returns>命令返回</returns>
        public string DoRequest(string cmd)
        {
            if(defaultExpectPattern==null)
            {
                return DoRequest(cmd, 2000);
            }
            else
            {
                return DoRequest(cmd, defaultExpectPattern);
            }
        }

        public void DisConnect()
        {
            if (mySocket==null)
            {
                return;
            }
            if (mySocket.Connected)
            {
                mySocket.Shutdown(SocketShutdown.Both);
                mySocket.Close();
            }
        }


    }
}