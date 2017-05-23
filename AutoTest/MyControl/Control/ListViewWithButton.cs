using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyCommonControl
{
    public partial class ListViewWithButton : ListViewExDB
    {
        public ListViewWithButton()
        {
            InitializeComponent();
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

        private int _cpadding = -2;

        private int _buttonIndex = -1;

        public event EventHandler ButtonClickEvent;

        /// <summary>
        /// progressBar显示模式
        /// </summary>
        [DescriptionAttribute("在第几列添加button[在添加行时该列必须是存在的]")]
        public int ButtonIndex
        {
            get { return _buttonIndex; }
            set { _buttonIndex = value; }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PAINT)
            {
                if (_buttonIndex >= 0)
                {
                    foreach (ListViewItem tempItem in this.Items)
                    {
                        if (tempItem == null)
                        {
                            continue;
                        }
                        if (tempItem.Tag != null && _buttonIndex < tempItem.SubItems.Count)
                        {
                            if (!(tempItem.Tag is Button))
                            {
                                continue;
                            }
                            Button nowC = (Button)tempItem.Tag;

                            //Button
                            ListViewItem.ListViewSubItem mySub = tempItem.SubItems[_buttonIndex];
                            Rectangle r = mySub.Bounds;

                            if (r.Y > 10 && r.Y < this.ClientRectangle.Height)
                            {
                                nowC.Bounds = new Rectangle(r.X + _cpadding, r.Y + _cpadding + 1, r.Width - (2 * _cpadding), r.Height - (2 * _cpadding + 2));
                                nowC.Visible = true;
                            }
                            else
                            {
                                nowC.Visible = false;
                            }
                        }
                    }

                }
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 添加含有Control的ListViewItem  
        /// </summary>
        /// <param name="yourItem">ListViewItem 的tag 需要指向目标Control （且Control的tag会在AddItemEx指向yourItem，请勿在应用业务中使用） ，且存放Control的列也要在ListViewItem被填充</param>
        public void AddItemEx(ListViewItem yourItem)
        {
            this.Items.Add(yourItem);
            if ((yourItem.Tag is System.Windows.Forms.Control) && _buttonIndex > -1 && _buttonIndex < yourItem.SubItems.Count)
            {
                System.Windows.Forms.Control tempControl = yourItem.Tag as System.Windows.Forms.Control;
                this.Controls.Add(tempControl);
                tempControl.Tag = yourItem;
                tempControl.Click += tempControl_Click;
            }
        }

        /*    eg:
                        ListViewItem tempLvi = new ListViewItem(new string[] {"",""});
                        tempLvi.Tag = new Button();
                        ((Button)tempLvi.Tag).Text = "";
                        lv_orderSnList.AddItemEx(tempLvi);
         * */



        /// <summary>
        /// 当加入Control被触发Click时，即执行此处（所有Control的click只需要订阅ButtonClickEvent即可）
        /// </summary>
        /// <param name="sender">被点击的行的ListViewItem</param>
        /// <param name="e"></param>
        void tempControl_Click(object sender, EventArgs e)
        {
                if(ButtonClickEvent!=null)
                {
                    this.ButtonClickEvent(((System.Windows.Forms.Control)sender).Tag, e);
                }
        }

        public void DelItemEx(ListViewItem yourItem)
        {
            if (yourItem.Tag is System.Windows.Forms.Control)
            {
                this.Controls.Remove(yourItem.Tag as System.Windows.Forms.Control);
            }
            yourItem.Tag = null;
            this.Items.Remove(yourItem);
        }

        public void ClearEx()
        {
            this.Items.Clear();
            this.Controls.Clear();
        }
       
    }
}
