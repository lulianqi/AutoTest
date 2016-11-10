using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoTest.myDialogWindow
{
    public partial class MyMessageListWindow : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public MyMessageListWindow(string yourWindowName, bool yourAutoRefresh, Dictionary<string, string> yourInfoList)
        {
            InitializeComponent();
            windowName = yourWindowName;
            isAutoRefresh = yourAutoRefresh;
            myInfoList = yourInfoList;
        }

        bool isAutoRefresh = false;
        string windowName = "Infor";
        Dictionary<string, string> myInfoList;
        AutoRunner myParentWindow;
        Timer myUpdataTime = new Timer();

        private void MyMessageListWindow_Load(object sender, EventArgs e)
        {
            lb_windowInfo.Text = windowName;
            this.TopMost = false;
            myParentWindow = (AutoRunner)this.Owner;
            refreshlistView_MyMessageListWindow();
            myUpdataTime.Interval = 1000;
            myUpdataTime.Tick += new EventHandler(myUpdataTime_Tick);
            myUpdataTime.Enabled = isAutoRefresh;
        }

        void myUpdataTime_Tick(object sender, EventArgs e)
        {
            updatalistView_MyMessageListWindow();
        }

        public void refreshlistView_MyMessageListWindow()
        {
            listView_infoList.BeginUpdate();
            listView_infoList.Items.Clear();
            foreach (KeyValuePair<string, string> tempKvp in myInfoList)
            {
                listView_infoList.Items.Add(new ListViewItem(new string[] { tempKvp.Key, tempKvp.Value }));
            }
            listView_infoList.EndUpdate();
        }

        public void updatalistView_MyMessageListWindow()
        {
            if (listView_infoList.Items.Count < myInfoList.Count)
            {
                refreshlistView_MyMessageListWindow();
            }
        }


        private void pictureBox_delAll_Click(object sender, EventArgs e)
        {
            myInfoList.Clear();
            refreshlistView_MyMessageListWindow();
        }

        private void pictureBox_refresh_Click(object sender, EventArgs e)
        {
            refreshlistView_MyMessageListWindow();
        }

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool isMoveForm = false;
        Point myFormStartPos = new Point(0, 0);

        private void MyMessageListWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMoveForm = true;
                myFormStartPos = new Point(-e.X, -e.Y);
            }
        }

        private void MyMessageListWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoveForm)
            {
                Point nowMousePos = Control.MousePosition;
                nowMousePos.Offset(myFormStartPos);
                this.Location = nowMousePos;
            }
        }

        private void MyMessageListWindow_MouseUp(object sender, MouseEventArgs e)
        {
            isMoveForm = false;
        }

        private void listView_infoList_DoubleClick(object sender, EventArgs e)
        {
            if (listView_infoList.SelectedItems.Count==0)
            {
                return;
            }
            if(listView_infoList.SelectedItems[0]!=null)
            {
                MessageBox.Show(listView_infoList.SelectedItems[0].SubItems[1].Text, "Info");
            }
        }

    }
}
