using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MyCommonControl;
using AutoTest.RemoteServiceReference;


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


namespace AutoTest.myControl
{
    public partial class ListView_RemoteRunnerView : ListViewExDB
    {
        public ListView_RemoteRunnerView()
        {
            InitializeComponent();
            MyInitialize();
        }

        private void MyInitialize()
        {
            PlayStateDictionary.Add("Pause", PlayButton.PlayButtonState.Pause);
            PlayStateDictionary.Add("Running", PlayButton.PlayButtonState.Run);
            PlayStateDictionary.Add("Stop", PlayButton.PlayButtonState.Stop);
            PlayStateDictionary.Add("Stoping", PlayButton.PlayButtonState.Stop);
            PlayStateDictionary.Add("Trying", PlayButton.PlayButtonState.Run);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private const UInt32 LVM_FIRST = 0x1000;   //指定Listview控件的首个消息,其它相关消息用LVM_FIRST + X的形式定义,比如:LVM_GETBKCOLOR为LVM_FIRST + 0
        private const UInt32 LVM_SCROLL = (LVM_FIRST + 20); //在Listview控件中移动滚动条,宏:ListView_Scroll
        private const int WM_HSCROLL = 0x114;  //当窗口的标准水平滚动条产生一个滚动事件时,发送本消息给该窗口
        private const int WM_VSCROLL = 0x115;  //当窗口的标准垂直滚动条产生一个滚动事件时,发送本消息给该窗口
        private const int WM_MOUSEWHEEL = 0x020A;  //当鼠标轮子转动时,发送本消息给当前拥有焦点的控件
        private const int WM_PAINT = 0x000F; //窗口重绘

        private int _cpadding = 0;

        private Dictionary<string, MyCommonControl.PlayButton.PlayButtonState> PlayStateDictionary = new Dictionary<string, PlayButton.PlayButtonState>();

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PAINT)
            {
                foreach (ListViewItem tempItem in this.Items)
                {
                    foreach (System.Windows.Forms.ListViewItem.ListViewSubItem subItem in tempItem.SubItems)
                    {
                        if (subItem.Tag != null)
                        {
                            Control myControl = subItem.Tag as Control;
                            if (myControl != null)
                            {
                                Rectangle r = subItem.Bounds;
                                if (r.Y > 10 && r.Y < this.ClientRectangle.Height)
                                {
                                    myControl.Bounds = new Rectangle(r.X + _cpadding, r.Y + _cpadding + 1, r.Width - (2 * _cpadding), r.Height - (2 * _cpadding + 2));
                                    myControl.Visible = true;
                                }
                                else
                                {
                                    myControl.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 添加一个空执行器
        /// </summary>
        public void AddEmptyRunner()
        {
            ProgressBarList runerProgressBar = new ProgressBarList();
            PlayButton runnerButton = new PlayButton();
            ListViewItem myAddItem = new ListViewItem(new string[] { "", "", "", "", "", "", "", "" });
       
            this.Controls.Add(runerProgressBar);
            this.Controls.Add(runnerButton);

            myAddItem.SubItems[5].Tag = runerProgressBar;
            myAddItem.SubItems[7].Tag = runnerButton;

            this.Items.Add(myAddItem);
        }

        /// <summary>
        /// 添加执行器(该方法未判断索引正确性)
        /// </summary>
        /// <param name="runnerState"></param>
        public void AddRunner(RunnerState runnerState)
        {
            if(runnerState!=null)
            {
                ProgressBarList runerProgressBar=new ProgressBarList();
                PlayButton runnerButton=new PlayButton();
                ListViewItem myAddItem = new ListViewItem(new string[] { runnerState.RunnerName, runnerState.NowCell, runnerState.RunDetails, runnerState.Time, runnerState.CellResult, "", runnerState.State, "" });

                if (PlayStateDictionary.ContainsKey(runnerState.State))
                {
                    runnerButton.OnChangeState(PlayStateDictionary[runnerState.State]);
                }
                else
                {
                    MyCommonTool.ErrorLog.PutInLogEx("unkonw runnerState find in ListView_RemoteRunnerView");
                }

                if (runnerState.RunnerProgress != null)
                {
                    runerProgressBar.UpdateList((runnerState.RunnerProgress).ToList());
                }
                else
                {
                    MyCommonTool.ErrorLog.PutInLogEx("no RunnerProgress find in ListView_RemoteRunnerView");
                }

                this.Controls.Add(runerProgressBar);
                this.Controls.Add(runnerButton);

                myAddItem.SubItems[5].Tag = runerProgressBar;
                myAddItem.SubItems[7].Tag = runnerButton;

                this.Items.Add(myAddItem);
            }
        }

        /// <summary>
        /// 更新指定执行器
        /// </summary>
        /// <param name="runnerState"></param>
        /// <returns></returns>
        public bool UpdataRunner(RunnerState runnerState)
        {
            int updataIndex = runnerState.RunnerID;
            if (this.Items.Count > updataIndex)
            {
                this.Items[updataIndex].SubItems[0].Text = runnerState.RunnerName;
                this.Items[updataIndex].SubItems[1].Text = runnerState.NowCell;
                this.Items[updataIndex].SubItems[2].Text = runnerState.RunDetails;
                this.Items[updataIndex].SubItems[3].Text = runnerState.Time;
                this.Items[updataIndex].SubItems[4].Text = runnerState.CellResult;
                ((ProgressBarList)this.Items[updataIndex].SubItems[5].Tag).UpdateListMinimal(runnerState.RunnerProgress.ToList());
                this.Items[updataIndex].SubItems[6].Text = runnerState.State;
                if (PlayStateDictionary.ContainsKey(runnerState.State))
                {
                    ((PlayButton)this.Items[updataIndex].SubItems[7].Tag).OnChangeState(PlayStateDictionary[runnerState.State]);
                }
                else
                {
                    MyCommonTool.ErrorLog.PutInLogEx("unkonw runnerState find in ListView_RemoteRunnerView");
                }
                return true;
            }
            else
            { 
                while(this.Items.Count < updataIndex)
                {
                    AddEmptyRunner();
                }
                AddRunner(runnerState);
            }
            return false;
        }

        /// <summary>
        /// 删除指定执行器
        /// </summary>
        /// <param name="delIndex"></param>
        /// <returns></returns>
        public bool DelRunner(int delIndex)
        {
            if(this.Items.Count>delIndex)
            {
                ListViewItem myDelItem = this.Items[delIndex];
                this.Controls.Remove((Control)myDelItem.SubItems[5].Tag);
                this.Controls.Remove((Control)myDelItem.SubItems[7].Tag);
                myDelItem.SubItems[5].Tag = null;
                myDelItem.SubItems[7].Tag = null;
                this.Items.Remove(myDelItem);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 刷新有变化的执行器
        /// </summary>
        /// <param name="remoteRunnerInfo"></param>
        /// <returns></returns>
        public bool RefreshRunner(RemoteRunnerInfo remoteRunnerInfo)
        {
            bool isAllLegal = true;
            if(remoteRunnerInfo==null)
            {
                return false;
            }
            if(remoteRunnerInfo.RunnerStateList==null)
            {
                return false;
            }
            if (remoteRunnerInfo.RunnerStateList.Length > 0)
            {
                this.BeginUpdate();
                foreach (RunnerState tempRunnerState in remoteRunnerInfo.RunnerStateList)
                {
                    if (tempRunnerState != null)
                    {
                        isAllLegal = false;
                    }
                    if(!UpdataRunner(tempRunnerState))
                    {
                        isAllLegal = false;
                    }
                }
                this.EndUpdate();
            }
            return isAllLegal;
        }

        /// <summary>
        /// 刷新全部执行器
        /// </summary>
        /// <param name="remoteRunnerInfo"></param>
        /// <returns></returns>
        public bool RefreshAllRunner(RemoteRunnerInfo remoteRunnerInfo)
        {
            if(remoteRunnerInfo==null)
            {
                return false;
            }
            if(remoteRunnerInfo.RunnerStateList==null)
            {
                return false;
            }
            if(remoteRunnerInfo.RunnerStateList.Length>0)
            {
                this.BeginUpdate();
                ClearAllRunner();

                foreach(RunnerState tempRunnerState in remoteRunnerInfo.RunnerStateList)
                {
                    if(tempRunnerState==null)
                    {
                        ClearAllRunner();
                        return false;
                    }
                    AddRunner(tempRunnerState);
                }

                this.EndUpdate();
            }
            return true;
        }

        /// <summary>
        /// 清除容器内容
        /// </summary>
        public void ClearAllRunner()
        {
            this.Items.Clear();
            this.Controls.Clear();
        }
    }
}
