using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AutoTest.MyTool;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.NetworkInformation;
using AutoTest.myDialogWindow;
using System.Collections;
using MyCommonHelper;
using CaseExecutiveActuator;
using CaseExecutiveActuator.Cell;


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


namespace AutoTest
{
    public partial class AutoRunner
    {

        private List<CaseRunner> caseRunnerList = new List<CaseRunner>();

        System.Windows.Forms.Timer AT_CaseRunner_Timer = new System.Windows.Forms.Timer();

        CaseRunner selctRunner = null;

        private bool pictureBox_selRunnerMaxTag = false;                                           //GwList窗口是否处于最大化状态(任何时候都请不要单独设置该值)

        /// <summary>
        /// get the value that direction is listView_SelectRunner is in max mode 【the value is same as pictureBox_GwListMaxTag】
        /// </summary>
        public bool SelRunnerMaxTag
        {
            get { return pictureBox_selRunnerMaxTag; }
        }

        #region event
        public void AT_CaseRunnerLoad()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 2000;
            AT_CaseRunner_Timer.Interval = 1000;
            AT_CaseRunner_Timer.Tick += AT_CaseRunner_Timer_Tick;
            AT_CaseRunner_Timer.Enabled = true;
        }

        void AT_CaseRunner_Timer_Tick(object sender, System.EventArgs e)
        {
            UpdateRunnerView();
        }

        public void AT_CaseRunner_Resize(object sender, EventArgs e)
        {
            listView_CaseRunner.Width = this.Width - 6;
            listView_CaseRunner.Height = this.Height - 167;

            lb_cr_info1.Location = new Point(lb_cr_info1.Location.X, this.Height - 74);
            lb_cr_info2.Location = new Point(lb_cr_info2.Location.X, this.Height - 74);
            lb_cr_info3.Location = new Point(lb_cr_info3.Location.X, this.Height - 74);

            reDrawSelRunnerSize();
        }

        //关闭窗口
        public void AT_CaseRunner_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isUserRunning = false;
            foreach (CaseRunner tempRunner in caseRunnerList)
            {
                if(tempRunner.RunnerState== CaseActuatorState.Running)
                {
                    isUserRunning = true;
                    break;
                }
            }
            if (isUserRunning)
            {
                if (MessageBox.Show("您有用户任务还在运行，是否强制关闭？", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    foreach (CaseRunner tempRunner in caseRunnerList)
                    {
                        if (tempRunner.RunnerState == CaseActuatorState.Running)
                        {
                            tempRunner.StopQuiet();
                        }
                    }
                    Thread.Sleep(100);
                    foreach (CaseRunner tempRunner in caseRunnerList)
                    {
                        if (tempRunner.RunnerState == CaseActuatorState.Running)
                        {
                            tempRunner.RunerActuator.KillAll();
                        }
                    }
                }
            }
        }

