using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MyCommonHelper.NetHelper;
using System.Xml;
using System.Diagnostics;

namespace ShanxiHuala_Interface
{
   
    public partial class ShanxiHuala_Interface : Form
    {
        public ShanxiHuala_Interface()
        {
            InitializeComponent();
        }

        private string tipInfoPath = System.Environment.CurrentDirectory + "\\interfaceData.xml";

        private String host = "http://192.168.200.142:8091";

        private String app_secret = "secret";

        private String client_id = "b2c";

        private String client_secret = "secret";

        private String id = "oauth2-resource";

        private String bearer = "";


        private XmlDocument myTip = new XmlDocument();

        private XmlNode nowNode;

        private Point initialSendLocation;

        private void ShanxiHuala_Interface_Load(object sender, EventArgs e)
        {
            tb_host.Text = host;
            cb_httpMethod.SelectedIndex = 1;
            ck_isSgin.Checked = true;
            initialSendLocation = bt_send.Location;
            LoadApiList();
            

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
            List<KeyValuePair<string, string>> myHeads = new List<KeyValuePair<string, string>>();
            if(ck_isSgin.Checked)
            {
                tb_sendTime.Text = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000).ToString();
                string sginOrginStr = string.Format("appSecret={0}&sendTime={1}", app_secret, tb_sendTime.Text);
                tb_sign.Text = MyCommonHelper.MyEncryption.CreateMD5Key(sginOrginStr).ToLower();

                myHeads.Add(new KeyValuePair<string, string>("Content-type", "application/json;charset=UTF-8"));
                myHeads.Add(new KeyValuePair<string, string>("Authorization", "bearer " + tb_access_token.Text));
                myHeads.Add(new KeyValuePair<string, string>("sign", tb_sign.Text));
                myHeads.Add(new KeyValuePair<string, string>("sendTime", tb_sendTime.Text));
                myHeads.Add(new KeyValuePair<string, string>("User-Agent", "Tester"));
            }
            
            //<Api name="api">rtb_sendBody.Text.Replace("    "," ")</Api>
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string response = MyWebTool.MyHttp.SendData(string.Format("{0}{1}", tb_host.Text, tb_url.Text), rtb_sendBody.Text, cb_httpMethod.Text, myHeads);
            if (nowNode!=null)
            {
                nowNode.InnerText = rtb_sendBody.Text;
            }
            sw.Stop();
            rtb_response.AddDate(string.Format("耗时：{0} ms", sw.ElapsedMilliseconds), Color.BlueViolet, true);
            rtb_response.AddDate(response, (response.StartsWith("Error:")) ? Color.Red : Color.Black, true);
        }

        private void tb_url_TextChanged(object sender, EventArgs e)
        {
            //XmlNodeList tempTipNodes = myTip.ChildNodes[1].SelectNodes(string.Format(".{0}", tb_url.Text));
            XmlNode tempTipNode=FindApiTipNode(tb_url.Text);
            if(tempTipNode!=null)
            {
                rtb_sendBody.Text = tempTipNode.InnerText;
                if (tempTipNode.Attributes["method"] != null)
                {
                    cb_httpMethod.SelectedIndex = (tempTipNode.Attributes["method"].Value == "GET" ? 0 : 1);
                }
                nowNode = tempTipNode;
            }
            else
            {
                nowNode = null;
            }
        }

        private void listView_InterfaceList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView_InterfaceList.SelectedItems.Count != 0)
            {
                if (tb_url.Text != listView_InterfaceList.SelectedItems[0].Text)
                {
                    tb_url.Text = listView_InterfaceList.SelectedItems[0].Text;
                }
            }
        }


        private void rtb_response_OnShowInNewWindowChange(object sender, MyCommonControl.DataRecordBox.ShowInNewWindowEventArgs e)
        {
            if(e.isShow)
            {
                bt_send.Location = new Point(bt_send.Location.X, this.Height - 70);
                rtb_sendBody.Height = this.Height - 143;
            }
            else
            {
                bt_send.Location = initialSendLocation;
                rtb_sendBody.Height = this.Height - 343;
            }
        }

        private void ShanxiHuala_Interface_FormClosing(object sender, FormClosingEventArgs e)
        {
            myTip.Save(tipInfoPath);
        }



        #region function
        private void LoadApiList()
        {
            myTip.Load(tipInfoPath);
            foreach(XmlNode tempNode in myTip.ChildNodes[1])
            {
                ListViewItem tempItem = new ListViewItem(tempNode.Attributes["name"].Value);
                listView_InterfaceList.Items.Add(tempItem);
            }
        }

        private XmlNode FindApiTipNode(string apiName)
        {
            foreach (XmlNode tempTipNode in myTip.ChildNodes[1])
            {
                if (tempTipNode.Attributes["name"].Value == apiName)
                {
                    return tempTipNode;
                }
            }
            return null;
        }

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
