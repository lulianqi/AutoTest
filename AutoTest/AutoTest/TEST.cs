using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutoTest.MyTool;
using System.Threading;
using MyCommonHelper;
using MyCommonHelper.NetHelper;
using MyCommonHelper.FileHelper;
using System.IO;
using MyCommonControl;

namespace AutoTest
{
    public partial class TEST : Form
    {
        public TEST()
        {
            //Control.CheckForIllegalCrossThreadCalls = false;   
            InitializeComponent();
        }

        MySerialPort mySP;

        class cc
        {
            public string str = "";
            public int i = 8;
            public int getI()
            {
                return i;
            }
        }

        class ccc:cc
        {
             new public int getI()
            {
                return i+1;
            }
        }
        struct ss
        {
            public string str;
            public int i;
            public cc sc;

            public List<string> strs;
            public ss(cc x)
            {
                str = "sCNM";
                i = 18;
                sc = x;
                strs = null;
            }

            public void addstr(string yorStr)
            {
                if (strs == null)
                {
                    strs = new List<string>();
                }
                strs.Add(yorStr);
            }
        }

        struct myTestStruct
        {
            public List<string> strs;
            public cc sc;
            public void addstr(string yorStr)
            {
                if (strs == null)
                {
                    strs = new List<string>();
                }
                strs.Add(yorStr);
            }
        }

        class cs
        {
            public List<string> strs;
            public string str;
            public int i;
            public ss ms;
        }

        public void AT_RemoteRunnerLoad()
        {
            //this.IsMdiContainer = true;

            //Form f = new Form();
            //f.MdiParent = this;
            //f.Show();
            //return;

            MyControl.MyChildWindow myWindow = new MyControl.MyChildWindow();
            myWindow.TopLevel = false;
            myWindow.Parent = this.richTextBox1;
            myWindow.Show();

        }

