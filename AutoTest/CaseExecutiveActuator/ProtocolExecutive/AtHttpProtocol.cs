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

            static HttpClient()
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(
                    (sender, certificate, chain, sslPolicyErrors) => { return true; });                
            }

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <returns>back </returns>
            public static string SendData(string url, string data, string method, MyExecutionDeviceResult myEdr)
            {
                return SendData(url, data, method, null, myEdr);
            }

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <returns>back </returns>
            public static string SendData(string url, string data, string method, List<KeyValuePair<string,string>> heads, MyExecutionDeviceResult myEdr)
            {
                string re = "";
                bool hasBody = !string.IsNullOrEmpty(data);
                bool needBody = method.ToUpper() == "POST" || method.ToUpper() == "PUT";

                Stopwatch myWatch = new Stopwatch();
                try
                {
                    //except POST other data will add the url,if you want adjust the ruleschange here
                    if (!needBody && hasBody)
                    {
                        url += "?" + data;
                    }
                    WebRequest wr = WebRequest.Create(url);
                    wr.Timeout = httpTimeOut;
                    //HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);
                    wr.Method = method;
                    if (heads == null)
                    {
                        wr.ContentType = "application/x-www-form-urlencoded";
                    }

                    MyWebTool.HttpHelper.AddHttpHeads((HttpWebRequest)wr, heads);
                    
                    //((HttpWebRequest)wr).KeepAlive = true;
                    //wr.Headers.Remove(HttpRequestHeader.Connection);
                    //wr.Headers.Add(HttpRequestHeader.Connection, "close");
                    //wr.Headers.Add(HttpRequestHeader.KeepAlive, "false");
                    char[] reserved = { '?', '=', '&' };
                    StringBuilder UrlEncoded = new StringBuilder();
                    byte[] SomeBytes = null;
                    myEdr.startTime = DateTime.Now.ToString("HH:mm:ss");
                    myWatch.Start();
                    if (needBody)
                    {
                        if (hasBody)
                        {
                            SomeBytes = Encoding.UTF8.GetBytes(data);
                            wr.ContentLength = SomeBytes.Length;
                            myWatch.Reset();
                            myWatch.Start();
                            Stream newStream = wr.GetRequestStream();
                            newStream.Write(SomeBytes, 0, SomeBytes.Length);
                            myEdr.requestTime = myWatch.ElapsedMilliseconds.ToString();
                            //myWatch.Stop();
                            newStream.Close();
                        }
                        else
                        {
                            wr.ContentLength = 0;
                        }
                    }
                   
                    WebResponse result = wr.GetResponse();

                    myWatch.Stop();

                    Stream receiveStream = result.GetResponseStream();

                    using (var httpStreamReader = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        re += httpStreamReader.ReadToEnd();
                    }

                }
                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            re = "StatusCode:  " + Convert.ToInt32(((HttpWebResponse)wex.Response).StatusCode) + "\r\n";
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                re += reader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        re = wex.Message;
                    }
                }
                catch (Exception ex)
                {
                    re = "Unknow Error: " + ex.Message;
                    ErrorLog.PutInLog("ID:0957  " + ex.Message);
                }

                if (myWatch.IsRunning)
                {
                    myWatch.Stop();
                }
                myEdr.spanTime = myWatch.ElapsedMilliseconds.ToString();

                myEdr.backContent = re;
                return re;
            }

            /// <summary>
            /// i will Send Data （逐字节保存）
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param </param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <param name="saveFileName">the file will save with this name</param>
            /// <returns>back</returns>
            public static string SendDataSaveEx(string url, string data, string method, MyExecutionDeviceResult myEdr, string saveFileName)
            {
                return SendDataSaveEx(url, data, method, null, myEdr, saveFileName);
            }

            /// <summary>
            /// i will Send Data （逐字节保存）
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param </param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <param name="saveFileName">the file will save with this name</param>
            /// <returns>back</returns>
            public static string SendDataSaveEx(string url, string data, string method, List<KeyValuePair<string, string>> heads, MyExecutionDeviceResult myEdr, string saveFileName)
            {
                string re = "";
                bool hasBody = !string.IsNullOrEmpty(data);
                bool needBody = method.ToUpper() == "POST" || method.ToUpper() == "PUT";

                Stopwatch myWatch = new Stopwatch();
                try
                {
                    //except POST other data will add the url,if you want adjust the ruleschange here
                    if (!needBody && hasBody)
                    {
                        url += "?" + data;
                    }
                    WebRequest wr = WebRequest.Create(url);
                    wr.Timeout = httpTimeOut;
                    //HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);
                    wr.Method = method;
                    if (heads==null)
                    {
                        wr.ContentType = "application/x-www-form-urlencoded";
                    }
                    MyWebTool.HttpHelper.AddHttpHeads((HttpWebRequest)wr, heads);
                    
                    //((HttpWebRequest)wr).KeepAlive = true;
                    //wr.Headers.Remove(HttpRequestHeader.Connection);
                    //wr.Headers.Add(HttpRequestHeader.Connection, "close");
                    //wr.Headers.Add(HttpRequestHeader.KeepAlive, "false");
                    char[] reserved = { '?', '=', '&' };
                    StringBuilder UrlEncoded = new StringBuilder();
                    byte[] SomeBytes = null;
                    myEdr.startTime = DateTime.Now.ToString("HH:mm:ss");
                    myWatch.Start();
                    if (needBody)
                    {
                        if (hasBody)
                        {
                            SomeBytes = Encoding.UTF8.GetBytes(data);
                            wr.ContentLength = SomeBytes.Length;
                            myWatch.Reset();
                            myWatch.Start();
                            Stream newStream = wr.GetRequestStream();
                            newStream.Write(SomeBytes, 0, SomeBytes.Length);
                            myEdr.requestTime = myWatch.ElapsedMilliseconds.ToString();
                            //myWatch.Stop();
                            newStream.Close();
                        }
                        else
                        {
                            wr.ContentLength = 0;
                        }
                    }

                   
                    WebResponse result = wr.GetResponse();

                    myWatch.Stop();

                    Stream ReceiveStream = result.GetResponseStream();

                    /*
                    Byte[] read = new Byte[512];
                    int bytes = ReceiveStream.Read(read, 0, 512);

                    re = "";
                    while (bytes > 0)
                    {
                        Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                        re += encode.GetString(read, 0, bytes);
                        bytes = ReceiveStream.Read(read, 0, 512);
                    }
                    */
                    byte[] infbytes = new byte[10240];

                    int tempLen = 512;
                    int offset = 0;

                    //数据最多20k可以不需要分段读取
                    while (tempLen - 512 >= 0)
                    {
                        tempLen = ReceiveStream.Read(infbytes, offset, 512);
                        offset += tempLen;
                    }
                    byte[] bytesToSave = new byte[offset];
                    for (int i = 0; i < offset; i++)
                    {
                        bytesToSave[i] = infbytes[i];
                    }
                    File.WriteAllBytes(saveFileName, bytesToSave);

                    //直接一次读取
                    //tempLen = ReceiveStream.Read(infbytes, 0, 20480);
                    //byte[] bytesToSave = new byte[tempLen];
                    //for (int i = 0; i < tempLen; i++)
                    //{
                    //    bytesToSave[i] = infbytes[i];
                    //}
                    //File.WriteAllBytes(System.Windows.Forms.Application.StartupPath + "\\dataToDown\\" + "mydata", bytesToSave);

                    re = "保存至文件" + saveFileName;

                }
                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            re = "StatusCode:  " + Convert.ToInt32(((HttpWebResponse)wex.Response).StatusCode) + "\r\n";
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                re += reader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        re = wex.Message;
                    }
                }
                catch (Exception ex)
                {
                    re = "Unknow Error";
                    ErrorLog.PutInLog("ID:1096  " + ex.Message);
                }

                if (myWatch.IsRunning)
                {
                    myWatch.Stop();
                }
                myEdr.spanTime = myWatch.ElapsedMilliseconds.ToString();

                myEdr.backContent = re;
                return re;
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
            public static string SendData(string url, string data, string method, MyExecutionDeviceResult myEdr, string saveFileName)
            {
                return SendData(url, data, method, null, myEdr, saveFileName);
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
            public static string SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads, MyExecutionDeviceResult myEdr, string saveFileName)
            {
                string re = "";
                bool hasBody = !string.IsNullOrEmpty(data);
                bool needBody = method.ToUpper() == "POST" || method.ToUpper() == "PUT";

                Stopwatch myWatch = new Stopwatch();
                try
                {
                    //except POST other data will add the url,if you want adjust the ruleschange here
                    if (method.ToUpper() != "POST" && data != null)
                    {
                        url += "?" + data;
                        data = null;
                    }

                    string tempUrl = url;
                    //由于下载限制，实际对于需要下载大文件的请求，也使用的是GET
                    if (!needBody && hasBody)
                    {
                         tempUrl = tempUrl + "?" + data;
                    }

                    myEdr.startTime = DateTime.Now.ToString("HH:mm:ss");
                    using (WebClient client = new WebClient())
                    {
                        MyWebTool.HttpHelper.AddHttpHeads(client.Headers, heads);
                        myWatch.Start();
                        client.DownloadFile(tempUrl, saveFileName);
                        myWatch.Stop();
                    }


                    re = "保存至文件" + saveFileName;

                }
                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            re = "StatusCode:  " + Convert.ToInt32(((HttpWebResponse)wex.Response).StatusCode) + "\r\n";
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                re += reader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        re = wex.Message;
                    }
                }
                catch (Exception ex)
                {
                    re = "Unknow Error";
                    ErrorLog.PutInLog("ID:1174  " + ex.Message);
                }

                if (myWatch.IsRunning)
                {
                    myWatch.Stop();
                }
                myEdr.spanTime = myWatch.ElapsedMilliseconds.ToString();
                myEdr.backContent = re;
                return re;
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
            public static string HttpPostData(string url, int timeOut, string name, string filename, bool isFile, string filePath, string bodyParameter, MyExecutionDeviceResult myEdr)
            {
                string responseContent =null;
                NameValueCollection stringDict = new NameValueCollection();

                if (bodyParameter != null)
                {
                    string[] sArray = bodyParameter.Split('&');
                    foreach (string tempStr in sArray)
                    {
                        int myBreak = tempStr.IndexOf('=');
                        if (myBreak == -1)
                        {
                            myEdr.backContent = "can't find =";
                            return "can't find =";
                        }
                        stringDict.Add(tempStr.Substring(0, myBreak), tempStr.Substring(myBreak + 1));
                    }
                }

                var memStream = new MemoryStream();
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                // 边界符
                var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                // 边界符
                var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                // 最后的结束符
                var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

                // 设置属性
                webRequest.Method = "POST";
                webRequest.Timeout = timeOut;

                //是否带文件提交
                if (filePath != null)
                {
                    webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                    // 写入文件
                    const string filePartHeader = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" + "Content-Type: application/octet-stream\r\n\r\n";
                    var header = string.Format(filePartHeader, name, filename);
                    var headerbytes = Encoding.UTF8.GetBytes(header);

                    memStream.Write(beginBoundary, 0, beginBoundary.Length);
                    memStream.Write(headerbytes, 0, headerbytes.Length);

                    if (isFile)
                    {
                        try
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            {
                                var buffer = new byte[1024];
                                int bytesRead; // =0
                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    memStream.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            responseContent = "Error:  " + ex.Message + "\r\n";
                            ErrorLog.PutInLog("ID:0544 " + ex.InnerException);
                            myEdr.backContent = responseContent;
                            return responseContent;
                        }
                    }
                    else
                    {
                        byte[] myCmd = Encoding.UTF8.GetBytes(filePath);
                        memStream.Write(myCmd, 0, myCmd.Length);
                    }
                }

                //写入POST非文件参数
                if (bodyParameter != null)
                {
                    //写入字符串的Key
                    var stringKeyHeader = "\r\n--" + boundary +
                                           "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                           "\r\n\r\n{1}";


                    for (int i = 0; i < stringDict.Count; i++)
                    {
                        try
                        {
                            byte[] formitembytes = Encoding.UTF8.GetBytes(string.Format(stringKeyHeader, stringDict.GetKey(i), stringDict.Get(i)));
                            memStream.Write(formitembytes, 0, formitembytes.Length);
                        }
                        catch (Exception ex)
                        {
                            return "can not send :" + ex.Message;
                        }
                    }
                    memStream.Write(Encoding.ASCII.GetBytes("\r\n"), 0, Encoding.ASCII.GetBytes("\r\n").Length);

                }
                else
                {
                    memStream.Write(Encoding.ASCII.GetBytes("\r\n"), 0, Encoding.ASCII.GetBytes("\r\n").Length);
                }

                //写入最后的结束边界符
                //memStream.Write(Encoding.ASCII.GetBytes("\r\n"), 0, Encoding.ASCII.GetBytes("\r\n").Length);
                memStream.Write(endBoundary, 0, endBoundary.Length);

                webRequest.ContentLength = memStream.Length;

                Stopwatch myWatch = new Stopwatch();
                myEdr.startTime = DateTime.Now.ToString("HH:mm:ss");
                myWatch.Start();

                //开始请求
                try
                {
                    var requestStream = webRequest.GetRequestStream();

                    memStream.Position = 0;
                    var tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();

                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    requestStream.Close();

                    var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

                    using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                    {
                        if (showResponseHeads)
                        {
                            responseContent = httpWebResponse.Headers.ToString();
                        }
                        responseContent = httpStreamReader.ReadToEnd();
                    }
                    myWatch.Stop();
                    httpWebResponse.Close();
                    webRequest.Abort();
                }
                catch (WebException wex)
                {
                    responseContent = "";
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            responseContent = "StatusCode:  " + Convert.ToInt32(((HttpWebResponse)wex.Response).StatusCode) + "\r\n";
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                responseContent += reader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        responseContent = wex.Message;
                    }

                }

                catch (Exception ex)
                {
                    responseContent = "Error:  " + ex.Message + "\r\n";
                    ErrorLog.PutInLog("ID:0090 " + ex.InnerException);
                }

                if (myWatch.IsRunning)
                {
                    myWatch.Stop();
                }
                myEdr.spanTime = myWatch.ElapsedMilliseconds.ToString();

                myEdr.backContent = responseContent;
                return responseContent;
            }

            public static string HttpPostData(string url, List<KeyValuePair<string, string>> heads, string bodyData, List<MyWebTool.HttpMultipartDate> multipartDateList, string bodyMultipartParameter, int timeOut, Encoding yourBodyEncoding, MyExecutionDeviceResult myEdr)
            {
                string responseContent = null;
                Encoding httpBodyEncoding = Encoding.UTF8;
                string defaultMultipartContentType = "application/octet-stream";
                NameValueCollection stringDict = new NameValueCollection();
                if (yourBodyEncoding != null)
                {
                    httpBodyEncoding = yourBodyEncoding;
                }

                //解析快捷Multipart表单形式post参数
                if (bodyMultipartParameter != null)
                {
                    string[] sArray = bodyMultipartParameter.Split('&');
                    foreach (string tempStr in sArray)
                    {
                        int myBreak = tempStr.IndexOf('=');
                        if (myBreak == -1)
                        {
                            myEdr.backContent = "can't find '=' in [bodyMultipartParameter]";
                            return "can't find =";
                        }
                        stringDict.Add(tempStr.Substring(0, myBreak), tempStr.Substring(myBreak + 1));
                    }
                }

                var memStream = new MemoryStream();
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                //写入http头
                MyWebTool.HttpHelper.AddHttpHeads(webRequest, heads);

                // 边界符
                var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                // 边界符
                var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                // 最后的结束符
                var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

                // 设置属性
                webRequest.Method = "POST";
                webRequest.Timeout = timeOut;
                webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

                //写如常规body
                if (bodyData != null)
                {
                    var bodybytes = httpBodyEncoding.GetBytes(bodyData);
                    memStream.Write(bodybytes, 0, bodybytes.Length);
                }

                if (multipartDateList != null)
                {
                    foreach (MyWebTool.HttpMultipartDate nowMultipart in multipartDateList)
                    {
                        //Console.WriteLine(System.DateTime.Now.Ticks);
                        //const string filePartHeader = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" + "Content-Type: {2}\r\n\r\n";
                        string nowPartHeader = "Content-Disposition: form-data";
                        if (nowMultipart.Name != null)
                        {
                            nowPartHeader += string.Format("; name=\"{0}\"", nowMultipart.Name);
                        }
                        if (nowMultipart.FileName != null)
                        {
                            nowPartHeader += string.Format("; filename=\"{0}\"", nowMultipart.FileName);
                        }
                        nowPartHeader += "\r\n";
                        nowPartHeader += string.Format("Content-Type: {0}", nowMultipart.ContentType == null ? defaultMultipartContentType : nowMultipart.ContentType);
                        nowPartHeader += "\r\n\r\n";
                        //Console.WriteLine(System.DateTime.Now.Ticks);
                        byte[] nowHeaderbytes = httpBodyEncoding.GetBytes(nowPartHeader);
                        memStream.Write(Encoding.ASCII.GetBytes("\r\n"), 0, Encoding.ASCII.GetBytes("\r\n").Length);
                        memStream.Write(beginBoundary, 0, beginBoundary.Length);
                        memStream.Write(nowHeaderbytes, 0, nowHeaderbytes.Length);
                        //MultipartDate
                        if (nowMultipart.IsFile)
                        {
                            try
                            {
                                using (var fileStream = new FileStream(nowMultipart.FileData, FileMode.Open, FileAccess.Read))
                                {
                                    byte[] buffer = new byte[1024];
                                    int bytesRead; // =0
                                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                    {
                                        memStream.Write(buffer, 0, bytesRead);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                responseContent = "Error:  " + ex.Message + "\r\n";
                                ErrorLog.PutInLog("ID:0544 " + ex.InnerException);
                                myEdr.backContent = responseContent;
                                return responseContent;
                            }
                        }
                        else
                        {
                            byte[] myCmd = httpBodyEncoding.GetBytes(nowMultipart.FileData == null ? "" : nowMultipart.FileData);
                            memStream.Write(myCmd, 0, myCmd.Length);
                        }
                    }
                }

                //快捷写入写入POST非文件参数
                if (bodyMultipartParameter != null)
                {
                    //写入字符串的Key
                    string bodyParameterFormat = "\r\n--" + boundary +
                                           "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                           "\r\n\r\n{1}";
                    for (int i = 0; i < stringDict.Count; i++)
                    {
                        try
                        {
                            byte[] formitembytes = httpBodyEncoding.GetBytes(string.Format(bodyParameterFormat, stringDict.GetKey(i), stringDict.Get(i)));
                            memStream.Write(formitembytes, 0, formitembytes.Length);
                        }
                        catch (Exception ex)
                        {
                            responseContent = "can not send :" + ex.Message;
                            myEdr.backContent = responseContent;
                            return responseContent;
                        }
                    }
                }

                //写入最后的结束边界符
                if (!(bodyMultipartParameter == null && multipartDateList == null))
                {
                    memStream.Write(Encoding.ASCII.GetBytes("\r\n"), 0, Encoding.ASCII.GetBytes("\r\n").Length);
                    memStream.Write(endBoundary, 0, endBoundary.Length);
                }

                webRequest.ContentLength = memStream.Length;

                Stopwatch myWatch = new Stopwatch();
                myEdr.startTime = DateTime.Now.ToString("HH:mm:ss");
                myWatch.Start();

                //开始请求
                try
                {
                    var requestStream = webRequest.GetRequestStream();

                    memStream.Position = 0;
                    var tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();

                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    requestStream.Close();

                    HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

                    using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                    {
                        if (showResponseHeads)
                        {
                            responseContent = httpWebResponse.Headers.ToString();
                        }
                        responseContent += httpStreamReader.ReadToEnd();
                    }
                    myWatch.Stop();
                    httpWebResponse.Close();
                    webRequest.Abort();
                }
                catch (WebException wex)
                {
                    responseContent = string.Format("Error:{0}\r\n", wex.Message);
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            responseContent += "StatusCode:  " + Convert.ToInt32(((HttpWebResponse)wex.Response).StatusCode) + "\r\n";
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                responseContent += reader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        responseContent += "WebException->Response is null";
                    }
                }

                catch (Exception ex)
                {
                    responseContent = ex.Message;
                    ErrorLog.PutInLog("ID:0090 " + ex.InnerException);
                }
                if (myWatch.IsRunning)
                {
                    myWatch.Stop();
                }
                myEdr.spanTime = myWatch.ElapsedMilliseconds.ToString();
                myEdr.backContent = responseContent;

                return responseContent;
            }


        }
    }
}
