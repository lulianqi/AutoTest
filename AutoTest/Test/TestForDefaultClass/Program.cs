using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCommonHelper;
using System.Diagnostics;

namespace TestForDefaultClass
{
    class Program
    {
        static void Main(string[] args)
        {
            RunMyHiPerformanceTickTest();
            string str = "";
            var tempValue = str.Split(new char[] { ',' }, StringSplitOptions.None);
            string str_more = "123".Substring(2, 1);

            List<string> strList = new List<string> { "1","2"};
            //strList.Insert(4, "4");
            RunMyHttpTest();
        }

        public static void RunMyHiPerformanceTickTest()
        {
            Console.ReadLine();
            MyHiPerformanceTick myTick = new MyHiPerformanceTick();
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
    }
}
