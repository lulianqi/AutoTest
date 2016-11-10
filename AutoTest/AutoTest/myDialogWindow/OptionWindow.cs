using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using AutoTest.myTool;
using MyCommonTool;

/*******************************************************************************
* Copyright (c) 2013,浙江风向标
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   20131231           创建人: 测试部 李杰 15158155511
* 描    述: 创建
*******************************************************************************/

namespace AutoTest.myDialogWindow
{
    public partial class OptionWindow : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public OptionWindow()
        {
            InitializeComponent();
        }

        AutoRunner myOwner;

        private void EditCasebody_Load(object sender, EventArgs e)
        {
            myOwner = (AutoRunner)this.Owner;
            //tb_ow_waittime.Text = (myOwner.SleepTime).ToString();
            if (myOwner.nowCaseActionActuator != null)
            {
                tb_ow_waittime.Text = (myOwner.nowCaseActionActuator.ExecutiveThinkTime).ToString();
            }
            else
            {
                tb_ow_waittime.Text = "未加载CASE";
                tb_ow_waittime.Enabled = false;
            }
            tb_ow_maxline.Text = myOwner.MaxLine.ToString();
            bt_ow_other.Text = "can not set";
            try
            {
                cb_ow_postDes.SelectedIndex = myOwner.postDataDes;
            }
            catch (Exception ex)
            {
                cb_ow_postDes.SelectedIndex = 0;
                ErrorLog.PutInLogEx(ex.Message);
            }
        }

        private void bt_sw_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lb_sw_ok_Click(object sender, EventArgs e)
        {
            try
            {
                myOwner.sleepTime = int.Parse(tb_ow_waittime.Text);
                if (myOwner.nowCaseActionActuator != null)
                {
                    myOwner.nowCaseActionActuator.ExecutiveThinkTime = int.Parse(tb_ow_maxline.Text);
                }
                myOwner.postDataDes = cb_ow_postDes.SelectedIndex;
                myini.IniWriteValue("vane", "sleeptime", tb_ow_waittime.Text, System.Environment.CurrentDirectory + "\\seting\\seting.ini");
                myini.IniWriteValue("vane", "maxline", tb_ow_maxline.Text, System.Environment.CurrentDirectory + "\\seting\\seting.ini");
                myini.IniWriteValue("postsetting", "body",cb_ow_postDes.SelectedIndex.ToString() , System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            this.Close();
        }


    }
}