﻿using MyCommonHelper.FileHelper;
using MySqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
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
   
        private static void CreatSeckillActivityOrder(string hltoken ,string addressId ,string seckillActivityId , string seckillActivityGoodsId)
        {
            List<KeyValuePair<string,string>> heads=new List<KeyValuePair<string,string>>();
            heads.Add(new KeyValuePair<string,string>("Content-Type","application/json"));
            heads.Add(new KeyValuePair<string,string>("Cookie",string.Format("hltoken={0}",hltoken)));
            string tempResponse=null;
            tempResponse = MyCommonHelper.NetHelper.MyWebTool.MyHttp.SendData(string.Format("https://wxwyjtest.huala.com/huala/v3/seckillActivity/canBuy?seckillActivityId={0}&seckillActivityGoodsId={1}", seckillActivityId, seckillActivityGoodsId), null, "GET", heads);
            Console.WriteLine();
        }

        private static void TestForAllInOneInterface()
        {
            //MySqlDrive mySql = new MySqlDrive("Server=192.168.200.152;UserId=root;Password=xpsh;Database=huala_test");
            MySqlDrive mySql = new MySqlDrive("Server=192.168.200.24;UserId=qa;Password=123456;Database=xinyunlian_member");
            mySql.OnDriveStateChange += mySql_OnDriveStateChange;
            mySql.OnGetErrorMessage += mySql_OnGetErrorMessage;
            mySql.OnGetInfoMessage += mySql_OnGetInfoMessage;

            DataTable myTable = mySql.ExecuteQuery("select store_name , SUID,UUID from store limit 100 , 300");
            if (myTable!=null)
            {
                foreach (DataRow rows in myTable.Rows)
                {
                    bool isDataOk = true;
                    foreach (var filed in rows.ItemArray)
                    {
                        if(filed==null)
                        {
                            isDataOk = false;
                            break;
                        }
                    }
                    if(!isDataOk)
                    {
                        Console.WriteLine("find Error data");
                        break;
                    }
                    Console.WriteLine(String.Format("{0},开始同步......."));
                    string tempRespans = MyCommonHelper.NetHelper.MyWebTool.MyHttp.SendData(String.Format("http://wxv4.huala.com/huala/weixin/createQrCode?suid={0}", nowSuuid));
                    Console.WriteLine(tempRespans);
                }
            }
            else
            {
                Console.WriteLine(mySql.NowError);
            }


            mySql.OnDriveStateChange -= mySql_OnDriveStateChange;
            mySql.OnGetErrorMessage -= mySql_OnGetErrorMessage;
            mySql.OnGetInfoMessage -= mySql_OnGetInfoMessage;
            mySql.Dispose();
        }

        static void mySql_OnGetInfoMessage(object sender, string InfoMessage)
        {
            Console.WriteLine("mySql_OnGetInfoMessage");
            Console.WriteLine(InfoMessage);
        }

        static void mySql_OnGetErrorMessage(object sender, string ErrorMessage)
        {
            Console.WriteLine("mySql_OnGetErrorMessage");
            Console.WriteLine(ErrorMessage);
        }

        static void mySql_OnDriveStateChange(object sender, bool isCounect)
        {
            Console.WriteLine("mySql_OnDriveStateChange");
            Console.WriteLine(isCounect.ToString());
        }
    }
}
