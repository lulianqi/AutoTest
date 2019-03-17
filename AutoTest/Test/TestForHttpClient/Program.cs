using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Threading;
using MyCommonHelper.NetHelper;

namespace TestForHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            myHttp = new MyWebTool.MyHttp();
            Console.ReadLine();
            for (int i = 0; i < 500; i++)
            {
                Do();
            }
            //Do2();
            Console.ReadLine();
        }

        static MyWebTool.MyHttp myHttp;
        static ManualResetEvent mr = new ManualResetEvent(false);
        static void Do2()
        {
            

            List<Thread> ll = new List<Thread>();
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            st.Start();
            for (int i = 0; i < 500; i++)
            {
                Thread checkUpgradeTaskThread = new Thread(new ThreadStart(CheckUpgrade));
                checkUpgradeTaskThread.Name = "CheckUpgradeTaskThread";
                ll.Add(checkUpgradeTaskThread);
            }
            st.Stop();
            System.Diagnostics.Debug.WriteLine("----------------------------------------");
            System.Diagnostics.Debug.WriteLine(st.ElapsedMilliseconds);

            st.Reset();
            for (int i = 0; i < 500; i++)
            {
                ll[i].Start();
            }
            st.Stop();
            System.Diagnostics.Debug.WriteLine(st.ElapsedMilliseconds);

            Thread.Sleep(10000);
            mr.Set();

        }

        static void CheckUpgrade()
        {
            myHttp.SendData("http://lulianqi.com/sns/hello", null, "GET", null, null, mr);
        }
        static async Task Do()
        {
            // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    //http://api.lulianqi.com/UpdateCheck/v1?user=Null
                    //http://lulianqi.com/sns/hello
                    HttpResponseMessage response = await client.GetAsync("http://lulianqi.com/sns/hello");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
        }

    }
}
