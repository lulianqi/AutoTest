using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

/*******************************************************************************
* Copyright (c) 2013,杭州信雅达科技有限公司
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   20130603           创建人: 测试部 李杰 15158155511
* 描    述: 创建
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