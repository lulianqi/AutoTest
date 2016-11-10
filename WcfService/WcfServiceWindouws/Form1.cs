using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace WcfServiceWindouws
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ServiceHost host = null;  

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (host != null)
                    host.Close();
                host = new ServiceHost(typeof(WcfService.Service1));
                host.Open();
                MessageBox.Show("open");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                if (host != null)
                    host.Close();
            }

        }
    }
}
