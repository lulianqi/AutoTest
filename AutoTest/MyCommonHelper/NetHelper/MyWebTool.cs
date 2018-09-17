using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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


namespace MyCommonHelper.NetHelper
{
    public class MyWebTool
    {
        public class HttpMultipartDate
        {
            /** eg：
            -----------------8d46c074125a195
            Content-Disposition: form-data; name="name"; filename="filenmae"
            Content-Type: application/octet-stream

            testdata
            -----------------8d46c074125a195--
             * */
          
            /// <summary>
            /// name属性值,为null则不加
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// filename属性值,为null则不加
            /// </summary>
            public string FileName { get; set; }
            /// <summary>
            /// Multipart下Content-Type: application/octet-stream,为null则不加
            /// </summary>
            public string ContentType { get; set; }
            /// <summary>
            /// 是否把fileData当作文件路径处理
            /// </summary>
            public bool IsFile { get; set; }
            /// <summary>
            /// 文件内容或文件路径。为null则当作""
            /// </summary>
            public string FileData { get; set; }
            public HttpMultipartDate()
            {
                Name = FileName = ContentType = FileData = null;
            }

            /// <summary>
            /// 初始化 HttpMultipartDate
            /// </summary>
            /// <param name="yourName">name属性值,为null则不加</param>
            /// <param name="yourFileName">filename属性值,为null则不加</param>
            /// <param name="yourContentType">Multipart下Content-Type: application/octet-stream,为null则为默认值application/octet-stream</param>
            /// <param name="yourIsFile">是否把fileData当作文件路径处理</param>
            /// <param name="yourFileData">文件内容或文件路径。为null则当作""（作为路径时如果路径不存在将会返回错误）</param>
            public HttpMultipartDate(string yourName,string yourFileName,string yourContentType,bool yourIsFile,string yourFileData)
            {
                Name = yourName;
                FileName = yourFileName;
                ContentType = yourContentType;
                IsFile = yourIsFile;
                FileData = yourFileData;
            }

        }

        public class HttpHelper
        {
            private delegate void SetHeadAttributeCallback(HttpWebRequest yourRequest, string yourHeadValue);

