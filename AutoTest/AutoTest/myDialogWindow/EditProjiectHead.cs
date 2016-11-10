using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

/*******************************************************************************
* Copyright (c) 2013,�������Ŵ�Ƽ����޹�˾
* All rights reserved.
* 
* �ļ�����: 
* ����ժҪ: mycllq@hotmail.com
* 
* ��ʷ��¼:
* ��	  ��:   20130603           ������: ���Բ� ��� 15158155511
* ��    ��: ����
*******************************************************************************/

namespace AutoTest.myDialogWindow
{
    public partial class EditProjiectHead : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public EditProjiectHead()
        {
            InitializeComponent();
        }

        AutoRunner myOwner;

        private void EditProjiectHead_Load(object sender, EventArgs e)
        {
            myOwner = (AutoRunner)this.Owner;
            tb_dw1_ProjectName.Text = myOwner.showNode.Attributes[0].Value;
            rtb_dw1_ProjectRemark.Text = myOwner.showNode.Attributes[1].Value;
        }

        private void lb_dw1_ok_Click(object sender, EventArgs e)
        {
            myOwner.showNode.Attributes[0].Value=tb_dw1_ProjectName.Text;
            myOwner.showNode.Attributes[1].Value =rtb_dw1_ProjectRemark.Text;
            myOwner.myCase.mySave();
            this.Close();
        }

        private void bt_dw1_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}