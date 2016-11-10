using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYDControls;
using System.Threading;

namespace SYDControls
{
    /// <summary>
    /// 圆形进度条
    /// </summary>
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 开始运行圆形进度条
        /// </summary>
        /// <param name="win">父窗口</param>
        public void Start(IWin32Window win)
        {
            ParameterizedThreadStart parStart = new ParameterizedThreadStart(ThreadFun);
            Thread th = new Thread(parStart, 0);
            th.Start(win);
        }

        /// <summary>
        /// 进度条停止
        /// </summary>
        public void Stop()
        {
            Action del = delegate()
            {
                this.progress.Stop();
                this.Close();
            };

            this.Invoke(del);
        }

        /// <summary>
        /// 线程函数 
        /// </summary>
        /// <param name="obj"></param>
        private void ThreadFun(object obj)
        {
            this.progress.Start();
            this.ShowDialog((IWin32Window)obj);
        }
       
    }
}
