using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCommonHelper;

namespace TestForDefaultClass
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "";
            var tempValue = str.Split(new char[] { ',' }, StringSplitOptions.None);
            string str_more = "123".Substring(2, 1);

            List<string> strList = new List<string> { "1","2"};
            //strList.Insert(4, "4");
            RunMyHttpTest();
        }

        public static void RunMyHttpTest()
        {
            Console.ReadLine();
            string testData_1 = " 0x01 0x02 0x03 0x04 0x05 0x06 0x06";
            byte[] result = MyEncryption.HexStringToByte(testData_1, 16, MyEncryption.ShowHexMode.spitSpace0x);
            byte[] tm = new byte[] { 1, 2, 3, 4, 244 };
            Console.WriteLine(MyCommonHelper.MyEncryption.ByteToHexString(tm, MyEncryption.HexaDecimal.hex16, MyEncryption.ShowHexMode.spitSpace0x));
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
