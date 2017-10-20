using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace huala_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            Sync_goods();
            Console.ReadLine();
        }

        private static void Sync_goods()
        {
            string goodStr = "1008010,1008011,1008012,1008013,1008014,1008015,1008016,1008017,1008018,1008019,1008020,1008021,1008022,1008023,1008024,1008025,1008026,1008027,1008028,1008029,1008030,1008031,1008032,1008033,1008034,1008035,1008036,1008037,1008038,1008039,1008040,1008041,1008042,1008043,1008044,1008045,1008046,1008047,1008075,1008076,1008077,1008078,1008079,1008080,1008081,1008082,1008083,1008084,1008085,1008086,1008087,1008088,1008089,1008090,1008091,1008092,1008093,1008094,1008095,1008096,1008097,1008098,1008099,1008100,1008101,1008102,1008103";
            string[] goodArr = goodStr.Split(',');
            foreach(string goodId in goodArr)
            {
                Console.WriteLine(MyCommonHelper.NetHelper.MyWebTool.MyHttp.SendData(String.Format("http://wxtest.huala.com/huala/v3/sync-goods?goodsId={0}", goodId)));
            }

        }
    }
}
