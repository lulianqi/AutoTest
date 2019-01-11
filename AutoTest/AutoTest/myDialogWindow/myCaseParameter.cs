using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutoTest.MyTool;
using MyCommonHelper;
using MyCommonControl;
using MyCommonControl;


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
    public partial class myCaseParameter : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public enum ShowRunTimeParameterType
        {
            KeyValue,
            Parameter,
            DataSouce
        }
        public myCaseParameter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (myParentWindow.nowCaseActionActuator != null)
            {
                myParentWindow.nowCaseActionActuator.OnActuatorParameterListChanged -= nowCaseActionActuator_OnActuatorParameterListChanged;
            }
            myUpdataTime.Stop();
            myUpdataTime = null;
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        bool isCaceParameter = true;
        ShowRunTimeParameterType nowShowType = ShowRunTimeParameterType.KeyValue;
        AutoRunner myParentWindow;
        Timer myUpdataTime = new Timer();
       
        private void myCaseParameter_Load(object sender, EventArgs e)
        {
            this.TopMost = false;
            myParentWindow = (AutoRunner)this.Owner;
            pictureBox_next.Visible = pictureBox_set.Visible = false;
            if (myParentWindow.nowCaseActionActuator!=null)
            {
                myParentWindow.nowCaseActionActuator.OnActuatorParameterListChanged += nowCaseActionActuator_OnActuatorParameterListChanged;
                updatalistView_CaseParameter();
                myUpdataTime.Interval = 20000;
                myUpdataTime.Tick += new EventHandler(myUpdataTime_Tick);
                myUpdataTime.Start();
            }
            else
            {
                MessageBox.Show("未发现执行数据源");
                this.Close();
            }
        }

        void nowCaseActionActuator_OnActuatorParameterListChanged()
        {
            if (listView_CaseParameter.InvokeRequired)
            {
                listView_CaseParameter.BeginInvoke(new Action(updatalistView_CaseParameter));
            }
            else
            {
                updatalistView_CaseParameter();
            }
            
        }

        void myUpdataTime_Tick(object sender, EventArgs e)
        {
            if(myParentWindow.nowCaseActionActuator!=null)
            {
                if (myParentWindow.nowCaseActionActuator.OnActuatorParameterListChanged == null)
                {
                    myParentWindow.nowCaseActionActuator.OnActuatorParameterListChanged += nowCaseActionActuator_OnActuatorParameterListChanged;
                }
            }
            updatalistView_CaseParameter();
        }

        private void lb_info_runTimeParameter_Click(object sender, EventArgs e)
        {
            ShowRunTimeParameterType hereType;
            if (Enum.TryParse<ShowRunTimeParameterType>(((Label)sender).Text, out hereType))
            {
                ShowInfoChange(hereType);
            }
        }


        public void updatalistView_CaseParameter()
        {
            MyControlHelper.SetControlFreeze(listView_CaseParameter);
            listView_CaseParameter.BeginUpdate();
            listView_CaseParameter.Items.Clear();
            try
            {
                switch (nowShowType)
                {
                    case ShowRunTimeParameterType.KeyValue:
                        foreach (KeyValuePair<string, string> tempKvp in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataKeyList)
                        {
                            listView_CaseParameter.Items.Add(new ListViewItem(new string[] { tempKvp.Key, tempKvp.Value }));
                        }
                        break;
                    case ShowRunTimeParameterType.Parameter:
                        foreach (KeyValuePair<string, CaseExecutiveActuator.IRunTimeStaticData> tempKvp in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataParameterList)
                        {
                            listView_CaseParameter.Items.Add(new ListViewItem(new string[] { tempKvp.Key, tempKvp.Value.DataCurrent() }));
                        }
                        break;
                    case ShowRunTimeParameterType.DataSouce:
                        foreach (KeyValuePair<string, CaseExecutiveActuator.IRunTimeDataSource> tempKvp  in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataSouceList)
                        {
                            listView_CaseParameter.Items.Add(new ListViewItem(new string[] { tempKvp.Key, tempKvp.Value.DataCurrent() }));
                        }
                        break;
                    default:
                        //not this way
                        break;
                }
            }
            catch
            {
                //RunActuatorStaticDataCollection可能在执行线程中被修改
            }
            listView_CaseParameter.EndUpdate();
            MyControlHelper.SetControlUnfreeze(listView_CaseParameter);
        }


        private void listView_CaseParameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_CaseParameter.SelectedItems.Count>0)
            {
                tb_keyAdd.Text = listView_CaseParameter.SelectedItems[0].SubItems[0].Text;
                tb_valueAdd.Text = listView_CaseParameter.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void ShowInfoChange(ShowRunTimeParameterType showParameter)
        {
            switch (showParameter)
            {
                case ShowRunTimeParameterType.KeyValue:
                    nowShowType = ShowRunTimeParameterType.KeyValue;
                    lb_info_keyValue.ForeColor = Color.SaddleBrown;
                    lb_info_parameter.ForeColor = lb_info_dataSouce.ForeColor = Color.DarkGray;
                    pictureBox_add.Image = AutoTest.Properties.Resources._20140924023908574_easyicon_net_128;
                    this.toolTip_info.SetToolTip(this.pictureBox_add, "修改&添加数据");
                    tb_valueAdd.Width = 220;
                    pictureBox_next.Visible = pictureBox_set.Visible = false;
                    break;
                case ShowRunTimeParameterType.Parameter:
                    nowShowType = ShowRunTimeParameterType.Parameter;
                    lb_info_parameter.ForeColor = Color.SaddleBrown;
                    lb_info_keyValue.ForeColor = lb_info_dataSouce.ForeColor = Color.DarkGray;
                    pictureBox_add.Image = (Image)Properties.Resources.ResourceManager.GetObject("2015070304121672223_easyicon_net_128");
                    this.toolTip_info.SetToolTip(this.pictureBox_add, "重置所有数据");
                    tb_valueAdd.Width = 171;
                    pictureBox_next.Visible = pictureBox_set.Visible = true;
                    break;
                case ShowRunTimeParameterType.DataSouce:
                    nowShowType = ShowRunTimeParameterType.DataSouce;
                    lb_info_dataSouce.ForeColor = Color.SaddleBrown;
                    lb_info_keyValue.ForeColor = lb_info_parameter.ForeColor = Color.DarkGray;
                    pictureBox_add.Image = (Image)Properties.Resources.ResourceManager.GetObject("2015070304121672223_easyicon_net_128");
                    this.toolTip_info.SetToolTip(this.pictureBox_add, "查看所有数据");
                    tb_valueAdd.Width = 171;
                    pictureBox_next.Visible = pictureBox_set.Visible = true;
                    break;
                default:
                    break;
            }
            updatalistView_CaseParameter();
        }

        #region pictureBox button
        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
        private void pictureBox_refresh_Click(object sender, EventArgs e)
        {
            updatalistView_CaseParameter();
        }

        private void pictureBox_next_Click(object sender, EventArgs e)
        {
            bool tempIsFindVaule = false;
            switch (nowShowType)
            {
                case ShowRunTimeParameterType.Parameter:
                    foreach (KeyValuePair<string, CaseExecutiveActuator.IRunTimeStaticData> tempKvp in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataParameterList)
                    {
                        if (tempKvp.Key == tb_keyAdd.Text)
                        {
                            tempIsFindVaule = true;
                            tb_valueAdd.Text = tempKvp.Value.DataMoveNext();
                            updatalistView_CaseParameter();
                            break;
                        }
                    }
                    break;
                case ShowRunTimeParameterType.DataSouce:
                    foreach (KeyValuePair<string, CaseExecutiveActuator.IRunTimeDataSource> tempKvp in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataSouceList)
                    {
                        if (tempKvp.Key == tb_keyAdd.Text)
                        {
                            tempIsFindVaule = true;
                            tb_valueAdd.Text = tempKvp.Value.DataMoveNext();
                            updatalistView_CaseParameter();
                            break;
                        }
                    }
                    break;
                default:
                    //not this way
                    break;
            }
            if (!tempIsFindVaule)
            {
                MessageBox.Show("未发现指定运行时参数", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void pictureBox_set_Click(object sender, EventArgs e)
        {
            bool tempIsFindVaule = false;
            switch (nowShowType)
            {
                case ShowRunTimeParameterType.Parameter:
                    foreach (KeyValuePair<string, CaseExecutiveActuator.IRunTimeStaticData> tempKvp in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataParameterList)
                    {
                        if (tempKvp.Key == tb_keyAdd.Text)
                        {
                            tempIsFindVaule = true;
                            if (!tempKvp.Value.DataSet(tb_valueAdd.Text))
                            {
                                MessageBox.Show("指定值无法匹配该类型运行时参数", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                tb_valueAdd.Text = tempKvp.Value.DataCurrent();
                            }
                            updatalistView_CaseParameter();
                            break;
                        }
                    }
                    break;
                case ShowRunTimeParameterType.DataSouce:
                    foreach (KeyValuePair<string, CaseExecutiveActuator.IRunTimeDataSource> tempKvp in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataSouceList)
                    {
                        if (tempKvp.Key == tb_keyAdd.Text)
                        {
                            tempIsFindVaule = true;
                            if (!tempKvp.Value.DataSet(tb_valueAdd.Text))
                            {
                                MessageBox.Show("指定值无法匹配该类型运行时参数", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                tb_valueAdd.Text = tempKvp.Value.DataCurrent();
                            }
                            updatalistView_CaseParameter();
                            break;
                        }
                    }
                    break;
                default:
                    //not this way
                    break;
            }
            if (!tempIsFindVaule)
            {
                MessageBox.Show("未发现指定运行时参数", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void pictureBox_add_Click(object sender, EventArgs e)
        {
            switch (nowShowType)
            {
                case ShowRunTimeParameterType.KeyValue:
                    foreach (KeyValuePair<string, CaseExecutiveActuator.IRunTimeStaticData> tempKvp in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataParameterList)
                    {
                        if (tempKvp.Key == tb_keyAdd.Text)
                        {
                            if(!myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.SetStaticData(tb_keyAdd.Text, tb_valueAdd.Text))
                            {
                                MessageBox.Show(string.Format("[{0}]不能应用于指定的【CaseStaticData】键", tb_valueAdd.Text), "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            return;
                        }
                    }
                    myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.AddStaticDataKey(tb_keyAdd.Text, tb_valueAdd.Text);
                    tb_keyAdd.Text = tb_valueAdd.Text = "";
                    //使用ActuatorStaticDataCollection内方法更新数据会触发委托，不要单独更新页面
                    break;
                case ShowRunTimeParameterType.Parameter:
                    foreach (KeyValuePair<string, CaseExecutiveActuator.IRunTimeStaticData> tempKvp in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataParameterList)
                    {
                        tempKvp.Value.DataReset();
                    }
                    tb_keyAdd.Text = tb_valueAdd.Text = "";
                    updatalistView_CaseParameter();
                    break;
                case ShowRunTimeParameterType.DataSouce:
                    foreach (KeyValuePair<string, CaseExecutiveActuator.IRunTimeDataSource> tempKvp in myParentWindow.nowCaseActionActuator.RunActuatorStaticDataCollection.RunActuatorStaticDataSouceList)
                    {
                        tempKvp.Value.DataReset();
                    }
                    tb_keyAdd.Text = tb_valueAdd.Text = "";
                    updatalistView_CaseParameter();
                    break;
                default:
                    //not this way
                    break;
            }
          
        } 
        #endregion


        #region lb_info click helper
        private void lb_info_MouseMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).BackColor = Color.LavenderBlush;
        }

        private void lb_info_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.FromArgb(194, 217, 247);
        }

        #endregion
       

        #region window move

        bool isMoveForm = false;
        Point myFormStartPos = new Point(0, 0);

        private void myCaseParameter_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMoveForm = true;
                myFormStartPos = new Point(-e.X, -e.Y);
            }
        }

        private void myCaseParameter_MouseUp(object sender, MouseEventArgs e)
        {
            isMoveForm = false;
        }

        private void myCaseParameter_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoveForm)
            {
                Point nowMousePos = Control.MousePosition;
                nowMousePos.Offset(myFormStartPos);
                this.Location = nowMousePos;
            }
        }
        #endregion

    }
}
