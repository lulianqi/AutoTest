using SelWCFClient.DuaServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SelWCFClient
{
    class MyClass
    {
    }
    public class CallBackHandler : IDuplexServiceCallback
    {
        public CallBackHandler(RichTextBox rtb_info)
        {
            richTextBox_info = rtb_info;
        }
        RichTextBox richTextBox_info;
        private void AddInfo(string info)
        {
            richTextBox_info.AppendText(info + "\r\n");
        }

        public void ReportTime(string time)
        {
            AddInfo("IDuplexServiceCallback> " + time);
        }

        public void ReportState(string info)
        {
            AddInfo("IDuplexServiceCallback> " + info);
        }
    }
}
