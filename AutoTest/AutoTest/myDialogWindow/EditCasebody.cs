using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using AutoTest.myTool;
using CaseExecutiveActuator;
using CaseExecutiveActuator.Cell;

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
    public partial class EditCasebody : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public EditCasebody(TreeNode yourTreeNode)
        {
            InitializeComponent();
            myTreeNode = yourTreeNode;
        }

        AutoRunner myOwner;
        TreeNode myTreeNode;

        private void EditCasebody_Load(object sender, EventArgs e)
        {
            myOwner = (AutoRunner)this.Owner;
            rtb_dw2_CaseBody.Text = ((CaseCell)myTreeNode.Tag).CaseXmlNode.InnerXml;
            tb_dw2_Id.Text = ((CaseCell)myTreeNode.Tag).CaseRunData.id.ToString();
            tb_dw2_Target.Text = ((CaseCell)myTreeNode.Tag).CaseRunData.contentProtocol.ToString();
            return;
        }

        private void bt_dw2_ok_Click(object sender, EventArgs e)
        {
            try
            {
                ((CaseCell)myTreeNode.Tag).CaseXmlNode.InnerXml = rtb_dw2_CaseBody.Text;
                ((CaseCell)myTreeNode.Tag).CaseXmlNode.Attributes[0].Value = tb_dw2_Id.Text;
                ((CaseCell)myTreeNode.Tag).CaseXmlNode.Attributes[1].Value = tb_dw2_Target.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "STOP");
                return;
            }
            //myOwner.myCase.mySave();
            this.Close();
        }

        private void bt_dw2_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_dw2_Step_KeyPress(object sender, KeyPressEventArgs e)
        {
            //'\b' is backspace
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


    }
}