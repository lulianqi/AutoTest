using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;


namespace TestForCLR
{
    class TestForGC_1
    {
        public void Run()
        {
            Timer t = new Timer(MyTimerCallback, null, 0, 1000);
            Console.ReadLine();
            GC.Collect();
            Console.ReadLine();
        }

        public void RunAsThread()
        {
            Thread t = new Thread(new ParameterizedThreadStart(MyTimerCallback));
            t.Start();
            Console.ReadLine();
            GC.Collect();
            Console.ReadLine();
        }

        private void MyTimerCallback(object state)
        {
             Console.WriteLine(DateTime.Now);
        }
        private void MyTimerCallback()
        {
            while (true)
            {
                Console.WriteLine(DateTime.Now);
                Thread.Sleep(1000);
            }
        }
    }
}
