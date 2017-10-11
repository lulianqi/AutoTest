using MyPipeHttpHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PipeHttpRuner
{
    public partial class PipeHttpRuner : Form
    {
        public PipeHttpRuner()
        {
            InitializeComponent();
        }

        private void PipeHttpRuner_Load(object sender, EventArgs e)
        {
            PipeHttp.GlobalRawRequest.ConnectHost = "www.baidu.com";
            PipeHttp.GlobalRawRequest.StartLine = "GET http://www.baidu.com/ HTTP/1.1";
            PipeHttp.GlobalRawRequest.Headers.Add("Content-Type: application/x-www-form-urlencoded");
            PipeHttp.GlobalRawRequest.Headers.Add(string.Format("Host: {0}", PipeHttp.GlobalRawRequest.ConnectHost));
            PipeHttp.GlobalRawRequest.Headers.Add("Connection: Keep-Alive");
            PipeHttp.GlobalRawRequest.CreateRawData();
            PipeHttp ph = new PipeHttp(100, true);
            ph.pipeRequest = PipeHttp.GlobalRawRequest;
            ph.OnPipeResponseReport += ph_OnPipeResponseReport;
            ph.OnPipeStateReport += ph_OnPipeStateReport;
            ph.Connect();
            ph.Send(1000);
        }

        void ph_OnPipeStateReport(string mes, int id)
        {
            System.Diagnostics.Debug.WriteLine("-------------------------------------");
            System.Diagnostics.Debug.WriteLine(string.Format("ID:{0} [{1}]", id, mes));
            System.Diagnostics.Debug.WriteLine("-------------------------------------");
        }

        void ph_OnPipeResponseReport(byte[] response, int id)
        {
            string resposeStr = Encoding.UTF8.GetString(response);
            System.Diagnostics.Debug.Write(resposeStr);
        }
    }
}
