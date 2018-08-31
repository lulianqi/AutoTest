using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutoTest.RemoteServiceReference;
using AutoTest.MyTool;
using System.ServiceModel;


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


namespace AutoTest.MyControl
{
    public partial class RemoteClientNode : DevComponents.AdvTree.Node
    {
        //protected override void OnPaint(PaintEventArgs pe)
        //{
        //    base.OnPaint(pe);
        //}

        RemoteClient remoteClient;

        private Dictionary<string, int> RunnerStateDictionary = new Dictionary<string, int>();

        public RemoteClientNode(EndpointAddress connectHostAddress)
        {
            InitializeComponent();
            FillStateDictionary();
            this.CheckBoxVisible = true;
            this.Cells[0].Text = "正在连接";
            remoteClient = new RemoteClient(connectHostAddress, null);
            remoteClient.OnClientStateChange += remoteClient_OnClientStateChange;
            remoteClient.OnPutAllRunnerInfor += remoteClient_OnPutAllRunnerInfor;
            remoteClient.OnRunnerStateChange += remoteClient_OnRunnerStateChange;
            remoteClient.OnClientErrorInfor += remoteClient_OnClientErrorInfor;
            remoteClient.StartClient();
        }


        public RemoteClient RemoteClient
        {
            get { return remoteClient; }
            set { remoteClient = value; }
        }


        private void FillStateDictionary()
        {
            RunnerStateDictionary.Add("Pause", 6);
            RunnerStateDictionary.Add("Running", 5);
            RunnerStateDictionary.Add("Stop", 8);
            RunnerStateDictionary.Add("Stoping", 8);
            RunnerStateDictionary.Add("Trying", 5);
        }

        void remoteClient_OnClientErrorInfor(RemoteClient sender, string errorInfo)
        {
            this.TreeControl.BeginInvoke(new Action<string>((agr)=>MessageBox.Show(agr)), errorInfo);
        } 

        void remoteClient_OnClientStateChange(RemoteClient sender, RemoteClient.RemoteClientState nowSate)
        {
            if (this.TreeControl.InvokeRequired)
            {
                this.TreeControl.Invoke(new Action<RemoteClient.RemoteClientState>(ChangeConnectState), nowSate);
            }
            else
            {
                 ChangeConnectState(nowSate);
            }
        }

        void remoteClient_OnPutAllRunnerInfor(RemoteClient sender, RemoteRunnerInfo remoteRunnerInfo)
        {
            if (this.TreeControl.InvokeRequired)
            {
                this.TreeControl.Invoke(new Func<RemoteRunnerInfo, bool>(RefreshAllRunner), remoteRunnerInfo);
            }
            else
            {
                RefreshAllRunner(remoteRunnerInfo);
            }
        }

        void remoteClient_OnRunnerStateChange(RemoteClient sender, RemoteRunnerInfo remoteRunnerInfo)
        {
            if (this.TreeControl.InvokeRequired)
            {
                this.TreeControl.Invoke(new Func<RemoteRunnerInfo, bool>(RefreshRunner), remoteRunnerInfo);
            }
            else
            {
                RefreshRunner(remoteRunnerInfo);
            }
        }

        private void ChangeConnectState(RemoteClient.RemoteClientState nowSate)
        {
            switch (nowSate)
            {
                case MyTool.RemoteClient.RemoteClientState.Break:
                    this.Cells[0].Text = this.remoteClient.ToString()+"尝试重新连接";
                    this.ImageIndex = 1;
                    break;
                case MyTool.RemoteClient.RemoteClientState.Connecting:
                    this.Cells[0].Text = this.remoteClient.ToString() + "正在连接连接";
                    this.ImageIndex = 2;
                    break;
                case MyTool.RemoteClient.RemoteClientState.Connected:
                    this.Cells[0].Text = this.remoteClient.ToString();
                    this.ImageIndex = 0;
                    this.Expand();
                    break;
                case MyTool.RemoteClient.RemoteClientState.Lost:
                    this.Cells[0].Text = this.remoteClient.ToString() + "失去连接";
                    this.ImageIndex = 3;
                    break;
                default:
                    break;
            }
        }

       
        private void SetImage(DevComponents.AdvTree.Node yourNode,int index)
        {
            if(this.TreeControl.ImageList!=null)
            {
                yourNode.ImageIndex = index;
            }
        }

