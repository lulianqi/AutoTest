using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Threading;


/*******************************************************************************
* Copyright (c) 2015 lijie
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201505016           创建人: 李杰 15158155511
* 描    述: 创建
*******************************************************************************/


namespace MyCommonHelper.NetHelper
{
    public class MyTcpClient
    {
        string myErrorMes = "";

        IPAddress ipa ;
        TcpListener myListener ;
        Socket myNowSocket;


        bool isTcpClientConnected = false;
        bool isAutoReceive = false;
        IPEndPoint myIpe;
        TcpClient myTcpClient;
        NetworkStream myNetworkStream;

        System.Timers.Timer myReceiveTimer;


        #region Attribute 
        /// <summary>
        /// is this tcp Connected
        /// </summary>
        public bool IsTcpClientConnected
        {
            get
            {
                if (myTcpClient == null)
                {
                    return false;
                }
                else
                {
                    return myTcpClient.Connected;
                }
            }
            set
            {
                isTcpClientConnected = value;
            }
        }

        /// <summary>
        /// get now Erroer Message
        /// </summary>
        public string ErroerMessage
        {
            get
            {
                return myErrorMes;
            }
        }

        /// <summary>
        /// get isAutoReceive   (如果为true，则表示将使用事件的方式接收数据)
        /// </summary>
        public bool IsAutoReceive
        {
            get
            {
                return isAutoReceive;
            }
        }
        #endregion
        

        //ReceiveData
        public delegate void delegateReceiveData(byte[] yourData);
        public event delegateReceiveData OnReceiveData;

        //Connect   连接成功返回"",失败返回错误信息
        public delegate void delegateConnected(string yourInfo);
        public event delegateConnected OnTcpConnected;

        //ConnectionLost
        public delegate void ConnectionLosted();
        public event ConnectionLosted OnTcpConnectionLosted;


        /// <summary>
        /// Initialization a mySocket （当前重载需要手动获取接收数据）
        /// </summary>
        /// <param name="yourIPEndPoint"></param>
        /// <param name="yourReceiveTick">the receive time(ms)</param>
        public MyTcpClient(IPEndPoint yourIPEndPoint)
        {
            myErrorMes = "";
            myIpe = yourIPEndPoint;
            isAutoReceive = false;
        }

        /// <summary>
        /// Initialization a mySocket (it may thow an error you need try catch it)
        /// </summary>
        /// <param name="endPointStr">a ip str like 10.10.10.10:80</param>
        public MyTcpClient(string endPointStr)
            : this(new IPEndPoint(IPAddress.Parse(endPointStr.Split(':')[0]), int.Parse(endPointStr.Split(':')[1])))
        { }

        /// <summary>
        /// Initialization a mySocket  （当前重载表示将使用事件的方式接收数据）
        /// </summary>
        /// <param name="yourIPEndPoint"></param>
        /// <param name="yourReceiveTick">the receive time(ms)</param>
        public MyTcpClient(IPEndPoint yourIPEndPoint,int yourReceiveTick)
        {
            myErrorMes = "";
            myIpe = yourIPEndPoint;

            myReceiveTimer = new System.Timers.Timer(yourReceiveTick);
            myReceiveTimer.Elapsed += new ElapsedEventHandler(ReceiveTimer_Elapsed);
            myReceiveTimer.AutoReset = true;
            myReceiveTimer.Enabled = false;
            isAutoReceive = true;
        }

