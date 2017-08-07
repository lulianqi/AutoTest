using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoForMyHelper
{
    class DemoForMySocket
    {
        MySocket ms;
        public DemoForMySocket()
        {
            ms = new MySocket("192.168.78.110:60000", 1000);
            ms.OnReceiveData += ms_OnReceiveData;
            ms.OnTcpConnected += ms_OnTcpConnected;
            ms.OnTcpConnectionLosted += ms_OnTcpConnectionLosted;
            
        }

        public void StartTcp()
        {

            if(!ms.connectClient())
            {
                Console.WriteLine(ms.myErroerMessage);
            }
            for (int i = 0; i < 10; i++)
            {
                if(!ms.sendData("it is test data", Encoding.UTF8))
                {
                    Console.WriteLine(ms.myErroerMessage);
                }
            }

            Console.WriteLine("any key to disconnect");
            Console.ReadKey();
            if (!ms.sendData("it is test data", Encoding.UTF8))
            {
                Console.WriteLine(ms.myErroerMessage);
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
        }

        void ms_OnReceiveData(byte[] yourData)
        {
            Console.WriteLine(Encoding.UTF8.GetString(yourData));
            Console.WriteLine(MyCommonHelper.MyEncryption.ByteToHexString(yourData,MyCommonHelper.MyEncryption.HexaDecimal.hex16,MyCommonHelper.MyEncryption.ShowHexMode.space));
        }
        
    }
}