        private void TEST_Load(object sender, EventArgs e)
        {



            //string filePath = @"C:\Users\administer\Desktop\asd\encode";

            //Stream myStm = CsvFileHelper.OpenFile(@"C:\Users\administer\Desktop\encode");
            //Stream myStm1 = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            //Stream myStm2= new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

            StreamReader myCsvRd = new StreamReader(@"C:\Users\administer\Desktop\encode", Encoding.Unicode);
            string tempReader = myCsvRd.ReadLine();
            myCsvRd.Dispose();
            StreamWriter myCsvSw = new StreamWriter(@"C:\Users\administer\Desktop\encode", false, Encoding.Unicode);
            myCsvSw.Write("abc𪚥");
            myCsvSw.Dispose();
            

            CsvFileHelper myCsv = new CsvFileHelper(@"C:\Users\administer\Desktop\my8.csv", Encoding.UTF8);
            var xxxx = myCsv.GetListCsvData();

            myCsv.Dispose();

            //CsvFileHelper.SaveCsvFile(@"C:\Users\administer\Desktop\my6.csv", xxxx, true, new System.Text.UTF8Encoding(false));

            //CsvFileHelper.SaveCsvFile(@"C:\Users\administer\Desktop\my6.csv", xxxx);
            CsvFileHelper.SaveCsvFile(@"C:\Users\administer\Desktop\my8.csv", xxxx);
            CsvFileHelper.SaveCsvFile(@"C:\Users\administer\Desktop\my9.csv", xxxx, true, new System.Text.UTF8Encoding(false));

            //MessageBox.Show(CsvFileHelper.name);

            for(int i=0;i<10;i++)
            {
                ListViewItem tempLv = new ListViewItem(new string[] { "11", "22", "33" });
                Button tempButton = new Button();
                tempButton.Text = "test";
                //tempButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                //tempButton.FlatStyle = FlatStyle.Flat;
                tempLv.Tag = tempButton;
                listViewWithButton1.AddItemEx(tempLv);
            }
            listViewWithButton1.ButtonClickEvent += new EventHandler((ag1, ag2) => { MessageBox.Show((ag1 as ListViewItem).Index.ToString()); });

            //AT_RemoteRunnerLoad();
            //AT_RemoteRunnerLoad();
            //AT_RemoteRunnerLoad();
            //AT_RemoteRunnerLoad();

            string xxxxx = null;
            xxxxx=xxxxx.MyAddValue("cnm");
            xxxxx = xxxxx.MyAddValue("cnm2");
            xxxxx = xxxxx.MyAddValue("cnm3");
            xxxxx = xxxxx.MyAddValue("cn44m");
            return;
            
            System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
            
            threadTest2();
            
            int mt = 0xffff;
            for (int i = 0; i < 32; i++)
            {
                mt = mt >> 1;
            }
            
            

            List<string> myRandom = new List<string>();
            myWatch.Start();
            Thread.Sleep(5);
            myWatch.Stop();
            MessageBox.Show("time:" + myWatch.ElapsedMilliseconds + "*" + myWatch.Elapsed);
            for (int i = 0; i < 100; i++)
            {
                myRandom.Add(MyCommonTool.GenerateRandomStr(30, 10));
            }
            for (int i = 0; i < 100; i++)
            {
                myRandom.Add(MyCommonTool.GenerateRandomStr(30, 1));
            }
            for (int i = 0; i < 100; i++)
            {
                myRandom.Add(MyCommonTool.GenerateRandomStr(30, 2));
            }
            for (int i = 0; i < 100; i++)
            {
                myRandom.Add(MyCommonTool.GenerateRandomStr(30, 3));
            }
            for (int i = 0; i < 100; i++)
            {
                myRandom.Add(MyCommonTool.GenerateRandomStr(30, 4));
            }

            threadTest3();
            //return;

            myWatch.Start();
            var y=0;
            var x = 10;
            while(y < x) 
            { 
                 y=y+1;
            } 

            myWatch.Stop();
            MessageBox.Show("time:" + myWatch.ElapsedMilliseconds + "*" + myWatch.Elapsed);

            ////测试方法重写后，强制调用父类方法
            //cc kk = new ccc();
            //MessageBox.Show(" "+kk.getI());

            ////测试堆栈数据（class struck）
            //cc myCc = new cc();
            //cc myCc1 = new cc();
            //cc myCc2 = new cc();
            //ss mySs1 = new ss(myCc);
            //ss mySs2 = new ss(myCc);
            //ss mySs3 = mySs1;
            //object mySs4 = new ss(myCc);
            //var mySs5 = new ss(myCc);
            //ss mySs6 = mySs5;
            //cs myCs1 = new cs();
            //ss cnm = myCs1.ms;
            //cnm.i = 3456;
            //myCc.i = 9;
            //myCc.str = "cm";
            //mySs1.i = 19;
            //mySs1.str = "sNM";
            //mySs1.sc.i = 7;
            //mySs1.sc.str = "cnm";
            //mySs3.sc.i = 77;
            //mySs1.sc.str = "cnmcnm";
            //myTest1(mySs1);
            //myTest1(mySs3);
            //myTest1(myCs1.ms);
            //myTest1(ref myCs1.ms);
            //myTest2(mySs1);
            //myTest2(mySs3);
            //myTest2(myCs1.ms);

            //var tempTest3 = myTest3();
            //MessageBox.Show(tempTest3.strs[0]);

            
            ////测试CPU时间片
            //threadTest();
   

            string tempStr=null;
            bool stempBool = true;
            myWatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                if (stempBool == true)
                {
                    continue;
                }
            }
            myWatch.Stop();
            MessageBox.Show("time:" + myWatch.ElapsedMilliseconds + "*" + myWatch.Elapsed);

            ////测试拆装性能
            //myWatch.Reset();
            //myWatch.Start();
            //for (int i = 0; i < 10000; i++)
            //{
            //    object myObj = myCc;
            //}
            //myWatch.Stop();
            //MessageBox.Show("time:" + myWatch.ElapsedMilliseconds + "*" + myWatch.Elapsed);

