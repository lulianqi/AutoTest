using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections;
namespace TestForProxy
{
    public class HttpProxy
    {
        int ProxyPort;
        /// <summary>
        /// 代理服务器入口类构造函数
        /// </summary>
        /// <param name="Port">Http Proxy监听的端口</param>
        public HttpProxy(int Port)
        {
            ProxyPort = Port;
        }
        /// <summary>
        /// 启动Http代理服务器
        /// </summary>
        public void Start()
        {
            TcpListener tcplistener = null;
            try
            {
                // 开始监听端口
                tcplistener = new TcpListener(Dns.GetHostAddresses(Dns.GetHostName())[0], ProxyPort);
                tcplistener.Start();
                Console.WriteLine("侦听端口号: " + ProxyPort.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("启动代理服务器失败: " + e.Message);
            }
            while (true)
            {
                try
                {
                    // 接受客户端连接
                    Socket socket = tcplistener.AcceptSocket();
                    HttpSession Session = new HttpSession(socket);
                    // 启动新线程，处理连接
                    Thread thread = new Thread(new ThreadStart(Session.Start));
                    thread.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine("接受客户端连接异常: " + e.Message);
                }
            }
        }
    }
    public class HttpSession
    {
        // 客户端socket
        Socket ClientSocket;
        // 设定编码
        Encoding ASCII = Encoding.ASCII;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="socket">客户端socket</param>
        public HttpSession(Socket socket)
        {
            this.ClientSocket = socket;
        }
        public void Start()
        {
            // 客户端缓冲区，读取客户端命令
            Byte[] ReadBuff = new byte[1024 * 10];
            try
            {
                int Length = ClientSocket.Receive(ReadBuff);
                // 没有读到数据
                if (0 == Length)
                {
                    Console.WriteLine("从客户端读取命令错误");
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                    return;
                }
            }
            // 读取出现异常
            catch (Exception e)
            {
                Console.WriteLine("读取客户端异常: " + e.Message);
            }
            // 来自客户端的HTTP请求字符串
            string ClientMsg = ASCII.GetString(ReadBuff);
            // 根据rnrn截取请求行
            string Line = ClientMsg.Substring(0, ClientMsg.IndexOf("rn"));
            string[] CmdArray = Line.Split(' ');
            // GET http://www.test.com:80/index.php HTTP/1.1
            // CONNECT www.test.com:443 HTTP/1.1
            string Cmd = CmdArray[0];
            string RawUrl = CmdArray[1];
            Console.WriteLine("原始请求: {0}", Line);
            // CONNECT请求
            if (Cmd == "CONNECT")
            {
                DoConnect(RawUrl);
            }
            // GET,POST和其他
            else
            {
                DoOther(RawUrl, ClientMsg);
            }
        }
        /// <summary>
        /// 处理CONNECT命令，此处作用是支持QQ，MSN，以及多级代理串联等
        /// </summary>
        /// <param name="RawUrl"></param>
        private void DoConnect(string RawUrl)
        {
            string[] Args = RawUrl.Split(':');
            string Host = Args[0];
            int Port = int.Parse(Args[1]);
            Socket ServerSocket = null;
            try
            {
                IPAddress[] IpList = Dns.GetHostEntry(Host).AddressList;
                Console.WriteLine("尝试连接{0}:{1}", IpList[0], Port);
                ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ServerSocket.Connect(IpList[0], Port);
            }
            catch (Exception e)
            {
                Console.WriteLine("连接真实服务器异常: " + e.Message);
            }
            // 连接真实服务器成功
            if (ServerSocket.Connected)
            {
                ClientSocket.Send(ASCII.GetBytes("HTTP/1.0 200 Connection establishedrnrn"));
            }
            else
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
            }
            // 开始转发数据
            ForwardTcpData(ClientSocket, ServerSocket);
        }
        /// <summary>
        /// 处理GET，POST等命令。使用了POLL，在代理服务器中强制去掉了Keep-Alive能力
        /// </summary>
        /// <param name="RawUrl"></param>
        /// <param name="ClientMsg"></param>
        public void DoOther(string RawUrl, string ClientMsg)
        {
            RawUrl = RawUrl.Substring(0 + "http://".Length);
            int Port;
            string Host;
            string Url;
            // 下面是分割处理请求，此处应该用正则匹配，不过我不擅长，因此手动切割，—_—!
            int index1 = RawUrl.IndexOf(':');
            // 没有端口
            if (index1 == -1)
            {
                Port = 80;
                int index2 = RawUrl.IndexOf('/');
                // 没有目录
                if (index2 == -1)
                {
                    Host = RawUrl;
                    Url = "/";
                }
                else
                {
                    Host = RawUrl.Substring(0, index2);
                    Url = RawUrl.Substring(index2);
                }
            }
            else
            {
                int index2 = RawUrl.IndexOf('/');
                // 没有目录
                if (index2 == -1)
                {
                    Host = RawUrl.Substring(0, index1);
                    Port = Int32.Parse(RawUrl.Substring(index1 + 1));
                    Url = "/";
                }
                else
                {
                    // /出现在:之前，则说明:后面的不是端口
                    if (index2 < index1)
                    {
                        Host = RawUrl.Substring(0, index2);
                        Port = 80;
                    }
                    else
                    {
                        Host = RawUrl.Substring(0, index1);
                        Port = Int32.Parse(RawUrl.Substring(index1 + 1, index2 - index1 - 1));
                    }
                    Url = RawUrl.Substring(index2);
                }
            }
            Console.WriteLine("Host is:{0}, Port is:{1}, Url is:{2}", Host, Port, Url);
            IPAddress[] address = null;
            try
            {
                IPHostEntry IPHost = Dns.GetHostEntry(Host);
                address = IPHost.AddressList;
                Console.WriteLine("Web服务器IP地址: " + address[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("解析服务器地址异常: " + e.Message);
            }
            Socket IPsocket = null;
            try
            {
                // 连接到真实WEB服务器
                IPEndPoint ipEndpoint = new IPEndPoint(address[0], Port);
                IPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPsocket.Connect(ipEndpoint);
                // 对WEB服务器端传送HTTP请求命令，将原始HTTP请求中HTTP PROXY部分包装去掉
                string ReqData = ClientMsg;
                // 改写头中的URL,从http://www.test.com/index.php改为/index.php
                ReqData = ReqData.Replace("http://" + RawUrl, Url);
                // 按照rn切分HTTP头
                string[] ReqArray = ReqData.Split(new string[1] { "rn" }, StringSplitOptions.None);
                ReqData = "";
                // 改写Keep-Alive等字段
                for (int index = 0; index < ReqArray.Length; index++)
                {
                    /*
                    if (ReqArray[index].StartsWith("Accept-Encoding:"))
                    {
                    ReqArray[index] = "Accept-Encoding: deflate";
                    }
                    */
                    if (ReqArray[index].StartsWith("Proxy-Connection:"))
                    {
                        ReqArray[index] = ReqArray[index].Replace("Proxy-Connection:", "Connection:");
                        //ReqArray[index] = "Connection: close";
                    }
                    /*
                    else if (ReqArray[index].StartsWith("Keep-Alive:"))
                    {
                    ReqArray[index] = "";
                    }
                    */
                    // 修改后的字段组合成请求
                    if (ReqArray[index] != "")
                    {
                        ReqData = ReqData + ReqArray[index] + "rn";
                    }
                }
                ReqData = ReqData.Trim();
                byte[] SendBuff = ASCII.GetBytes(ReqData);
                IPsocket.Send(SendBuff);
            }
            catch (Exception e)
            {
                Console.WriteLine("发送请求到服务器异常: " + e.Message);
            }
            // 使用Poll来判断完成，某些站点会出问题
            while (true)
            {
                Byte[] RecvBuff = new byte[1024 * 20];
                try
                {
                    if (!IPsocket.Poll(15 * 1000 * 1000, SelectMode.SelectRead))
                    {
                        Console.WriteLine("HTTP超时，关闭连接");
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Poll: " + e.Message);
                    break;
                }
                int Length = 0;
                try
                {
                    Length = IPsocket.Receive(RecvBuff);
                    if (0 == Length)
                    {
                        Console.WriteLine("服务端关闭");
                        break;
                    }
                    Console.WriteLine("从服务端收到{0}字节", Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Recv: " + e.Message);
                    break;
                }
                try
                {
                    Length = ClientSocket.Send(RecvBuff, Length, 0);
                    Console.WriteLine("发送{0}字节到客户端", Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Send: " + e.Message);
                }
            }
            /*
            // 根据接收字节数来判断完成，某些站点会出问题
            try
            {
            while (true)
            {
            Byte[] RecvBuff = new byte[1024 * 10];
            int Length = IPsocket.Receive(RecvBuff);
            if (Length <= 0)
            {
            Console.WriteLine("从服务端接收数据完成");
            break;
            }
            Console.WriteLine("从服务端收到{0}字节", Length);
            Length = ClientSocket.Send(RecvBuff, Length, 0);
            Console.WriteLine("发送{0}字节到客户端", Length);
            }
            }
            catch (Exception e)
            {
            Console.WriteLine(e.Message);
            }
            */
            try
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
                IPsocket.Shutdown(SocketShutdown.Both);
                IPsocket.Close();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 在客户端和服务器之间中转数据
        /// </summary>
        /// <param name="client">客户端socket</param>
        /// <param name="server">服务端socket</param>
        private void ForwardTcpData(Socket client, Socket server)
        {
            ArrayList ReadList = new ArrayList(2);
            while (true)
            {
                ReadList.Clear();
                ReadList.Add(client);
                ReadList.Add(server);
                try
                {
                    Socket.Select(ReadList, null, null, 1 * 1000 * 1000);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Select error: " + e.Message);
                    break;
                }
                // 超时
                if (ReadList.Count == 0)
                {
                    //Console.WriteLine("Time out");
                    continue;
                }
                // 客户端可读
                if (ReadList.Contains(client))
                {
                    byte[] Recv = new byte[1024 * 10];
                    int Length = 0;
                    try
                    {
                        Length = client.Receive(Recv, Recv.Length, 0);
                        if (Length == 0)
                        {
                            Console.WriteLine("Client is disconnect.");
                            break;
                        }
                        Console.WriteLine(" Recv {0} bytes from client", Length);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Read from client error: " + e.Message);
                        break;
                    }
                    try
                    {
                        Length = server.Send(Recv, Length, 0);
                        Console.WriteLine(" Write {0} bytes to server", Length);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Write data to server error: " + e.Message);
                        break;
                    }
                }
                // 真实服务端可读
                if (ReadList.Contains(server))
                {
                    byte[] Recv = new byte[1024 * 10];
                    int Length = 0;
                    try
                    {
                        Length = server.Receive(Recv, Recv.Length, 0);
                        if (Length == 0)
                        {
                            Console.WriteLine("Server is disconnect");
                            break;
                        }
                        Console.WriteLine("Recv {0} bytes from server", Length);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Read from server error: " + e.Message);
                        break;
                    }
                    try
                    {
                        Length = client.Send(Recv, Length, 0);
                        Console.WriteLine(" Write {0} bytes to client", Length);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Write data to client error: " + e.Message);
                        break;
                    }
                }
            }
            try
            {
                client.Shutdown(SocketShutdown.Both);
                server.Shutdown(SocketShutdown.Both);
                client.Close();
                server.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("转发完毕");
            }
        }
    }
    public class MainClass
    {
        public static void Main()
        {
            const int Port = 8080;
            HttpProxy Proxy = new HttpProxy(Port);
            Proxy.Start();
        }
    }
}