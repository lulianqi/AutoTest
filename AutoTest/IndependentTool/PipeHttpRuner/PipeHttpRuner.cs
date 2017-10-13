﻿using MyPipeHttpHelper;
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
            MyInitializeComponent();
        }

        private List<PipeHttp> pipeList;

        private void MyInitializeComponent()
        {
            cb_responseType.SelectedIndex = 0;
            PipeHttp.GlobalRawRequest.CreateRawData(Encoding.UTF8, tb_rawRequest.Text);
            tb_pileHost_TextChanged(null, null);
            tb_pilePort_TextChanged(null, null);

        }

        private void ReportMyMessage(string mes)
        {
            rtb_dataRecieve.AddDate(mes, Color.Bisque, true);
        }

        void ph_OnPipeStateReport(string mes, int id)
        {
            rtb_dataRecieve.AddDate(string.Format("ID:[{0}] : {1}",id , mes), Color.Indigo, true);
            //System.Diagnostics.Debug.WriteLine("-------------------------------------");
            //System.Diagnostics.Debug.WriteLine(string.Format("ID:{0} [{1}]", id, mes));
            //System.Diagnostics.Debug.WriteLine("-------------------------------------");
        }

        void ph_OnPipeResponseReport(byte[] response, int id)
        {
            string resposeStr = Encoding.UTF8.GetString(response);
            System.Diagnostics.Debug.Write(resposeStr);
            
        }

        private void AddPipeList(PipeHttp ph)
        {
            pipeList.Add(ph);
            ListViewItem lvt = new ListViewItem(new string[] { ph.Id.ToString(), ph.GetreReconectCount.ToString() });
            lvt.Tag = ph;
            lv_pipeList.Items.Add(lvt);
        }

        private void ClearPipeList()
        {
            foreach(PipeHttp tempPh in pipeList)
            {
                tempPh.Dispose();
            }
            foreach(ListViewItem tempLvt in lv_pipeList.Items)
            {
                tempLvt.Tag = null;
            }
            lv_pipeList.Items.Clear();
            pipeList.Clear();
        }

        private void PipeHttpRuner_Load(object sender, EventArgs e)
        {
            pipeList = new List<PipeHttp>();

            return;
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
            ph.Send(100);
        }




        //添加管道
        private void bt_addPile_Click(object sender, EventArgs e)
        {
            int reConnectTime = 0;
            int pileAddCount = 0;
            if(int.TryParse(tb_reConTime.Text,out reConnectTime)&&int.TryParse(tb_addTime.Text,out pileAddCount))
            {
                if(reConnectTime<0)
                {
                    reConnectTime = 0;
                    tb_reConTime.Text = "0";
                    ReportMyMessage("ReConTime can not less than 0 ,so we set it 0");
                }
                if (pileAddCount < 1)
                {
                    pileAddCount = 1;
                    tb_addTime.Text = "1";
                    ReportMyMessage("PileAddCount can not less than 1 ,so we set it 1");
                }
                for(int i=0;i<pileAddCount;i++)
                {
                    PipeHttp tempPipeHttp = new PipeHttp(reConnectTime, cb_responseType.SelectedIndex == 0);
                    tempPipeHttp.OnPipeStateReport += ph_OnPipeStateReport;
                    tempPipeHttp.OnPipeResponseReport += ph_OnPipeResponseReport;
                    tempPipeHttp.pipeRequest = PipeHttp.GlobalRawRequest;
                    AddPipeList(tempPipeHttp);
                }
            }
            else
            {
                MessageBox.Show("illegal reConTime or addTime text");
            }
        }

        private void bt_connectAllPile_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem tempLvt in lv_pipeList.Items)
            {
                PipeHttp tempPh = (PipeHttp)tempLvt.Tag;
                //tempPh.IsReportResponse = cb_isRecieve.Checked;
                if(tempPh.Connect())
                {
                    tempLvt.BackColor = Color.LightGreen;
                    lv_pipeList.Update();
                }
                else
                {
                    ReportMyMessage(string.Format("ID:[{0}] connect fail", tempPh.Id.ToString()));
                }
            }
        }

        private void bt_sendRequest_Click(object sender, EventArgs e)
        {
            int sendCount = 1;
            if(int.TryParse(tb_RequstCount.Text,out sendCount))
            {
                foreach(PipeHttp tempPh in pipeList)
                {
                    if (cb_isAsynSend.Checked)
                    {
                        ReportMyMessage(string.Format("ID:[{0}] AsynSend  [{1}]", tempPh.Id.ToString(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff")));
                        tempPh.AsynSend(sendCount, 1, 0);
                    }
                    else
                    {
                        ReportMyMessage(string.Format("ID:[{0}] send start [{1}]", tempPh.Id.ToString(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff")));
                        tempPh.Send(sendCount);
                        ReportMyMessage(string.Format("ID:[{0}] send complete [{1}]", tempPh.Id.ToString(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff")));
                    }
                }
            }
            else
            {
                MessageBox.Show("illegal RequstCount text");
            }
        }

        //设置全局请求数据
        private void tb_rawRequest_Leave(object sender, EventArgs e)
        {
            PipeHttp.GlobalRawRequest.CreateRawData(Encoding.UTF8, tb_rawRequest.Text);
        }

        private void tb_pileHost_TextChanged(object sender, EventArgs e)
        {
            PipeHttp.GlobalRawRequest.ConnectHost = tb_pileHost.Text;
        }

        private void tb_pilePort_TextChanged(object sender, EventArgs e)
        {
            int tempCounectPort=80;
            if (int.TryParse(tb_pilePort.Text, out tempCounectPort))
            {
                if(tempCounectPort>65532)
                {
                    tempCounectPort = 80;
                    tb_pilePort.Text = "80";
                }
            }
            else
            {
                tb_pilePort.Text = "80";
            }
            PipeHttp.GlobalRawRequest.ConnectPort = tempCounectPort;
        }

        
       

       

       

    }
}
