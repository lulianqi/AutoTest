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
* Copyright (c) 2013,�㽭�����
* All rights reserved.
* 
* �ļ�����: 
* ����ժҪ: mycllq@hotmail.com
* 
* ��ʷ��¼:
* ��	  ��:   20131231           ������: ���Բ� ��� 15158155511
* ��    ��: ����
*******************************************************************************/

namespace AutoTest.myDialogWindow
{
    public partial class SetingWindow : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public SetingWindow()
        {
            InitializeComponent();
        }

        AutoRunner myOwner;

        private void EditCasebody_Load(object sender, EventArgs e)
        {
            myOwner = (AutoRunner)this.Owner;
            tb_sw_url.Text = myReceiveData.vaneUrl;
            tb_sw_key.Text = myReceiveData.vaneApp_key;
            bt_sw_secret.Text = myReceiveData.vaneApp_secret;
        }

        private void bt_sw_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lb_sw_ok_Click(object sender, EventArgs e)
        {
            myReceiveData.vaneUrl = tb_sw_url.Text;
            myReceiveData.vaneApp_key = tb_sw_key.Text;
            myReceiveData.vaneApp_secret = bt_sw_secret.Text;

            myini.IniWriteValue("vaneinterface", "vaneUrl", myReceiveData.vaneUrl, System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            myini.IniWriteValue("vaneinterface", "app_key", myReceiveData.vaneApp_key, System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            myini.IniWriteValue("vaneinterface", "app_secret", myReceiveData.vaneApp_secret, System.Environment.CurrentDirectory + "\\seting\\seting.ini");

            this.Close();
        }


    }
}