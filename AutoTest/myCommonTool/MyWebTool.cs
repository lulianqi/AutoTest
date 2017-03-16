using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Linq;


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


namespace MyCommonHelper
{
    public static class MyWebTool
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
            /// <param name="yourContentType">Multipart下Content-Type: application/octet-stream,为null则不加</param>
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
                dicHeadSetFun.Add("Connection".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Connection = yourHeadValue));
                //dicHeadSetFun.Add("Date".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => { DateTime tempTime; if (!DateTime.TryParse(yourHeadValue, out tempTime)) tempTime = DateTime.Now; yourRequest.Date = tempTime; }));  //2009-05-01 14:57:32 //修改该头需要4.0版本支持，如果升级4.0可以取消该注释，启用该功能
                //dicHeadSetFun.Add("KeepAlive".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.KeepAlive = yourHeadValue));//该头可以直接使用Headers.Add
                dicHeadSetFun.Add("Transfer-Encoding".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.TransferEncoding = yourHeadValue));
                dicHeadSetFun.Add("Content-Length".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => { int tempLen; if (!int.TryParse(yourHeadValue, out tempLen)) tempLen = 0; yourRequest.ContentLength = tempLen; }));
                dicHeadSetFun.Add("Content-Type".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.ContentType = yourHeadValue));
                dicHeadSetFun.Add("Expect".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Expect = yourHeadValue));
                //dicHeadSetFun.Add("Host".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Host = yourHeadValue)); //修改该头需要4.0版本支持，如果升级4.0可以取消该注释，启用该功能
                dicHeadSetFun.Add("IfModifiedSince".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Referer = yourHeadValue));
                dicHeadSetFun.Add("Referer".ToUpper(), new SetHeadAttributeCallback((yourRequest, yourHeadValue) => yourRequest.Accept = yourHeadValue));
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

        public static class MyHttp
        {
            public static int httpTimeOut = 100000;                                            //http time out , HttpPostData will not use this value   （连接超时）
            public static int httpReadWriteTimeout = 300000;                                   //WebRequest.ReadWriteTimeout 该属性暂时未设置           （读取/写入超时）

            /// <summary>
            /// i will Send Data 
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <returns>back </returns>
            public static string SendData(string url, string data, string method)
            {
                return SendData(url, data, method, null);
            }

            /// <summary>
            /// i will Send Data (you can put Head in Request)
            /// </summary>
            /// <param name="url"> url </param>
            /// <param name="data"> param if method is not POST it will add to the url</param>
            /// <param name="method">GET/POST</param>
            /// <param name="heads">http Head list</param>
            /// <returns>back</returns>
            public static string SendData(string url, string data, string method, List<KeyValuePair<string,string>> heads)
            {
                string re = "";
                bool hasBody = !string.IsNullOrEmpty(data);
                bool needBody = method.ToUpper() == "POST" || method.ToUpper() == "PUT";

                try
                {
                    //except POST other data will add the url,if you want adjust the rules change here
                    if (!needBody && hasBody)
                    {
                        url += "?" + data;
                        data = null;           //make sure the data is null when Request is not post
                    }
                    WebRequest wr = WebRequest.Create(url);
                    wr.Timeout = httpTimeOut;
                    wr.Method = method;
                    wr.ContentType = "application/x-www-form-urlencoded";
                    
                    HttpHelper.AddHttpHeads((HttpWebRequest)wr, heads);

                    //wr.ContentType = "multipart/form-data";
                    char[] reserved = { '?', '=', '&' };
                    StringBuilder UrlEncoded = new StringBuilder();
                    byte[] SomeBytes = null;
                    if (needBody)
                    {
                        if (hasBody)
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


            public static string HttpPostData(string url, List<KeyValuePair<string, string>> heads, string bodyData, List<HttpMultipartDate> multipartDateList, string bodyParameter ,int timeOut,Encoding yourBodyEncoding)
            {
                string responseContent;
                Encoding httpBodyEncoding = Encoding.UTF8;
                string defaultMultipartContentType = "application/octet-stream";
                NameValueCollection stringDict = new NameValueCollection();
                if (yourBodyEncoding!=null)
                {
                    httpBodyEncoding = yourBodyEncoding;
                }

                //解析快捷Multipart表单形式post参数
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
                webRequest.Timeout = timeOut;

                //写如常规body
                if (bodyData!=null)
                {
                    var bodybytes = httpBodyEncoding.GetBytes(bodyData);
                    memStream.Write(bodybytes, 0, bodybytes.Length);
                }

                if (multipartDateList!=null)
                {
                    webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

                    foreach(HttpMultipartDate nowMultipart in multipartDateList)
                    {
                        //const string filePartHeader = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" + "Content-Type: {2}\r\n\r\n";
                        string nowPartHeader = "Content-Disposition: form-data";
                        if(nowMultipart.Name!=null)
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
                        byte[] nowHeaderbytes = httpBodyEncoding.GetBytes(nowPartHeader);
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
                            byte[] myCmd = httpBodyEncoding.GetBytes(nowMultipart.FileData);
                            memStream.Write(myCmd, 0, myCmd.Length);
                        }
                    }
                }
                 
                //快捷写入写入POST非文件参数
                if (bodyParameter != null)
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
                            return "can not send :" + ex.Message;
                        }
                    }
                }

                //写入最后的结束边界符
                memStream.Write(Encoding.ASCII.GetBytes("\r\n"), 0, Encoding.ASCII.GetBytes("\r\n").Length);
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

                    HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

                    using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),Encoding.GetEncoding("utf-8")))
                    {
                        responseContent = httpStreamReader.ReadToEnd();
                    }

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

                return responseContent;
            }

           

        }
    }
}
