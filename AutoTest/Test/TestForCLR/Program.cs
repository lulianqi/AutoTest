using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestForCLR
{
    class Program
    {
        static void Main(string[] args)
        {

            new TestForGC_2().Run();
            //new TestForGC_1().RunAsThread();
            Console.ReadLine();
            GC.Collect();
            Console.WriteLine("Over put any key to exit");
            Console.ReadKey();
        }
    }
}
