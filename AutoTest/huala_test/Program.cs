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
            CreateQrCode();
            Console.ReadLine();
        }

        private static void Sync_goods()
        {
            string goodStr = "1008010,1008011,1008012,1008013,1008014,1008015,1008016,1008017,1008018,1008019,1008020,1008021,1008022,1008023,1008024,1008025,1008026,1008027,1008028,1008029,1008030,1008031,1008032,1008033,1008034,1008035,1008036,1008037,1008038,1008039,1008040,1008041,1008042,1008043,1008044,1008045,1008046,1008047,1008075,1008076,1008077,1008078,1008079,1008080,1008081,1008082,1008083,1008084,1008085,1008086,1008087,1008088,1008089,1008090,1008091,1008092,1008093,1008094,1008095,1008096,1008097,1008098,1008099,1008100,1008101,1008102,1008103";
            string[] goodArr = goodStr.Split(',');
            foreach (string goodId in goodArr)
            {
                Console.WriteLine(MyCommonHelper.NetHelper.MyWebTool.MyHttp.SendData(String.Format("http://wxtest.huala.com/huala/v3/sync-goods?goodsId={0}", goodId)));
            }

        }

        private static void CreateQrCode()
        {
            string suuids = "8fc493d2-a9d1-4a11-a007-34ed59ca813f,4527c714-b16f-44db-8b86-c384cd3abf4b,a20c170d-ddac-48dd-9b26-201bd4791aa2,3711af7a-350e-4ce4-a0a9-a01030b5bad8,88d0ef3f-322f-46a3-8fa9-6754dd57f964,add27d4c-3a77-449b-ae70-f32a850a026e,2a49c3a3-945d-444a-863c-3e859e12b66b,b8d63b97-dc59-4bea-be27-1f394a4e90d9,36f0e30d-ddaf-41eb-a8b3-3f74994a7ff0,a9bc04e2-b9d7-4a13-90a7-88e8d860cf14,a647b85b-4d24-4b7e-b2b1-2e3f184b2d70,a6fd5b9d-da8a-4168-82fc-881818c6b258,c3f5ad39-0f42-4c86-941c-727a927d7fd6,454df581-f2e2-4530-826a-74fc9626e248,ee9ab441-e6f7-4677-a566-307a81c5afeb,a6ec2ad0-4a34-42e2-9b1a-708aa7fafe8f,b0d52016-45f7-11e7-b83e-005056824593,fe1457ad-45da-11e7-b83e-005056824593,26192659-45df-11e7-b83e-005056824593,1b502c04-45df-11e7-b83e-005056824593,3c449c41-ace6-4d85-b1a8-dfd883f1a074,8b89cb24-137e-4297-b32b-9721a5dbcab7,fd074bf7-f79e-4e2b-9e14-e0e5a31f0695,b0da768e-45f7-11e7-b83e-005056824593,41d5fd95-45dc-11e7-b83e-005056824593,1913cf7b-506f-4c5c-a6c8-2e1a1a18183d,1b5207ea-45df-11e7-b83e-005056824593,497c9977-6bf6-4ebe-878b-c90174b2083d,152de762-536d-4681-98f4-537e34de3c40";
            string[] suuidAr = suuids.Split(',');
            foreach (string nowSuuid in suuidAr)
            {
                System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------------------------");
                System.Diagnostics.Debug.WriteLine(nowSuuid);
                string tempRespans = MyCommonHelper.NetHelper.MyWebTool.MyHttp.SendData(String.Format("http://wxv4.huala.com/huala/weixin/createQrCode?suid={0}", nowSuuid));
                Console.WriteLine(tempRespans);
                System.Diagnostics.Debug.WriteLine(tempRespans);
            }

        }
    }
}
