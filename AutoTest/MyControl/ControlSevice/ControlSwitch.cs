using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCommonControl.ControlSevice
{
    class ControlSwitch
    {
        //private  System.Timers.Timer mySystime = new System.Timers.Timer();
        System.Windows.Forms.Timer mySystime = new System.Windows.Forms.Timer();
        private int mySpeed;
        private int myLen;
        private System.Windows.Forms.Panel p1, p2;

        #region 构造函数
        /// <summary>
        /// 使用默认值55/1初始化实例！
        /// </summary>
        internal ControlSwitch()
        {
            //默认构造函数
            mySpeed = 55;
            myLen = 1;
        }

        internal ControlSwitch(int speed, int len)
        {
            mySpeed = speed;
            myLen = len;
        }
        #endregion

        #region 属性访问器
        /// <summary>
        /// 获取或设置动画速度！
        /// </summary>
        internal int RollSpeed
        {
            get
            {
                return mySpeed;
            }
            set
            {
                mySpeed = value;
            }
        }

        /// <summary>
        /// 获取或设置动画幅度！
        /// </summary>
        internal int RollLen
        {
            get
            {
                return myLen;
            }
            set
            {
                myLen = value;
            }
        }
        #endregion


        #region 标记变量
        //private bool isAtoB=true;
        private int myWidth = 0;
        private int myHeight = 0;
        private System.Drawing.Point myStartPosition = new System.Drawing.Point(0, 0);
        //private int times = 0;
        #endregion

        //反转变化

        private void mySystime_Tick_AtoB(object sender, EventArgs e)
        {

            if (p1.Visible == true)
            {
                if (p1.Width < myLen * 2)
                {
                    p1.Visible = false;
                    p2.Visible = true;
                    p2.Location = p1.Location;
                    p2.Width = p1.Width;
                }
                else
                {
                    p1.Location = new System.Drawing.Point(p1.Location.X + myLen, p1.Location.Y);
                    p1.Width -= myLen * 2;
                }
            }
            else
            {
                if (p2.Width + myLen * 2 < myWidth)
                {
                    p2.Location = new System.Drawing.Point(p2.Location.X - myLen, p2.Location.Y);
                    p2.Width += myLen * 2;
                }
                else
                {
                    p2.Location = new System.Drawing.Point(p2.Location.X - myLen, p2.Location.Y);
                    p2.Width += myLen * 2;
                    mySystime.Enabled = false;
                }
            }
        }

        private void mySystime_Tick_BtoA(object sender, EventArgs e)
        {

            if (p2.Visible == true)
            {
                if (p2.Width < myLen * 2)
                {
                    p2.Visible = false;
                    p1.Visible = true;
                    p1.Location = p2.Location;
                    p1.Width = p2.Width;
                }
                else
                {
                    p2.Location = new System.Drawing.Point(p2.Location.X + myLen, p2.Location.Y);
                    p2.Width -= myLen * 2;
                }
            }
            else
            {
                if (p1.Width + myLen * 2 < myWidth)
                {
                    p1.Location = new System.Drawing.Point(p1.Location.X - myLen, p1.Location.Y);
                    p1.Width += myLen * 2;
                }
                else
                {
                    p1.Location = myStartPosition;
                    p1.Width = myWidth;
                    mySystime.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 未实现的方法
        /// </summary>
        /// <param name="controlA"></param>
        /// <param name="controlB"></param>
        internal static void BeginChange(System.Windows.Forms.Control controlA, System.Windows.Forms.Control controlB)
        {
            //do someing
        }

        /// <summary>
        /// 注意调用此方法会直接改变mySpeed和mylen的值，如有需要请调用相关属性修改回原值！
        /// </summary>
        /// <param name="PaneleA">第一个panel</param>
        /// <param name="Paneleb">第二个panel</param>
        /// <param name="width">传入变化宽度范围</param>
        /// <param name="startposition">传入初始位置</param>
        /// <param name="speed">移动速度</param>
        /// <param name="len">每次移动的距离</param>
        internal void BeginChange(System.Windows.Forms.Panel PaneleA, System.Windows.Forms.Panel PaneleB, int width, System.Drawing.Point startposition, int speed, int len)
        {
            mySpeed = speed;
            myLen = len;
            BeginChange(PaneleA, PaneleB, width, startposition);
        }

        /// <summary>
        /// 传入panel可以显示特效，但重复调用可能会出现控件变形，在不可控制的情况下请选择传入宽度和位置！
        /// </summary>
        /// <param name="PaneleA">第一个panel</param>
        /// <param name="Paneleb">第二个panel</param>
        internal void BeginChange(System.Windows.Forms.Panel PaneleA, System.Windows.Forms.Panel PaneleB)
        {
            p1 = PaneleA;
            p2 = PaneleB;
            myStartPosition = p1.Location;
            mySystime.Enabled = false;
            mySystime.Tick -= new EventHandler(mySystime_Tick_BtoA);
            mySystime.Tick -= new EventHandler(mySystime_Tick_AtoB);
            if (p1.Visible)
            {
                myWidth = p1.Width;
                mySystime.Tick += new EventHandler(mySystime_Tick_AtoB);
            }
            else
            {
                myWidth = p2.Width;
                mySystime.Tick += new EventHandler(mySystime_Tick_BtoA);
            }
            mySystime.Interval = mySpeed;
            mySystime.Enabled = true;
        }

        /// <summary>
        /// 请务必传入具体的width和startposition值请不要使用控件属性获取！
        /// </summary>
        /// <param name="PaneleA">第一个panel</param>
        /// <param name="Paneleb">第二个panel</param>
        /// <param name="width">传入变化宽度范围</param>
        /// <param name="startposition">传入初始位置</param>
        internal void BeginChange(System.Windows.Forms.Panel PaneleA, System.Windows.Forms.Panel PaneleB, int width, System.Drawing.Point startposition)
        {
            p1 = PaneleA;
            p2 = PaneleB;
            myWidth = width;
            myStartPosition = startposition;
            mySystime.Enabled = false;
            mySystime.Tick -= new EventHandler(mySystime_Tick_BtoA);
            mySystime.Tick -= new EventHandler(mySystime_Tick_AtoB);
            if (p1.Visible)
            {
                mySystime.Tick += new EventHandler(mySystime_Tick_AtoB);
            }
            else
            {
                mySystime.Tick += new EventHandler(mySystime_Tick_BtoA);
            }
            mySystime.Interval = mySpeed;
            mySystime.Enabled = true;
        }

        //平移变化

        private void mySystime_Tick_AupB(object sender, EventArgs e)
        {
            if (myStartPosition.Y - p2.Location.Y <= mySpeed)
            {
                p1.Location = new System.Drawing.Point(myStartPosition.X, p1.Location.Y - mySpeed);
                p2.Location = new System.Drawing.Point(myStartPosition.X, p2.Location.Y - mySpeed);
            }
            else
            {
                p1.Location = new System.Drawing.Point(myStartPosition.X, myStartPosition.Y + myHeight);
                p2.Location = myStartPosition;
                p1.Visible = false;
                mySystime.Enabled = false;
            }
        }

        /// <summary>
        /// 向上滚动的简单运动，请务必传入具体的height和startposition值，请不要使用控件属性获取！
        /// </summary>
        /// <param name="PaneleA">现在显示的panle</param>
        /// <param name="PaneleB">将要出现的panle</param>
        /// <param name="height">高度</param>
        /// <param name="startposition">起始位置</param>
        internal void BeginRowUp(System.Windows.Forms.Panel PaneleA, System.Windows.Forms.Panel PaneleB, int height, System.Drawing.Point startposition)
        {
            p1 = PaneleA;
            p2 = PaneleB;
            myHeight = height;
            myStartPosition = startposition;
            mySystime.Enabled = false;
            p2.Location = new System.Drawing.Point(myStartPosition.X, myStartPosition.Y + myHeight);
            p1.Visible = p2.Visible = true;
            mySystime.Tick -= new EventHandler(mySystime_Tick_AupB);
            mySystime.Tick += new EventHandler(mySystime_Tick_AupB);
            mySystime.Interval = mySpeed;
            mySystime.Enabled = true;
        }
    }
}