        /// <summary>
        /// 添加一个空执行器
        /// </summary>
        public void AddEmptyRunner()
        {
            DevComponents.AdvTree.Node emptyNode = new DevComponents.AdvTree.Node();
            SetImage(emptyNode,1);
            emptyNode.CheckBoxVisible = true;
            emptyNode.Cells.Add(new DevComponents.AdvTree.Cell());
            emptyNode.Cells.Add(new DevComponents.AdvTree.Cell());
            emptyNode.Cells.Add(new DevComponents.AdvTree.Cell());
            emptyNode.Cells.Add(new DevComponents.AdvTree.Cell());
            emptyNode.Cells[0].Text = "未知";
            emptyNode.ImageIndex = 7;
            this.Nodes.Add(emptyNode);
        }

        /// <summary>
        /// 添加执行器(该方法未判断索引正确性)
        /// </summary>
        /// <param name="runnerState"></param>
        public void AddRunner(RunnerState runnerState)
        {
            if (runnerState != null)
            {
                DevComponents.AdvTree.Node emptyNode = new DevComponents.AdvTree.Node();
                SetImage(emptyNode, 1);
                emptyNode.CheckBoxVisible = true;
                emptyNode.Cells.Add(new DevComponents.AdvTree.Cell(runnerState.NowCell));
                emptyNode.Cells.Add(new DevComponents.AdvTree.Cell(runnerState.CellResult));
                emptyNode.Cells.Add(new DevComponents.AdvTree.Cell(runnerState.Time));
                emptyNode.Cells.Add(new DevComponents.AdvTree.Cell(runnerState.State));
                if (RunnerStateDictionary.ContainsKey(runnerState.State))
                {
                    emptyNode.ImageIndex = RunnerStateDictionary[runnerState.State];
                }
                else
                {
                    emptyNode.ImageIndex = 7;
                }
                emptyNode.Cells[0].Text = runnerState.RunnerName;
                this.Nodes.Add(emptyNode);
            }
        }

        /// <summary>
        /// 更新指定执行器(需保证传入参数不为null)
        /// </summary>
        /// <param name="runnerState"></param>
        /// <returns>是否正常更新，如果索引不符合预期则会返回false</returns>
        public bool UpdataRunner(RunnerState runnerState)
        {
            int updataIndex = runnerState.RunnerID;
            if (this.Nodes.Count > updataIndex)
            {
                this.Nodes[updataIndex].Cells[0].Text = runnerState.RunnerName;
                this.Nodes[updataIndex].Cells[1].Text = runnerState.NowCell;
                this.Nodes[updataIndex].Cells[2].Text = runnerState.CellResult;
                this.Nodes[updataIndex].Cells[3].Text = runnerState.Time;
                this.Nodes[updataIndex].Cells[4].Text = runnerState.State;
                if (RunnerStateDictionary.ContainsKey(runnerState.State))
                {
                    this.Nodes[updataIndex].ImageIndex = RunnerStateDictionary[runnerState.State];
                }
                else
                {
                    this.Nodes[updataIndex].ImageIndex = 7;
                }
                return true;
            }
            else
            {
                while (this.Nodes.Count < updataIndex)
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
            if (this.Nodes.Count > delIndex)
            {
                this.Nodes.RemoveAt(delIndex);
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
            if (remoteRunnerInfo == null)
            {
                return false;
            }
            if (remoteRunnerInfo.RunnerStateList == null)
            {
                return false;
            }
            if (remoteRunnerInfo.RunnerStateList.Length > 0)
            {
                foreach (RunnerState tempRunnerState in remoteRunnerInfo.RunnerStateList)
                {
                    if (tempRunnerState != null)
                    {
                        isAllLegal = false;
                    }
                    if (!UpdataRunner(tempRunnerState))
                    {
                        isAllLegal = false;
                    }
                }
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

            if (remoteRunnerInfo == null)
            {
                return false;
            }
            if (remoteRunnerInfo.RunnerStateList == null)
            {
                return false;
            }
            if (remoteRunnerInfo.RunnerStateList.Length > 0)
            {
                ClearAllRunner();

                foreach (RunnerState tempRunnerState in remoteRunnerInfo.RunnerStateList)
                {
                    if (tempRunnerState == null)
                    {
                        ClearAllRunner();
                        return false;
                    }
                    AddRunner(tempRunnerState);
                }
                
            }
            return true;
        }

        /// <summary>
        /// 清除容器内容
        /// </summary>
        public void ClearAllRunner()
        {
            this.Nodes.Clear();
        }
    }
}
