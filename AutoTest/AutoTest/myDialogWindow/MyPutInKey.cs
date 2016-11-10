using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*******************************************************************************
* Copyright (c) 2014,浙江风向标
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   20141209           创建人: 测试部 李杰 15158155511
* 描    述: 创建
*******************************************************************************/

namespace AutoTest.myDialogWindow
{
    public partial class MyPutInKey : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public MyPutInKey()
        {
            InitializeComponent();
        }

        MyVaneConfig myParentWindow;

        private void MyPutInKey_Load(object sender, EventArgs e)
        {
            myParentWindow = (MyVaneConfig)this.Owner;
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            myParentWindow._myGwKey = this.tb_key.Text;
            myParentWindow._isKeyNeed = true;
            this.Close();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            myParentWindow._isKeyNeed = false;
            this.Close();
        }
    }
}
