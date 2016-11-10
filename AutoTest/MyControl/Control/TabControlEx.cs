using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace MyCommonControl
{
    public class TabControlEx : TabControl
    {

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            StringFormat sf = new StringFormat();
            //设置文字是居中的
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            //画出选项卡文字
            e.Graphics.DrawString((this).TabPages[e.Index].Text, System.Windows.Forms.SystemInformation.MenuFont, new SolidBrush(Color.Black), e.Bounds, sf);
        }
    }
}