            //object Obj = myCc;
            //myWatch.Reset();
            //myWatch.Start();
            //for (int i = 0; i < 10000; i++)
            //{
            //    cc nowCc = (cc)Obj;
            //}
            //myWatch.Stop();
            //MessageBox.Show("time:" + myWatch.ElapsedMilliseconds + "*" + myWatch.Elapsed);

            //var x =(CaseAction)Enum.Parse(typeof(CaseAction), "Goto");
            //var x = CaseAction.Alarm;
            //string cc = null;
            mySP = new MySerialPort(true);
            mySP.Encoding = System.Text.Encoding.GetEncoding("utf-8");
            mySP.OnMySerialPortReceiveData += new MySerialPort.delegateReceiveData(mySP_OnMySerialPortReceiveData);
            mySP.OnMySerialPortThrowError += new MySerialPort.delegateThrowError(mySP_OnMySerialPortThrowError);
        }


        List<TimeSpan> myTimeList1 = new List<TimeSpan>();
        List<TimeSpan> myTimeList2 = new List<TimeSpan>();
        void threadTest()
        {
            Thread myThreadTest = new Thread(new ThreadStart(myDo));
            myThreadTest.IsBackground = true;
            myThreadTest.Start();

            //Thread myThreadTest2 = new Thread(new ThreadStart(myDo));
            //myThreadTest2.IsBackground = true;
            //myThreadTest2.Start();
        }

        static ManualResetEvent myResetEvent = new ManualResetEvent(false);

        void threadTest2()
        {
            for(int i=0;i<10;i++)
            {
                Thread myThreadTest = new Thread(new ThreadStart(myDo2));
                myThreadTest.IsBackground = true;
                myThreadTest.Start();
            }

        }

        void threadTest3()
        {
            Thread myThreadTest = new Thread(new ThreadStart(myDo3));
            myThreadTest.IsBackground = true;
            myThreadTest.Start();
        }

        Thread myThread1, myThread2;
        void threadTest4()
        {
            myThread1 = new Thread(new ThreadStart(myDo3));
            myThread1.IsBackground = true;
            myThread1.Start();
        }

        void myDo()
        {
            int j=0;
            System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
            myWatch.Start();
            while(j<1000000)
            {
                myTimeList1.Add(myWatch.Elapsed);
                int tempIndex=myTimeList1.Count;
                if(tempIndex>1)
                {
                    if ((myTimeList1[tempIndex - 1].Subtract(myTimeList1[tempIndex - 2])).TotalMilliseconds > 0.01)
                    {
                        myTimeList2.Add(myTimeList1[tempIndex - 1].Subtract(myTimeList1[tempIndex - 2]));
                    }
                }
                j++;
            }
        }

        void myDo2()
        {
            //do something
            myResetEvent.WaitOne();
            MessageBox.Show("i am back");
        }

        delegate void testDelegate1();
        delegate void testDelegate2(string str);
        void myDo3()
        {
            MessageBox.Show("hi1");
            this.Invoke(new testDelegate1(() => { MessageBox.Show("hi2"); }));
            this.Invoke(new testDelegate1(() => MessageBox.Show("hi3")));
            this.Invoke(new testDelegate2((str) => { MessageBox.Show(str); }),"hi4");

            myThread2 = new Thread(new ThreadStart(myDo4));
            myThread2.IsBackground = false;
            myThread2.Start();
        }

        void myDo4()
        {
            while(true)
            {
                int ss = 10;
                Thread.Sleep(100);
            }
        }


        void myTest1(ss mySs)
        {
            mySs.str = "myTest1";
        }

        void myTest1(ref ss mySs)
        {
            mySs.str = "myTest1";
        }

        void myTest2(ss mySs)
        {
            mySs.i = 987;
        }

        myTestStruct myTest3()
        {
            myTestStruct testStuct = new myTestStruct();
            testStuct.addstr("strstr");
            return testStuct;
        }

