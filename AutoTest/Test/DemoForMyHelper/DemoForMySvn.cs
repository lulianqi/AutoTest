using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySvnHelper;

namespace DemoForMyHelper
{
    class DemoForMySvn
    {
        MySvn mySvn = new MySvn();

        public void Run()
        {
            Console.WriteLine("Start Run");
            if (mySvn.OnGetSnvMessage == null)
            {
                mySvn.OnGetSnvMessage += new MySvn.delegateGetSnvMessageEventHandler((obj, mes) => { Console.WriteLine(mes); });
            }
            if (mySvn.OnGetSnvStateInfo == null)
            {
                mySvn.OnGetSnvStateInfo += new MySvn.delegateGetSnvMessageEventHandler((obj, mes) => { Console.WriteLine(mes); });
            }
            mySvn.CheckOut(@"https://192.168.200.30:18080/svn/P1003/branches/html/huala-vue", @"D:\Ds\test");
            mySvn.GetLogs(@"D:\Ds\test");
        }
    }
}
