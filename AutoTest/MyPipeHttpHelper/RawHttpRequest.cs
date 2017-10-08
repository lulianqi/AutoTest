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
        private string host = "";
        private int hostPort = 80;
        //请求行
        private string startLine = "";
        //请求头
        private List<string> headers = new List<string>();
        //请求实体
        private string entityBody = "";
        //原始数据
        private byte[] rawRequest = null;

        public string Host
        {
            get { return host; }
            set { if (value != null) { host = value; } }
        }

        public int HostPort
        {
            get { return hostPort; }
            set { if (value != null) { hostPort = value; } }
        }

        public string StartLine
        {
            get { return startLine; }
            set { if (value != null) { startLine = value; } }
        }

        public List<string> Headers
        {
            get { return headers; }
            set { if (value != null) { headers = value; } }
        }

        public byte[] RawRequest
        {
            get { return rawRequest; }
            set { rawRequest = value; }
        }

        public RawHttpRequest()
        {

        }

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

        public void CreateRawData()
        {
            CreateRawData(Encoding.UTF8);
        }
    }
}
