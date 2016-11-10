using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


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

namespace MyCommonControl
{
    public partial class ProgressBarList : Panel
    {
        public enum BarListShowMode
        {
            BarListAdd=0,
            BarListFill=1,
            BarListQueue = 2
        }

        public ProgressBarList()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        private void MyInitializeComponent()
        {
            myProgressBarList=new List<ProgressBar>();
            this.AutoScroll = true;
            //this.Controls.Add(myBarInfoLabel);
            //myBarInfoLabel.Text = "lijie";
            //myBarInfoLabel.AutoSize = true;
            //myBarInfoLabel.Height = this.Height;
            //myBarInfoLabel.TextAlign = ContentAlignment.TopCenter;
            this.Resize += ProgressBarList_Resize;

            myToolTip.AutoPopDelay = 2000;
            myToolTip.InitialDelay = 1000;
            myToolTip.ReshowDelay = 500;
        }

        /// <summary>
        /// 尺寸的被动调整
        /// </summary>
        void ProgressBarList_Resize(object sender, EventArgs e)
        {
            if(myProgressBarList.Count>0)
            {
                if (myProgressBarList.Count > maxCount)
                {
                    foreach (ProgressBar tempBar in myProgressBarList)
                    {
                        tempBar.Width = this.Width-18;
                    }
                }
                else
                {
                    foreach (ProgressBar tempBar in myProgressBarList)
                    {
                        tempBar.Width = this.Width;
                    }
                    this.AutoScroll = false;
                    this.AutoScroll = true;
                }
            }
        }

        private List<ProgressBar> myProgressBarList;

        //private Label myBarInfoLabel = new Label();

        private ToolTip myToolTip = new ToolTip();

        private BarListShowMode showMode = BarListShowMode.BarListQueue;

        private int progressBarHight = 10;

        private int maxCount = 3;

        private bool isShowMore = true;

        private bool isShowTip = true;

        /// <summary>
        /// progressBar显示模式
        /// </summary>
        [DescriptionAttribute("progressBar显示模式")]
        public BarListShowMode ShowMode
        {
            get { return showMode; }
            set { showMode = value; }
        }

        /// <summary>
        /// progressBar默认高度
        /// </summary>
        [DescriptionAttribute("progressBar默认高度")]
        public int ProgressBarHight
        {
            get { return progressBarHight; }
            set
            {
                if (value > 2)
                {
                    progressBarHight = value;
                }
            }
        }


        /// <summary>
        /// progressBar最大显示个数
        /// </summary>
        [DescriptionAttribute("progressBar最大显示个数")]
        public int MaxCount
        {
            get { return maxCount; }
            set
            {
                if (value > 1)
                {
                    maxCount = value;
                }
            }
        }

        /// <summary>
        /// progressBar是否显示多项
        /// </summary>
        [DescriptionAttribute("progressBar是否显示多项")]
        public bool IsShowMore
        {
            get { return isShowMore; }
            set { isShowMore = value; }
        }

        /// <summary>
        /// progressBar是否显示Tip提示
        /// </summary>
        [DescriptionAttribute("progressBar是否显示Tip提示")]
        public bool IsShowTip
        {
            get { return isShowTip; }
            set { isShowTip = value; }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public void Add()
        {
            ProgressBar tempPB=new ProgressBar();
            tempPB.Size=new System.Drawing.Size(this.Width,18);
            tempPB.Location = new Point(0, myProgressBarList.Count * 18);
            myProgressBarList.Add(tempPB);
            this.Height = myProgressBarList.Count * 18;
            tempPB.Maximum = 100;
            tempPB.Value = 30;
            this.Controls.Add(tempPB);
        }

        public void UpdateList(List<KeyValuePair<int ,int >> yourProgress)
        {
            myProgressBarList.Clear();
            int tempProgressCount = yourProgress.Count;
            if (tempProgressCount > 0)
            {
                for (int i = 0; i < tempProgressCount; i++)
                {
                    ProgressBar tempPB = new ProgressBar();
                    tempPB.Maximum = yourProgress[i].Key;
                    tempPB.Value = yourProgress[i].Value;
                    if (isShowTip)
                    {
                        myToolTip.SetToolTip(tempPB, string.Format("【{0}/{1}】", yourProgress[i].Key, yourProgress[i].Value));
                    }
                    if (tempProgressCount > maxCount)
                    {
                        tempPB.Size = new System.Drawing.Size(this.Width - 18, this.Height / maxCount);
                        tempPB.Location = new Point(0, (this.Height / maxCount) * i);
                    }
                    else
                    {
                        tempPB.Size = new System.Drawing.Size(this.Width, this.Height / tempProgressCount);
                        tempPB.Location = new Point(0, (this.Height / tempProgressCount) * i);
                    }
                    
                    myProgressBarList.Add(tempPB);
                }
            }

            this.Controls.Clear();
            foreach(var tempBar in myProgressBarList)
            {
                this.Controls.Add(tempBar);
            }
        }

        public void UpdateListMinimal(List<KeyValuePair<int, int>> yourProgress)
        {
            if(myProgressBarList.Count>0)
            {
                if (yourProgress.Count == myProgressBarList.Count && yourProgress[yourProgress.Count - 1].Key == myProgressBarList[yourProgress.Count - 1].Maximum)
                {
                    myProgressBarList[yourProgress.Count - 1].Value = yourProgress[yourProgress.Count - 1].Value;
                    if (isShowTip)
                    {
                        myToolTip.SetToolTip(myProgressBarList[yourProgress.Count - 1], string.Format("【{0}/{1}】", yourProgress[yourProgress.Count - 1].Key, yourProgress[yourProgress.Count - 1].Value));
                    }
                }
                else
                {
                    UpdateList(yourProgress);
                }
            }
            else
            {
                UpdateList(yourProgress);
            }
        }
    }
}
