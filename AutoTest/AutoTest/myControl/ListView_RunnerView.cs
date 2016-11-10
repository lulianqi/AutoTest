using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyCommonControl;


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


namespace AutoTest.myControl
{
    /// <summary>
    /// 为CaseRunner提供绑定的ListView，并在指定列添加制定Controls
    /// </summary>
    public partial class ListView_RunnerView : ListViewExDB
    {
        public ListView_RunnerView()
        {
            InitializeComponent();
            MyInitialize();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void MyInitialize()
        {
            //this.OwnerDraw = true;
            //this.DrawItem += ListViewAdd_DrawItem;
        }

      

        private const UInt32 LVM_FIRST = 0x1000;   //指定Listview控件的首个消息,其它相关消息用LVM_FIRST + X的形式定义,比如:LVM_GETBKCOLOR为LVM_FIRST + 0
        private const UInt32 LVM_SCROLL = (LVM_FIRST + 20); //在Listview控件中移动滚动条,宏:ListView_Scroll
        private const int WM_HSCROLL = 0x114;  //当窗口的标准水平滚动条产生一个滚动事件时,发送本消息给该窗口
        private const int WM_VSCROLL = 0x115;  //当窗口的标准垂直滚动条产生一个滚动事件时,发送本消息给该窗口
        private const int WM_MOUSEWHEEL = 0x020A;  //当鼠标轮子转动时,发送本消息给当前拥有焦点的控件
        private const int WM_PAINT = 0x000F; //窗口重绘

        private int _cpadding = 0;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PAINT)
            {
                foreach (ListViewItem tempItem in this.Items)
                {
                    if (tempItem==null)
                    {
                        continue;
                    }
                    if (tempItem.Tag != null)
                    {
                        CaseRunner runnerTag = (CaseRunner)tempItem.Tag;
                        Control nowC=null;
                        //bar
                        ListViewItem.ListViewSubItem mySub = tempItem.SubItems[6];
                        Rectangle r = mySub.Bounds;
                        nowC = runnerTag.runerProgressBar;
                        if (r.Y > 10 && r.Y < this.ClientRectangle.Height)
                        {
                            nowC.Bounds = new Rectangle(r.X + _cpadding, r.Y + _cpadding+1, r.Width - (2 * _cpadding), r.Height - (2 * _cpadding+2));
                            nowC.Visible = true;
                        }
                        else
                        {
                            nowC.Visible = false;
                        }

                        //but
                        mySub = tempItem.SubItems[8];
                        r = mySub.Bounds;
                        nowC = runnerTag.runnerButton;
                        if (r.Y > 10 && r.Y < this.ClientRectangle.Height)
                        {
                            nowC.Bounds = new Rectangle(r.X + _cpadding, r.Y + _cpadding, r.Width - (2 * _cpadding), r.Height - (2 * _cpadding));
                            nowC.Visible = true;
                        }
                        else
                        {
                            nowC.Visible = false;
                        }
                    }
                }

            }
            base.WndProc(ref m);
        }

        public void AddRunner(CaseRunner yourRunner)
        {
            yourRunner.tagItem = new ListViewItem(new string[] { yourRunner.RunnerName, yourRunner.StartCellName, "", "","", "", "", "Stop","" });
            yourRunner.tagItem.UseItemStyleForSubItems = false;
            yourRunner.tagItem.Tag = yourRunner;

            this.Items.Add(yourRunner.tagItem);
            this.Controls.Add(yourRunner.runerProgressBar);
            this.Controls.Add(yourRunner.runnerButton);
        }

        public void DelRunner(CaseRunner yourRunner)
        {
            this.Controls.Remove(yourRunner.runerProgressBar);
            this.Controls.Remove(yourRunner.runnerButton);
            this.Items.Remove(yourRunner.tagItem);

            yourRunner.tagItem.Tag = null;
            yourRunner.tagItem = null ;
            
        }

    }
}
