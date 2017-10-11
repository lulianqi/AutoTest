#define TESTMODE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyPipeHttpHelper
{
    public class PipeHttp
    {
        public static RawHttpRequest GlobalRawRequest = new RawHttpRequest();
        private static int idIndex = 0;
        private static object idIndexLock = new object();

        public RawHttpRequest pipeRequest = new RawHttpRequest();

        public delegate void delegatePipeStateOut(string mes, int id);
        public delegate void delegatePipeResponseOut(byte[] response, int id);

        public event delegatePipeStateOut OnPipeStateReport;
        public event delegatePipeResponseOut OnPipeResponseReport;

        private int id = 0;
        private PipeState state = PipeState.NotConnected;   //仅表示当前PipeHttp Socket状态，当前PipeHttp可能创建多个Socket连接，之前创建的状态不再维护。
        private Socket mySocket;        //管道连接
        private bool isReportResponse = false; //是否将返回数据推送给上层应用
        private int reConectCount = 0;  //在指定数目后更新管道(默认0表示一直使用初始管道)（因为部分nginx都有100的限制），设置后会让单条管道发送性能下降
        private int nowConectCount = 0;
        Thread reciveThread;           //接收线程
        private IPAddress connctHost;
        private int reciveBufferSize = 1024 * 128;  //接收缓存，当需要大量PipeHttp时请设置较小值（当isReportResponse为false该值无效）


        public PipeHttp():this(0,true)
        {
        }

        
        public PipeHttp(int yourReConectCount, bool yourIsReportResponse)
        {
            lock (idIndexLock)
            {
                id = idIndex;
                idIndex++;
            }
            reConectCount = yourReConectCount;
            isReportResponse = yourIsReportResponse;
            //Connect();
        }

        /// <summary>
        /// get or set ReciveBufferSize (连接前设置有效)
        /// </summary>
        public int ReciveBufferSize
        {
            get { return reciveBufferSize; }
            set { reciveBufferSize = value; }
        }

        /// <summary>
        /// get now pepe state 
        /// </summary>
        public PipeState GetState
        {
            get { return state; }
        }

        /// <summary>
        /// get now reConectCount (if it is 0 that is say it will never reconnect)
        /// </summary>
        public int GetreReconectCount
        {
            get { return reConectCount; }
        }

        private void ReportPipeState(string mes)
        {
            if (OnPipeStateReport!=null)
            {
                OnPipeStateReport(mes, id);
            }
        }



        private void ReportPipeResponse(byte[] bytes)
        {
            if (OnPipeResponseReport != null)
            {
                OnPipeResponseReport(bytes, id);
            }
        }

        public bool Connect()
        {
            state = PipeState.Connecting;
            if (string.IsNullOrEmpty(pipeRequest.ConnectHost))
            {
                state = PipeState.DisConnected;
                return false;
            }
            try
            {
                if (!IPAddress.TryParse(pipeRequest.ConnectHost, out connctHost))
                {
                    System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(pipeRequest.ConnectHost);
                    connctHost = host.AddressList[0];
#if TESTMODE
                    System.Diagnostics.Debug.WriteLine("-------------------------------------");
                    System.Diagnostics.Debug.WriteLine("Dns back");
                    foreach (var tempIp in host.AddressList)
                    {
                        System.Diagnostics.Debug.WriteLine(tempIp.ToString());
                    }
                    System.Diagnostics.Debug.WriteLine("-------------------------------------");
#endif
                }
                mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint hostEndPoint = new IPEndPoint(connctHost, pipeRequest.ConnectPort);
                mySocket.Connect(hostEndPoint);
                if (isReportResponse)
                {
                    reciveThread = new Thread(new ParameterizedThreadStart(ReceviData));
                    reciveThread.IsBackground = true;
                    reciveThread.Start(mySocket);
                }
                ReportPipeState("connect ok");
            }
            catch(Exception ex)
            {
                ReportPipeState(ex.Message);
                state = PipeState.DisConnected;
                return false;
            }
            state = PipeState.Connected;
            return true;
        }


        public void SendOne(byte[] requestRawBytes)
        {
            if (requestRawBytes==null)
            {
                return;
            }
            try
            {
                mySocket.Send(requestRawBytes);
                if (reConectCount > 0)
                {
                    nowConectCount++;
                    if (nowConectCount >= reConectCount)
                    {
                        nowConectCount = 0;
                        ReConnect();
                    }
                }
            }
            catch (Exception ex)
            {
                ReportPipeState(ex.Message);
                ReConnect();
            }
        }

        public void SendOne()
        {
            SendOne(pipeRequest.RawRequest);
        }

        public void Send(int sendCount)
        {
            for (int i = 0; i < sendCount; i++)
            {
                SendOne();
            }
        }

        //异步发送，(由此触发的重连，新线程的创建都会使用异步的方式)如果要更新管道请设置reConectCount
        public void AsynSend(int times, int repeatTimes, int waitTime)
        {
            Thread asynSendThread = new Thread(new ParameterizedThreadStart(
                (object ob) =>
                {
                    for (int i = 0; i < ((int[])ob)[0]; i++)
                    {
                        Send(((int[])ob)[1]);
                        if (((int[])ob)[2] > 0)
                        {
                            if (((int[])ob)[2] > 0)
                            {
                                Thread.Sleep(((int[])ob)[2]);
                            }
                        }
                    }
                    ReportPipeState("asynSendThread complete");
                }));
            asynSendThread.IsBackground = true;
            asynSendThread.Start(new int[] { times, repeatTimes, waitTime });
        }

        private void ReConnect()
        {
            //reciveThread.Abort();
            ReportPipeState("ReConnect");
            reciveThread.Name = "close";
            Connect();
        }

        private void ReceviData(object yourSocket)
        {
            byte[] nowReciveBytes = new byte[1024 * 128];
            Socket nowSocket = (Socket)yourSocket;
            int receiveCount = 0;
            int freeTime = 0;
            while (true)
            {
                if (!nowSocket.Connected)
                {
                    ReportPipeState("the tcp is disconnect");
                    state = PipeState.DisConnected;
                    break;
                }
                try
                {
                    receiveCount = nowSocket.Receive(nowReciveBytes);
                    if (receiveCount > 0)
                    {
                        freeTime = 0;
                        byte[] tempOutBytes = new byte[receiveCount];
                        Array.Copy(nowReciveBytes, tempOutBytes, receiveCount);
                        ReportPipeResponse(tempOutBytes);
                        //System.Diagnostics.Debug.WriteLine(string.Format("\r\n----------------------{0}------------------------", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff")));
                        //string respose = Encoding.UTF8.GetString(nowReciveBytes, 0, receviCount);
                        //System.Diagnostics.Debug.Write(respose);
                        //Thread.Sleep(10);
                    }
                    else
                    {
                        //超过40S没有数据
                        if (freeTime < 200)
                        {
                            freeTime++;
                        }
                        else if (Thread.CurrentThread.Name == "close")
                        {
                            ReportPipeState("the abandon socket receive task close by no data receive d");
                            nowSocket.Close();  //该链接是一个被抛弃的连接，关闭他不要改变当前PipeHttp状态（因为被遗弃前可能还有未接收完成的数据所以没有马上关闭）
                            break;
                        }
                        Thread.Sleep(freeTime);
                    }
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                    ReportPipeState("Applications active close ");//应用程序主动关闭接收线程
                    nowSocket.Close();
                    if (!(Thread.CurrentThread.Name == "close"))
                    {
                        state = PipeState.DisConnected;
                    }
                    break;
                }
                catch (Exception ex)
                {
                    ReportPipeState(ex.Message);//应用程序主动关闭接收线程
                    nowSocket.Close();
                    if (!(Thread.CurrentThread.Name == "close"))
                    {
                        state = PipeState.DisConnected;
                    }
                    break;
                }
                finally
                {
                    
                }
            }
        }
    }
}
