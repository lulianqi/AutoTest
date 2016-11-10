using AutoTest.RemoteServiceReference;
using MyCommonTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using AutoTest.myTool;

namespace AutoTest.myDialogWindow
{
    public partial class RemoteRunner : Form
    {
       
        public RemoteRunner()
        {
            InitializeComponent();
        }


        public RemoteClient tagRemoteClient = null;
        private void RemoteRunner_Load(object sender, EventArgs e)
        {
            if(tagRemoteClient!=null)
            {
                this.Text = tagRemoteClient.ToString();
            }
            else
            {
                this.Text = "NULL RemoteClient";
            }
        }

        public void RefreshRemoteRunnerView(RemoteRunnerInfo remoteRunnerInfo)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<RemoteRunnerInfo>(RefreshRemoteRunnerView), remoteRunnerInfo);
            }
            else
            {
                RemoteRunnerView.RefreshAllRunner(remoteRunnerInfo);
            }
        }

        public void UpdataRemoteRunnerView(RemoteRunnerInfo remoteRunnerInfo)
        {
           
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<RemoteRunnerInfo>(UpdataRemoteRunnerView), remoteRunnerInfo);
            }
            else
            {
                 RemoteRunnerView.RefreshRunner(remoteRunnerInfo);
            }
        }

        public void ShowError(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(ShowError), message);
            }
            else
            {
                
            }
        }


        private void RemoteRunner_Click(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void RemoteRunner_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(tagRemoteClient!=null)
            {
                tagRemoteClient.ShowWindow = null;
                tagRemoteClient = null;
            }
        }

    }
}
