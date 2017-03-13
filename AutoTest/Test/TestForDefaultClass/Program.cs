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
    }
}
