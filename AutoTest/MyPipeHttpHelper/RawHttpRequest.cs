using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPipeHttpHelper
{
    public class RawHttpRequest
    {
        //请求host建立连接时需要使用该地址进行握手
        private string connectHost = "";
        private int connectPort = 80;
        //请求行
        private string startLine = "";
        //请求头
        private List<string> headers = new List<string>();
        //请求实体
        private string entityBody = "";
        //原始数据
        private byte[] rawRequest = null;

        /// <summary>
        /// get or set Connect Host(for the tcp link)
        /// </summary>
        public string ConnectHost
        {
            get { return connectHost; }
            set { if (value != null) { connectHost = value; } }
        }

        /// <summary>
        /// get or set Connect Port(for the tcp link)
        /// </summary>
        public int ConnectPort
        {
            get { return connectPort; }
            set { if (value != null) { connectPort = value; } }
        }

        /// <summary>
        /// get or set http startline
        /// </summary>
        public string StartLine
        {
            get { return startLine; }
            set { if (value != null) { startLine = value; } }
        }

        /// <summary>
        /// get or set http heads
        /// </summary>
        public List<string> Headers
        {
            get { return headers; }
            set { if (value != null) { headers = value; } }
        }

        /// <summary>
        /// get or set http entity body
        /// </summary>
        public string EntityBody
        {
            get { return entityBody; }
            set { if (value != null) { entityBody = value; } }
        }

        /// <summary>
        /// get the rawRequest (now rawRequest)
        /// </summary>
        public byte[] RawRequest
        {
            get { return rawRequest; }
        }

        public RawHttpRequest()
        {

        }

        /// <summary>
        /// get the rawRequest by your encoding  (now rawRequest)
        /// </summary>
        /// <param name="yourEncoding">encoding</param>
        /// <returns>rawRequest</returns>
        public string GetRequestText(Encoding yourEncoding)
        {
            return yourEncoding.GetString(rawRequest);
        }

        /// <summary>
        /// get the rawRequest by utf 8  (now rawRequest)
        /// </summary>
        /// <returns>rawRequest</returns>
        public string GetRequestText()
        {
            return GetRequestText(Encoding.UTF8);
        }

        /// <summary>
        /// Create RawData wtih you data that set by StartLine/Headers/EntityBody
        /// </summary>
        /// <param name="yourEncoding">your encoding</param>
        public void CreateRawData(Encoding yourEncoding)
        {
            StringBuilder requestSb = new StringBuilder();
            requestSb.AppendLine(startLine);
            foreach (string tempHeader in headers)
            {
                requestSb.AppendLine(tempHeader);
            }
            requestSb.AppendLine();
            if (!string.IsNullOrEmpty(entityBody))
            {
                requestSb.AppendLine(entityBody);
                requestSb.AppendLine();
            }
            rawRequest = yourEncoding.GetBytes(requestSb.ToString());
        }

        /// <summary>
        /// Create RawData wtih you data that set by StartLine/Headers/EntityBody (utf 8)
        /// </summary>
        public void CreateRawData()
        {
            CreateRawData(Encoding.UTF8);
        }

        public void CreateRawData(byte[] yourRawRequest)
        {
            rawRequest = yourRawRequest;
        }

        /// <summary>
        /// Create RawData wtih you data that set by yourRawRequest
        /// </summary>
        public void CreateRawData(Encoding yourEncoding, string yourRawRequest)
        {
            if (yourRawRequest != null)
            {
                rawRequest = yourEncoding.GetBytes(yourRawRequest);
            }
        }
    }
}
