using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
    public partial class PlayButton : UserControl
    {
        /// <summary>
        /// Paly状态
        /// </summary>
        public enum PlayButtonState
        {
            Stop = 0,
            Run = 1,
            Pause = 2
        }

        /// <summary>
        /// PlayButtonEventArgs
        /// </summary>
        public class PlayButtonEventArgs : EventArgs
        {
            private PlayButtonState playState;

            public PlayButtonEventArgs(PlayButtonState yourState)
            {
                playState = yourState;
            }

            /// <summary>
            /// 当前状态命令
            /// </summary>
            public PlayButtonState PlayState
            {
                get { return playState; }
            }
        }

        /// <summary>
        /// 当有状态命令时发生
        /// </summary>
        public event EventHandler<PlayButtonEventArgs> ButtonStateChangedEvent;

        /// <summary>
        /// 当设置被触发时发生
        /// </summary>
        public event EventHandler ButtonSetClickEvent;

        /// <summary>
        /// 当设置被触发时发生
        /// </summary>
        public event EventHandler ButtonOutClickEvent;

        /// <summary>
        /// 当设置被触发时发生
        /// </summary>
        public event EventHandler ButtonDelClickEvent;

        public PlayButton()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        private void MyInitializeComponent()
        {
            buttonState = PlayButtonState.Stop;
            if(!isShowSet)
            {
                pictureBox_Set.Visible = pictureBox_outReport.Visible = pictureBox_Remove.Visible = false;
            }
            OnChangeState(buttonState);
            AdjustButtonImageSize(this.Size.Height);//因为控件默认是先初始化之后，然后在设置控件各种属性，所有此处并不是所有方设置的值，所有在Resize里设置是必不可少的
        }

        /// <summary>
        /// 调整Image的尺寸适应容器
        /// </summary>
        /// <param name="yourSize">边长</param>
        private void AdjustButtonImageSize(int yourSize)
        {
            pictureBox_Play.Size = new Size(yourSize, yourSize);
            pictureBox_Pause.Size = new Size(yourSize, yourSize);
            pictureBox_Set.Size = new Size(yourSize, yourSize);
            pictureBox_outReport.Size = new Size(yourSize, yourSize);
            pictureBox_Remove.Size = new Size(yourSize, yourSize);
            pictureBox_Pause.Location = new Point(yourSize + 1, 0);
            pictureBox_Set.Location = new Point(yourSize*2 + 2, 0);
            pictureBox_outReport.Location = new Point(yourSize * 3 + 3, 0);
            pictureBox_Remove.Location = new Point(yourSize * 4 + 4, 0);
        }

        /// <summary>
        /// 储存当前状态
        /// </summary>
        private PlayButtonState buttonState;

        /// <summary>
        /// 是否显示设置按钮
        /// </summary>
        private bool isShowSet =true;

        /// <summary>
        /// 是否显示设置按钮
        /// </summary>
        [DescriptionAttribute("是否显示设置按钮")]
        public bool IsShowSet
        {
            get { return isShowSet; }
            set { isShowSet = value; }
        }

        /// <summary>
        /// 获取当前状态
        /// </summary>
        public PlayButtonState ButtonState
        {
            get { return buttonState; }
        }

        /// <summary>
        /// 触发Play发出状态改变命令
        /// </summary>
        /// <param name="yourState"></param>
        private void OnReportButtonState(PlayButtonState yourState)
        {
            if(ButtonStateChangedEvent!=null)
            {
                this.ButtonStateChangedEvent(this, new PlayButtonEventArgs(yourState));
            }
        }

        /// <summary>
        /// 真正执行者完成命令后的返回确认信息
        /// </summary>
        /// <param name="yourState">确认状态</param>
        public void OnChangeState(PlayButtonState yourState)
        {
            buttonState = yourState;
            switch (buttonState)
            {
                case PlayButtonState.Stop:
                    pictureBox_Play.Image = imageList_ForPlayButton.Images[2];
                    pictureBox_Pause.Image = imageList_ForPlayButton.Images[1];

                    pictureBox_Set.Image = imageList_ForPlayButton.Images[8];
                    pictureBox_outReport.Image = imageList_ForPlayButton.Images[4];
                    pictureBox_Remove.Image = imageList_ForPlayButton.Images[6];
                    break;
                case PlayButtonState.Run:
                    pictureBox_Play.Image = imageList_ForPlayButton.Images[3];
                    pictureBox_Pause.Image = imageList_ForPlayButton.Images[0];

                    pictureBox_Set.Image = imageList_ForPlayButton.Images[9];
                    pictureBox_outReport.Image = imageList_ForPlayButton.Images[5];
                    pictureBox_Remove.Image = imageList_ForPlayButton.Images[7];
                    break;
                case PlayButtonState.Pause:
                    pictureBox_Play.Image = imageList_ForPlayButton.Images[2];
                    pictureBox_Pause.Image = imageList_ForPlayButton.Images[1];

                    pictureBox_Set.Image = imageList_ForPlayButton.Images[9];
                    pictureBox_outReport.Image = imageList_ForPlayButton.Images[5];
                    pictureBox_Remove.Image = imageList_ForPlayButton.Images[7];
                    break;
                default:
                    break;
            }
        }


        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Plum ;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
        }

        private void PlayButton_Resize(object sender, EventArgs e)
        {
            AdjustButtonImageSize(this.Size.Height);
        }

        private void pictureBox_Play_Click(object sender, EventArgs e)
        {
            switch (buttonState)
            {
                case PlayButtonState.Stop:
                    OnReportButtonState(PlayButtonState.Run);
                    break;
                case PlayButtonState.Run:
                    OnReportButtonState(PlayButtonState.Stop);
                    break;
                case PlayButtonState.Pause:
                    OnReportButtonState(PlayButtonState.Run);
                    break;
                default:
                    break;
            }
        }

        private void pictureBox_Pause_Click(object sender, EventArgs e)
        {
            switch (buttonState)
            {
                case PlayButtonState.Stop:
                    break;
                case PlayButtonState.Run:
                    OnReportButtonState(PlayButtonState.Pause);
                    break;
                case PlayButtonState.Pause:
                    break;
                default:
                    break;
            }
        }

        private void pictureBox_Set_Click(object sender, EventArgs e)
        {
            if(ButtonSetClickEvent!=null)
            {
                this.ButtonSetClickEvent(sender, e);
            }
            //或者可以直接如下，为了保证设计文件一致性，没有采用
            //this.pictureBox_Set.Click += ButtonSetClickEvent;
        }

        private void pictureBox_outReport_Click(object sender, EventArgs e)
        {
            if (ButtonOutClickEvent != null)
            {
                this.ButtonOutClickEvent(sender, e);
            }
        }

        private void pictureBox_Remove_Click(object sender, EventArgs e)
        {
            if (ButtonDelClickEvent != null)
            {
                this.ButtonDelClickEvent(sender, e);
            }
        }


    }
}