        //控制按钮
        private void pictureBox_CR_Window_Click(object sender, EventArgs e)
        {
            switch (((PictureBox)sender).Name)
            {
                case "pictureBox_cr_addUser":
                    AddRunner newSet = new AddRunner();
                    newSet.StartPosition = FormStartPosition.CenterParent;
                    newSet.ShowDialog(this);
                    break;
                case "pictureBox_cr_runSelect":
                    foreach(ListViewItem tempItem in listView_CaseRunner.CheckedItems)
                    {
                        ((CaseRunner)tempItem.Tag).RunQuiet();
                    }
                    break;
                case "pictureBox_cr_StopSelect":
                    foreach (ListViewItem tempItem in listView_CaseRunner.CheckedItems)
                    {
                        ((CaseRunner)tempItem.Tag).StopQuiet();
                    }
                    break;
                case "pictureBox_cr_delSelect":
                    foreach (ListViewItem tempItem in listView_CaseRunner.CheckedItems)
                    {
                        if (((CaseRunner)tempItem.Tag).RunnerState == CaseActuatorState.Stop)
                        {
                            DelRunner((CaseRunner)tempItem.Tag);
                        }
                    }
                    break;
                default:
                    MessageBox.Show("功能模块丢失", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
            }
        }

        //全选
        private void cb_cr_SelectAll_CheckStateChanged(object sender, EventArgs e)
        {
            if(cb_cr_SelectAll.Checked)
            {
                if (!cb_cr_isCb.Checked)
                {
                    cb_cr_isCb.Checked = true;
                }
                foreach (ListViewItem tempItem in listView_CaseRunner.Items)
                {
                    tempItem.Checked = true; 
                }
            }
            else
            {
                foreach (ListViewItem tempItem in listView_CaseRunner.CheckedItems)
                {
                    tempItem.Checked = false; 
                }
            }
        }

        //复选
        private void cb_cr_isCb_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_cr_isCb.Checked)
            {
                listView_CaseRunner.CheckBoxes = true;
            }
            else
            {
                cb_cr_SelectAll.Checked = false;
                listView_CaseRunner.CheckBoxes = false;
            }
        }

        //选择Runner
        private void listView_CaseRunner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView_CaseRunner.SelectedItems!=null)
            {
                if (listView_CaseRunner.SelectedItems.Count > 0)
                {
                    selctRunner = (CaseRunner)listView_CaseRunner.SelectedItems[0].Tag;
                    if (selctRunner != null)
                    {
                        llb_showRunner.Text = selctRunner.RunnerName + " >";
                        FillExecutionResult(selctRunner.RunerActuator.NowExecutionResultList);
                    }
                }
            }
        }

        //llb显示固定
        private void llb_showRunner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            selctRunner = null;
            listView_SelectRunner.BackColor = Color.LightSlateGray;
        }

        //listView_SelectRunner 双击
        private void listView_SelectRunner_DoubleClick(object sender, EventArgs e)
        {
            if (listView_SelectRunner.SelectedItems[0] != null)
            {
                Point myPosition = new Point(listView_SelectRunner.SelectedItems[0].Bounds.X + 5 + listView_SelectRunner.Location.X, listView_SelectRunner.SelectedItems[0].Bounds.Y + 75 + listView_SelectRunner.Location.Y);
                myDialogWindow.MyCBalloonResultInfo myListViewCBallon = null;
                myListViewCBallon = new myDialogWindow.MyCBalloonResultInfo(listView_SelectRunner.SelectedItems[0]);
                //myListViewCBallon.Owner = this;
                myListViewCBallon.HasShadow = true;
                myListViewCBallon.setBalloonPosition(this, myPosition, new Size(1, 1));
                myListViewCBallon.Show();
            }
        }

        //listView_SelectRunner 最大化显示
        private void pictureBox_selRunerMax_Click(object sender, EventArgs e)
        {
            setSelRunnerMax(!pictureBox_selRunnerMaxTag);
        }

        #endregion

        #region action

        /// <summary>
        /// 判断用户列表下是否有同名用户
        /// </summary>
        /// <param name="yourName">用户名</param>
        /// <returns>是否有同名用户</returns>
        public bool IsContainRunnerName(string yourName)
        {
            foreach(CaseRunner tempRunner in caseRunnerList)
            {
                if(yourName==tempRunner.RunnerName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加一个新用户
        /// </summary>
        /// <param name="yourCaseRunner">新用户</param>
        public void AddRunner(CaseRunner yourCaseRunner)
        {
            listView_CaseRunner.AddRunner(yourCaseRunner);
            caseRunnerList.Add(yourCaseRunner);
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="yourCaseRunner">被删除用户</param>
        public void DelRunner(CaseRunner yourCaseRunner)
        {
            listView_CaseRunner.DelRunner(yourCaseRunner);
            caseRunnerList.Remove(yourCaseRunner);
        }

        /// <summary>
        /// 更新CaseRunner界面
        /// </summary>
        private void UpdateRunnerView()
        {
            if (caseRunnerList.Count > 0)
            {
                if (selctRunner!=null)
                {
                    List<MyExecutionDeviceResult> tempExecutionDeviceResults = selctRunner.RunerActuator.NowExecutionResultList;
                    listView_SelectRunner.BeginUpdate();
                    if (tempExecutionDeviceResults.Count < listView_SelectRunner.Items.Count)
                    {
                        FillExecutionResult(tempExecutionDeviceResults);
                    }
                    else
                    {
                        while (tempExecutionDeviceResults.Count > listView_SelectRunner.Items.Count)
                        {
                            MyExecutionDeviceResult tempRseult = tempExecutionDeviceResults[listView_SelectRunner.Items.Count];
                            listView_SelectRunner.Items.Add(new ListViewItem(new string[] { listView_SelectRunner.Items.Count.ToString(), tempRseult.caseId.ToString(), tempRseult.startTime, tempRseult.spanTime, tempRseult.result.ToString(), tempRseult.caseTarget + "->" + tempRseult.backContent, tempRseult.additionalRemark }));
                        }
                    }
                    if (listView_SelectRunner.Items.Count - 1 > 0)
                    {
                        listView_SelectRunner.EnsureVisible(listView_SelectRunner.Items.Count - 1);
                    }
                    listView_SelectRunner.EndUpdate();
                }
                foreach (CaseRunner tempRunner in caseRunnerList)
                {
                    if (tempRunner.RunnerState == CaseActuatorState.Running)
                    {
                        tempRunner.UpdateProgressBar();
                    }
                }
            }
        }

        /// <summary>
        /// 重新填充ListView Result
        /// </summary>
        /// <param name="yourResults">数据源</param>
        private void FillExecutionResult(List<MyExecutionDeviceResult> yourResults)
        {
            if(yourResults!=null)
            {
                listView_SelectRunner.BeginUpdate();
                listView_SelectRunner.Items.Clear();
                listView_SelectRunner.BackColor = Color.AliceBlue;
                // foreach (myExecutionDeviceResult tempRseult in yourResults) //数据源修改权是又另外一个线程控制，数据可能在遍历时被修改
                MyExecutionDeviceResult tempRseult;
                for (int i = 0; i < yourResults.Count;i++ )
                {
                    tempRseult = yourResults[i];
                    listView_SelectRunner.Items.Add(new ListViewItem(new string[] { listView_SelectRunner.Items.Count.ToString(), tempRseult.caseId.ToString(), tempRseult.startTime, tempRseult.spanTime, tempRseult.result.ToString(), tempRseult.caseTarget + "->" + tempRseult.backContent, tempRseult.additionalRemark }));
                }
                if (listView_SelectRunner.Items.Count - 1 > 0)
                {
                    listView_SelectRunner.EnsureVisible(listView_SelectRunner.Items.Count - 1);
                }
                listView_SelectRunner.EndUpdate();
            }
        }

        /// <summary>
        /// set the listView_SelectRunner size mode
        /// </summary>
        /// <param name="isMax">is max</param>
        public void setSelRunnerMax(bool isMax)
        {
            if (pictureBox_selRunnerMaxTag != isMax)
            {
                pictureBox_selRunnerMaxTag = isMax;
                reDrawSelRunnerSize();
            }
        }

        /// <summary>
        /// ajust the listView_SelectRunner Size from GwListMaxTag【pictureBox_SelRunnerMaxTag】
        /// </summary>
        private void reDrawSelRunnerSize()
        {
            if (pictureBox_selRunnerMaxTag)
            {
                listView_SelectRunner.Location = new Point(3, 0);
                listView_SelectRunner.Size = new System.Drawing.Size(this.Width - 6, this.Height - 84);
                pictureBox_selRunerMax.Location = new System.Drawing.Point(this.Width - 27, 0);
                pictureBox_selRunerMax.Image = imageListForButton.Images[7];
            }
            else
            {
                listView_SelectRunner.Location = new Point(212, 0);
                listView_SelectRunner.Size = new System.Drawing.Size(785, 90);
                listView_SelectRunner.Width = this.Width - 215;
                pictureBox_selRunerMax.Location = new System.Drawing.Point(this.Width - 27, 0);
                pictureBox_selRunerMax.Image = imageListForButton.Images[6];
            }
        }

        #endregion

    }
}
