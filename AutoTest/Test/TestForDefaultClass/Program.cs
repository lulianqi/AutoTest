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

            List<string> strList = new List<string> { "1","2"};
            strList.Insert(4, "4");
        }
    }
}