        void mySP_OnMySerialPortThrowError(string errorMes)
        {
            MessageBox.Show(errorMes);
        }

        void mySP_OnMySerialPortReceiveData(byte[] yourbytes, string yourStr)
        {
            if (yourStr != null)
            {
                richTextBox1.Text += yourStr;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MyWebTool.MyHttp.HttpPostData("http://pv.sohu.com/cityjson?ie=utf-8", 10000, "name", "filenmae", false, "testdata", null);
            (new MyCommonHelper.NetHelper.MyWebTool.MyHttp()).SendData("http://pv.sohu.com/cityjson?ie=utf-8", null, "GET");
            //mySP.openSerialPort("COM6", 57600);
        }


        List<KeyValuePair<int, int>> tv = new List<KeyValuePair<int, int>>();
        private void button2_Click(object sender, EventArgs e)
        {
            //progressBarList1.UpdateListMinimal(tv);
            tv.Add(new KeyValuePair<int, int>(123, 23));
            //listViewAdd1.Items.Add(new ListViewItem(new string[] {"1","2","3"}));


            return;
            mySP.comm.Write("\n");
            mySP.comm.Write(textBox1.Text);
            mySP.comm.Write("\n");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //richTextBox1.i
            //myCommonTool.SetControlFreeze(ref richTextBox1);
            //myCommonTool.SetControlUnfreeze(ref richTextBox1);

            timer1.Start();
            //for (int i = 0; i < 2; i++)
            //{
            //    richTextBox1.AppendText("nmbprivate void button2_Click(object sender, EventArgs e)\r\n");
            //    //richTextBox1.Focus();
            //    //richTextBox1.Text += "nmbprivate void button2_Click(object sender, EventArgs e)\r\n";
            //    //MessageBox.Show(" " + richTextBox1.SelectionStart);
            //    //richTextBox1.Focus();
            //}
            return;
            for (int i = 0; i < 1; i++)
            {
                MyControlHelper.setRichTextBoxContent(ref richTextBox1, "nmbprivate void button2_Click(object sender, EventArgs e)", Color.Red, true);
                MyControlHelper.setRichTextBoxContent(ref richTextBox1, "nmbprivate void button2_Click(object sender, EventArgs e)", Color.Beige, true);
                MyControlHelper.setRichTextBoxContent(ref richTextBox1, "nmprivate void button2_Click(object sender, EventArgs e)b", Color.Blue, true);
                MyControlHelper.setRichTextBoxContent(ref richTextBox1, "nmprivate void button2_Click(object sender, EventArgs e)b", Color.BurlyWood, true);
                MyControlHelper.setRichTextBoxContent(ref richTextBox1, "nmprivate void button2_Click(object sender, EventArgs e)b", Color.Black, true);
            }

            return;
            myResetEvent.Set();
            mySP.comm.Write(textBox1.Text);
            mySP.comm.Write("\t");
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MyControlHelper.SetControlFreeze(richTextBox1);
            int tempStart = richTextBox1.SelectionStart;
            int tempEnd = richTextBox1.SelectionLength;

            richTextBox1.AppendText("nmbprivate void button2_Click(object sender, EventArgs e+++++)\r\n");
            richTextBox1.AppendText("nmbprivate void button2_Click(object sender, EventArgs e)\r\n");
            //myCommonTool.setControlContentBottom(ref richTextBox1, "nmprivate void button2_Click(object sender, EventArgs e)b", Color.BurlyWood, true);
            //myCommonTool.setControlContentBottom(ref richTextBox1, "nmprivate void button2_Click(object sender, EventArgs e)b", Color.Black, true);
            richTextBox1.Select(tempStart, tempEnd);
            MyControlHelper.SetControlUnfreeze(richTextBox1);

            dataRecordBox1.AddDate("nmprivate void button2_Click(object sender, EventArgs e)b", Color.Blue, true);
            dataRecordBox1.AddDate("nmprivate void button2_Click(object sender, EventArgs e)b", Color.Black, true);
        }

      
    }
}