            private static Dictionary<string, SetHeadAttributeCallback> dicHeadSetFun = new Dictionary<string, SetHeadAttributeCallback>();
            static HttpHelper()
            {
                dicHeadSetFun.Add("Accept".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Accept = yourHeadValue));
                dicHeadSetFun.Add("Connection".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => { string tempHeadVaule = yourHeadValue.ToLower(); if (tempHeadVaule.IndexOf("keep-alive") != -1) { yourRequest.KeepAlive = true; } else if (tempHeadVaule.IndexOf("closee") != -1) { yourRequest.KeepAlive = false; } else { yourRequest.Connection = yourHeadValue; } }));
                dicHeadSetFun.Add("Date".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => { DateTime tempTime; if (!DateTime.TryParse(yourHeadValue, out tempTime)) tempTime = DateTime.Now; yourRequest.Date = tempTime; }));  //2009-05-01 14:57:32 //修改该头需要4.0版本支持，如果升级4.0可以取消该注释，启用该功能
                //dicHeadSetFun.Add("KeepAlive".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.KeepAlive = yourHeadValue));//该头可以直接使用Headers.Add
                dicHeadSetFun.Add("Transfer-Encoding".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.TransferEncoding = yourHeadValue));
                dicHeadSetFun.Add("Content-Length".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => { int tempLen; if (!int.TryParse(yourHeadValue, out tempLen)) tempLen = 0; yourRequest.ContentLength = tempLen; }));
                dicHeadSetFun.Add("Content-Type".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.ContentType = yourHeadValue));
                dicHeadSetFun.Add("Expect".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Expect = yourHeadValue));
                dicHeadSetFun.Add("Host".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Host = yourHeadValue)); //修改该头需要4.0版本支持，如果升级4.0可以取消该注释，启用该功能
                dicHeadSetFun.Add("IfModifiedSince".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Referer = yourHeadValue));
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
            /// 添加http请求头属性（全部使用默认header.Add进行添加，失败后使用SetHeaderValue进行添加，不过依然可能失败）
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

            /// <summary>
            /// 设置请求头（注意该方法未经过测试，使用前请先测试）
            /// </summary>
            /// <param name="header">WebHeaderCollection</param>
            /// <param name="name">key</param>
            /// <param name="value">value</param>
            public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
            {
                var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (property != null)
                {
                    var collection = property.GetValue(header, null) as NameValueCollection;
                    collection[name] = value;
                }
            }
        }

        public class HttpTimeLine
        {
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime StartTime { get; set; }
            /// <summary>
            /// 耗时（毫秒为单位）
            /// </summary>
            public long ElapsedTime { get; set; }

            public HttpTimeLine()
            {
                ElapsedTime = 0;
            }
        }

        public class MyHttp
        {
            public int httpTimeOut = 100000;                                              //http time out ,SendData and HttpPostData will use this value   （连接超时）
            public int httpReadWriteTimeout = 300000;                                     //WebRequest.ReadWriteTimeout 该属性暂时未设置           （读取/写入超时）
            public bool showResponseHeads = false;                                        //是否返回http返回头
            public Encoding requestEncoding = System.Text.Encoding.GetEncoding("UTF-8");  //需要发送数据，将使用此编码（HttpPostData 不使用该设置，需要单独设置）
            public Encoding responseEncoding = System.Text.Encoding.GetEncoding("UTF-8"); //如果要显示返回数据，返回数据将使用此编码
            public string defaultContentType = null;
            public bool withDefaultCookieContainer = false;                               //是否默认启用CookieContainer，如果启用则默认会管理所有使用MyHttp的cookie内容

            private readonly string EOF = "\r\n";
            private CookieContainer cookieContainer;


            static MyHttp()
            {
                //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(
                //    (sender, certificate, chain, sslPolicyErrors) => { return true; });
                ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
                //Console.WriteLine(ServicePointManager.DefaultConnectionLimit); //默认最大并发数有限，可以使用System.Net.ServicePointManager.DefaultConnectionLimit重设该值
                System.Net.ServicePointManager.DefaultConnectionLimit = 2000;
            }

            private static bool MyRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

            public MyHttp()
            {
                //cookieContainer = new CookieContainer(5000, 500, 1000);
                cookieContainer = new CookieContainer();
            }

            public MyHttp(bool isShowResponseHeads, bool isWithDefaultCookieContainer)
                : this()
            {
                showResponseHeads = isShowResponseHeads;
                withDefaultCookieContainer = isWithDefaultCookieContainer;
            }
           

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url (if[GET].. url+?+data / if[PUT]or[POST] it will in body})</param>
            /// <param name="method">GET/POST</param>
            /// <returns>back </returns>
            public string SendData(string url, string data, string method)
            {
                return SendData(url, data, method, null,null);
            }

            /// <summary>
            /// i will Send Data with Get
            /// </summary>
            /// <param name="url">url</param>
            /// <returns>back</returns>
            public string SendData(string url)
            {
                return SendData(url, null, "GET", null,null);
            }

             /// <summary>
            /// i will Send Data (you can put Head in Request)
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url (if[GET].. url+?+data / if[PUT]or[POST] it will in body})</param>
            /// <param name="method">GET/POST</param>
            /// <param name="heads">http Head list （if not need set it null）(header 名是不区分大小写的)</param>
            /// <returns>back</returns>
            public string SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads)
            {
                return SendData(url, data, method, heads, null);
            }

            /// <summary>
            /// i will Send Data (you can put Head in Request)
            /// </summary>
            /// <param name="url"> url [http://,https:// ,ftp:// ,file:// ]</param>
            /// <param name="data"> param if method is not POST it will add to the url (if[GET].. url+?+data / if[PUT]or[POST] it will in body})</param>
            /// <param name="method">GET/POST</param>
            /// <param name="heads">http Head list （if not need set it null）(header 名是不区分大小写的)</param>
            /// <param name="saveFileName">save your response as file （if not need set it null）</param>
            /// <returns>back</returns>
            public string SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads, string saveFileName)
            {
                return SendData(url, data, method, heads, saveFileName,null);
            }

             /// <summary>
            /// i will Send Data (you can put Head in Request) （CookieContainer will use withDefaultCookieContainer）
            /// </summary>
            /// <param name="url"> url [http://,https:// ,ftp:// ,file:// ]</param>
            /// <param name="data"> param if method is not POST it will add to the url (if[GET].. url+?+data / if[PUT]or[POST] it will in body})</param>
            /// <param name="method">GET/POST</param>
            /// <param name="heads">http Head list （if not need set it null）(header 名是不区分大小写的)</param>
            /// <param name="saveFileName">save your response as file （if not need set it null）</param>
            /// <param name="manualResetEvent">ManualResetEvent 并发集合点 （if not need set it null）</param>
            /// <returns>back</returns>
            public string SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads, string saveFileName, System.Threading.ManualResetEvent manualResetEvent)
            {
                return SendData(url, data, method, heads, withDefaultCookieContainer, saveFileName, null);
            }

            /// <summary>
            /// i will Send Data (you can put Head in Request)
            /// </summary>
            /// <param name="url"> url [http://,https:// ,ftp:// ,file:// ]</param>
            /// <param name="data"> param if method is not POST it will add to the url (if[GET].. url+?+data / if[PUT]or[POST] it will in body})</param>
            /// <param name="method">GET/POST</param>
            /// <param name="heads">http Head list （if not need set it null）(header 名是不区分大小写的)</param>
            /// <param name="isAntoCookie">is use static CookieContainer （是否使用默认CookieContainer管理cookie，优先级高于withDefaultCookieContainer）</isAntoCookie>
            /// <param name="saveFileName">save your response as file （if not need set it null）</param>
            /// <param name="manualResetEvent">ManualResetEvent 并发集合点 （if not need set it null）</param>
            /// <returns>back</returns>
            public string SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads,bool isAntoCookie ,string saveFileName, System.Threading.ManualResetEvent manualResetEvent)
            {
                return SendData(url, data, method, heads, isAntoCookie, saveFileName, manualResetEvent, null);
            }

            /// <summary>
            /// i will Send Data (you can put Head in Request)
            /// </summary>
            /// <param name="url"> url [http://,https:// ,ftp:// ,file:// ]</param>
            /// <param name="data"> param if method is not POST it will add to the url (if[GET].. url+?+data / if[PUT]or[POST] it will in body})</param>
            /// <param name="method">GET/POST</param>
            /// <param name="heads">http Head list （if not need set it null）(header 名是不区分大小写的)</param>
            /// <param name="isAntoCookie">is use static CookieContainer （是否使用默认CookieContainer管理cookie，优先级高于withDefaultCookieContainer）</param>
            /// <param name="saveFileName">save your response as file （if not need set it null）</param>
            /// <param name="manualResetEvent">ManualResetEvent 并发集合点 （if not need set it null）</param>
            /// <param name="timeline">请求时间线，请求耗时(如果不需要请传null)</param>
            /// <returns>back</returns>
            public string SendData(string url, string data, string method, List<KeyValuePair<string, string>> heads, bool isAntoCookie, string saveFileName, System.Threading.ManualResetEvent manualResetEvent,HttpTimeLine timeline)
            {
                Stopwatch myWatch = null;
                string re = null;

                Action WaitStartSignal = () =>
                {
                    if (manualResetEvent != null)
                    {
                        manualResetEvent.WaitOne();
                    }

                    if(timeline!=null)
                    {
                        timeline.StartTime = DateTime.Now;
                        myWatch.Start();
                    }
                };

                if(timeline!=null)
                {
                    myWatch = new Stopwatch();
                }
                bool hasBody = !string.IsNullOrEmpty(data);
                bool needBody = method.ToUpper() == "POST" || method.ToUpper() == "PUT";
                WebRequest webRequest = null;
                WebResponse webResponse = null;

                try
                {
                    //except POST other data will add the url,if you want adjust the rules change here
                    if (!needBody && hasBody)
                    {
                        url += "?" + data;
                        data = null;           //make sure the data is null when Request is not post
                    }
                    webRequest = WebRequest.Create(url);
                    webRequest.Timeout = httpTimeOut;
                    webRequest.Method = method;
                    if (heads == null && defaultContentType != null)
                    {
                        webRequest.ContentType = defaultContentType;
                    }
                    //((HttpWebRequest)wr).KeepAlive = true;
                    //((HttpWebRequest)wr).Pipelined = true;
                    HttpHelper.AddHttpHeads((HttpWebRequest)webRequest, heads);

                    //wr.ContentType = "multipart/form-data";
                    char[] reserved = { '?', '=', '&' };
                    StringBuilder UrlEncoded = new StringBuilder();
                    byte[] SomeBytes = null;
                    if (isAntoCookie)
                    {
                        ((HttpWebRequest)webRequest).CookieContainer = cookieContainer;
                    }

                    if (needBody)
                    {
                        if (hasBody)
                        {
                            SomeBytes = requestEncoding.GetBytes(data);
                            webRequest.ContentLength = SomeBytes.Length;
                            WaitStartSignal();                                       //尽可能确保所有manualResetEvent都在数据完全准备完成后
                            Stream newStream = webRequest.GetRequestStream();        //连接建立Head已经发出，POST请求体还没有发送 (服务器可能会先回http 100)  (包括tcp及TLS链接建立都在这里)
                            newStream.Write(SomeBytes, 0, SomeBytes.Length);         //请求交互完成
                            newStream.Close();                                       //释放写入流（MSDN的示例也是在此处释放）(执行到此处请求就已经结束)
                            webResponse = webRequest.GetResponse();                  //此处的GetResponse不会发起任何网络请求，只是为了填充webResponse
                            if (timeline != null)
                            {
                                myWatch.Stop();
                            }
                        }
                        else
                        {
                            webRequest.ContentLength = 0;
                            WaitStartSignal();
                            webResponse = webRequest.GetResponse();
                            if (timeline != null)
                            {
                                myWatch.Stop();
                            }
                        }
                    }
                    else
                    {
                        WaitStartSignal();
                        webResponse = webRequest.GetResponse();                       //GetResponse 方法向 Internet 资源发送请求并返回 WebResponse 实例。如果该请求已由 GetRequestStream 调用启动，则 GetResponse 方法完成该请求并返回任何响应。
                        if (timeline != null)
                        {
                            myWatch.Stop();
                        }
                    }

                    Stream receiveStream = webResponse.GetResponseStream();

                    if (isAntoCookie)
                    {

                        if (((HttpWebResponse)webResponse).Cookies != null && ((HttpWebResponse)webResponse).Cookies.Count > 0)
                        {
                            cookieContainer.Add(((HttpWebResponse)webResponse).Cookies);
                        }
                    }

                    if (saveFileName == null)
                    {
                        if (showResponseHeads)
                        {
                            re = webResponse.Headers.ToString();
                        }
                        using (var httpStreamReader = new StreamReader(receiveStream, responseEncoding))
                        {
                            re += httpStreamReader.ReadToEnd();
                        }

                        //使用如下方法自己读取byte[] 是可行的，不过在Encoding 可变编码方式时，不能确保分段不被截断，直接使用内置StreamReader也是可以的
                        /**  
                        Byte[] read = new Byte[512];
                        int bytes = receiveStream.Read(read, 0, 512);
                        if (showResponseHeads)
                        {
                            re = result.Headers.ToString();
                        }
                        while (bytes > 0)
                        {
                            re += responseEncoding.GetString(read, 0, bytes);
                            bytes = receiveStream.Read(read, 0, 512);
                        }
                         * */
                    }
                    //save file
                    else
                    {
                        using (FileStream stream = new FileStream(saveFileName, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            int tempReadCount = 1024;
                            byte[] infbytes = new byte[tempReadCount]; //反复使用前也不要清空，因为后面写入会指定有效长度
                            int tempLen = tempReadCount;
                            int offset = 0;
                            while (tempLen >= tempReadCount)
                            {
                                tempLen = receiveStream.Read(infbytes, 0, tempReadCount);
                                stream.Write(infbytes, 0, tempLen);//FileStream 内建缓冲区，不用自己构建缓存写入,FileStream的offset会自动维护，也可以使用stream.Position强制指定
                                offset += tempLen;
                            }
                            re = string.Format("file save success in [ {0} ]  with {1}byte", saveFileName, offset);
                        }
                        #region WriteAllBytes
                        /**
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
                        * */
                        #endregion
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
                    ErrorLog.PutInLog(ex);
                }

                finally
                {
                    if (webResponse != null)
                    {
                        webResponse.Close();
                    }
                    if (timeline != null)
                    {
                        if (myWatch.IsRunning)
                        {
                            myWatch.Stop();
                        }
                        timeline.ElapsedTime = myWatch.ElapsedMilliseconds;
                    }
                }
                return re;
            }

            /// <summary>
            /// DownloadFile with http
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="heads">heads</param>
            /// <param name="saveFileName">save File path</param>
            public void DownloadFile(string url, List<KeyValuePair<string, string>> heads, string saveFileName)
            {
                using (WebClient client = new WebClient())
                {
                    HttpHelper.AddHttpHeads(client.Headers, heads);
                    client.DownloadFile(url, saveFileName);
                }
            }

            /// <summary>
            /// DownloadFile with http 
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="saveFileName">save File path</param>
            public void DownloadFile(string url, string saveFileName)
            {
                using (WebClient client = new WebClient())
                {
                    HttpHelper.AddHttpHeads(client.Headers, null);
                    client.DownloadFile(url, saveFileName);
                }
            }

            /// <summary>
            /// ***********该重载停止维护************
            /// i will Send Data with multipart,if you do not want updata any file you can set isFile is false and set filePath is null (not maintain 请使用以HttpMultipartDate为参数的重载版本)
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="timeOut">timeOut</param>
            /// <param name="name">Parameter name</param>
            /// <param name="filename">filename</param>
            /// <param name="isFile">is a file</param>
            /// <param name="filePath">file path or cmd</param>
            /// <param name="bodyParameter">the other Parameter in body</param>
            /// <returns>back</returns>
            public string HttpPostData(string url, int timeOut, string name, string filename,bool isFile ,string filePath, string bodyParameter)
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

                    using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(), responseEncoding))
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
            /// post multipart data
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="heads">heads (if not need it ,just set it null)</param>
            /// <param name="isAntoCookie">is use static CookieContainer （是否使用默认CookieContainer管理cookie，优先级高于withDefaultCookieContainer）</param>
            /// <param name="bodyData">normal body (if not need it ,just set it null)</param>
            /// <param name="multipartDateList">MultipartDate list(if not need it ,just set it null)</param>
            /// <param name="bodyMultipartParameter">celerity MultipartParameter your should set it like "a=1&amp;b=2&amp;c=3" and it will send in multipart format (if not need it ,just set it null)</param>
            /// <param name="yourBodyEncoding">the MultipartParameter Encoding (if set it null ,it will be utf 8)</param>
            /// <param name="manualResetEvent">ManualResetEvent 并发集合点 （if not need set it null）</param>
            /// <param name="timeline">请求时间线，请求耗时(如果不需要请传null)</param>
            /// <returns>back data</returns>
            public string HttpPostData(string url, List<KeyValuePair<string, string>> heads, bool isAntoCookie, string bodyData, List<HttpMultipartDate> multipartDateList, string bodyMultipartParameter, Encoding yourBodyEncoding, System.Threading.ManualResetEvent manualResetEvent, HttpTimeLine timeline)
            {
                string responseContent = null;
                Encoding httpBodyEncoding = Encoding.UTF8;
                string defaultMultipartContentType = "application/octet-stream";
                NameValueCollection stringDict = new NameValueCollection();
                HttpWebRequest webRequest = null;
                HttpWebResponse httpWebResponse = null;
                Stopwatch myWatch = null;

                if (timeline != null)
                {
                    myWatch = new Stopwatch();
                }
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
                            return "can't find =";
                        }
                        stringDict.Add(tempStr.Substring(0, myBreak), tempStr.Substring(myBreak + 1));
                    }
                }

                var memStream = new MemoryStream();
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                //写入http头
                HttpHelper.AddHttpHeads(webRequest, heads);

                // 边界符
                var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                // 边界符
                var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                // 最后的结束符
                var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

                // 设置属性
                webRequest.Method = "POST";
                webRequest.Timeout = httpTimeOut;
                webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

                //写如常规body
                if (bodyData != null)
                {
                    var bodybytes = httpBodyEncoding.GetBytes(bodyData);
                    memStream.Write(bodybytes, 0, bodybytes.Length);
                }

                if (multipartDateList != null)
                {
                    foreach (HttpMultipartDate nowMultipart in multipartDateList)
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

                //设置CookieContainer
                if (isAntoCookie)
                {
                    ((HttpWebRequest)webRequest).CookieContainer = cookieContainer;
                }

                //开始请求
                try
                {
                    var requestStream = webRequest.GetRequestStream();
                    memStream.Position = 0;
                    var tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();
                    if (manualResetEvent != null)
                    {
                        manualResetEvent.WaitOne();
                    }
                    if(timeline!=null)
                    {
                        timeline.StartTime = DateTime.Now;
                        myWatch.Start();
                    }
                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    requestStream.Close();

                    httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                    if (timeline != null)
                    {
                        myWatch.Stop();
                    }

                    if (isAntoCookie)
                    {

                        if (httpWebResponse.Cookies != null && httpWebResponse.Cookies.Count > 0)
                        {
                            cookieContainer.Add(httpWebResponse.Cookies);
                        }
                    }

                    if (showResponseHeads)
                    {
                        responseContent = httpWebResponse.Headers.ToString();
                    }
                    using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(), responseEncoding))
                    {
                        responseContent += httpStreamReader.ReadToEnd();
                    }

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

                finally
                {
                    if (httpWebResponse != null)
                    {
                        httpWebResponse.Close();
                    }
                    if (timeline != null)
                    {
                        if (myWatch.IsRunning)
                        {
                            myWatch.Stop();
                        }
                        timeline.ElapsedTime = myWatch.ElapsedMilliseconds;
                    }
                }

                return responseContent;
            }

            /// <summary>
            /// post multipart data
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="heads">heads (if not need it ,just set it null)</param>
            /// <param name="isAntoCookie">is use static CookieContainer （是否使用默认CookieContainer管理cookie，优先级高于withDefaultCookieContainer）</param>
            /// <param name="bodyData">normal body (if not need it ,just set it null)</param>
            /// <param name="multipartDateList">MultipartDate list(if not need it ,just set it null)</param>
            /// <param name="bodyMultipartParameter">celerity MultipartParameter your should set it like "a=1&amp;b=2&amp;c=3" and it will send in multipart format (if not need it ,just set it null)</param>
            /// <param name="yourBodyEncoding">the MultipartParameter Encoding (if set it null ,it will be utf 8)</param>
            /// <returns>back data</returns>
            public string HttpPostData(string url, List<KeyValuePair<string, string>> heads, bool isAntoCookie, string bodyData, List<HttpMultipartDate> multipartDateList, string bodyMultipartParameter, Encoding yourBodyEncoding)
            {
                return HttpPostData(url, heads, isAntoCookie, bodyData, multipartDateList, bodyMultipartParameter, yourBodyEncoding, null,null);
            }


            /// <summary>
            /// post multipart data
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="heads">heads (if not need it ,just set it null)</param>
            /// <param name="bodyData">normal body (if not need it ,just set it null)</param>
            /// <param name="HttpMultipartDate">MultipartDate list(if not need it ,just set it null)</param>
            /// <param name="bodyMultipartParameter">celerity MultipartParameter like "a=1&amp;b=2&amp;c=3" (if not need it ,just set it null)</param>
            /// <param name="yourBodyEncoding">the MultipartParameter Encoding (if set it null ,it will be utf 8)</param>
            /// <returns>back data</returns>
            public string HttpPostData(string url, List<KeyValuePair<string, string>> heads, string bodyData, List<HttpMultipartDate> multipartDateList, string bodyMultipartParameter, Encoding yourBodyEncoding)
            {
                return HttpPostData(url, heads, withDefaultCookieContainer, bodyData, multipartDateList, bodyMultipartParameter, yourBodyEncoding);
            }


            /// <summary>
            /// post multipart data
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="HttpMultipartDate">MultipartDate list(if not need it ,just set it null)</param>
            /// <returns>back data</returns>
            public string HttpPostData(string url,HttpMultipartDate HttpMultipartDate)
            {
                return HttpPostData(url, null, null, new List<HttpMultipartDate>() { HttpMultipartDate }, null , null);
            }
        }
    }
}
