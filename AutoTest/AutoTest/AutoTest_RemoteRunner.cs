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
using System.ServiceModel;
using AutoTest.MyControl;


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

        EndpointAddress connectHostAddress;

        public EndpointAddress WillConnectHostAddress
        {
            get { return connectHostAddress;}
            set { connectHostAddress = value; }
        }

        #region event
        public void AT_RemoteRunnerLoad()
        {
            
        }


        public void AT_RemoteRunner_Resize(object sender, EventArgs e)
        {
            advTree_remoteTree.Height = this.Height - 112;
            panel_RemoteRunner.Width = this.Width - 397;
            panel_RemoteRunner.Height = this.Height - 60;
        }

        //关闭窗口
        public void AT_RemoteRunner_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void advTree_remoteTree_AfterCheck(object sender, DevComponents.AdvTree.AdvTreeCellEventArgs e)
        {
            if (e.Cell.Parent.HasChildNodes)
            {
                foreach (DevComponents.AdvTree.Node tempNode in e.Cell.Parent.Nodes)
                {
                    tempNode.Checked = e.Cell.Checked;
                }
            }
        }

        private void advTree_remoteTree_DoubleClick(object sender, EventArgs e)
        {
             DevComponents.AdvTree.Node selectedRemoteNode=advTree_remoteTree.SelectedNode;
             if (selectedRemoteNode is RemoteClientNode)
             {
                 if (((RemoteClientNode)selectedRemoteNode).RemoteClient.ShowWindow == null)
                 {
                     RemoteRunner myWindow = new RemoteRunner();
                     myWindow.TopLevel = false;
                     myWindow.Parent = this.panel_RemoteRunner;
                     myWindow.tagRemoteClient = ((RemoteClientNode)selectedRemoteNode).RemoteClient;
                     ((RemoteClientNode)selectedRemoteNode).RemoteClient.ShowWindow = myWindow;
                     myWindow.Show();
                     ((RemoteClientNode)selectedRemoteNode).RemoteClient.GetAllRunnerInfor();
                 }
                 else
                 {
                     ((RemoteClientNode)selectedRemoteNode).RemoteClient.ShowWindow.BringToFront();
                 }
             }
                        
                        
        }


        private void pictureBox_RR_Window_Click(object sender, EventArgs e)
        {
            switch (((PictureBox)sender).Name)
            {
                case "pictureBox_ConnectHost":
                    connectHostAddress = null;
                    AddRemoteHost newAddRemoteHost = new AddRemoteHost();
                    newAddRemoteHost.StartPosition = FormStartPosition.CenterParent;
                    newAddRemoteHost.ShowDialog(this);
                    if (connectHostAddress != null)
                    {
                        foreach (DevComponents.AdvTree.Node hostNode in advTree_remoteTree.Nodes)
                        {
                            RemoteClientNode clientNode = hostNode as RemoteClientNode;
                            if(clientNode!=null)
                            {
                                if(clientNode.RemoteClient.ClientEp.Uri.ToString()==connectHostAddress.Uri.ToString())
                                {
                                    MessageBox.Show("您已经连接过该主机");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Find Error Node");
                            }
                        }
                        AddRunnerHost(connectHostAddress);
                    }
                    break;
                case "pictureBox_rr_RunSelect":
                    SendRemoteOrder(1);
                    break;
                case "pictureBox_rr_StopSelect":
                    SendRemoteOrder(2);
                    break;
                case "pictureBox_rr_PuaseSelect":
                    SendRemoteOrder(3);
                    break;
                case "pictureBox_rr_RefreshSelect":
                    if (advTree_remoteTree.Nodes.Count > 0)
                    {
                        foreach (DevComponents.AdvTree.Node hostNode in advTree_remoteTree.Nodes)
                        {
                            RemoteClientNode clientNode = hostNode as RemoteClientNode;
                            if (clientNode == null)
                            {
                                break;
                            }
                            if (clientNode.RemoteClient.ClientState == RemoteClient.RemoteClientState.Connected)
                            {
                                clientNode.RemoteClient.GetAllRunnerInfor();
                            }
                            else if(clientNode.RemoteClient.ClientState == RemoteClient.RemoteClientState.Lost)
                            {
                                clientNode.RemoteClient.StartClient();
                            }
                        }
                    }
                    break;
                case "pictureBox_rr_DelSelect":
                    if (advTree_remoteTree.SelectedNode!=null)
                    {
                        DevComponents.AdvTree.Node selectedRemoteNode=advTree_remoteTree.SelectedNode;
                        while(!(selectedRemoteNode is RemoteClientNode))
                        {
                            if(selectedRemoteNode.Parent!=null)
                            {
                                selectedRemoteNode = selectedRemoteNode.Parent;
                            }
                            else
                            {
                                MessageBox.Show("主机状态异常","Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        }
                        DelRunnerHost(selectedRemoteNode as RemoteClientNode);
                    }
                    else
                    {
                        MessageBox.Show("未选择任何主机进行移除","Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    break;
                default:
                    MessageBox.Show("功能模块丢失", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
            }
        }

        #endregion

        #region action

        private void SendRemoteOrder(int order)
        {
            if (advTree_remoteTree.Nodes.Count > 0)
            {
                foreach (DevComponents.AdvTree.Node hostNode in advTree_remoteTree.Nodes)
                {
                    if (!hostNode.HasChildNodes)
                    {
                        continue;
                    }
                    RemoteClientNode clientNode = hostNode as RemoteClientNode;
                    if (clientNode == null)
                    {
                        continue;
                    }
                    if (clientNode.RemoteClient.ClientState == RemoteClient.RemoteClientState.Connected)
                    {
                        List<int> tempOrderList = new List<int>();
                        for (int i = 0; i < clientNode.Nodes.Count; i++)
                        {
                            if (clientNode.Nodes[i].Checked)
                            {
                                tempOrderList.Add(i);
                            }
                        }
                        if (tempOrderList.Count > 0)
                        {
                            if (order == 1)
                            {
                                clientNode.RemoteClient.StartRunner(tempOrderList);
                            }
                            else if(order == 2)
                            {
                                clientNode.RemoteClient.StopRunner(tempOrderList);
                            }
                            else if (order == 3)
                            {
                                clientNode.RemoteClient.PauseRunner(tempOrderList);
                            }
                        }
                    }
                }
            }
        }

        private void AddRunnerHost(EndpointAddress hostAddress)
        {
            RemoteClientNode remoteClientNode = new RemoteClientNode(hostAddress);
            remoteClientNode.ImageIndex = 4;
            advTree_remoteTree.Nodes.Add(remoteClientNode);
        }

        private void DelRunnerHost(RemoteClientNode remoteNode)
        {
            if (remoteNode.RemoteClient.ShowWindow!=null)
            {
                remoteNode.RemoteClient.ShowWindow.Close();
            }
            remoteNode.RemoteClient.StopClient();
            remoteNode.RemoteClient.Dispose();
            this.advTree_remoteTree.Nodes.Remove(remoteNode);
        }

        #endregion
    }
}
