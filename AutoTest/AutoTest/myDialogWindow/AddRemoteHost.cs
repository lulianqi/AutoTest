using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutoTest.MyControl;
using System.ServiceModel;

namespace AutoTest.myDialogWindow
{
    public partial class AddRemoteHost : MyChildWindow
    {
        public AddRemoteHost()
        {
            InitializeComponent();
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            try
            {
                ((AutoRunner)this.Owner).WillConnectHostAddress = new EndpointAddress(tb_hostAddress.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                ((AutoRunner)this.Owner).WillConnectHostAddress = null;
                return;
            }
            this.Close();
        }

    }
}
