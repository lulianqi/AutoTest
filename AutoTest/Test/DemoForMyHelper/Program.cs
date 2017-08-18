using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoForMyHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            //new DemoForMySvn().Run();
            //new DemoForMySsh().RunShellTest();
            //new DemoForMySql().RunTest();
            //new DemoForMyVoice().Run();
            //DemoForActionMQ.RunTest();
            //new DemoForMySocket().StartTcp();
            //new DemoForMySerialPort().MySerialPortStart();
            new DemoForMyTelnet().Strat();
            Console.WriteLine("end of demo");
            Console.ReadKey();

        }
    }
}
