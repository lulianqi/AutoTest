using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            strList.Insert(4, "4");
            
        }

        public static void RunMyHttpTest()
        {
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

            MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", null, null, null, null, 1000, null);
            MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", null, null, ntds, null, 1000, null);
            MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", heads, "body", ntds, "a=1&b=2", 1000, Encoding.UTF8);
        }
    }
}
