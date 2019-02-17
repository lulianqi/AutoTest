using MyCommonHelper.FileHelper;
using MySqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;

namespace huala_test
{
    class Program
    {
        static MyCommonHelper.NetHelper.MyWebTool.MyHttp myHttp = new MyCommonHelper.NetHelper.MyWebTool.MyHttp();

        public class TestBaseClass
        {
            public TestBaseClass()
            {
                Console.WriteLine("initialize TestBaseClass");
            }
            public void DoTest()
            {
                Console.WriteLine("TestBaseClass");
            }

            public static explicit operator int(TestBaseClass fahr)
            {
                return 0;
            }
        }

        public class TestClass : TestBaseClass
        {
            public string Name = "TestClass";

            public TestClass()
            {
                Console.WriteLine("initialize TestClass");
            }

            public TestClass(string value)
            {
                Console.WriteLine("initialize "+ value);
            }
            public new void DoTest()
            {
                Console.WriteLine("TestClass");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("any key to start");
            Console.ReadLine();
            var xx = MyCommonHelper.EncryptionHelper.MyRSA.VerifyPwdData(@"b1FtfA/sGjCCwC/hQJ/z023hdkK1I3dADHKoc30Pca23kFEV+sTSb+Gg3nl/fpAveO3n6fCR4kk1voKGkqV5+Yt1PIcamVawpR4N31hUadOCrF2OjfN93e35Ls1HGEjwMPIgwMkHRLfxfiiYyTZcQvsHW0afBgLJipImCyFRYxk=", @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDC7kw8r6tq43pwApYvkJ5laljaN9BZb21TAIfT/vexbobzH7Q8SUdP5uDPXEBKzOjx2L28y7Xs1d9v3tdPfKI2LR7PAzWBmDMn8riHrDDNpUpJnlAGUqJG9ooPn8j7YNpcxCa1iybOlc2kEhmJn5uwoanQq+CA6agNkqly2H4j6wIDAQAB");
            byte[] tempBytes = MyCommonHelper.EncryptionHelper.MyRSA.Encrypt(Encoding.UTF8.GetBytes("9999"), Convert.FromBase64String("ALeCvfr7nJaqOxsGU30RE7De2C/WCsIQlCKQO+zMMMJ/8ANJbsndk4ZFFlaFa4A6lBEPDxFL4NfVtCupTJmKzDF33FPhaNrRgZYJNxRz38k2L/TVVFIYFxPaNDJlSjmAQ19tf35AUr2ZBNqnkR8cSRbYcoxNTaAKX2jS5ENL8QSn"), Convert.FromBase64String("AQAB"));
            Console.WriteLine(Convert.ToBase64String(tempBytes));
            System.Diagnostics.Debug.WriteLine(System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(tempBytes)));
            Console.ReadLine();
            LoginBaiwandian("15158155511", "9999");
            Console.ReadLine();
            AnalysisGbCode();
            Console.WriteLine("any key to start");
            Console.ReadLine();
            AnalysisIpLog();
            Console.ReadLine();
            System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------------------------");
            GetCompanyInfo("济南市历城区永隆商店");
            System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------------------------");
            GetCompanyInfo("济南市历城区便民食品商店");
            System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------------------------");
            GetCompanyInfo("济南市历城区自信商行");
            System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------------------------");
            GetCompanyInfo("济南市长清区常来商店");

            //AnalysisIpLog();
            //AnalysisDTBLog();
            Console.ReadLine();
            MoreTestForAllInOneInterface();
            //TestForAllInOneInterface();
            Console.WriteLine("any key to exit");
            Console.ReadLine();
        }

