using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WcfTest1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public ServiceReference1.Service1Client sc;
        private void Form1_Load(object sender, EventArgs e)
        {
              sc = new ServiceReference1.Service1Client();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            

            MessageBox.Show(sc.GetData(2));


            //ServiceReference1.CompositeType ct = new ServiceReference1.CompositeType();
            //ct.BoolValue = true;
            //ct.StringValue = "StringValue";
            //MessageBox.Show(sc.GetDataUsingDataContract(ct).StringValue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            sc.GetDataOneWay(2);
            MessageBox.Show("ok");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string myStr="";
            sc.GetDataOut(10);
            MessageBox.Show(sc.GetDataOut(10));
        }

    }
}
