using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestForPipeHttp
{
    class Program
    {

        public static class HttpRawData
        {
            public static string host = "www.baidu.com";
            public static string startLine = "GET http://www.baidu.com/ HTTP/1.1";
            //public static string startLine = "GET http://wxv4.huala.com/app.html HTTP/1.1";
            //public static string startLine = "GET http://www.youku.com/?spm=a2h0z.8244218.qheader.5~5~5!3~1~3~A HTTP/1.1";
            //public static string startLine = "GET http://r1.ykimg.com/material/0A03/201709/0925/132217/70x100a.png HTTP/1.1";
            //public static string startLine = "GET http://pv.sohu.com/cityjson?ie=utf-8 HTTP/1.1";
            //public static string startLine = "GET http://wl.jd.com/wl.js HTTP/1.1";
            //public static string startLine = "GET http://shared-https.ydstatic.com/gouwuex/ext/script/load_url_s.txt?v=1507378 HTTP/1.1";   //jd  接口 没有100的限制
            //public static string startLine = "GET http://wxv4.huala.com/huala/v3/seller/detail/562 HTTP/1.1";
            //public static string startLine = "GET http://hz.meituan.com/ptapi/getHotCinema HTTP/1.1";
            
            public static List<string> headers = new List<string>();
            public static string entityBody = "";

        }

        public class PipeHttp
        {
            //高压无接收模式
            public Socket mySocket;
            public Byte[] requestRawBytes;
            public int reConectCount=0;  //设置后会让单条管道发送性能下降
            private int nowConectCount = 0;
            Thread reciveThread;
            private IPAddress dnsIp;

            public PipeHttp()
            {
                Connect();
            }
            public PipeHttp(int yourReConectCount)
            {
                reConectCount = yourReConectCount;
                Connect();
            }
            private void Connect()
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(HttpRawData.host);
                mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                dnsIp = host.AddressList[0];
                IPEndPoint hostEndPoint = new IPEndPoint(host.AddressList[0], 80);
                mySocket.Connect(hostEndPoint);
                reciveThread = new Thread(new ParameterizedThreadStart(ReceviData));
                reciveThread.IsBackground = true;
                reciveThread.Start(mySocket);
                Console.WriteLine("connect ok");
                StringBuilder requestSb = new StringBuilder();
                requestSb.AppendLine(HttpRawData.startLine);
                requestSb.AppendLine("Content-Type: application/x-www-form-urlencoded");
                requestSb.AppendLine(string.Format("Host: {0}", HttpRawData.host));
                requestSb.AppendLine("Connection: Keep-Alive");
                requestSb.AppendLine();
                requestRawBytes = Encoding.UTF8.GetBytes(requestSb.ToString());
            }

            //private void ConnectEx()
            //{
            //    mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //    IPEndPoint hostEndPoint = new IPEndPoint(dnsIp, 80);
            //    mySocket.Connect(hostEndPoint);
            //    reciveThread = new Thread(new ParameterizedThreadStart(ReceviData));
            //    reciveThread.IsBackground = true;
            //    reciveThread.Start(mySocket);
            //    Console.WriteLine("connect ok");
            //    StringBuilder requestSb = new StringBuilder();
            //    requestSb.AppendLine(HttpRawData.startLine);
            //    requestSb.AppendLine("Content-Type: application/x-www-form-urlencoded");
            //    requestSb.AppendLine(string.Format("Host: {0}", HttpRawData.host));
            //    requestSb.AppendLine("Connection: Keep-Alive");
            //    requestSb.AppendLine();
            //    requestRawBytes = Encoding.UTF8.GetBytes(requestSb.ToString());
            //}

            public void SendOne()
            {
                try
                {
                    mySocket.Send(requestRawBytes);
                    if(reConectCount>0)
                    {
                        nowConectCount++;
                        if(nowConectCount>=reConectCount)
                        {
                            nowConectCount = 0;
                            ReConnect();
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ReConnect();
                }
            }

            public void Send(int sendCount)
            {
                for (int i = 0; i < sendCount; i++)
                {
                    SendOne();
                }
            }

            //异步发送，(由此触发的重连，新线程的创建都会使用异步的方式)如果要更新管道请设置reConectCount
            public void AsynSendEx(int times,int repeatTimes,int waitTime)
            {
                Thread asynSendThread=new Thread(new ParameterizedThreadStart(
                    (object ob)=>{
                        for(int i=0;i<((int[])ob)[0];i++)
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
                        Console.WriteLine("asynSendThread stop");
                        }));
                asynSendThread.IsBackground = true;
                asynSendThread.Start(new int[] { times, repeatTimes, waitTime });
            }

            public void AsynSend(int times, int repeatTimes, int waitTime)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((object ob) =>
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
                    ReportPipeInfo("asynSendThread complete");
                }), new int[] { times, repeatTimes, waitTime });
            }
            private void ReConnect()
            {
                //reciveThread.Abort();
                reciveThread.Name = "close";
                Connect();
            }
        }

        static void Main(string[] args)
        {
            //PipeHttp ph = new PipeHttp();
            //ph.reConectCount = 100;
            //Console.ReadLine();
            //ph.AsynSend(10, 20, 10);
            //ph.Send(1000);

            Console.ReadLine();


            List<PipeHttp> phs = new List<PipeHttp>();
            
            for(int i =0 ;i<1;i++)
            {
                phs.Add(new PipeHttp(100));
            }
            Console.WriteLine("press enter to send");
            Console.ReadLine();
            foreach(PipeHttp tph in phs)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"));
                //tph.Send(100);
                tph.AsynSend(1000, 1, 0);
                Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"));
            }
            Console.ReadLine();
        }

        public static void DoTest()
        {
            Console.WriteLine("enter to contine");
            Console.ReadLine();
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(HttpRawData.host);
            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint hostEndPoint = new IPEndPoint(host.AddressList[0], 80);
            mySocket.Connect(hostEndPoint);
            Thread reciveThread = new Thread(new ParameterizedThreadStart(ReceviData));
            reciveThread.IsBackground = true;
            reciveThread.Start(mySocket);
            Console.WriteLine("connect ok");
            StringBuilder requestSb = new StringBuilder();
            requestSb.AppendLine(HttpRawData.startLine);
            requestSb.AppendLine("Content-Type: application/x-www-form-urlencoded");
            requestSb.AppendLine(string.Format("Host: {0}", HttpRawData.host));
            requestSb.AppendLine("Connection: Keep-Alive");
            requestSb.AppendLine();
            Byte[] requestRawBytes = Encoding.UTF8.GetBytes(requestSb.ToString());

            mySocket.Send(requestRawBytes);
            Console.ReadLine();

            mySocket.Send(requestRawBytes);
            Console.ReadLine();

            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"));
            for (int i = 0; i < 100; i++)
            {
                mySocket.Send(requestRawBytes);
            }
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"));

            Console.ReadLine();

            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"));
            for (int i = 0; i < 100; i++)
            {
                mySocket.Send(requestRawBytes);
            }
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"));

            Console.ReadLine();

            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"));
            for (int i = 0; i < 100; i++)
            {
                mySocket.Send(requestRawBytes);
            }
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"));

            Console.ReadLine();
            System.Diagnostics.Debug.WriteLine("System.Diagnostics.Debug.Write");
            Console.ReadLine();
        }
       
        public static void ReceviData(object yourSocket)
        {
            byte[] nowReciveBytes=new byte[1024*128];
            Socket nowSocket = (Socket)yourSocket;
            int receviCount = 0;
            int freeTime = 0;
            while (true)
            {
                if(!nowSocket.Connected)
                {
                    Console.WriteLine("dis connected");
                    break;
                }
                try
                {
                    receviCount = nowSocket.Receive(nowReciveBytes);
                    if (receviCount > 0)
                    {
                        freeTime = 0;
                        System.Diagnostics.Debug.WriteLine(string.Format("\r\n----------------------{0}------------------------", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff")));
                        string respose = Encoding.UTF8.GetString(nowReciveBytes, 0, receviCount);
                        System.Diagnostics.Debug.Write(respose);
                        Thread.Sleep(10);
                    }
                    else
                    {
                        //超过40S没有数据
                        if(freeTime<200)
                        {
                            freeTime++;
                        }
                        else if (Thread.CurrentThread.Name=="close")
                        {
                            Console.WriteLine("no data recevi so close the task");
                            break;
                        }
                        Thread.Sleep(freeTime);
                    }
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                    Console.WriteLine("应用程序主动关闭接收线程");
                    break;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                finally
                {

                }
            }
        }

    }
}
