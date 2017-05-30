using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace MyCommonHelper
{
    [System.Security.SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
    }

    public class MyCommonTool
    {
        private const int WM_SETREDRAW = 0xB;

        /// <summary>
        /// seed for GenerateRandomStr
        /// </summary>
        private static int externRandomSeed=0;


        public static string GenerateRandomNum(int codeCount)
        {
            externRandomSeed++;
            string myRandomStr = string.Empty;
            long mySeed = DateTime.Now.Ticks + externRandomSeed;
            Random random = new Random((int)(mySeed & 0x0000ffff));
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next();
                myRandomStr = myRandomStr + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
            }
            return myRandomStr;
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="strCount">字符串长度</param>
        /// <param name="GenerateType">生成模式： 0-是有可见ASCII / 1-数字 / 2-大写字母 / 3-小写字母 / 4-特殊字符 / 5-大小写字母 / 6-字母和数字</param>
        /// <returns>随机字符串</returns>
        public static string GenerateRandomStr(int strCount, int GenerateType)
        {
            externRandomSeed++;
            StringBuilder myRandomStr = new StringBuilder(strCount);
            long mySeed = DateTime.Now.Ticks + externRandomSeed;
            Random random = new Random((int)(mySeed & 0x0000ffff));
            for (int i = 0; i < strCount; i++)
            {
                char tempCh;
                int num = random.Next();
                switch (GenerateType)
                {
                    case 1:
                        tempCh = (char)(0x30 + (num % 10));
                        break;
                    case 2:
                        tempCh = (char)(0x41 + (num % 26));
                        break;
                    case 3:
                        tempCh = (char)(0x61 + (num % 26));
                        break;
                    case 4:
                        int tempValue = 0x20 + ((num % 95));
                        if ((tempValue>=0x30&&tempValue<=0x39)||(tempValue>=0x41&&tempValue<=0x5a)||(tempValue>=0x61&&tempValue<=0x7a))
                        {
                            i--;
                            continue;
                        }
                        else
                        {
                            tempCh = (char)tempValue;
                        }
                        break;
                    case 5:
                        tempValue = 0x20 + ((num % 95));
                        if ((tempValue >= 0x41 && tempValue <= 0x5a) || (tempValue >= 0x61 && tempValue <= 0x7a))
                        {
                            tempCh = (char)tempValue;
                        }
                        else
                        {
                            i--;
                            continue;
                        }
                        break;
                    case 6:
                        tempValue = 0x20 + ((num % 95));
                        if ((tempValue >= 0x30 && tempValue <= 0x39) || (tempValue >= 0x41 && tempValue <= 0x5a) || (tempValue >= 0x61 && tempValue <= 0x7a))
                        {
                            tempCh = (char)tempValue;
                        }
                        else
                        {
                            i--;
                            continue;
                        }
                        break;
                    default:
                        tempCh = (char)(0x20 + (num % 95));
                        break;
                }
                myRandomStr.Append(tempCh);
            }
            return myRandomStr.ToString();
        }

        /// <summary>
        /// 检查两组数组里面的值是否一致（用于数组中的数据都是唯一项，对于引用对象则比较引用地址）
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="yourSouceAr">源数组</param>
        /// <param name="yourTargetAr">目标数组</param>
        /// <param name="addAr">要添加的项（如果不一致）</param>
        /// <param name="delAr">要删除的项（如果不一致）</param>
        /// <returns></returns>
        public static  bool IsMyArrayIndexSame<T>(T[] yourSouceAr,T[]yourTargetAr,out List<T>addAr,out List<T>delAr)
        {
            return IsMyArrayIndexSame<T>(new List<T>(yourSouceAr), new List<T>(yourTargetAr), out addAr, out delAr);
        }

        /// <summary>
        /// 检查两组数组里面的值是否一致（用于数组中的数据都是唯一项，对于引用对象则比较引用地址）
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="yourSouceAr">源数组</param>
        /// <param name="yourTargetAr">目标数组</param>
        /// <param name="addAr">要添加的项（如果不一致）</param>
        /// <param name="delAr">要删除的项（如果不一致）</param>
        /// <returns></returns>
        public static bool IsMyArrayIndexSame<T>(List<T> yourSouceAr, List<T> yourTargetAr,out List<T> addAr,out List<T> delAr)
        {
            addAr = new List<T>();
            delAr = yourSouceAr.LightClone<T>();
            foreach (var tempVaule in yourTargetAr)
            {
                if (delAr.Contains(tempVaule))
                {
                    delAr.Remove(tempVaule);
                }
                else
                {
                    addAr.Add(tempVaule);
                }
            }
            if (addAr.Count == 0 && delAr.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 启动其他程序
        /// </summary>
        /// <param name="FileName">需要启动的外部程序名称</param>
        public static bool OpenPress(string FileName, string Arguments)
        {
            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            if (System.IO.File.Exists(FileName))
            {
                pro.StartInfo.FileName = FileName;
                pro.StartInfo.Arguments = Arguments;
                try
                {
                    pro.Start();
                }
                catch(Exception ex)
                {
                    ErrorLog.PutInLog(ex);
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// find the file name from filepath
        /// </summary>
        /// <param name="myPath"> file path</param>
        /// <returns>file name</returns>
        public static string findFileName(string myPath)
        {
            string tempFileName = myPath.Remove(0, myPath.LastIndexOf('\\') + 1);
            return tempFileName;
        }

        /// <summary>
        /// find the file Directory name from filepath
        /// </summary>
        /// <param name="myPath"> file path</param>
        /// <returns>file Directory</returns>
        public static string findFileDirectory(string myPath)
        {
            string tempFileName = myPath.Remove(myPath.LastIndexOf('\\'));
            return tempFileName;
        }

        /// <summary>
        /// 将xml字符串格式化
        /// </summary>
        /// <param name="originalString">原始字符串</param>
        /// <param name="formatString">格式化后的数据，或错误信息</param>
        /// <returns>是否成功</returns>
        public static bool FormatXmlString(string originalString,out string formatString)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Xml.XmlTextWriter xtw = new System.Xml.XmlTextWriter(sw);
            xtw.Formatting = System.Xml.Formatting.Indented;
            xtw.Indentation = 1;
            xtw.IndentChar = '\t';
            try
            {
                xmlDoc.LoadXml(originalString);
                xmlDoc.WriteTo(xtw);
                formatString = sw.ToString();
                return true;
            }
            catch(Exception ex)
            {
                formatString = ex.Message;
                return false;
            }
            finally
            {
                sw.Dispose();
            }
            
        }

        /// <summary>
        /// 将json字符串格式化
        /// </summary>
        /// <param name="originalString">原始字符串</param>
        /// <param name="formatString">格式化后的数据</param>
        /// <returns>是否成功（未判断格式是否合法，只要不为null都返回成功）</returns>
        public static bool FormatJsonString(string originalString,out string formatString)
        {
            // { [ 后面 换行，缩进+1
            // } ] 前面 缩进-1，换行
            // ,   后面 换行
            // ""  名字 或 string 值内含的 {} [] , 不处理
            // ""  名字 或 string 外的多余空格 去除  （ascii 0x20 以下的不可见字符包括换行回车全部去掉）
            // ""  内可能含有未转义过的引号，不能以此作为引号的结束
            //去除 unicode  C1 控制符处理 

            formatString = string.Empty;
            char jsonRetract = '\t';
            char[] jsonNewLine=new char[]{'\r','\n'};
            int nowRetractCount = 0;
            bool isInString = false;

            #region 仅会在函数内使用的匿名函数

            Func<char, bool> IsEndString = (dealChar) =>
                {
                    if (dealChar=='"')
                    { 

                    }
                    return false;
                };

            Func<char,int> GetNowRetractType = (dealChar) =>
                {
                    if (dealChar < 0x21 || (dealChar>127 && dealChar<162))
                    {
                        return -99;
                    }
                    switch (dealChar)
                    {
                        case '{':
                            return 1;
                            //break;
                        case '[':
                            return 1;
                            //break;
                        case '}':
                            return -1;
                            //break;
                        case ']':
                            return -1;
                            //break;
                        case ',':
                            return 0;
                            //break;
                        case '"':
                            return 2;
                        default:
                            return -101;
                            //break;
                    }
                };

            Func<int, char[]> GetNowRetractChars = (retractCount) =>
                {
                    if(retractCount<0)
                    { return null; }
                    char[] retractChars = new char[retractCount];
                    for(int i =0;i<retractCount;i++)
                    {
                        retractChars[i] = jsonRetract;
                    }
                    return retractChars;
                };
            #endregion
           
            if (originalString==null)
            {
                return false;
            }

            StringBuilder jsonSb = new StringBuilder(originalString);  //不应该直接使用Insert/Remove反复操作StringBuilder，性能明显若与使用新的StringBuilder拼接
            StringBuilder FormatJsonSb = new StringBuilder((int)(originalString.Length * 1.2));
            int retractTypeRusult = 0;;
            for (int i = 0; i < jsonSb.Length;i++ )
            {
                if (isInString)
                {
                    if (jsonSb[i] == '"')
                    {
                        if(jsonSb[i-1]!='\\')
                        {
                            isInString = false;
                        }
                    }
                    FormatJsonSb.Append(jsonSb[i]);
                }
                else
                {
                    retractTypeRusult = GetNowRetractType(jsonSb[i]);
                    if (retractTypeRusult > -100)
                    {
                        if (retractTypeRusult == -99)
                        {
                            //jsonSb.Remove(i, 1);
                            //i--;
                            continue;
                        }
                        else if (retractTypeRusult == 1)
                        {
                            nowRetractCount++;
                            //jsonSb.Insert(i + 1, jsonNewLine);
                            //i += jsonNewLine.Length;
                            //jsonSb.Insert(i + 1, GetNowRetractChars(nowRetractCount));
                            //i += nowRetractCount;
                            FormatJsonSb.Append(jsonSb[i]);
                            FormatJsonSb.Append(jsonNewLine);
                            FormatJsonSb.Append(GetNowRetractChars(nowRetractCount));
                            
                        }
                        else if (retractTypeRusult == 0)
                        {
                            //jsonSb.Insert(i + 1, jsonNewLine);
                            //i += jsonNewLine.Length;
                            //jsonSb.Insert(i + 1, GetNowRetractChars(nowRetractCount));
                            //i += nowRetractCount;
                            FormatJsonSb.Append(jsonSb[i]);
                            FormatJsonSb.Append(jsonNewLine);
                            FormatJsonSb.Append(GetNowRetractChars(nowRetractCount));
                            
                        }
                        else if (retractTypeRusult == -1)
                        {
                            nowRetractCount--;
                            //jsonSb.Insert(i, jsonNewLine);
                            //i += jsonNewLine.Length;
                            //jsonSb.Insert(i, GetNowRetractChars(nowRetractCount));
                            //i += nowRetractCount;
                            FormatJsonSb.Append(jsonNewLine);
                            FormatJsonSb.Append(GetNowRetractChars(nowRetractCount));
                            FormatJsonSb.Append(jsonSb[i]);
                        }
                        else if (retractTypeRusult == 2)
                        {
                            FormatJsonSb.Append(jsonSb[i]);
                            if (jsonSb[i - 1] != '\\')
                            {
                                isInString = true;
                            }
                        }
                    }
                    else
                    {
                        FormatJsonSb.Append(jsonSb[i]);
                    }
                }
            }


            //formatString = jsonSb.ToString();
            formatString = FormatJsonSb.ToString();
            return true;;
        }

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
        public static void ChangeSelectionColor(RichTextBox rtb,Color fontColor)
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
        public static void ChangeSelectionColor(RichTextBox rtb,int startIndex,int len ,Color fontColor)
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
        public static void SetControlFreeze(Control yourCtr)
        {
            UnsafeNativeMethods.SendMessage(yourCtr.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 恢复控件刷新
        /// </summary>
        /// <param name="yourCtr">your Control</param>
        public static void SetControlUnfreeze(Control yourCtr)
        {
            UnsafeNativeMethods.SendMessage(yourCtr.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            yourCtr.Refresh();
        }

    }

    public class StringHelper
    {
        public static string EncodeXml(string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml))
            {
                return "";
            }
            strHtml = strHtml.Replace("&", "&amp;");
            strHtml = strHtml.Replace("<", "&lt;");
            strHtml = strHtml.Replace(">", "&gt;");
            strHtml = strHtml.Replace("'", "&apos;");
            strHtml = strHtml.Replace("\"", "&quot;");
            return strHtml;
        }
        public static string EncodeXmlEx(string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml))
            {
                return "";
            }
            return System.Web.HttpUtility.HtmlEncode(strHtml);
        }

        public static string StrListAdd(List<string> strList, string yourSplit)
        {
            StringBuilder myOurStb = new StringBuilder();
            if (strList != null)
            {
                for (int i = 0; i < strList.Count; i++)
                {
                    myOurStb.Append(strList[i]);
                    if (i != strList.Count - 1)
                    {
                        myOurStb.Append(yourSplit);
                    }
                }
            }
            return myOurStb.ToString();
        }
    }

    /// <summary>
    /// 控件动态服务，使用完成后请释放
    /// </summary>
    public class NewRoll
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
        internal NewRoll()
        {
            //默认构造函数
            mySpeed = 55;
            myLen = 1;
        }

        internal NewRoll(int speed, int len)
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


    /// <summary>
    /// 文件服务
    /// </summary>
    public class FileService
    {

        /* eg:
                FileStream fs = new FileStream(FilePathEx, File.Exists(FilePathEx) ? FileMode.Append : FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                swex.WriteLine("++++++++++++++++++++");
                sw.Close();
                fs.Close();
         * */
        // or  File.WriteAllBytes(saveFileName, bytesToSave);  or WriteAllText()
        

        /// <summary>
        /// 根据文件名返回文件路径（容错上可能受相对路径影响，未测试请勿使用可能出现错误的路径格式）
        /// </summary>
        /// <param name="yourPath">文件路径</param>
        /// <returns>文件夹路径</returns>
        public static string GetDirectory(string yourPath)
        {
            FileInfo fi = new FileInfo(yourPath);
            DirectoryInfo di = fi.Directory;
            return di.FullName;
        }

        /// <summary>
        /// 保存数据到文件【string】
        /// </summary>
        /// <param name="yourDate">需要保存的数据</param>
        /// <param name="yourPath">保存路径，如果为null即为默认路径</param>
        /// <param name="isAdd">是否使用追加模式</param>
        /// <param name="isAsLog">是否写入保存时间</param>
        /// <returns></returns>
        public static bool SaveFile(string yourDate, string yourPath, bool isAdd,bool isAsLog)
        {
            FileStream fs;
            if (yourPath == null)
            {
                yourPath = System.Windows.Forms.Application.StartupPath + "\\unknowLog\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\unknowLog\\"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\unknowLog\\");
                }
            }
            if (yourDate==null)
            {
                return false;
            }
            if (File.Exists(yourPath))
            {
                if (isAdd)
                {
                    fs = new FileStream(yourPath, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    for (int i = 0; i < 2000; i++)
                    {
                        if (!File.Exists(yourPath + ".bak" + i))
                        {
                            Directory.Move(yourPath, yourPath + ".bak" + i);
                            break;
                        }
                    }
                    fs = new FileStream(yourPath, FileMode.Create, FileAccess.Write); 
                }
            }
            else
            {
                fs = new FileStream(yourPath, FileMode.Create, FileAccess.Write); 
            }
            StreamWriter sw = new StreamWriter(fs);
            if(isAsLog)
            {
                sw.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            }
            sw.Write(yourDate);
            sw.Close();
            return true;
        }

        public static bool SaveFile(string yourDate,string yourPath)
        {
            return SaveFile(yourDate, yourPath, false, false);
        }

        /// <summary>
        /// 保存数据到文件【byte[]】
        /// </summary>
        /// <param name="yourDate"></param>
        /// <param name="yourPath"></param>
        /// <returns></returns>
        public static bool SaveFile(byte[] yourDate, string yourPath)
        {
            if (File.Exists(yourPath))
            {
                for (int i = 0; i < 2000; i++)
                {
                    if (!File.Exists(yourPath + ".bak" + i))
                    {
                        Directory.Move(yourPath, yourPath + ".bak" + i);
                        break;
                    }
                }
            }
            FileStream fs = new FileStream(yourPath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(yourDate);
            sw.Close();
            return true;
        }

        /// <summary>
        /// 使用追加的方式保存文件【string[]】
        /// </summary>
        /// <param name="DataRecordLines">要保存的数据</param>
        /// <param name="lineLen">保存的数量，0表示全部保存</param>
        /// <param name="yourPath">保存的路径，null表示使用默认路径</param>
        private void SaveDataRecord(string[] DataRecordLines, int lineLen, string yourPath)
        {
            FileStream fs;
            if (yourPath == null)
            {
                yourPath = System.Windows.Forms.Application.StartupPath + "\\unknowLog\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt"; 
            }
            if (File.Exists(yourPath))
            {
                fs = new FileStream(yourPath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(yourPath, FileMode.Create, FileAccess.Write);
            }
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            sw.WriteLine(DateTime.Now.ToString());
            sw.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            if (lineLen > DataRecordLines.Length || lineLen <= 0)
            {
                foreach (string tempData in DataRecordLines)
                {
                    sw.WriteLine(tempData);
                }
            }
            else
            {
                for (int i = 0; i < lineLen; i++)
                {
                    sw.WriteLine(DataRecordLines[i]);
                }
            }
            sw.Close();
        }

        public static bool SaveLogFile(string yourDate, string yourPath)
        {
            return SaveFile(yourDate, yourPath, true, true);
        }

        public static void CreateDirectory(string yourPath)
        {
            if (!Directory.Exists(yourPath))
            {
                Directory.CreateDirectory(yourPath);
            }
        }

        /// <summary>
        /// get all file in your path
        /// </summary>
        /// <param name="yourPath">your path</param>
        /// <returns>all file in path</returns>
        public static FileInfo[] GetAllFiles(string yourPath)
        {

            if (!Directory.Exists(yourPath))
            {
                return null;
            }
            DirectoryInfo theFolder = new DirectoryInfo(yourPath);
            return theFolder.GetFiles("*", SearchOption.AllDirectories);
        }

        public static void GetMyFiles(DirectoryInfo yourDirectory, out DirectoryInfo[] outDirectoryInfos, out FileInfo[] outFileInfos)
        {
            outDirectoryInfos = yourDirectory.GetDirectories();
            outFileInfos = yourDirectory.GetFiles();
        }
    }


    /// <summary>
    /// read and write ini file
    /// </summary>
    public class myini
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void IniWriteValue(string Section, string Key, string Value, string filepath)//对ini文件进行写操作的函数
        {
            try
            {
                WritePrivateProfileString(Section, Key, Value, filepath);
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog("ID:D314  " + ex.Message);
            }
        }

        public static string IniReadValue(string Section, string Key, string filepath)//对ini文件进行读操作的函数
        {
            StringBuilder temp = new StringBuilder(255);
            try
            {
                int i = GetPrivateProfileString(Section, Key, "", temp, 255, filepath);
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog("ID:D327  " + ex.Message);
            }
            return temp.ToString();
        }


    }

    /// <summary>
    /// put in error message in file
    /// </summary>
    [System.Runtime.Remoting.Contexts.Synchronization] //将该属性应用于某个对象时，在共享该属性实例的所有上下文中只能执行一个线程。
    public class ErrorLog
    {
        #region 内部成员
        private static string FilePath = System.Windows.Forms.Application.StartupPath + "\\log\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";       //log path
        private static bool isStart = false;
        private static string logNewLineFlag = "-------------------------------------------------------";
        private static bool isUsing = false;                                                                                                            //this log path may using where you call him
        private static FileStream fs;
        private static StreamWriter sw;
        private static List<string> unHandleLogs = new List<string>();                                                                                  //this log you not deal with
        #endregion

        static ErrorLog()
        {
            if(!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\log"))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\log");
            }
            Start();
        }

        /// <summary>
        /// 获取或设置一个值，表示当前日志是否启动
        /// </summary>
        public static bool IsStart
        {
            get { return isStart; }
            set
            {
                if(value)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            }
        }

        /// <summary>
        /// 启动日志
        /// </summary>
        public static void Start()
        {
            if (!isStart)
            {
                fs = new FileStream(FilePath, File.Exists(FilePath) ? FileMode.Append : FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                isStart = true;
            }
        }

        /// <summary>
        /// 关闭日志
        /// </summary>
        public static void Stop()
        {
            if (isStart)
            {
                closeLog();
                sw.Close();
                fs.Close();
                isStart = false;
            }
        }

        /// <summary>
        /// get now line
        /// </summary>
        /// <returns>line</returns>
        public static int GetLineNum(int skipFrames)
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(skipFrames, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// get now file
        /// </summary>
        /// <returns>file</returns>
        public static string GetCurSourceFileName(int skipFrames)
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(skipFrames, true);
            return st.GetFrame(0).GetFileName();
        }

        /// <summary>
        /// here i will save the log not handle 
        /// </summary>
        private static void savaUnHandleLogs()
        {
            if (!isStart)
            {
                return;
            }
            if (unHandleLogs.Count > 0)
            {
                if (isUsing == false)
                {
                    isUsing = true;
                    foreach (string tempLog in unHandleLogs)
                    {
                        sw.WriteLine(logNewLineFlag);
                        sw.WriteLine(tempLog);
                    }
                    sw.Flush();
                    unHandleLogs.Clear();
                    isUsing = false;
                }
            }
        }


        /// <summary>
        /// i will pu int log in the file with any other message 
        /// </summary>
        /// <param name="errorMessage"> your message</param>
        public static void PutInLog(string errorMessage)
        {
            if (!isStart)
            {
                return;
            }
            savaUnHandleLogs();
            if (isUsing == false)
            {
                isUsing = true;
                sw.WriteLine(logNewLineFlag);
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(string.Format("ErrorFile:{0}\r\nErrorLine:{1}\r\n", GetCurSourceFileName(2), GetLineNum(2)));
                sw.WriteLine(errorMessage);//开始写入值
                sw.Flush();
                isUsing = false;
            }
            else
            {
                unHandleLogs.Add(errorMessage);
            }

        }

        public static void PutInLog(Exception errorException)
        {
            if(!isStart)
            {
                return;
            }
            savaUnHandleLogs();
            if (isUsing == false)
            {
                isUsing = true;
                sw.WriteLine(logNewLineFlag);
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(errorException.Message);
                sw.WriteLine(errorException.StackTrace);
                sw.Flush();
                isUsing = false;
            }
            else
            {
                unHandleLogs.Add(string.Format("{0}/r/n{1}/r/n{2}/r/n", DateTime.Now.ToString(), errorException.Message, errorException.StackTrace));
            }

        }


        /// <summary>
        /// when you close your application you can call me to deal with the log you not put in the file 
        /// </summary>
        public static void closeLog()
        {
            savaUnHandleLogs();
            Stop();
        }
    }
}
