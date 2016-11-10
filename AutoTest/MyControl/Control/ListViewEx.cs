using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyCommonControl
{
    /// <summary>
    /// 使ListView使用纵向滚动条
    /// </summary>
    public class ListViewEx : ListView
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int ShowScrollBar(IntPtr hWnd, int iBar, int bShow);
        const int SB_HORZ = 0;
        const int SB_VERT = 1;
        protected override void WndProc(ref Message m)
        {
            if (this.View == View.List)
            {
                ShowScrollBar(this.Handle, SB_VERT, 1);
                ShowScrollBar(this.Handle, SB_HORZ, 0);
            }
            if (this.View == View.Details)
            {
                ShowScrollBar(this.Handle, SB_VERT, 1);
                ShowScrollBar(this.Handle, SB_HORZ, 0);
            }
            base.WndProc(ref m);
        }
    }
}
