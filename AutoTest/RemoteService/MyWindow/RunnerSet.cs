using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


/*******************************************************************************
* Copyright (c) 2015 lijie
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201505016           创建人: 李杰 15158155511
* 描    述: 创建
*******************************************************************************/


namespace RemoteService.MyWindow
{
    public partial class RunnerSet : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public RunnerSet(CaseRunner yourRunner)
        {
            InitializeComponent();
            nowRunner = yourRunner;
        }

        CaseRunner nowRunner;

        BindingSource bsPCell = new BindingSource();
        BindingSource bsCCell = new BindingSource();

        private void RunnerSet_Load(object sender, EventArgs e)
        {
            if(nowRunner!=null)
            {
                tb_runnerName.Text = nowRunner.RunnerName;
                tb_waitTime.Text = nowRunner.RunerActuator.ExecutiveThinkTime.ToString();

                bsPCell.DataSource = nowRunner.RunerActuator.RunTimeCaseDictionary;
                cb_pList.DataSource = bsPCell;
                cb_pList.DisplayMember = "Key";
                cb_pList.ValueMember = "Value";

                bsCCell.DataSource = cb_pList.SelectedValue;
                cb_cList.DataSource = bsCCell;
                cb_cList.DisplayMember = "Key";
                cb_cList.ValueMember = "Value";

                foreach (var tempKpv in nowRunner.RunerActuator.RunTimeCaseDictionary)
                {
                    if(tempKpv.Value.ContainsValue(nowRunner.StartCell))
                    {
                        cb_pList.SelectedValue = tempKpv.Value;
                        break;
                    }
                }

                cb_cList.SelectedValue = nowRunner.StartCell;
            }
            else
            {
                lb_sw_ok.Enabled = false;
            }
        }

        //Num only press
        private void tb_waitTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            //'\b' is backspace
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Select change
        private void cb_pList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bsCCell.DataSource = cb_pList.SelectedValue;
            cb_cList.DataSource = bsCCell;
            cb_cList.DisplayMember = "Key";
            cb_cList.ValueMember = "Value";
        }

        private void lb_sw_ok_Click(object sender, EventArgs e)
        {
            try
            {
                nowRunner.RunerActuator.ExecutiveThinkTime = int.Parse(tb_waitTime.Text);
            }
            catch
            {
                nowRunner.RunerActuator.ExecutiveThinkTime = 0;
                MessageBox.Show("WaitTime Set Error");
            }
            nowRunner.RunnerName = tb_runnerName.Text;
            nowRunner.StartCell = (CaseExecutiveActuator.Cell.CaseCell)cb_cList.SelectedValue;
            nowRunner.tagItem.SubItems[0].Text = nowRunner.RunnerName;
            this.Close();
        }

        private void bt_sw_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
