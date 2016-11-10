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


namespace MyCommonTool
{
    public class mySocket
    {
        string myErrorMes = "";

        IPAddress ipa ;
        TcpListener myListener ;
        Socket myNowSocket;

        IPEndPoint myIpe;
        TcpClient myTcpClient;
        NetworkStream myNetworkStream;

        System.Timers.Timer myReceiveTimer;


        #region Attribute
        bool _isTcpClientConnected = false;
        /// <summary>
        /// is this tcp Connected
        /// </summary>
        public bool isTcpClientConnected
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
                _isTcpClientConnected = value;
            }
        }
        #endregion
        

        //ReceiveData
        public delegate void delegateReceiveData(byte[] yourData);
        public event delegateReceiveData OnReceiveData;

        //Connect
        private delegate void delegateMyConnected(string yourInfo);//use in this class only private
        private event delegateMyConnected OnMyTcpConnected;

        public delegate void delegateConnected(string yourInfo);
        public event delegateConnected OnTcpConnected;

        //ConnectionLost
        public delegate void ConnectionLosted();
        public event ConnectionLosted OnTcpConnectionLosted;

        /// <summary>
        /// Connect Thread To Connect 
        /// </summary>
        /// <param name="ConnectTcpClient"> List<object> { TcpClient, IPEndPoint } </param>
        public void newConnectThread(object ConnectTcpClient)
        {
            try
            {
                ((TcpClient)(((List<object>)ConnectTcpClient)[0])).Connect((IPEndPoint)(((List<object>)ConnectTcpClient)[1]));
                this.OnMyTcpConnected("");
            }
            catch (Exception ex)
            {
                this.OnMyTcpConnected(ex.Message);
            }
            
        }

        /// <summary>
        /// Initialization a mySocket
        /// </summary>
        /// <param name="yourIPEndPoint"></param>
        /// <param name="yourReceiveTick">the receive time</param>
        public mySocket(IPEndPoint yourIPEndPoint,int yourReceiveTick)
        {
            myErrorMes = "";
            myIpe = yourIPEndPoint;

            myReceiveTimer = new System.Timers.Timer(yourReceiveTick);
            myReceiveTimer.Elapsed += new ElapsedEventHandler(myReceiveTimer_Elapsed);
            myReceiveTimer.AutoReset = true;
            myReceiveTimer.Enabled = false;
        }

        //Receive date in temer with a new thread
        void myReceiveTimer_Elapsed(object sender, ElapsedEventArgs e)
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
                disConnectClient();
                this.OnTcpConnectionLosted();              
            }
        }

        /// <summary>
        /// get now Erroer Message
        /// </summary>
        public string myErroerMessage
        {
            get
            {
                return myErrorMes;
            }
        }

        /// <summary>
        /// here i will connect the Client
        /// </summary>
        /// <returns>is ok</returns>
        public bool connectClient()
        {
            myTcpClient = new TcpClient();
            //this.OnMyTcpConnected += new delegateMyConnected(mySocket_OnMyTcpConnected);
            try
            {
                myTcpClient.Connect(myIpe);
                myNetworkStream = myTcpClient.GetStream();
                myReceiveTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLogEx(ex.Message);
                myErrorMes = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// here i will connect the Client with Async (you should use the OnTcpConnected to make sure is OK)
        /// </summary>
        /// <returns>you should use the OnTcpConnected to make sure is OK</returns>
        public bool connectClientAsync()
        {
            myTcpClient = new TcpClient();
            this.OnMyTcpConnected += new delegateMyConnected(mySocket_OnMyTcpConnected);
            try
            {
                Thread myThread = new Thread(new ParameterizedThreadStart(newConnectThread));
                myThread.Start(new List<object> { myTcpClient, myIpe });

            }
            catch (Exception ex)
            {
                ErrorLog.PutInLogEx(ex.Message);
                myErrorMes = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// when you use connectClientAsync here i will out the message that you have Connected
        /// </summary>
        /// <param name="yourInfo">err message if is "" that mean ok</param>
        void mySocket_OnMyTcpConnected(string yourInfo)
        {
            if (yourInfo == "")
            {
                if (myTcpClient.Connected)
                {
                    myNetworkStream = myTcpClient.GetStream();
                    myReceiveTimer.Enabled = true;
                    if (OnTcpConnected != null)
                    {
                        this.OnTcpConnected("");
                    }
                }
            }
            else
            {
                if (OnTcpConnected != null)
                {
                    this.OnTcpConnected(yourInfo);
                }
                this.OnMyTcpConnected -= new delegateMyConnected(mySocket_OnMyTcpConnected);
            }
        }

        /// <summary>
        /// i will disConnect the Client
        /// </summary>
        /// <returns>is ok</returns>
        public bool disConnectClient()
        {
            myTcpClient.Close();
            myNetworkStream = null;
            if (myReceiveTimer != null)
            {
                myReceiveTimer.Enabled = false;
                //myReceiveTimer = null;
            }
            return true;
        }

        /// <summary>
        /// here i will send data by bytes
        /// </summary>
        /// <param name="yourData">your Data</param>
        /// <returns>>is ok</returns>
        public bool sendData(byte[] yourData)
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
                ErrorLog.PutInLogEx(ex.Message);
                myErrorMes = ex.Message;
                return false;
            }
            return true;

        }

        /// <summary>
        ///here i will send data by string
        /// </summary>
        /// <param name="yourData">your Data</param>
        /// <param name="yourEncoding">your Encoding</param>
        /// <returns>is ok</returns>
        public bool sendData(string yourData, Encoding yourEncoding)
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
                ErrorLog.PutInLogEx(ex.Message);
                myErrorMes = ex.Message;
                return false;
            }
            return true;
        }
    
    }

    public class myUdp
    {
        string myErrorMes = "";
        IPEndPoint myNowEp;
        UdpClient udpClient;


        public string myErroerMessage
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

        public myUdp()
        {
            udpClient = new UdpClient();
        }
        public myUdp(int yourPort)
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
