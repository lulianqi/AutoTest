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

            new TestForGC_3().RunTest();
            //new TestForGC_1().RunAsThread();
            Console.ReadLine();
            GC.Collect();
            byte[] bytes = new byte[1024];
            for (int i = 0; i < 1024;i++ )
            {
                bytes[i] = 0xaa;
            }

                Console.WriteLine("Over put any key to exit");
            Console.ReadKey();
        }
    }
}
