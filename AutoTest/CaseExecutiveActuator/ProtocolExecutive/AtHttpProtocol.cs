using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Linq;
using MyCommonHelper;
using MyCommonHelper.NetHelper;
using CaseExecutiveActuator;
using MyCommonHelper.FileHelper;


/*******************************************************************************
* Copyright (c) 2015 lijie
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201505016           创建人: 李杰 15158155511
* 描    述: 创建
*******************************************************************************/


namespace CaseExecutiveActuator.ProtocolExecutive
{
    public static class AtHttpProtocol
    {
        public class HttpClient
        {
            public static int httpTimeOut = 100000;                                            //http time out , HttpPostData will not use this value
            public static int httpReadWriteTimeout = 300000;                                   //WebRequest.ReadWriteTimeout 该属性暂时未设置
            public static bool showResponseHeads = false;                                      //是否返回http返回头
            public bool withDefaultCookieContainer = false;                                    //是否默认启用CookieContainer，如果启用则默认会管理所有使用MyHttp的cookie内容
            
            private MyWebTool.MyHttp myHttp;


            public HttpClient()
            {
                myHttp = new MyWebTool.MyHttp();
            }

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <returns>back </returns>
            public void SendData(string url, string data, string method, MyExecutionDeviceResult myEdr)
            {
                SendData(url, data, method, null, myEdr);
            }

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <returns>back </returns>
            public void SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads, MyExecutionDeviceResult myEdr)
            {
                MyWebTool.HttpTimeLine timeline = new MyWebTool.HttpTimeLine();
                myEdr.backContent = myHttp.SendData(url, data, method, heads, myHttp.withDefaultCookieContainer, null, null, timeline);
                myEdr.startTime = timeline.StartTime.ToString("HH:mm:ss");
                myEdr.requestTime = myEdr.spanTime = timeline.ElapsedTime.ToString();
            }

           

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param </param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <param name="saveFileName">the file will save with this name</param>
            /// <returns>back</returns>
            public void SendData(string url, string data, string method, MyExecutionDeviceResult myEdr, string saveFileName)
            {
                SendData(url, data, method, null, myEdr, saveFileName);
            }

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param </param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <param name="saveFileName">the file will save with this name</param>
            /// <returns>back</returns>
            public void SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads, MyExecutionDeviceResult myEdr, string saveFileName)
            {
                MyWebTool.HttpTimeLine timeline = new MyWebTool.HttpTimeLine();
                myEdr.backContent = myHttp.SendData(url, data, method, heads, myHttp.withDefaultCookieContainer, saveFileName, null, timeline);
                myEdr.startTime = timeline.StartTime.ToString("HH:mm:ss");
                myEdr.requestTime = myEdr.spanTime = timeline.ElapsedTime.ToString();
            }


            /// <summary>
            /// i will Send Data with multipart,if you do not want updata any file you can set isFile is false and set filePath is null
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="timeOut">timeOut</param>
            /// <param name="name">Parameter name</param>
            /// <param name="filename">filename</param>
            /// <param name="isFile">is a file</param>
            /// <param name="filePath">file path or cmd</param>
            /// <param name="bodyParameter">the other Parameter in body</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <returns>back</returns>
            public void HttpPostData(string url, int timeOut, string name, string filename, bool isFile, string filePath, string bodyParameter, MyExecutionDeviceResult myEdr)
            {
                MyWebTool.HttpTimeLine timeline = new MyWebTool.HttpTimeLine();
                myEdr.backContent = myHttp.HttpPostData(url, null, myHttp.withDefaultCookieContainer, null, new List<MyWebTool.HttpMultipartDate>() { new MyWebTool.HttpMultipartDate() }, bodyParameter, null, null, timeline);
                myEdr.startTime = timeline.StartTime.ToString("HH:mm:ss");
                myEdr.requestTime = myEdr.spanTime = timeline.ElapsedTime.ToString();
            }

            public void HttpPostData(string url, List<KeyValuePair<string, string>> heads, string bodyData, List<MyWebTool.HttpMultipartDate> multipartDateList, string bodyMultipartParameter, int timeOut, Encoding yourBodyEncoding, MyExecutionDeviceResult myEdr)
            {
                MyWebTool.HttpTimeLine timeline = new MyWebTool.HttpTimeLine();
                myEdr.backContent = myHttp.HttpPostData(url, heads, myHttp.withDefaultCookieContainer, bodyData, multipartDateList, bodyMultipartParameter, yourBodyEncoding,null, timeline);
                myEdr.startTime = timeline.StartTime.ToString("HH:mm:ss");
                myEdr.requestTime = myEdr.spanTime = timeline.ElapsedTime.ToString();
            }


        }
    }
}
