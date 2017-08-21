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
                telnet.WaitFor("login");
                telnet.WriteLine("telnet");
                telnet.WaitFor("password");
                telnet.WriteLine("lijie1515");
                Console.WriteLine(telnet.WorkingData);

                //Console.WriteLine("-------------------------------------------");
                //Console.WriteLine(telnet.SessionLog);
                //Console.WriteLine("-------------------------------------------");

                Console.ReadLine();
                telnet.WriteLine("ls");
                telnet.WaitFor("$");
                Console.WriteLine(telnet.WorkingData);

                //Console.WriteLine("-------------------------------------------");
                //Console.WriteLine(telnet.SessionLog);
                //Console.WriteLine("-------------------------------------------");
            }
        }
    }
}
