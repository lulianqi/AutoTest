﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MyCommonHelper.NetHelper;

namespace ShanxiHuala_Interface
{
    public partial class ShanxiHuala_Interface : Form
    {
        public ShanxiHuala_Interface()
        {
            InitializeComponent();
        }

        private String host = "http://192.168.200.142:8091";

        private String app_secret = "secret";

        private String client_id = "b2c";

        private String client_secret = "secret";

        private String id = "oauth2-resource";

        private String bearer = "";

        private void ShanxiHuala_Interface_Load(object sender, EventArgs e)
        {
            tb_host.Text = host;
            cb_httpMethod.SelectedIndex = 1;
        }

        private void bt_oauth_Click(object sender, EventArgs e)
        {
            string response = MyWebTool.MyHttp.SendData(string.Format("{0}/oauth/token", host), "grant_type=client_credentials&scope=trust+read+write", "POST",
                new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("Authorization", string.Format(" Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes(client_id + ":" + client_secret)))) });
            string nowToken= GetAccess_token(response);
            if(nowToken==null)
            {
                tb_access_token.Text = "";
                MessageBox.Show(response);
            }
            else
            {
                tb_access_token.Text = (nowToken);
            }
        }

        private void bt_send_Click(object sender, EventArgs e)
        {
            tb_sendTime.Text = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
            string sginOrginStr = string.Format("appSecret={0}&sendTime={1}", app_secret, tb_sendTime.Text);
            List<KeyValuePair<string, string>> myHeads = new List<KeyValuePair<string, string>>();
            myHeads.Add(new KeyValuePair<string, string>("Content-type", "application/json;charset=UTF-8"));
            myHeads.Add(new KeyValuePair<string, string>("Authorization", "bearer "+tb_access_token.Text));
            myHeads.Add(new KeyValuePair<string, string>("sign", MyCommonHelper.MyEncryption.CreateMD5Key(sginOrginStr).ToLower()));
            myHeads.Add(new KeyValuePair<string, string>("sendTime",tb_sendTime.Text));
            myHeads.Add(new KeyValuePair<string, string>("User-Agent", "Tester"));

            string response = MyWebTool.MyHttp.SendData(string.Format("{0}{1}", tb_host.Text, tb_url.Text), rtb_sendBody.Text, cb_httpMethod.Text, myHeads);

            rtb_response.Text = response;
        }

        #region function
        private string GetAccess_token(string souceData)
        {
            string outStr = null;
            if (souceData!=null)
            {
                if(souceData.Contains("access_token"))
                {
                    int startIndex = souceData.IndexOf("access_token", 0)+15;
                    int endIndex = souceData.IndexOf("\"", startIndex);
                    outStr = souceData.Substring(startIndex, (endIndex - startIndex));
                }
            }
            return outStr;
        }
        #endregion

       
    }
}