        private static void LoginBaiwandian(string name,string pwd)
        {
            myHttp.IsWithDefaultCookieContainer = true;
            string tempHost = "ecom-qa.baiwandian.cn";
            string tempRequest;
            string tempToken;
            tempRequest = myHttp.SendHttpRequest(string.Format(@"http://{0}/xinyunlian-weixin-ecom/wx/login.jhtml", tempHost),null,"GET",null,true,null,null).ResponseRaw;
            Console.WriteLine(tempRequest);
            //Set-Cookie: token=aa62a95c-ca89-4ab8-a7ee-fd9561055213;path=/
            tempToken = PickStrParameter("token=", ";path=/", tempRequest);
            if (tempToken == null) 
            {
                Console.WriteLine("can not find token");
                return;
            }
            tempRequest = myHttp.SendHttpRequest(string.Format(@"http://{0}/xinyunlian-weixin-ecom/common/public_key.jhtml?_={1}", tempHost, (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000), null, "GET", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("token", tempToken) },true,null,null).ResponseRaw;
            Console.WriteLine(tempRequest);
            string tempExponent = PickStrParameter("\"exponent\":\"", "\"", tempRequest);
            string tempModulus = PickStrParameter("\"modulus\":\"", "\",", tempRequest);
            if(tempExponent==null || tempModulus==null)
            {
                Console.WriteLine("can not find public key");
                return;
            }
            byte[] keyModulus =  Convert.FromBase64String(tempModulus);
            //byte[] keyModulus_ = new byte[keyModulus.Length - 1];
            //Array.Copy(keyModulus, 1, keyModulus_, 0, keyModulus.Length - 1);
            //keyModulus = keyModulus_;
            //BigInteger.Parse()
            //Array.Reverse(keyModulus);
            byte[] tempPwdBytes = MyCommonHelper.EncryptionHelper.MyRSA.Encrypt(Encoding.UTF8.GetBytes("9999"), keyModulus, Convert.FromBase64String(tempExponent));
            //Array.Reverse(tempPwdBytes);
            string tempPwd = System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(tempPwdBytes));
            tempRequest = myHttp.SendHttpRequest(string.Format(@"http://{0}/xinyunlian-weixin-ecom/wx/login/submit.jhtml", tempHost), string.Format("username=15158155511&enPassword={0}&sn=&agentName=", tempPwd), "POST", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("token", tempToken), new KeyValuePair<string, string>("X-Requested-With", "XMLHttpRequest"), new KeyValuePair<string, string>("Content-Type", " application/x-www-form-urlencoded; charset=UTF-8")},true,null,null).ResponseRaw;
            Console.WriteLine(tempRequest);
            tempRequest = myHttp.SendHttpRequest(string.Format(@"http://{0}/xinyunlian-weixin-ecom/wx/login/submit.jhtml", tempHost), string.Format("username=15158155511&enPassword={0}&sn=&agentName=", tempPwd), "POST", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("token", tempToken), new KeyValuePair<string, string>("X-Requested-With", "XMLHttpRequest"), new KeyValuePair<string, string>("Content-Type", " application/x-www-form-urlencoded; charset=UTF-8"), new KeyValuePair<string, string>("Cookie", " test cookies") }, false, null, null).ResponseRaw;
            Console.WriteLine(tempRequest);
        }

        private static string PickStrParameter(string yourTarget, string yourStrEnd, string yourSouce)
        {
            if (yourSouce.Contains(yourTarget))
            {
                string tempPickStr;
                int tempStart = yourSouce.IndexOf(yourTarget) + yourTarget.Length;
                tempPickStr = yourSouce.Remove(0, tempStart);
                if (tempPickStr.Contains(yourStrEnd))
                {
                    int tempEnd = tempPickStr.IndexOf(yourStrEnd);
                    tempPickStr = tempPickStr.Remove(tempEnd);
                    return tempPickStr;
                }
            }
            return null;
        }

        private static void Sync_goods()
        {
            string goodStr = "1008010,1008011,1008012,1008013,1008014,1008015,1008016,1008017,1008018,1008019,1008020,1008021,1008022,1008023,1008024,1008025,1008026,1008027,1008028,1008029,1008030,1008031,1008032,1008033,1008034,1008035,1008036,1008037,1008038,1008039,1008040,1008041,1008042,1008043,1008044,1008045,1008046,1008047,1008075,1008076,1008077,1008078,1008079,1008080,1008081,1008082,1008083,1008084,1008085,1008086,1008087,1008088,1008089,1008090,1008091,1008092,1008093,1008094,1008095,1008096,1008097,1008098,1008099,1008100,1008101,1008102,1008103";
            string[] goodArr = goodStr.Split(',');
            foreach (string goodId in goodArr)
            {
                Console.WriteLine(myHttp.SendData(String.Format("http://wxtest.huala.com/huala/v3/sync-goods?goodsId={0}", goodId)));
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
                string tempRespans = myHttp.SendData(String.Format("http://wxv4.huala.com/huala/weixin/createQrCode?suid={0}", nowSuuid));
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

        private static void AnalysisGbCode()
        {
            StreamReader sr = new StreamReader(@"D:\NG\gb.txt", Encoding.UTF8);
            List<List<string>> loginList = new List<List<string>>();
            string myRequestUri="https://xingzhengquhua.51240.com/{0}__xingzhengquhua/";
            string myFindKey=">{0}</a></li><li><a href=\"/";
            string myFindOne="<ul class=\"list\"><li><a href=\"/";
            string nowAdress = null;
            string nowGbCode = null;
            nowAdress = sr.ReadLine();
            while (nowAdress != null)
            {
                Console.WriteLine(nowAdress);
                List<string> tempGbInfoList = new List<string>();
                tempGbInfoList.Add(nowAdress);
                string nowFindKey = string.Format(myFindKey, nowAdress);
                string tempRespans = myHttp.SendData(string.Format(myRequestUri, nowAdress));
                System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------------------------");
                System.Diagnostics.Debug.WriteLine(tempRespans);
                int startIndex = 0;
                
                startIndex = tempRespans.IndexOf(nowFindKey, startIndex);
                while (startIndex >= 0)
                {
                    nowGbCode = tempRespans.Substring(startIndex + nowFindKey.Length, 12);
                    Console.WriteLine(nowGbCode);
                    tempGbInfoList.Add(nowGbCode);
                    startIndex = tempRespans.IndexOf(nowFindKey, startIndex + nowFindKey.Length);
                }
                if (tempGbInfoList.Count == 1)
                {
                    if (tempRespans.Contains(myFindOne))
                    {
                        startIndex = tempRespans.IndexOf(myFindOne, 0);
                        nowGbCode = tempRespans.Substring(startIndex + myFindOne.Length, 12);
                        Console.WriteLine(nowGbCode);
                        tempGbInfoList.Add(nowGbCode);
                    }
                }
                loginList.Add(tempGbInfoList);
                nowAdress = sr.ReadLine();
                Thread.Sleep(5000);
            }
            CsvFileHelper.SaveCsvFile(@"D:\NG\whf.csv", loginList, false, new System.Text.UTF8Encoding(false));
        }


        private static void AnalysisDTBLog()
        {
            StreamReader sr = new StreamReader(@"D:\NG\dtb.txt", Encoding.UTF8);
            List<List<string>> loginList = new List<List<string>>();
            string temmUa = null;
            temmUa = sr.ReadLine();
            while (temmUa != null)
            {
                if (!temmUa.StartsWith("iPhone"))
                {
                    string phone = "null";
                    string osVersion = "null";
                    int osVersionIndex = temmUa.IndexOf("%2C");
                    int osVersionEndIndex=0;
                    if(osVersionIndex>0)
                    {
                        phone = System.Web.HttpUtility.UrlDecode(temmUa.Substring(0, osVersionIndex));

                        osVersionEndIndex = temmUa.IndexOf('&', osVersionIndex);
                        if(osVersionEndIndex>0)
                        {
                            osVersion = temmUa.Substring(osVersionIndex + 3, osVersionEndIndex - osVersionIndex-3);
                            

                            loginList.Add(new List<string>() { phone, osVersion, osVersion[0].ToString() });
                        }
                        else
                        {
                            Console.WriteLine("error data");
                            Console.WriteLine(temmUa);
                        }
                    }
                    else
                    {
                        Console.WriteLine("error data");
                        Console.WriteLine(temmUa);
                    }

                }
                temmUa = sr.ReadLine();
            }

            CsvFileHelper.SaveCsvFile(@"D:\NG\dtb.csv", loginList, false, new System.Text.UTF8Encoding(false));

        }



        private static void GetCompanyInfo(string companyName)
        {
            List<KeyValuePair<string,string>> myHeads =new List<KeyValuePair<string,string>>();
            myHeads.Add(new KeyValuePair<string,string>("authoration","apicode"));
            myHeads.Add(new KeyValuePair<string,string>("apicode","1f9ad9bd346d4f7caba8c62bf41d8522"));
            string tempRespans = myHttp.SendData(String.Format("https://api.yonyoucloud.com/apis/yonyoucloudresearch/enterpriseSearch/queryFull?fullname={0}", companyName), null, "GET", myHeads);
            Console.WriteLine(tempRespans);
        }

        private class IpInfo
        {
            string ip;
            int count;
            string affiliation;
        }

        private static void AnalysisIpLog()
        {
            StreamReader sr = new StreamReader(@"D:\NG\ip.txt", Encoding.UTF8);
            Dictionary<string,int> ipDc=new Dictionary<string,int>();
            List<List<string>> loginList = new List<List<string>>();
            string temIpTxt = null;
            temIpTxt = sr.ReadLine();
            while (temIpTxt != null)
            {
                temIpTxt = temIpTxt.TrimEnd(' ');
                if (temIpTxt != "" && temIpTxt.Length<17)
                {
                    if(ipDc.Keys.Contains(temIpTxt))
                    {
                        ipDc[temIpTxt]++;
                    }
                    else
                    {
                        ipDc.Add(temIpTxt, 1);
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("error data [{0}]",temIpTxt));
                }
                temIpTxt = sr.ReadLine();
            }

            foreach (var ipKv in ipDc)
            {
                string ipAffiliation = "";
                string ipSource ="";
                try
                {
                    //http://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query={0}&co=&resource_id=6006&t=1534239628331&ie=utf8&oe=utf8&cb=op_aladdin_callback&format=json&tn=baidu&cb=jQuery110207479682729924466_1534239324699&_=1534239324704
                    ipSource = myHttp.SendData(String.Format("https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query={0}&co=&resource_id=6006&t=1539666176249&ie=utf8&oe=utf-8&cb=op_aladdin_callback&format=json&tn=baidu&cb=jQuery1102018209732151379288_1539666075511&_=1539666075515 ", ipKv.Key));
                }
                catch(Exception ex)
                {
                    ipSource = ex.Message;
                }
                System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------------------------");
                System.Diagnostics.Debug.WriteLine(ipSource);
                int ipStart= ipSource.IndexOf("location\":\"");
                if(ipStart>0)
                {
                    ipStart=ipStart+11;
                    int ipEnd = ipSource.IndexOf('"', ipStart);
                    if (ipEnd > ipStart)
                    {
                        ipAffiliation = ipSource.Substring(ipStart, ipEnd - ipStart);
                    }
                    else
                    {
                        ipAffiliation = string.Format("error: [{0}]", ipSource);
                    }
                }
                else
                {
                    ipAffiliation = string.Format("error: [{0}]", ipSource);
                }
                loginList.Add(new List<string>() { ipKv.Key, ipAffiliation, ipKv.Value.ToString() });
            }

            CsvFileHelper.SaveCsvFile(@"D:\NG\ipLog.csv", loginList, false, new System.Text.UTF8Encoding(false));
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("AnalysisIpLog Complete");
        }


        private static void CreatSeckillActivityOrder(string hltoken ,string addressId ,string seckillActivityId , string seckillActivityGoodsId)
        {
            List<KeyValuePair<string,string>> heads=new List<KeyValuePair<string,string>>();
            heads.Add(new KeyValuePair<string,string>("Content-Type","application/json"));
            heads.Add(new KeyValuePair<string,string>("Cookie",string.Format("hltoken={0}",hltoken)));
            string tempResponse=null;
            tempResponse = myHttp.SendData(string.Format("https://wxwyjtest.huala.com/huala/v3/seckillActivity/canBuy?seckillActivityId={0}&seckillActivityGoodsId={1}", seckillActivityId, seckillActivityGoodsId), null, "GET", heads);
            Console.WriteLine();
        }

        private static void MoreTestForAllInOneInterface()
        {
            for(int i =0 ; i <50 ;i++ )
            {
                Thread tempThread = new Thread(new ParameterizedThreadStart(TestForAllInOneInterface));
                tempThread.Priority = ThreadPriority.Normal;
                tempThread.IsBackground = true;
                Console.WriteLine("TestForAllInOneInterface thread start");
                tempThread.Start(string.Format("select store_name , SUID,UUID from store where UUID !=\"\"  limit {0} , 10",500 + i * 10));
            }
        }

        private static void TestForAllInOneInterface()
        {
            TestForAllInOneInterface("select store_name , SUID,UUID from store where UUID !=\"\"  limit 1100 , 100");
        }

        private static void TestForAllInOneInterface(object obj)
        {
            string extSql = (string)obj;
            MySqlDrive mySql = new MySqlDrive("Server=192.168.200.152;UserId=root;Password=xpsh;Database=huala_goods");
            //MySqlDrive mySql = new MySqlDrive("Server=192.168.200.24;UserId=qa;Password=123456;Database=xinyunlian_member");
            mySql.OnDriveStateChange += mySql_OnDriveStateChange;
            mySql.OnGetErrorMessage += mySql_OnGetErrorMessage;
            mySql.OnGetInfoMessage += mySql_OnGetInfoMessage;

            //DataTable myTable = mySql.ExecuteQuery("select store_name , SUID,UUID from store limit 100 , 50");
            DataTable myTable = mySql.ExecuteQuery(extSql);
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
                    int ThreadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine(String.Format("【ThreadId:{1}】{0} :开始同步.......", rows[0].ToString(), ThreadId));
                    //string tempRespans = MyCommonHelper.NetHelper.MyWebTool.MyHttp.SendData(String.Format("https://wxv4.huala.com/huala/seller/login/AllInOneNative?suid={0}&uuid={1}", rows[1].ToString(), rows[2].ToString()));
                    string tempRespans = myHttp.SendData(String.Format("https://wxwyjtest.huala.com/huala/seller/login/AllInOneNative?suid={0}&uuid={1}", rows[1].ToString(), rows[2].ToString()));
                    if(tempRespans.Contains("\"success\":true"))
                    {
                        Console.WriteLine("同步完成");
                    }
                    else
                    {
                        Console.WriteLine("同步错误");
                    }
                    System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------------------------");
                    System.Diagnostics.Debug.WriteLine(String.Format("https://wxwyjtest.huala.com/huala/seller/login/AllInOneNative?suid={0}&uuid={1}", rows[1].ToString(), rows[2].ToString()));
                    System.Diagnostics.Debug.WriteLine(tempRespans);
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
