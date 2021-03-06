﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCommonHelper.EncryptionHelper;
using MyCommonHelper;
using MyCommonHelper.NetHelper;
using MyCommonHelper.FileHelper;

namespace DemoForMyHelper
{
    class DemoForMyCommonHelper
    {
        public static void DotestForRC4()
        {
            Console.WriteLine(MyBytes.StringToHexString("test data for test !@#$%^&*()ZXCVBNM<QWERTYUIOASDFGHJK",Encoding.UTF8 ,HexaDecimal.hex16, ShowHexMode.space));
            Console.WriteLine("RC4 Encrypt");
            byte[] data1 = MyRC4.Encrypt("test data for test !@#$%^&*()ZXCVBNM<QWERTYUIOASDFGHJK", "123", Encoding.UTF8);
            Console.WriteLine(MyBytes.ByteToHexString(data1, HexaDecimal.hex16, ShowHexMode.space));
            data1 = MyRC4.Encrypt("test data for test !@#$%^&*()ZXCVBNM<QWERTYUIOASDFGHJK", "123", Encoding.UTF8);
            Console.WriteLine(MyBytes.ByteToHexString(data1, HexaDecimal.hex16, ShowHexMode.space));
            Console.WriteLine(Convert.ToBase64String(data1));
            Console.WriteLine("RC4 Decrypt");
            byte[] data2 = MyRC4.Decrypt(data1, "123", Encoding.UTF8);
            Console.WriteLine(MyBytes.ByteToHexString(data2, HexaDecimal.hex16, ShowHexMode.space));
            Console.WriteLine(Encoding.UTF8.GetString(data2));
        }

        public static void DotestForMyhttp()
        {
            MyWebTool.MyHttp myHttp = new MyWebTool.MyHttp(false, true);
            myHttp.SendData("http://pv.sohu.com/cityjson?ie=utf-8", null, "POST", null, @"D:\shou1.txt");
            myHttp.SendData("http://pv.sohu.com/cityjson", "ie=utf-8", "GET");
            myHttp.SendData("http://pv.sohu.com/cityjson", "ie=utf-8", "POST");
            myHttp.SendData("http://pv.sohu.com/cityjson", "ie=utf-8", "POST", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("name", "DotestForMyhttp") }, null, null);

            List<MyWebTool.HttpMultipartDate> hml = new List<MyWebTool.HttpMultipartDate>();
            hml.Add(new MyWebTool.HttpMultipartDate("multipart name", "file name", null, false, "test data"));
            hml.Add(new MyWebTool.HttpMultipartDate("multipart name", null ,"comtenttype", false, "test data"));

            myHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("name", "DotestForMyhttp") }, "body data", hml, null, null);
            myHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("name", "DotestForMyhttp") }, "body data", hml, "a=1&b=2&c=4", null);
            hml.Add(new MyWebTool.HttpMultipartDate("multipart name", "file name", "comtenttype", true, @"D:\shou1.txt"));

            myHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("name", "DotestForMyhttp") }, "body data", hml, "a=1&b=2&c=4", null);
        }

        public static void DotestForMyhttp2()
        {
            MyWebTool.MyHttp myHttp = new MyWebTool.MyHttp(false, true);

            List<MyWebTool.HttpMultipartDate> hml = new List<MyWebTool.HttpMultipartDate>();
            hml.Add(new MyWebTool.HttpMultipartDate("multipart name", "file name", null, false, "test data"));
            hml.Add(new MyWebTool.HttpMultipartDate("multipart name", null, "comtenttype", false, "test data"));

            Console.WriteLine(myHttp.SendMultipartRequest("http://pv.sohu.com/cityjson?ie=utf-8", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("name", "DotestForMyhttp") }, true, "body data", hml, null, null,null, null).ResponseBody);
            Console.WriteLine(myHttp.SendMultipartRequest("http://pv.sohu.com/cityjson?ie=utf-8", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("name", "DotestForMyhttp") }, true, "body data", hml, "a=1&b=2&c=4", null, null, null).ResponseRaw);
            hml.Add(new MyWebTool.HttpMultipartDate("multipart name", "file name", "comtenttype", true, @"D:\shou1.txt"));
            Console.WriteLine(myHttp.SendMultipartRequest("http://pv.sohu.com/cityjson?ie=utf-8", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("name", "DotestForMyhttp") }, true, "body data", hml, "a=1&b=2&c=4", null, null, null).ResponseBody);
        }

        public static void DotestForMyhttps()
        {
            MyWebTool.MyHttp.EnableServerCertificateValidation = true;
            MyWebTool.MyHttp myHttp = new MyWebTool.MyHttp(false, true);
            //https://iuc.oppomobile.com/account/user-info
            MyCommonHelper.NetHelper.MyWebTool.MyHttpResponse myHttpResponse = myHttp.SendHttpRequest(@"https://lijie.com/hello", null, "GET", null, false, null, null);
            Console.WriteLine(myHttpResponse.ResponseRaw ?? myHttpResponse.ErrorMes);
            myHttpResponse = myHttp.SendHttpRequest(@"https://iuc.oppomobile.com/account/user-info", null, "GET", null, false, null, null);
            Console.WriteLine(myHttpResponse.ResponseRaw ?? myHttpResponse.ErrorMes);
            //Console.WriteLine(myHttp.SendData(@"https://api.weixin.qq.com/sns/oauth2/access_token?code=061v6AGK1pOTj40nF0EK1LNwGK1v6AGV&grant_type=authorization_code&appid=wx01f2ab6d9e41169a&secret=1d2f3f1bdd6b2790b48ae64c8bc9bfdb"));
        }

        public static void TestForCsv()
        {
            CsvFileHelper csv = new CsvFileHelper(null,"sd,dd,f,,\r\nsd,dd\r\n");
            CsvFileHelper csv2 = new CsvFileHelper(null,"sd,dd,f,,\"sd\r\n\"\"\r\ndd,ee\",ss\r\ndddd,dddd\r\n");
            var v1 = csv.GetListCsvData();
            var v2 = csv2.GetListCsvData();
        }
    }
}
