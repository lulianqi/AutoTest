using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Linq;
using MyCommonTool;
using CaseExecutiveActuator;


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


namespace CaseExecutiveActuator
{
    public static class myWebTool
    {
        /// <summary>
        /// 已经过时请勿使用，请使用HttpClient类
        /// </summary>
        public static class myHttp
        {
            public static int httpTimeOut = 100000;                                            //http time out , HttpPostData will not use this value

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <returns>back </returns>
            public static string SendData(string url, string data, string method)
            {
                string re = "";
                try
                {
                    //except POST other data will add the url,if you want adjust the ruleschange here
                    if (method.ToUpper() != "POST" && data != null)
                    {
                        url += "?" + data;
                        data = null;           //make sure the data is null when Request is not post
                    }
                    WebRequest wr = WebRequest.Create(url);
                    wr.Timeout = httpTimeOut;
                    wr.Method = method;
                    wr.ContentType = "application/x-www-form-urlencoded";
                    //wr.ContentType = "multipart/form-data";
                    char[] reserved = { '?', '=', '&' };
                    StringBuilder UrlEncoded = new StringBuilder();
                    byte[] SomeBytes = null;
                    if (data != null && method.ToUpper() == "POST")
                    {
                        SomeBytes = Encoding.UTF8.GetBytes(data);
                        wr.ContentLength = SomeBytes.Length;
                        Stream newStream = wr.GetRequestStream();                //连接建立Head已经发出，POST请求体还没有发送
                        newStream.Write(SomeBytes, 0, SomeBytes.Length);         //请求交互完成
                        newStream.Close();
                    }
                    else
                    {
                        wr.ContentLength = 0;
                    }


                    WebResponse result = wr.GetResponse();                       //GetResponse 方法向 Internet 资源发送请求并返回 WebResponse 实例。如果该请求已由 GetRequestStream 调用启动，则 GetResponse 方法完成该请求并返回任何响应。

                    Stream ReceiveStream = result.GetResponseStream();

                    Byte[] read = new Byte[512];
                    int bytes = ReceiveStream.Read(read, 0, 512);

                    re = "";
                    while (bytes > 0)
                    {
                        Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                        re += encode.GetString(read, 0, bytes);
                        bytes = ReceiveStream.Read(read, 0, 512);
                    }
                }

                catch (WebException wex)
                {
                    re = "Error:  " + wex.Message + "\r\n";
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            re += "StatusCode:  " + Convert.ToInt32(((HttpWebResponse)wex.Response).StatusCode) + "\r\n";
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                re += reader.ReadToEnd();
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    re = ex.Message;
                    ErrorLog.PutInLog("ID:0090 " + ex.InnerException);
                }
                return re;
            }

