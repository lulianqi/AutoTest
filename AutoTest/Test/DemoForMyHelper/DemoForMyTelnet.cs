using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoForMyHelper
{
    class DemoForMyTelnet
    {
        MyTelnet telnet;
        public DemoForMyTelnet()
        {
            telnet = new MyTelnet("192.168.200.150", 23, 5);
        }

        public void Strat()
        {

            if (telnet.Connect() == false)
            {
                Console.WriteLine("connect fail ");
            }
            else
            {
                telnet.WaitStr("login");
                telnet.WriteLine("telnet");
                telnet.WaitStr("password");
                telnet.WriteLine("lijie1515");
                telnet.WaitStr("$");
                Console.WriteLine(telnet.GetAndMoveShowData());

                //Console.WriteLine("-------------------------------------------");
                //Console.WriteLine(telnet.SessionLog);
                //Console.WriteLine("-------------------------------------------");

                
                
                Console.WriteLine(telnet.GetAndMoveShowData());
                telnet.WriteLine("ls");
                telnet.WaitStr("$");
                Console.WriteLine(telnet.GetAndMoveShowData());

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(telnet.AllLogData);
                Console.WriteLine("-------------------------------------------");

                Console.ReadLine();
                telnet.OnMesageReport += telnet_OnMesageReport;
                for(int i=0;i<10;i++)
                {
                    telnet.WriteLine("netstat");
                }
                telnet.OnMesageReport -= telnet_OnMesageReport;

                Console.ReadLine();
                Console.WriteLine("******************************************");
                Console.WriteLine(telnet.DoRequest("ll","$ "));
                Console.WriteLine("******************************************");
                Console.WriteLine(telnet.DoRequest("mkdir 123", "$ "));
                Console.WriteLine("******************************************");
                Console.WriteLine(telnet.DoRequest("ll", "$ "));
                Console.WriteLine("******************************************");

                Console.ReadLine();
                telnet.DisConnect();


            }
        }

        void telnet_OnMesageReport(string mesStr, TelnetMessageType mesType)
        {
            if(mesType== TelnetMessageType.ShowData)
            {
                Console.Write(mesStr);
            }else
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
        }
    }
}
