#define TESTMODE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MyCommonHelper;

namespace TLSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DoMyTest();

            byte[] clientHello = MyBytes.HexStringToByte("16030100c6010000c2030351adaa772a453edd1c42c48e85d98c671e1619b06fa8a88641f27b43d2797a3c00001c5a5ac02bc02fc02cc030cca9cca8c013c014009c009d002f0035000a0100007daaaa0000ff0100010000000014001200000f642e62616977616e6469616e2e636e0017000000230000000d00140012040308040401050308050501080606010201000500050100000000001200000010000e000c02683208687474702f312e3175500000000b00020100000a000a0008aaaa001d001700187a7a000100",
                HexaDecimal.hex16, ShowHexMode.@null);

            Console.ReadLine();
            MyTLS myTLS = new MyTLS();
            myTLS.Connect();
            Console.WriteLine("enter to say [ Handshake - Client Hello ]");
            Console.ReadLine();
            myTLS.SendData(clientHello);
            Console.ReadLine();
            myTLS.Dispose();
            Console.WriteLine("enter to exti");
            Console.ReadLine();
        }

        static void DoMyTest()
        {
            Console.ReadLine();
            TLSPacket.TLSPlaintext tp = new TLSPacket.TLSPlaintext(TLSPacket.TLSContentType.Handshake, new TLSPacket.ProtocolVersion(0x03, 0x01));
            string tempRaw = MyBytes.ByteToHexString(tp.GetRawData(198), HexaDecimal.hex16, ShowHexMode.space);
            Console.WriteLine(tempRaw);

            TLSPacket.ClientHello ch = new TLSPacket.ClientHello("d.baiwandian.cn");
            tempRaw = MyBytes.ByteToHexString(ch.GetProtocolRawData(), HexaDecimal.hex16, ShowHexMode.space);
            Console.WriteLine(tempRaw);


            Console.WriteLine("enter to DoMyTest");
            Console.ReadLine();
        }


    }

    class MyTLS
    {
        private Socket mySocket;
        private string originPath = "https://d.baiwandian.cn/login#/phoneLogin";
        private string host = "d.baiwandian.cn";
        private IPAddress connctHost;
        Thread reciveThread;
        public void Connect()
        {
            System.Net.IPHostEntry hostIps = System.Net.Dns.GetHostEntry(host);
            connctHost = hostIps.AddressList[0];

#if TESTMODE
                    System.Diagnostics.Debug.WriteLine("-------------------------------------");
                    System.Diagnostics.Debug.WriteLine("Dns back");
                    foreach (var tempIp in hostIps.AddressList)
                    {
                        System.Diagnostics.Debug.WriteLine(tempIp.ToString());
                    }
                    System.Diagnostics.Debug.WriteLine("-------------------------------------");
#endif

            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //mySocket.NoDelay = true;
                IPEndPoint hostEndPoint = new IPEndPoint(connctHost, 443);
                mySocket.Connect(hostEndPoint);
                {
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(ReceviData), mySocket);  //这里使用线程池将失去部分对线程的控制能力(创建及启动会自动被延迟)
                    reciveThread = new Thread(new ParameterizedThreadStart(ReceviData));
                    reciveThread.IsBackground = true;
                    reciveThread.Start(mySocket);
                }
                Console.WriteLine("connect ok");
        }

        public void SendData(byte[] requestRawBytes)
        {
            if (requestRawBytes == null)
            {
                return;
            }
            if (mySocket == null)
            {
                Console.WriteLine("the pipe is not connect");
                return;
            }
            if (!mySocket.Connected)
            {
                Console.WriteLine("the pipe is dis connect");
                return;
            }
            try
            {
                mySocket.Send(requestRawBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 
        private void ReceviData(object yourSocket)
        {
            byte[] nowReciveBytes = new byte[1024 * 128];
            Socket nowSocket = (Socket)yourSocket;
            int receiveCount = 0;
            while (true)
            {
                if (!nowSocket.Connected)
                {
                    Console.WriteLine("the tcp is disconnect");
                    break;
                }
                try
                {
                    receiveCount = nowSocket.Receive(nowReciveBytes);
                    {
                        byte[] tempOutBytes = new byte[receiveCount];
                        Array.Copy(nowReciveBytes, tempOutBytes, receiveCount);
                        //ReportPipeResponse(tempOutBytes);
                        System.Diagnostics.Debug.WriteLine(string.Format("\r\n----------------------{0}------------------------", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff")));
                        //string respose = Encoding.UTF8.GetString(nowReciveBytes, 0, receiveCount);
                        string respose = MyBytes.ByteToHexString(tempOutBytes, HexaDecimal.hex16, ShowHexMode.space);
                        System.Diagnostics.Debug.Write(respose);
                        Thread.Sleep(10);
                    }

                }
                catch (System.Threading.ThreadAbortException)
                {
                    Console.WriteLine("Applications active close ");//应用程序主动关闭接收线程
                    nowSocket.Close();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);//应用程序被动关闭接收线程
                    nowSocket.Close();
                    break;
                }
                finally
                {

                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (mySocket != null)
            {
                mySocket.Close();
                mySocket.Dispose();
                mySocket = null;
            }
            if (reciveThread != null)
            {
                reciveThread.Abort();
                reciveThread = null;
            }
        }

    }
}