        /// <summary>
        /// Initialization a mySocket (it may thow an error you need try catch it)
        /// </summary>
        /// <param name="endPointStr">a ip str like 10.10.10.10:80</param>
        /// <param name="yourReceiveTick">the receive time (ms)</param>
        public MyTcpClient(string endPointStr, int yourReceiveTick)
            : this(new IPEndPoint(IPAddress.Parse(endPointStr.Split(':')[0]), int.Parse(endPointStr.Split(':')[1])), yourReceiveTick)
        {}

       
        /// <summary>
        /// here i will connect the Client
        /// </summary>
        /// <returns>is ok</returns>
        public bool Connect()
        {
            myTcpClient = new TcpClient();
            try
            {
                myTcpClient.Connect(myIpe);
                myNetworkStream = myTcpClient.GetStream();
                if (isAutoReceive)
                {
                    myReceiveTimer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog(ex.Message);
                myErrorMes = ex.Message;
                return false;
            }
            return true;
        }

        
        /// <summary>
        /// here i will connect the Client with Async (you should use the OnTcpConnected to make sure is OK)
        /// </summary>
        /// <returns>you should use the OnTcpConnected to make sure is OK</returns>
        public bool ConnectAsync()
        {
            myTcpClient = new TcpClient();
            try
            {
                Thread myThread = new Thread(new ParameterizedThreadStart(NewConnectThread));
                myThread.Start(new List<object> { myTcpClient, myIpe });
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog(ex.Message);
                myErrorMes = ex.Message;
                return false;
            }
            return true;
        }

        
        /// <summary>
        /// i will disConnect the Client
        /// </summary>
        public void DisConnect()
        {
            myTcpClient.Close();
            myNetworkStream = null;
            if (myReceiveTimer != null)
            {
                myReceiveTimer.Enabled = false;
            }
        }

        /// <summary>
        /// receive data (如果连接断开或没有任何数据返回null)
        /// </summary>
        /// <returns>data</returns>
        public byte[] ReceiveData()
        {
            if (isAutoReceive)
            {
                throw (new Exception("if you want read just set your isAutoReceive false"));
            }
            byte[] outData = null;
            if (myTcpClient.Connected)
            {
                if (myTcpClient.Available > 0)
                {
                    outData = new byte[myTcpClient.Available];
                    myNetworkStream.Read(outData, 0, outData.Length);
                }
            }
            return outData;
        }

        /// <summary>
        /// receive all data (如果连接断开或没有任何数据返回null)
        /// </summary>
        /// <returns>all data</returns>
        public byte[] ReceiveAllData()
        {
            if (isAutoReceive)
            {
                throw (new Exception("if you want read just set your isAutoReceive false"));
            }
            byte[] outData = null;
            if (myTcpClient.Connected)
            {
                List<byte[]> bytesList = new List<byte[]>();
                while (myTcpClient.Available > 0)
                {
                    byte[] tempBuf = new byte[myTcpClient.Available];
                    myNetworkStream.Read(tempBuf, 0, tempBuf.Length);
                    bytesList.Add(tempBuf);
                }
                if(bytesList.Count>0)
                {
                    int byteLeng = 0;
                    foreach(byte[] tempBytes in bytesList)
                    {
                        byteLeng += tempBytes.Length;
                    }
                    outData = new byte[byteLeng];
                    int nowByteIndex=0;
                    foreach(byte[] tempBytes in bytesList)
                    {
                        Array.Copy(tempBytes, 0, outData, nowByteIndex, tempBytes.Length);
                        nowByteIndex += tempBytes.Length;
                    }
                }
            }
            return outData;
        }

        /// <summary>
        /// here i will send data by bytes
        /// </summary>
        /// <param name="yourData">your Data</param>
        /// <returns>>is ok</returns>
        public bool SendData(byte[] yourData)
        {
            try
            {
                if (!myTcpClient.Connected)
                {
                    myErrorMes = "TcpClient not connect";
                    return false;
                }
                myNetworkStream.Write(yourData, 0, yourData.Length);
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog(ex.Message);
                myErrorMes = ex.Message; 
                return false;
            }
            return true;

        }

        /// <summary>
        ///here i will send data by string (由于操作系统socket缓存，对于已经断开的连接发送数据可能也返回成功，实际测试断开后的第2次会Write失败)
        /// </summary>
        /// <param name="yourData">your Data</param>
        /// <param name="yourEncoding">your Encoding</param>
        /// <returns>is ok</returns>
        public bool SendData(string yourData, Encoding yourEncoding)
        {
            try
            {
                if (!myTcpClient.Connected)
                {
                    myErrorMes = "TcpClient not connect";
                    return false;
                }
                byte[] tempData = yourEncoding.GetBytes(yourData);
                myNetworkStream.Write(tempData, 0, tempData.Length);
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog(ex.Message);
                myErrorMes = ex.Message;
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #region function

        /// <summary>
        /// Connect Thread To Connect 
        /// </summary>
        /// <param name="ConnectTcpClient"> List<object> { TcpClient, IPEndPoint } </param>
        private void NewConnectThread(object ConnectTcpClient)
        {
            try
            {
                ((TcpClient)(((List<object>)ConnectTcpClient)[0])).Connect((IPEndPoint)(((List<object>)ConnectTcpClient)[1]));
                InnerSocket_OnMyTcpConnected("");
            }
            catch (Exception ex)
            {
                InnerSocket_OnMyTcpConnected(ex.Message);
            }

        }

        /// <summary>
        /// when you use connectClientAsync here i will out the message that you have Connected
        /// </summary>
        /// <param name="yourInfo">err message if is "" that mean ok</param>
        private void InnerSocket_OnMyTcpConnected(string yourInfo)
        {
            if (yourInfo == "")
            {
                if (myTcpClient.Connected)
                {
                    myNetworkStream = myTcpClient.GetStream();
                    if(isAutoReceive)
                    {
                        myReceiveTimer.Enabled = true;
                    }
                    if (OnTcpConnected != null)
                    {
                        this.OnTcpConnected("");
                    }
                }
                else
                {
                    this.OnTcpConnected("unknow error");
                }
            }
            else
            {
                if (OnTcpConnected != null)
                {
                    this.OnTcpConnected(yourInfo);
                }
            }
        }


        //Receive date in temer with a new thread
        private void ReceiveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //this.OnReceiveData(new byte[]{33,33});
            //System.Threading.Thread.Sleep(450);
            if (myTcpClient.Connected)
            {
                if (myTcpClient.Available > 0)
                {
                    byte[] tempBuf = new byte[myTcpClient.Available];
                    myNetworkStream.Read(tempBuf, 0, tempBuf.Length);
                    this.OnReceiveData(tempBuf);
                }
            }
            else
            {
                DisConnect();
                this.OnTcpConnectionLosted();
            }
        }

        #endregion
    }

    public class MyUdpClient
    {
        string myErrorMes = "";
        IPEndPoint myNowEp;
        UdpClient udpClient;


        public string ErroerMessage
        {
            get
            {
                return myErrorMes;
            }
        }

        public IPEndPoint myNowIPEndPoint
        {
            get
            {
                return myNowEp;
            }
        }

        public MyUdpClient()
        {
            udpClient = new UdpClient();
        }
        public MyUdpClient(int yourPort)
        {
            udpClient = new UdpClient(yourPort);
        }

        public byte[] myReceive()
        {
            IPEndPoint receivePoint = new IPEndPoint(IPAddress.Any, 8080);
            byte[] recData;
            try
            {
                recData = udpClient.Receive(ref receivePoint);
            }
            catch (Exception ex)
            {
                myErrorMes = ex.Message;
                return null;
            }
            myNowEp = receivePoint;
            return recData;
        }

        public void myClose()
        {
            udpClient.Close();
        }


    }
}
