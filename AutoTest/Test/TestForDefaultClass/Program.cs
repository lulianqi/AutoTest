using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCommonHelper;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TestForDefaultClass
{
    class ClassA
    {
        [MethodImplAttribute(MethodImplOptions.Synchronized)]
        public void ThearTest(string str)
        {
            
            for(int i =0;i<10;i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine(str);
            }
        }
    }
    class Program
    {
        public delegate void Mydelegate();
        static void Main(string[] args)
        {
            Process myProcess = Process.GetCurrentProcess();
            myProcess.StartInfo.FileName = "new test ";
            myProcess.PriorityClass = ProcessPriorityClass.High;

            ExecutionContext.SuppressFlow();

            Console.ReadKey(); 
            RunTestForManualResetEvent();
            //for (int i = 0; i < 500; i++)
            {
                Console.ReadKey();
                manualResetEvent.Set();
                //manualResetEvent.Reset();
            }
            Console.WriteLine("end of RunTestForManualResetEvent");
            Console.ReadKey(); 
            RunSetAndJudge();
            Console.ReadKey();
            RunMyHiPerformanceTickTest();
            string str = "";
            var tempValue = str.Split(new char[] { ',' }, StringSplitOptions.None);
            string str_more = "123".Substring(2, 1);

            List<string> strList = new List<string> { "1","2"};
            //strList.Insert(4, "4");
            RunMyHttpTest();
        }

        public static void RunSynchronizedTest()
        {
            Mydelegate md;
            md = null;
            md += new Mydelegate(() => { Console.WriteLine("1"); });
            md += new Mydelegate(() => { Console.WriteLine("2"); });
            md += new Mydelegate(() => { Console.WriteLine("4"); });
            md.Invoke();
            md = null;
            //md.Invoke();
            ClassA a = new ClassA();
            ClassA b = new ClassA();
            Thread t1 = new Thread(new ThreadStart(() => { a.ThearTest("1"); }));
            Thread t2 = new Thread(new ThreadStart(() => { a.ThearTest("2"); }));
            Thread t3 = new Thread(new ThreadStart(() => { b.ThearTest("3"); }));
            Thread t4 = new Thread(new ThreadStart(() => { b.ThearTest("4"); }));
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            Console.WriteLine("Stort");
        }
        public static void RunMyHiPerformanceTickTest()
        {
            Console.ReadLine();
            MyHiPerformanceTick myTick = new MyHiPerformanceTick();
            myTick.StartTick();
            System.Threading.Thread.Sleep(1000);
            myTick.EndTick();
            Console.WriteLine(myTick.GetElapsedTick().ToString());
            Console.WriteLine(myTick.ToString());
            Console.ReadLine();
            Stopwatch myStopWatch = new Stopwatch();
            long[] ls = new long[100];
            myStopWatch.Start();
            for (int i = 0; i < 100; i++)
            {
                ls[i] = myStopWatch.ElapsedTicks;
            }
            myStopWatch.Stop();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(ls[i]);
            }
            
            Console.WriteLine(myStopWatch.ElapsedTicks);
            long l1, l2, l3, l4, l5 = 0;
            long [] ll =new long[100];
            Console.WriteLine(myTick.ToString());
            for(int i=0; i<100; i++)
            {
                ll[i] = myTick.GetTick();
            }
            
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(ll[i]);
            }
            myTick.StartTick();
            myTick.EndTick();
            Console.WriteLine(myTick.GetElapsedTime());
            
            double[] dl = new double[100];
            for (int i = 0; i < 100; i++)
            {
                dl[i] = myTick.GetTime();
            }
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(dl[i]);
            }
            Console.WriteLine("----------------------------------------------");
            Console.ReadLine();
            myStopWatch.Restart();
            myTick.StartTick();
            for (int i = 0; i < 100; i++)
            {
                int temp = i / 4;
            }
            myTick.EndTick();
            myStopWatch.Stop();
            Console.WriteLine(myStopWatch.ElapsedTicks);
            Console.WriteLine(myTick.GetElapsedTime());
        }

        public static void RunMyHttpTest()
        {
            for (int m = 0; m < 100; m++)
            {
                Console.ReadLine();
                Console.WriteLine(Environment.TickCount);
                long l1 = DateTime.Now.Ticks;
                StringBuilder xxb = new StringBuilder("");
                for (int i = 0; i < 100; i++)
                {
                    xxb.Append("123456789abcdefg");
                }
                xxb.ToString();

                long l2 = DateTime.Now.Ticks;
                string xx = "";
                for (int i = 0; i < 100; i++)
                {
                    xx += "123456789abcdefg";
                }
                
                long l3 = DateTime.Now.Ticks;
                Console.WriteLine(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}", l1, l2, l3, l2 - l1, l3 - l2));
            }
            Console.ReadLine();
            Console.WriteLine(MyWebTool.MyHttp.SendData("http://pv.sohu.com/cityjson?ie=utf-8", null, "POST",null,@"D:\shou.txt"));
            Console.WriteLine(MyWebTool.MyHttp.SendData("http://pv.sohu.com/cityjson?ie=utf-8", null, "POST", null, @"D:\shou.txt"));
            Console.WriteLine(MyWebTool.MyHttp.SendData("http://pv.sohu.com/cityjson?ie=utf-8", null, "POST", null, @"D:\shou.txt"));
            Console.WriteLine(MyWebTool.MyHttp.SendData("https://pv.sohu.com/cityjson?ie=utf-8", null, "POST", null, @"D:\shou.txt"));
            Console.WriteLine(MyWebTool.MyHttp.SendData("https://pv.sohu.com/cityjson?ie=utf-8", null, "POST", null, @"D:\shou.txt"));
            Console.ReadLine();
            MyWebTool.MyHttp.showResponseHeads = true;
            Console.WriteLine(MyWebTool.MyHttp.SendData("http://www.baidu.com", null, "Get", null, @"D:\baidu.txt"));
            Console.ReadLine();
            MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", 10000, "name", "filenmae", false, "testdata", "a=1&b=2&c=3");
            MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", 10000, "name", "filenmae", false, "testdata", null);
            List<MyWebTool.HttpMultipartDate> ntds = new List<MyWebTool.HttpMultipartDate>();
            List<KeyValuePair<string, string>> heads = new List<KeyValuePair<string, string>>();
            heads.Add(new KeyValuePair<string, string>("Hostx", "HttpPostData"));
            heads.Add(new KeyValuePair<string, string>("xxx", "HttpPostData"));

            ntds.Add(new MyWebTool.HttpMultipartDate("name", "filename", null, false, "test data"));
            ntds.Add(new MyWebTool.HttpMultipartDate("name", "filename", "type", false, "test data"));
            ntds.Add(new MyWebTool.HttpMultipartDate(null, null, "type", false, ""));
            ntds.Add(new MyWebTool.HttpMultipartDate(null, null, "type", false, null));
            ntds.Add(new MyWebTool.HttpMultipartDate("name", "filename", null, true, @"C:\Users\administer\Desktop\new 2")); //@"C:\Users\cllq\Desktop\CSV\my.csv"

            Console.WriteLine(MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", null, null, null, null, 1000, null));
            Console.WriteLine(MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", null, null, ntds, null, 1000, null));
            Console.WriteLine(MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", heads, "body", ntds, "a=1&b=2", 1000, Encoding.UTF8));
            Console.ReadLine();
            MyWebTool.MyHttp.showResponseHeads = true;
            Console.WriteLine(MyWebTool.MyHttp.SendData("http://pv.sohu.com/cityjson?ie=utf-8", null, "Get"));
            Console.WriteLine(MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", heads, "body", ntds, "a=1&b=2", 1000, Encoding.UTF8));
            Console.ReadLine();
        }

        public static void RunSetAndJudge()
        {
            Stopwatch myWatch = new Stopwatch();
            int myInt = 0;
            long l1, l2 = 0l;
            for (int m = 0; m < 100; m++)
            {
                myWatch.Restart();
                for (int i = 0; i < 10000; i++)
                {
                    myInt = i;
                }
                myWatch.Stop();
                l1 = myWatch.ElapsedTicks;
                myWatch.Restart();
                for (int i = 0; i < 10000; i++)
                {
                    if (myInt == i)
                    {

                    }
                }
                myWatch.Stop();
                l2 = myWatch.ElapsedTicks;
                Console.WriteLine("Set:  @@@" + l1);
                Console.WriteLine("Judge:---" + l2);
            }
        }

        //public static AutoResetEvent manualResetEvent = new AutoResetEvent(false);
        public static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        public static void RunTestForManualResetEvent()
        {
            //ExecutionContext.SuppressFlow();
            for(int i=0;i<100000;i++)
            {
                Thread td = new Thread(new ParameterizedThreadStart((object ob) => {  Console.WriteLine("start > " + ((int)ob).ToString()); Thread.Sleep(1000); manualResetEvent.WaitOne(); Console.WriteLine("id is " + ((int)ob).ToString()); }), 0);
                //td.Priority = ThreadPriority.AboveNormal;
                td.Start(i);
            }
            //ExecutionContext.RestoreFlow();
        }
    }
}
