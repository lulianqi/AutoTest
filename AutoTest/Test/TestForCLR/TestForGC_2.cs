using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestForCLR
{
    class TestForGC_2
    {
        
        public void Run()
        {
            byte[] bts = new byte[] { 1, 2, 3, 4 };
            FileStream fs = new FileStream("lj", FileMode.Create);
            fs.Write(bts, 0, 4);
            //fs.Dispose();
            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
#if (DEBUG)
            Console.WriteLine("d");
#else
            Console.WriteLine("R");
#endif
            //GC.Collect();
            File.Delete("lj");
        }
    }
}