            /// <summary>
            /// i will Send Data (you can put Head in Request)
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <param name="heads">http Head list</param>
            /// <returns>back</returns>
            public static string SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads )
            {
                string re = "";
                try
                {
                    //except POST other data will add the url,if you want adjust the ruleschange here
                    if (method.ToUpper() != "POST" && data != null)
                    {
                        url += "?" + data;
                        data = null;           //make sure the data is null when Request is not post
                    }
                    WebRequest wr = WebRequest.Create(url);
                    wr.Timeout = httpTimeOut;
                    wr.Method = method;
                    wr.ContentType = "application/x-www-form-urlencoded";

                    if (heads != null)
                    {
                        //wr.Headers.Add(new NameValueCollection());
                        foreach (var Head in heads)
                        {

                            wr.Headers.Add(Head.Key, Head.Value);
                        }
                    }

                    //wr.ContentType = "multipart/form-data";
                    char[] reserved = { '?', '=', '&' };
                    StringBuilder UrlEncoded = new StringBuilder();
                    byte[] SomeBytes = null;
                    if (data != null && method.ToUpper() == "POST")
                    {
                        SomeBytes = Encoding.UTF8.GetBytes(data);
                        wr.ContentLength = SomeBytes.Length;
                        Stream newStream = wr.GetRequestStream();                //连接建立Head已经发出，POST请求体还没有发送
                        newStream.Write(SomeBytes, 0, SomeBytes.Length);         //请求交互完成
                        newStream.Close();
                    }
                    else
                    {
                        wr.ContentLength = 0;
                    }


                    WebResponse result = wr.GetResponse();                       //GetResponse 方法向 Internet 资源发送请求并返回 WebResponse 实例。如果该请求已由 GetRequestStream 调用启动，则 GetResponse 方法完成该请求并返回任何响应。

                    Stream ReceiveStream = result.GetResponseStream();

                    Byte[] read = new Byte[512];
                    int bytes = ReceiveStream.Read(read, 0, 512);

                    re = "";
                    while (bytes > 0)
                    {
                        Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                        re += encode.GetString(read, 0, bytes);
                        bytes = ReceiveStream.Read(read, 0, 512);
                    }
                }

                catch (WebException wex)
                {
                    re = "Error:  " + wex.Message + "\r\n";
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            re += "StatusCode:  " + Convert.ToInt32(((HttpWebResponse)wex.Response).StatusCode) + "\r\n";
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                re += reader.ReadToEnd();
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    re = ex.Message;
                    ErrorLog.PutInLog("ID:0090 " + ex.InnerException);
                }
                return re;
            }

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <returns>back </returns>
            public static string SendData(string url, string data, string method, ref myAutoHttpTest myAht)
            {
                string re = "";
                Stopwatch myWatch = new Stopwatch();
                try
                {
                    //except POST other data will add the url,if you want adjust the ruleschange here
                    if (method.ToUpper() != "POST" && data!=null)
                    {
                        url += "?" + data;
                        data = null;           //make sure the data is null when Request is not post
                    }
                    WebRequest wr = WebRequest.Create(url);
                    wr.Timeout = httpTimeOut;
                    //HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);
                    wr.Method = method;
                    wr.ContentType = "application/x-www-form-urlencoded";
                    //((HttpWebRequest)wr).KeepAlive = true;
                    //wr.Headers.Remove(HttpRequestHeader.Connection);
                    //wr.Headers.Add(HttpRequestHeader.Connection, "close");
                    //wr.Headers.Add(HttpRequestHeader.KeepAlive, "false");
                    char[] reserved = { '?', '=', '&' };
                    StringBuilder UrlEncoded = new StringBuilder();
                    byte[] SomeBytes = null;
                    myAht.startTime = DateTime.Now.ToString("HH:mm:ss");
                    myWatch.Start();
                    if (data != null && method.ToUpper() == "POST")
                    {
                        SomeBytes = Encoding.UTF8.GetBytes(data);
                        wr.ContentLength = SomeBytes.Length;
                        myWatch.Reset();
                        myWatch.Start();
                        Stream newStream = wr.GetRequestStream();
                        newStream.Write(SomeBytes, 0, SomeBytes.Length);
                        myWatch.Stop();
                        newStream.Close();
                    }
                    else
                    {
                        wr.ContentLength = 0;
                    }

                    if (data == null && method.ToUpper() != "POST")
                    {
                        myWatch.Start();
                    }
                    WebResponse result = wr.GetResponse();
                    if (data == null && method.ToUpper() != "POST")
                    {
                        myWatch.Stop();
                    }

                    Stream ReceiveStream = result.GetResponseStream();

                    Byte[] read = new Byte[512];
                    int bytes = ReceiveStream.Read(read, 0, 512);

                    re = "";
                    while (bytes > 0)
                    {
                        Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                        re += encode.GetString(read, 0, bytes);
                        bytes = ReceiveStream.Read(read, 0, 512);
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
                    re = "Unknow Error";
                    ErrorLog.PutInLog("ID:0192  " + ex.Message);
                }

                if (myWatch.IsRunning)
                {
                    myWatch.Stop();
                }
                myAht.spanTime = myWatch.ElapsedMilliseconds.ToString();
                
                myAht.result = re;
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
            public static string SendDataSaveEx(string url, string data, string method, ref myAutoHttpTest myAht, string saveFileName)
            {
                string re = "";
                Stopwatch myWatch = new Stopwatch();
                try
                {
                    //except POST other data will add the url,if you want adjust the ruleschange here
                    if (method.ToUpper() != "POST" && data != null)
                    {
                        url += "?" + data;
                        data = null;
                    }
                    WebRequest wr = WebRequest.Create(url);
                    wr.Timeout = httpTimeOut;
                    //HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);
                    wr.Method = method;
                    wr.ContentType = "application/x-www-form-urlencoded";
                    //((HttpWebRequest)wr).KeepAlive = true;
                    //wr.Headers.Remove(HttpRequestHeader.Connection);
                    //wr.Headers.Add(HttpRequestHeader.Connection, "close");
                    //wr.Headers.Add(HttpRequestHeader.KeepAlive, "false");
                    char[] reserved = { '?', '=', '&' };
                    StringBuilder UrlEncoded = new StringBuilder();
                    byte[] SomeBytes = null;
                    myAht.startTime = DateTime.Now.ToString("HH:mm:ss");
                    myWatch.Start();
                    if (data != null && method.ToUpper() == "POST")
                    {
                        SomeBytes = Encoding.UTF8.GetBytes(data);
                        wr.ContentLength = SomeBytes.Length;
                        myWatch.Reset();
                        myWatch.Start();
                        Stream newStream = wr.GetRequestStream();
                        newStream.Write(SomeBytes, 0, SomeBytes.Length);
                        myWatch.Stop();
                        newStream.Close();
                    }
                    else
                    {
                        wr.ContentLength = 0;
                    }

                    if (data == null && method.ToUpper() != "POST")
                    {
                        myWatch.Start();
                    }
                    WebResponse result = wr.GetResponse();
                    if (data == null && method.ToUpper() != "POST")
                    {
                        myWatch.Stop();
                    }

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
                    ErrorLog.PutInLog("ID:0331  " + ex.Message);
                }

                if (myWatch.IsRunning)
                {
                    myWatch.Stop();
                }
                myAht.spanTime = myWatch.ElapsedMilliseconds.ToString();

                myAht.result = re;
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
            public static string SendData(string url, string data, string method, ref myAutoHttpTest myAht, string saveFileName)
            {
                string re = "";
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
                    if (method == "POST")
                    {
                        if (data != null)
                        {
                            tempUrl = tempUrl + "?" + data;
                        }
                    }

                    using (WebClient client = new WebClient())
                    {
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
                    ErrorLog.PutInLog("ID:0408  " + ex.Message);
                }

                if (myWatch.IsRunning)
                {
                    myWatch.Stop();
                }
                myAht.spanTime = myWatch.ElapsedMilliseconds.ToString();

                myAht.result = re;
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
            /// <returns>back</returns>
            public static string HttpPostData(string url, int timeOut, string name, string filename,bool isFile ,string filePath, string bodyParameter)
            {
                string responseContent; 
                NameValueCollection stringDict = new NameValueCollection();

                if (bodyParameter != null)
                {
                    string[] sArray = bodyParameter.Split('&');
                    foreach (string tempStr in sArray)
                    {
                        int myBreak = tempStr.IndexOf('=');
                        if (myBreak == -1)
                        {
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

                    using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),
                                                                    Encoding.GetEncoding("utf-8")))
                    {
                        responseContent = httpStreamReader.ReadToEnd();
                    }

                    httpWebResponse.Close();
                    webRequest.Abort();
                }
                catch (WebException wex)
                {
                    responseContent = "Error:  " + wex.Message + "\r\n";
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
                        byte[] myCmd = Encoding.UTF8.GetBytes(filePath);
                        memStream.Write(myCmd, 0, myCmd.Length);
                    }
                }

                catch (Exception ex)
                {
                    responseContent = ex.Message;
                    ErrorLog.PutInLog("ID:0090 " + ex.InnerException);
                }

                return responseContent;
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
            public static string HttpPostData(string url, int timeOut, string name, string filename, bool isFile, string filePath, string bodyParameter ,ref myAutoHttpTest myAht)
            {
                string responseContent;
                NameValueCollection stringDict = new NameValueCollection();

                if (bodyParameter != null)
                {
                    string[] sArray = bodyParameter.Split('&');
                    foreach (string tempStr in sArray)
                    {
                        int myBreak = tempStr.IndexOf('=');
                        if (myBreak == -1)
                        {
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
                            myAht.result = responseContent;
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
                myAht.startTime = DateTime.Now.ToString("HH:mm:ss");
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

                    using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),Encoding.GetEncoding("utf-8")))
                    {
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
                myAht.spanTime = myWatch.ElapsedMilliseconds.ToString();
             
                myAht.result = responseContent;
                return responseContent;
            }

        }

        public class HttpHelper
        {
            private delegate void SetHeadAttributeCallback(HttpWebRequest yourRequest, string yourHeadValue);

            private static Dictionary<string, SetHeadAttributeCallback> dicHeadSetFun = new Dictionary<string, SetHeadAttributeCallback>();
            static HttpHelper()
            {
                dicHeadSetFun.Add("Accept".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Accept = yourHeadValue));
                dicHeadSetFun.Add("Connection".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Connection = yourHeadValue));
                dicHeadSetFun.Add("Date".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => { DateTime tempTime; if (!DateTime.TryParse(yourHeadValue, out tempTime)) tempTime = DateTime.Now; yourRequest.Date = tempTime; }));  //2009-05-01 14:57:32
                //dicHeadSetFun.Add("KeepAlive".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.KeepAlive = yourHeadValue));////该头可以直接使用Headers.Add
                dicHeadSetFun.Add("Transfer-Encoding".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.TransferEncoding = yourHeadValue));
                dicHeadSetFun.Add("Content-Length".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => { int tempLen; if (!int.TryParse(yourHeadValue, out tempLen)) tempLen = 0; yourRequest.ContentLength = tempLen; }));
                dicHeadSetFun.Add("Content-Type".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.ContentType = yourHeadValue));
                dicHeadSetFun.Add("Expect".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Expect = yourHeadValue));
                dicHeadSetFun.Add("Host".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Host = yourHeadValue));
                //dicHeadSetFun.Add("IfModifiedSince".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.IfModifiedSince = yourHeadValue));
                dicHeadSetFun.Add("Referer".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Referer = yourHeadValue));
                dicHeadSetFun.Add("User-Agent".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.UserAgent = yourHeadValue));
            }

            /// <summary>
            /// 添加http请求头属性（特殊属性自动转化为dicHeadSetFun中委托完成设置）
            /// </summary>
            /// <param name="httpWebRequest">HttpWebRequest</param>
            /// <param name="heads">属性列表</param>
            public static void AddHttpHeads(HttpWebRequest httpWebRequest, List<KeyValuePair<string, string>> heads)
            {
                if (httpWebRequest == null)
                {
                    return;
                }
                if (heads != null)
                {
                    foreach (var Head in heads)
                    {
                        if (dicHeadSetFun.ContainsKey(Head.Key.ToUpper()))
                        {
                            (dicHeadSetFun[Head.Key.ToUpper()])(httpWebRequest, Head.Value);
                        }
                        else
                        {
                            httpWebRequest.Headers.Add(Head.Key, Head.Value);
                        }
                    }
                }
            }

            /// <summary>
            /// 添加http请求头属性（全部使用默认header.Add进行添加，失败后使用SetHeaderValue进行添加，不过依然可能不超过）
            /// </summary>
            /// <param name="header">WebHeaderCollection</param>
            /// <param name="heads">属性列表</param>
            public static void AddHttpHeads(WebHeaderCollection header, List<KeyValuePair<string, string>> heads)
            {
                if (header == null)
                {
                    return;
                }
                if (heads != null)
                {
                    //wr.Headers.Add(new NameValueCollection());
                    foreach (var Head in heads)
                    {
                        try
                        {
                            header.Add(Head.Key, Head.Value);
                            //((HttpWebRequest)wr).Headers.Add(HttpRequestHeader.Host, "www.contoso.com"); //必须用适当的属性修改host   使用4.0也报必须使用适当的属性或方法修改“Host”标头
                            //((HttpWebRequest)wr).Headers.Add("Host", "192.168.0.1");//这样一样不行
                            //SetHeaderValue(wr.Headers, "Host", "www.contoso.com:8080");//即使是4.0也无法直接修改
                            //((HttpWebRequest)wr).Host = "www.contoso.com:8080";//只有这种方式在4.0可以生效
                        }
                        catch (Exception ex)
                        {
                            SetHeaderValue(header, Head.Key, Head.Value);
                            ErrorLog.PutInLog("ID:0929  " + ex.Message);
                        }
                    }
                }
            }

            public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
            {

                var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",

                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                if (property != null)
                {

                    var collection = property.GetValue(header, null) as NameValueCollection;

                    collection[name] = value;

                }

            }
        }

        public class HttpClient
        {
            public static int httpTimeOut = 100000;                                            //http time out , HttpPostData will not use this value
            public static int httpReadWriteTimeout = 300000;                                   //WebRequest.ReadWriteTimeout 该属性暂时未设置

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <param name="myAht">the myAutoHttpTest will fill the data</param>
            /// <returns>back </returns>
            public static string SendData(string url, string data, string method, myExecutionDeviceResult myEdr)
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
            public static string SendData(string url, string data, string method, List<KeyValuePair<string,string>> heads, myExecutionDeviceResult myEdr)
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
                    wr.ContentType = "application/x-www-form-urlencoded";

                    HttpHelper.AddHttpHeads((HttpWebRequest)wr, heads);
                    
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

                    Byte[] read = new Byte[512];
                    int bytes = ReceiveStream.Read(read, 0, 512);

                    re = "";
                    while (bytes > 0)
                    {
                        Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                        re += encode.GetString(read, 0, bytes);
                        bytes = ReceiveStream.Read(read, 0, 512);
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
            public static string SendDataSaveEx(string url, string data, string method, myExecutionDeviceResult myEdr, string saveFileName)
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
            public static string SendDataSaveEx(string url, string data, string method, List<KeyValuePair<string, string>> heads, myExecutionDeviceResult myEdr, string saveFileName)
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
                    wr.ContentType = "application/x-www-form-urlencoded";

                    HttpHelper.AddHttpHeads((HttpWebRequest)wr, heads);
                    
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
            public static string SendData(string url, string data, string method, myExecutionDeviceResult myEdr, string saveFileName)
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
            public static string SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads, myExecutionDeviceResult myEdr, string saveFileName)
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
                        HttpHelper.AddHttpHeads(client.Headers, heads);
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
            public static string HttpPostData(string url, int timeOut, string name, string filename, bool isFile, string filePath, string bodyParameter, myExecutionDeviceResult myEdr)
            {
                string responseContent;
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

        }
    }
}
