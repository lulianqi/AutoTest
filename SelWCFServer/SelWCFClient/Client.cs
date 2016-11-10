using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ServiceModel;
using SelWCFClient.SelServiceReference;
using SelWCFClient.DuaServiceReference;

namespace SelWCFClient
{

    public partial class Client : Form
    {

        public Client()
        {
            InitializeComponent();
        }

        private void AddInfo(string info)
        {
            richTextBox_info.AppendText(info + "\r\n");
        }

        BindingSource bsBinding = new BindingSource();
        Dictionary<string, System.ServiceModel.Channels.Binding> bingdings = new Dictionary<string, System.ServiceModel.Channels.Binding>();

        System.ServiceModel.Channels.Binding nowBinding;
        BasicHttpBinding myBinging = new BasicHttpBinding();
        EndpointAddress myEp = new EndpointAddress("http://localhost:8080/SelService");

        InstanceContext instanceContext = null;
        DuplexServiceClient duaSc = null;


        private void Form1_Load(object sender, EventArgs e)
        {
            instanceContext = new InstanceContext(new CallBackHandler(richTextBox_info));

            bingdings.Add("BasicHttpBinding", new BasicHttpBinding());
            bingdings.Add("WSHttpBinding", new WSHttpBinding());
            bingdings.Add("WSDualHttpBinding", new WSDualHttpBinding());
            bingdings.Add("NetTcpBinding ", new NetTcpBinding());

            bsBinding.DataSource = bingdings;

            comboBox_binding.DisplayMember = "Key";
            comboBox_binding.ValueMember = "Value";
            comboBox_binding.DataSource = bsBinding;
            
        }


        private void comboBox_binding_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nowBinding = ((KeyValuePair<string,System.ServiceModel.Channels.Binding>)comboBox_binding.SelectedValue).Value;
            nowBinding = ((System.ServiceModel.Channels.Binding)comboBox_binding.SelectedValue);
        }


        private void button_sayHello_Click(object sender, EventArgs e)
        {
            SelServiceClient sc = new SelServiceClient();

            EndpointAddress ep=new EndpointAddress("http://localhost:8080/SelService");
            BasicHttpBinding httpBinding = new BasicHttpBinding();

            SelServiceClient mySc = new SelServiceClient(nowBinding, ep);
            AddInfo(mySc.SayHello(12));
        }

        private void button_sayHello2_Click(object sender, EventArgs e)
        {
            BasicHttpBinding httpBinding = new BasicHttpBinding();
            EndpointAddress ep = new EndpointAddress("http://localhost:8080/SelService");

            ChannelFactory<ISelService> factory = new ChannelFactory<ISelService>(nowBinding);
            ISelService channel = factory.CreateChannel(ep);

            AddInfo(channel.SayHello(12));
        }

        private void button_sayBye_Click(object sender, EventArgs e)
        {
            SelServiceClient mySc = new SelServiceClient(nowBinding, myEp);
            mySc.SayBye(12);
            AddInfo("SayBye in  oneWay");
        }

        private void button_isWho_Click(object sender, EventArgs e)
        {
            SelServiceClient mySc = new SelServiceClient(nowBinding, myEp);
            MyData myData = new MyData();
            myData.Name="lj";
            myData.exInfo="ex";
            myData.IsMan=true;
            AddInfo(mySc.WhoIs(myData));
        }

        

        private void button_CallBack_Click(object sender, EventArgs e)
        {
            duaSc = new DuplexServiceClient(instanceContext);
            //duaSc.TryCallBack("lijie");
            duaSc.TryOneWayCallBack("lijie");
        }

    }
}
