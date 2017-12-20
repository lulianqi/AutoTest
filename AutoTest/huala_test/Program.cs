using MyCommonHelper.FileHelper;
using System;
using System.Collections.Generic;
using System.IO;
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
            string suuids = "4e74dc90-988e-4e58-beb0-dabc8b34019b,77d6b300-45dc-11e7-b83e-005056824593,82c09089-0863-4ff6-a729-42468de0c7f9,efc7f749-93c2-4386-be38-c9b455650af5,e9a9454d-5b27-4824-9a0c-ce46977a6b93,a7782913-1978-4a1a-9ab7-6878536c5fd3";
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

        private static void AnalysisNginxLog()
        {
            StreamReader sr = new StreamReader(@"D:\NG\login.txt", Encoding.UTF8);
            List<List<string>> loginList = new List<List<string>>();
            string temmUa=null;
            temmUa = sr.ReadLine();
            while(temmUa!=null)
            {
                int startIndex = temmUa.IndexOf("\"  \"", 0);
                startIndex = temmUa.IndexOf("\"  \"", startIndex+4);
                int endIndex = temmUa.IndexOf("\"", startIndex+4);
                if (startIndex > 0 && endIndex > 0)
                {
                    temmUa = temmUa.Substring(startIndex + 4, endIndex - startIndex-4);
                    #region 设备类型
                    string tempDivers;
                    if (temmUa.Contains("Android"))
                    {
                        tempDivers = "Android";
                    }
                    else if (temmUa.Contains("iPhone"))
                    {
                        tempDivers = "iPhone";
                    }
                    else
                    {
                        tempDivers = "Web";
                    } 
                    #endregion
                    
                    #region 浏览器
                    string tempBrowser = "unknow";
                    if (tempDivers == "Web")
                    {
                        if (temmUa.Contains("360SE"))
                        {
                            tempBrowser = "360SE";
                        }
                        else if (temmUa.Contains("360EE"))
                        {
                            tempBrowser = "360EE";
                        }
                        else if (temmUa.Contains("Maxthon"))
                        {
                            tempBrowser = "Maxthon";
                        }
                        else if (temmUa.Contains("Opera"))
                        {
                            tempBrowser = "Opera";
                        }
                        else if (temmUa.Contains("Firefox"))
                        {
                            tempBrowser = "Firefox";
                        }
                        else if (temmUa.Contains("MSIE"))
                        {
                            tempBrowser = temmUa.Substring(temmUa.IndexOf("MSIE"), 8);
                        }
                        else if (temmUa.Contains("Chrome"))
                        {
                            tempBrowser = temmUa.Substring(temmUa.IndexOf("Chrome"), 9);
                        }
                        else if (temmUa.Contains("Safari"))
                        {
                            tempBrowser = temmUa.Substring(temmUa.IndexOf("Safari"), 10);
                        }
                    } 
                    else
                    {
                        tempBrowser = "MobilePhone";
                    }
                    #endregion
                    loginList.Add((new string[] { temmUa, tempDivers ,tempBrowser}).ToList<string>());
                }
                else
                {
                    loginList.Add((new string[] { "errer", "errer", "errer" }).ToList<string>());
                }
                
                temmUa = sr.ReadLine();
            }

            CsvFileHelper.SaveCsvFile(@"D:\NG\new.csv", loginList, false , new System.Text.UTF8Encoding(false));

        }
   
        private static void CreatSeckillActivityOrder(string hltoken ,string userId ,string seckillActivityId , string seckillActivityGoodsId)
        {
            List<KeyValuePair<string,string>> heads=new List<KeyValuePair<string,string>>();
            heads.Add(new KeyValuePair<string,string>("Content-Type","application/json"));
            heads.Add(new KeyValuePair<string,string>("Cookie",string.Format("hltoken={0}; token=oN2Zfszme7aKT5VT3Jf0udj7PxvM; v3wx-token=\"5_FntEA12D0W_PoB7UQH4GlEx8EZNZM_YlyEOfQsoQ8UP_AwXUkdZKlJ41fomZH54R2gyZmYPpHDqx4EWoqV8Imw,oN2Zfszme7aKT5VT3Jf0udj7PxvM,o-r_hwFcvwXRdYBYd5fePChWiadk\"; USERID={1}; wxConfig=%7B%22signature%22%3A%22069f608d49f55f570f945bc1bb9449668994fb4f%22%2C%22appId%22%3A%22wx01f2ab6d9e41169a%22%2C%22nonceStr%22%3A%22ca4d171ec4fe43b6a0679cd9af28bad3%22%2C%22timestamp%22%3A%221513597811%22%7D; currentCity=%E5%AE%81%E6%B3%A2%E5%B8%82; currentLocation={%22address%22:%22%E6%B5%99%E6%B1%9F%E7%9C%81%E5%AE%81%E6%B3%A2%E5%B8%82%E6%B1%9F%E4%B8%9C%E5%8C%BA%E7%94%AC%E6%B1%9F%E5%A4%A7%E6%A1%A5%22%2C%22province%22:%22%E6%B5%99%E6%B1%9F%E7%9C%81%22%2C%22city%22:%22%E5%AE%81%E6%B3%A2%E5%B8%82%22%2C%22district%22:%22%E6%B1%9F%E4%B8%9C%E5%8C%BA%22%2C%22street%22:%22%E7%94%AC%E6%B1%9F%E5%A4%A7%E6%A1%A5%22%2C%22streetNumber%22:%22%22%2C%22lng%22:121.568649%2C%22lat%22:29.880267%2C%22signBuilding%22:%22%E7%94%AC%E6%B1%9F%E5%A4%A7%E6%A1%A5%E5%8D%9778%E7%B1%B3%22}",hltoken,userId)));
            string tempResponse=null;
            tempResponse = MyCommonHelper.NetHelper.MyWebTool.MyHttp.SendData(string.Format("https://wxwyjtest.huala.com/huala/v3/seckillActivity/canBuy?seckillActivityId={0}&seckillActivityGoodsId={1}", seckillActivityId, seckillActivityGoodsId), null, "GET", heads);
            Console.WriteLine();
        }

    }
}
