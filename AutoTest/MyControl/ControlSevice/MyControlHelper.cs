using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyCommonControl
{
    public class MyControlHelper
    {
        private const int WM_SETREDRAW = 0xB;

        /// <summary>
        /// 添加带颜色内容
        /// </summary>
        /// <param name="rtb">目标richtextbox</param>
        /// <param name="strInput">输入内容</param>
        /// <param name="fontColor">颜色</param>
        /// <param name="isNewLine">是否换行</param>
        public static void myAddRtbStr(ref RichTextBox rtb, string strInput, Color fontColor, bool isNewLine)
        {
            rtb.SelectionColor = fontColor;
            if (isNewLine)
            {
                rtb.AppendText(strInput + "\n");  //保留每行的所有颜色。 //  rtb.Text += strInput + "/n";  //添加时，仅当前行有颜色。    
            }
            else
            {
                rtb.AppendText(strInput);
            }
        }

        /// <summary>
        /// 改变选定项颜色
        /// </summary>
        /// <param name="rtb">目标richtextbox</param>
        /// <param name="fontColor">颜色</param>
        public static void ChangeSelectionColor(RichTextBox rtb, Color fontColor)
        {
            rtb.SelectionColor = fontColor;
        }

        /// <summary>
        /// 改变选定项颜色
        /// </summary>
        /// <param name="rtb">目标richtextbox</param>
        /// <param name="startIndex">开始位置</param>
        /// <param name="len">长度</param>
        /// <param name="fontColor">颜色</param>
        public static void ChangeSelectionColor(RichTextBox rtb, int startIndex, int len, Color fontColor)
        {
            if (startIndex + len <= rtb.TextLength)
            {
                rtb.Select(startIndex, len);
                rtb.SelectionColor = fontColor;
                rtb.DeselectAll();
            }
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="vaule">内容</param>
        /// <param name="myRtb">目标richtextbox</param>
        public static void myAddRecord(string name, string vaule, RichTextBox myRtb)
        {
            myAddRtbStr(ref myRtb, name, Color.Blue, true);
            myAddRtbStr(ref myRtb, vaule, Color.Black, true);
        }

        /// <summary>
        /// 将目标控件最底部显示
        /// </summary>
        /// <param name="yourRtb">目标控件</param>
        public static void setRichTextBoxContentBottom(ref RichTextBox yourRtb)
        {
            UnsafeNativeMethods.SendMessage(yourRtb.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
            yourRtb.SelectionStart = yourRtb.Text.Length;
            yourRtb.Focus();
            UnsafeNativeMethods.SendMessage(yourRtb.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            yourRtb.Refresh();
        }

        /// <summary>
        /// 添加文本并进行底部跟随
        /// </summary>
        /// <param name="yourRtb">RichTextBox</param>
        /// <param name="yourStr">your content</param>
        public static void setRichTextBoxContent(ref RichTextBox yourRtb, string yourStr)
        {
            UnsafeNativeMethods.SendMessage(yourRtb.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
            yourRtb.AppendText(yourStr);
            UnsafeNativeMethods.SendMessage(yourRtb.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            yourRtb.Refresh();

        }

        /// <summary>
        /// 添加文本并进行底部跟随【若要底部跟随需要设置RichTextBox HideSelection 为false,或者在修改完成后调用Focus()】
        /// </summary>
        /// <param name="yourRtb">目标richtextbox</param>
        /// <param name="yourStr">添加内容</param>
        /// <param name="fontColor">颜色</param>
        /// <param name="isNewLine">是否为新的一行</param>
        public static void setRichTextBoxContent(ref RichTextBox yourRtb, string yourStr, Color fontColor, bool isNewLine)
        {
            UnsafeNativeMethods.SendMessage(yourRtb.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
            myAddRtbStr(ref yourRtb, yourStr, fontColor, isNewLine);
            //yourRtb.SelectionStart = yourRtb.Text.Length;
            UnsafeNativeMethods.SendMessage(yourRtb.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            yourRtb.Refresh();
            //yourRtb.Focus();
            //Application.DoEvents();
        }

        /// <summary>
        /// 添加文本并进行底部跟随【若要底部跟随需要设置RichTextBox HideSelection 为false,或者在修改完成后调用Focus()】
        /// </summary>
        /// <param name="yourRtb">目标richtextbox</param>
        /// <param name="yourStr">添加内容</param>
        /// <param name="fontColor">颜色</param>
        /// <param name="isNewLine">是否为新的一行</param>
        /// <param name="isSelectNotChange">是否锁定选定项</param>
        public static void setRichTextBoxContent(ref RichTextBox yourRtb, string yourStr, Color fontColor, bool isNewLine, bool isSelectNotChange)
        {
            int tempStart = yourRtb.SelectionStart;
            int tempEnd = yourRtb.SelectionLength;
            UnsafeNativeMethods.SendMessage(yourRtb.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
            myAddRtbStr(ref yourRtb, yourStr, fontColor, isNewLine);
            if (isSelectNotChange)
            {
                yourRtb.Select(tempStart, tempEnd);
            }
            UnsafeNativeMethods.SendMessage(yourRtb.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            yourRtb.Refresh();

        }

        /// <summary>
        /// 停止控件刷新
        /// </summary>
        /// <param name="yourCtr">your Control</param>
        public static void SetControlFreeze(System.Windows.Forms.Control yourCtr)
        {
            UnsafeNativeMethods.SendMessage(yourCtr.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 恢复控件刷新
        /// </summary>
        /// <param name="yourCtr">your Control</param>
        public static void SetControlUnfreeze(System.Windows.Forms.Control yourCtr)
        {
            UnsafeNativeMethods.SendMessage(yourCtr.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            yourCtr.Refresh();
        }
    }
}
