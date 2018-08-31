using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoTest.MyTool;

namespace AutoTest.myDialogWindow
{
    public partial class MyCBalloonResultInfo : CBalloon.CBalloonBase
    {
        public MyCBalloonResultInfo(ListViewItem yourItem)
        {
            InitializeComponent();
            showItem = yourItem;
        }

        private ListViewItem showItem;

        private void MyCBalloonResultInfo_Load(object sender, EventArgs e)
        {
            lb_index.Text =  showItem.SubItems[0].Text;
            lb_caseId.Text = "CaseID:" + showItem.SubItems[1].Text;
            lb_remark.Text = showItem.SubItems[6].Text; 
            lb_ret.Text = "返回结果:" + showItem.SubItems[4].Text; 
            lb_spanTime.Text = "执行耗时:" + showItem.SubItems[3].Text; 
            lb_startTime.Text = "开始时间:" + showItem.SubItems[2].Text;
            rtb_testResult.Text = showItem.SubItems[5].Text; 
        }

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            this.Owner.Activate();
            this.Close();
        }
    }
}
