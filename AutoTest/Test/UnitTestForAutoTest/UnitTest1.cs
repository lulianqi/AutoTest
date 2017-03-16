using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyCommonHelper;
using CaseExecutiveActuator;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.Specialized;

namespace UnitTestForAutoTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CaseExecutiveActuator.CaseActionActuator myCAA = new CaseActionActuator();
            Type t = myCAA.GetType();
            foreach (System.Reflection.MemberInfo mi in t.GetMembers(BindingFlags.NonPublic))
            {
                Console.WriteLine("{0}/{1}", mi.MemberType, mi.Name);
            }

            Assert.IsTrue(true);
        }
    }

    [TestClass]
    public class UnitTestForMyCommonTool
    {
         [TestMethod]
        public void TestMethod_GenerateRandomNum()
        {
            string[] tempResult=new string[100];
             for(int i=0;i<100;i++)
             {
                 tempResult[i] = MyCommonTool.GenerateRandomNum(10);
                Console.WriteLine(tempResult[i]);
             }
             for (int i = 0; i < 100; i++)
             {
                 Assert.IsTrue(tempResult[i].Length == 10, "长度错误");
                 for (int m = 99; m >i; m--)
                 {
                     Assert.IsFalse(tempResult[i] == tempResult[m], "出现重复值");
                 }
             }
        }


         [TestMethod]
        public void TestMethod_GenerateRandomStr()
        {
            string[] tempResult=new string[100];
            for (int t = 0; t < 8; t++)
            {
                for (int i = 0; i < 100; i++)
                {
                    tempResult[i] = MyCommonTool.GenerateRandomStr(15, t);
                    Console.WriteLine(tempResult[i]);
                }
                for (int i = 0; i < 100; i++)
                {
                    Assert.IsTrue(tempResult[i].Length == 15, "长度错误");
                    for (int m = 99; m > i; m--)
                    {
                        Assert.IsFalse(tempResult[i] == tempResult[m], "出现重复值");
                    }
                }
            }
        }

    }

    [TestClass]
    public class UnitTestForCaseExecutiveActuator
    {
        [TestMethod]
        public void TestMethod_MyClone1()
        {
            Dictionary<string, ICaseExecutionDevice> dc1 = new Dictionary<string, ICaseExecutionDevice>();
            dc1.Add("Key1", new CaseProtocolExecutionForVanelife_http(new myConnectForVanelife_http()));
            dc1.Add("Key2", new CaseProtocolExecutionForVanelife_http(new myConnectForVanelife_http()));
            dc1.Add("Key3", new CaseProtocolExecutionForVanelife_http(new myConnectForVanelife_http()));
            Dictionary<string, ICaseExecutionDevice> dc2 = dc1.MyClone();
            Assert.AreNotSame(dc1, dc2, "对象引用相同");
            foreach(string tempKey in dc2.Keys)
            {
                Assert.AreNotSame(dc1[tempKey], dc2[tempKey], "Value对象引用相同");
            }
            
        }
       
        [TestMethod]
         public void TestMethod_PickJsonParameter()
         {
            string xx;
            xx = CaseTool.PickJsonParameter("subId", "{\"body\":{\"addTime\":1472526614000,\"addTimeStr\":\"2016-08-30 11:10\",\"address\":\"双城国际4号楼 江汉路1785号\",\"aliasName\":\"\",\"bestTime\":1472547600000,\"bestTimeStr\":\"2016-08-30 17:00\",\"bestTimeString\":\"今天\n17:00\n送达\",\"consignee\":\"双\",\"creditAmount\":0,\"deliverTime\":\"无\",\"discountAmount\":0,\"dueDate\":\"\",\"dueDateNum\":\"\",\"gainAmount\":0,\"goodsAmount\":12100,\"id\":7335,\"isFinish\":false,\"isFirst\":\"0\",\"isPurchase\":0,\"isRemind\":0,\"isReturn\":0,\"marker\":1,\"mobile\":\"15158155511\",\"oneself\":[],\"orderId\":10827571,\"orderSn\":\"KF1472526613161\",\"orderStatus\":\"派送中\",\"orderStatusStr\":\"shipping\",\"orderType\":\"1\",\"payAmount\":12600,\"payNote\":\"结算测试单\",\"payTimeStr\":\"\",\"retAmount\":0,\"sellerAddress\":\"\",\"sellerMobile\":\"\",\"sellerName\":\"\",\"shipAmount\":500,\"shippingAmount\":500,\"shippingTime\":1472526616000,\"shippingTimeStr\":\"2016-08-30 11:10\",\"shippingType\":\"0\",\"supermarket\":[],\"suppliers\":[{\"expressInfo\":{\"expressCode\":\"\",\"expressCompany\":\"\",\"expressSn\":\"\",\"useExpress\":false},\"goods\":[{\"goodsAttr\":\"0:0:件\",\"goodsDes\":\"\",\"goodsName\":\"高达1号机\",\"goodsNumber\":1,\"goodsSn\":\"00001\",\"goodsStatus\":\"已发货\",\"isSell\":0,\"name\":\"ShopForBalance10000\",\"orderStatuss\":\"have_purchase\",\"phone\":\"\",\"picUrl\":\"http://xp.liuxia8.cn/hlman-pic/goods/201601/1451973551583.jpg?s=220x220\",\"recPrice\":10000,\"remark\":\"1\",\"salePrice\":12100,\"sellerSkuId\":19100,\"sellerType\":\"1\",\"subId\":9324,\"supplierId\":10000}],\"info\":{\"aliasName\":\"\",\"avoid\":0,\"distribute\":0,\"isSupplier\":1,\"name\":\"\",\"phone\":\"\",\"sellerType\":1,\"start\":0,\"supplierId\":10000,\"supplierName\":\"ShopForBalance10000\"}}]},\"success\":true}");
            Console.WriteLine(xx);
            Assert.IsFalse(xx != "9324", xx);
            xx = CaseTool.PickJsonParameter("subId", "{\"body\":{\"addTime\":1472526614000,\"addTimeStr\":\"2016-08-30 11:10\",\"address\":\"双城国际4号楼 江汉路1785号\",\"aliasName\":\"\",\"bestTime\":1472547600000,\"bestTimeStr\":\"2016-08-30 17:00\",\"bestTimeString\":\"今天\n17:00\n送达\",\"consignee\":\"双\",\"creditAmount\":0,\"deliverTime\":\"无\",\"discountAmount\":0,\"dueDate\":\"\",\"dueDateNum\":\"\",\"gainAmount\":0,\"goodsAmount\":12100,\"id\":7335,\"isFinish\":false,\"isFirst\":\"0\",\"isPurchase\":0,\"isRemind\":0,\"isReturn\":0,\"marker\":1,\"mobile\":\"15158155511\",\"oneself\":[],\"orderId\":10827571,\"orderSn\":\"KF1472526613161\",\"orderStatus\":\"派送中\",\"orderStatusStr\":\"shipping\",\"orderType\":\"1\",\"payAmount\":12600,\"payNote\":\"结算测试单\",\"payTimeStr\":\"\",\"retAmount\":0,\"sellerAddress\":\"\",\"sellerMobile\":\"\",\"sellerName\":\"\",\"shipAmount\":500,\"shippingAmount\":500,\"shippingTime\":1472526616000,\"shippingTimeStr\":\"2016-08-30 11:10\",\"shippingType\":\"0\",\"supermarket\":[],\"suppliers\":[{\"expressInfo\":{\"expressCode\":\"\",\"expressCompany\":\"\",\"expressSn\":\"\",\"useExpress\":false},\"goods\":[{\"goodsAttr\":\"0:0:件\",\"goodsDes\":\"\",\"goodsName\":\"高达1号机\",\"goodsNumber\":1,\"goodsSn\":\"00001\",\"goodsStatus\":\"已发货\",\"isSell\":0,\"name\":\"ShopForBalance10000\",\"orderStatuss\":\"have_purchase\",\"phone\":\"\",\"picUrl\":\"http://xp.liuxia8.cn/hlman-pic/goods/201601/1451973551583.jpg?s=220x220\",\"recPrice\":10000,\"remark\":\"1\",\"salePrice\":12100,\"sellerSkuId\":19100,\"sellerType\":\"1\",\"subId\":9324,\"supplierId\":10000}],\"info\":{\"aliasName\":\"\",\"avoid\":0,\"distribute\":0,\"isSupplier\":1,\"name\":\"\",\"phone\":\"\",\"sellerType\":1,\"start\":0,\"supplierId\":10000,\"supplierName\":\"ShopForBalance10000\",\"subId\": 123}}]},\"success\":true}");
            Console.WriteLine(xx);
            Assert.IsFalse(xx != "9324,123", xx);
            xx = CaseTool.PickJsonParameter("subId", "[{\"goodsAttr\":\"0:0:件\",\"goodsDes\":\"\",\"goodsName\":\"高达1号机\",\"goodsNumber\":1,\"goodsSn\":\"00001\",\"goodsStatus\":\"已发货\",\"isSell\":0,\"name\":\"ShopForBalance10000\",\"orderStatuss\":\"have_purchase\",\"phone\":\"\",\"picUrl\":\"http://xp.liuxia8.cn/hlman-pic/goods/201601/1451973551583.jpg?s=220x220\",\"recPrice\":10000,\"remark\":\"1\",\"salePrice\":12100,\"sellerSkuId\":19100,\"sellerType\":\"1\",\"subId\":9324,\"supplierId\":10000},{\"goodsAttr\":\"0:0:件\",\"goodsDes\":\"\",\"goodsName\":\"高达1号机\",\"goodsNumber\":1,\"goodsSn\":\"00001\",\"goodsStatus\":\"已发货\",\"isSell\":0,\"name\":\"ShopForBalance10000\",\"orderStatuss\":\"have_purchase\",\"phone\":\"\",\"picUrl\":\"http://xp.liuxia8.cn/hlman-pic/goods/201601/1451973551583.jpg?s=220x220\",\"recPrice\":10000,\"remark\":\"1\",\"salePrice\":12100,\"sellerSkuId\":19100,\"sellerType\":\"1\",\"subId\":9324,\"supplierId\":10000,\"lijie\":[{\"goodsAttr\":\"0:0:件\",\"goodsDes\":\"\",\"goodsName\":\"高达1号机\",\"goodsNumber\":1,\"goodsSn\":\"00001\",\"goodsStatus\":\"已发货\",\"isSell\":0,\"name\":\"ShopForBalance10000\",\"orderStatuss\":\"have_purchase\",\"phone\":\"\",\"picUrl\":\"http://xp.liuxia8.cn/hlman-pic/goods/201601/1451973551583.jpg?s=220x220\",\"recPrice\":10000,\"remark\":\"1\",\"salePrice\":12100,\"sellerSkuId\":19100,\"sellerType\":\"1\",\"subId\":9324,\"supplierId\":10000},{\"goodsAttr\":\"0:0:件\",\"goodsDes\":\"\",\"goodsName\":\"高达1号机\",\"goodsNumber\":1,\"goodsSn\":\"00001\",\"goodsStatus\":\"已发货\",\"isSell\":0,\"name\":\"ShopForBalance10000\",\"orderStatuss\":\"have_purchase\",\"phone\":\"\",\"picUrl\":\"http://xp.liuxia8.cn/hlman-pic/goods/201601/1451973551583.jpg?s=220x220\",\"recPrice\":10000,\"remark\":\"1\",\"salePrice\":12100,\"sellerSkuId\":19100,\"sellerType\":\"1\",\"subId\":9324,\"supplierId\":10000}]}]");
            Console.WriteLine(xx);
            Assert.IsFalse(xx != "9324,9324,9324,9324", xx);
            xx = CaseTool.PickJsonParameter("token", " {\"body\":{\"account\":\"13000000001\",\"id\":10000,\"mobile\":\"13000000001\",\"name\":\"ShopForBalance\",\"roleList\":[\"seller\",\"seller\"],\"sellerList\":[{\"aliasName\":\"\",\"avoid\":0,\"distribute\":0,\"isDelete\":\"0\",\"isSupplier\":1,\"name\":\"ShopForBalance10000\",\"phone\":\"\",\"sellerId\":10000,\"start\":0,\"supplierId\":0,\"supplierName\":\"\"}],\"sellerQrcode\":\"http://xp.liuxia8.cn/huala/v3/qrcod-seller/\",\"sellerUrl\":\"http://xp.liuxia8.cn/huala/goshop/\",\"sex\":\"男\",\"state\":\"3\"},\"success\":true,\"token\":\"176c0e32-6072-40e2-89ac-df173cbbc2c6\"}");
            Console.WriteLine(xx);
            Assert.IsFalse(xx != "176c0e32-6072-40e2-89ac-df173cbbc2c6", xx); 
        }

        [TestMethod]
        public void TestMethod_MyClone2()
        {
            Dictionary<string, string> dc1 = new Dictionary<string, string>();
            dc1.Add("Key1", "V1");
            dc1.Add("Key2", "V1");
            dc1.Add("Key3", "V1");
            Dictionary<string, string> dc2 = dc1.MyClone<string, string>();
            Assert.AreNotSame(dc1, dc2, "对象引用相同");
            dc1["Key1"] = "CH";
            Console.WriteLine(dc1["Key1"]);
            Console.WriteLine(dc2["Key1"]);
            foreach (string tempKey in dc2.Keys)
            {
                Assert.AreNotSame((object)dc1[tempKey], (object)dc2[tempKey], "Value对象引用相同");
            }

        }

        [TestMethod]
        public void TestMethod_TryGetParametersAdditionData()
        {
            NameValueCollection testDataList = new NameValueCollection() { { "Parameter(+)", "+" }, { "Parameter(-)", "-" }, { "Parameter(@#$%^)", "@#$%^" }, { "Parameter()", "" } 
            , { "Parameter", "NULL" }, { "Parameter(1-2)", "1-2" }, { "Parameter(-", "NULL" }, { "Parameter-)", "NULL" }};
            string AdditionData = null;
            string ParametersData = null;
            foreach(string testData in testDataList.AllKeys)
            {
                ParametersData = CaseTool.TryGetParametersAdditionData(testData, out AdditionData);
                Console.WriteLine("--------------------------------");
                Console.WriteLine(testData);
                Console.WriteLine(ParametersData);
                Console.WriteLine(AdditionData.MyValue());
                Assert.IsTrue(AdditionData.MyValue() == testDataList[testData], "解析失败");
            }
        }

       
        //struct TestData { string _str1; string _str2; int _num; public TestData(string str1, string str2, int num) { _str1 = str1; _str2 = str2; _num = num; } };
        [TestMethod]
        public void TestMethod_MySplitIntEnd()
        {
            string str;
            int num;
            Console.WriteLine("--------------------------------");
            Assert.IsTrue("1123456-123".MySplitIntEnd('-', out str, out num));
            Assert.IsTrue(str == "1123456", "str error");
            Assert.IsTrue(num == 123, "num error");

            Console.WriteLine("--------------------------------");
            Assert.IsTrue("1123@@$@$456-13323".MySplitIntEnd('-', out str, out num));
            Assert.IsTrue(str == "1123@@$@$456", "str error");
            Assert.IsTrue(num == 13323, "num error");

            Console.WriteLine("--------------------------------");
            Assert.IsTrue("1-123-456--123".MySplitIntEnd('-', out str, out num));
            Assert.IsTrue(str == "1-123-456-", "str error");
            Assert.IsTrue(num == 123, "num error");

            Console.WriteLine("--------------------------------");
            Assert.IsTrue("--123".MySplitIntEnd('-', out str, out num));
            Assert.IsTrue(str == "-", "str error");
            Assert.IsTrue(num == 123, "num error");

            Console.WriteLine("--------------------------------");
            Assert.IsTrue("-123".MySplitIntEnd('-', out str, out num));
            Assert.IsTrue(str == "", "str error");
            Assert.IsTrue(num == 123, "num error");

            Console.WriteLine("--------------------------------");
            Assert.IsFalse("123333-12d3".MySplitIntEnd('-', out str, out num));
            Console.WriteLine("--------------------------------");
            Assert.IsFalse("123333-".MySplitIntEnd('-', out str, out num));
            Console.WriteLine("--------------------------------");
            Assert.IsFalse("-".MySplitIntEnd('-', out str, out num));
            Console.WriteLine("--------------------------------");
            Assert.IsFalse("".MySplitIntEnd('-', out str, out num));
            
        }

    }
}
