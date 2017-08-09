using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoForMyHelper
{
    class DemoForMySocket
    {
        MyTcpClient ms;
        public DemoForMySocket()
        {
            ms = new MyTcpClient("192.168.78.110:60000");
            ms.OnReceiveData += ms_OnReceiveData;
            ms.OnTcpConnected += ms_OnTcpConnected;
            ms.OnTcpConnectionLosted += ms_OnTcpConnectionLosted;
            
        }

        public void StartTcp()
        {

            //if(!ms.ConnectClient())
            if (!ms.ConnectAsync())
            {
                Console.WriteLine(ms.ErroerMessage);
            }
            System.Threading.Thread.Sleep(2000);
            for (int i = 0; i < 10; i++)
            {
                if(!ms.SendData("it is test data", Encoding.UTF8))
                {
                    Console.WriteLine(ms.ErroerMessage);
                }
            }

            Console.WriteLine("any key to disconnect");
            Console.ReadKey();
            for (int i = 0; i < 10; i++)
            {
                System.Threading.Thread.Sleep(10);
                if (!ms.SendData("it is test data", Encoding.UTF8))
                {
                    Console.WriteLine(ms.ErroerMessage);
                }
            }

            
            for (int i = 0; i < 10; i++)
            {
                Console.ReadKey();
                byte[] bs = ms.ReceiveAllData();
                if(bs !=null)
                 ms_OnReceiveData(bs);
            }
            //ms.disConnectClient();
        }

        void ms_OnTcpConnectionLosted()
        {
            Console.WriteLine("ms_OnTcpConnectionLosted");
        }

        void ms_OnTcpConnected(string yourInfo)
        {
            Console.WriteLine("ms_OnTcpConnected");
            Console.WriteLine(yourInfo);
        }

        void ms_OnReceiveData(byte[] yourData)
        {
            Console.WriteLine(Encoding.UTF8.GetString(yourData));
            Console.WriteLine(MyCommonHelper.MyBytes.ByteToHexString(yourData, MyCommonHelper.HexaDecimal.hex16, MyCommonHelper.ShowHexMode.space));
        }
        
    }
}
