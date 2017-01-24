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
            //new DemoForMySsh().RunFileMv();
            new DemoForMySql().RunTest();
            Console.ReadKey();

        }
    }
}
