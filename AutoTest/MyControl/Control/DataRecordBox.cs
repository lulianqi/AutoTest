using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MyCommonHelper;


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
    public partial class DataRecordBox : UserControl
    {
        public class ValuePair<T1, T2>
        {
            T1 value1;
            T2 value2;
            public ValuePair(T1 yourValue1,T2 yourValue2)
            {
                value1 = yourValue1;
                value2 = yourValue2;
            }
        }

        public DataRecordBox()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        /// <summary>
        /// 自定义初始化函数，加入后vs设计器有异常，控件正常运行
        /// </summary>
        private void MyInitializeComponent()
        {
            if (mianDirectory != "DataRecord")
            {
                defaultDirectory = System.Windows.Forms.Application.StartupPath + string.Format("\\{0}\\AutoSave\\", mianDirectory);
                defaultSavePath = System.Windows.Forms.Application.StartupPath + string.Format("\\{0}\\AutoSave\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt",mianDirectory);
                usersSavePath = System.Windows.Forms.Application.StartupPath + string.Format("\\{0}\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt", mianDirectory);
            }
            FileService.CreateDirectory(defaultDirectory);
        }

        public event Action<object, string> OnSaveDataHendle;

        private int maxLine = 5000;

        private Form showForm;

        /// <summary>
        /// 可用于显示的最大缓存行
        /// </summary>
       [DescriptionAttribute("可用于显示的最大缓存行")]
        public int MaxLine
        {
            get { return maxLine; }
            set
            {
                if (value > 2)
                {
                    maxLine = value;
                }
            }
        }

        private bool canFill = true;

        /// <summary>
        /// 右键菜单中是否支持最大化显示
        /// </summary>
        [DescriptionAttribute("右键菜单中是否支持最大化显示")]
        public bool CanFill
        {
            get { return canFill; }
            set { canFill = value; }
        }

        /// <summary>
        /// 默认存储的路径
        /// </summary>
        [DescriptionAttribute("文件保存的基础路径（相对执行文件的根目录，自动保存文件默认在该设置目录的AutoSave下）")]
        public string MianDirectory
        {
            get { return mianDirectory; }
            set { mianDirectory = value; }
        }
       

        private bool isPauseAdd = false;

        /// <summary>
        /// 是否停止追加文本
        /// </summary>
        public bool IsPauseAdd
        {
            get { return isPauseAdd; }
        }

        private bool isAlwaysGoBottom = true;

        /// <summary>
        /// 是否让追加文本显示一直显示
        /// </summary>
        public bool IsAlwaysGoBottom
        {
            get { return isAlwaysGoBottom; }
        }

        private bool isAutoSave = false;

        /// <summary>
        /// 是否开启自动保存记录
        /// </summary>
        public bool IsAutoSave
        {
            get { return isAutoSave; }
        }

        /// <summary>
        /// 是否处于填充显示状态
        /// </summary>
        private bool isBoxFill = false;
    
        public bool IsBoxFill
        {
            get { return isBoxFill; }
        }

        private string mianDirectory = "DataRecord";

        private string defaultDirectory = System.Windows.Forms.Application.StartupPath + "\\DataRecord\\AutoSave\\";

        private string defaultSavePath = System.Windows.Forms.Application.StartupPath + "\\DataRecord\\AutoSave\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";

        private string usersSavePath = System.Windows.Forms.Application.StartupPath + "\\DataRecord\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";   
  
        private List<ValuePair<string, Color>> puaseLines = new List<ValuePair<string, Color>>();


        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
             ((PictureBox)sender).BackColor = Color.Honeydew;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
             ((PictureBox)sender).BackColor = Color.Transparent;
        }

        private void pictureBox_dataAddclean_Click(object sender, EventArgs e)
        {
            if(isAutoSave)
            {
                SaveDataRecord(richTextBox_dataContainer.Lines, 0);
            }
            richTextBox_dataContainer.Clear();
        }

        private void pictureBox_dataAddSave_Click(object sender, EventArgs e)
        {
            if(OnSaveDataHendle!=null)
            {
                this.OnSaveDataHendle(this, richTextBox_dataContainer.Text);
            }
            else
            {
                if (richTextBox_dataContainer.Text == "")
                {
                    MessageBox.Show("未发现任何可用数据","STOP");
                }
                else
                {
                    SaveDataRecord(richTextBox_dataContainer.Lines, 0, usersSavePath);
                    MessageBox.Show("保存成功，请到DataRecord下查看");
                }
            }
        }

        private void pictureBox_pictureBox_AlwaysGoBottom_Click(object sender, EventArgs e)
        {
            ChangeAlwaysGoBottom(!isAlwaysGoBottom);
            if (isAlwaysGoBottom)
            {
                pictureBox_AlwaysGoBottom.Image = imageListForButton.Images[0];
            }
            else
            {
                pictureBox_AlwaysGoBottom.Image = imageListForButton.Images[1];
            }
        }

        private void FreezeText_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isPauseAdd)
            {
                ((ToolStripMenuItem)sender).Text = "冻结追加显示";
            }
            else
            {
                ((ToolStripMenuItem)sender).Text = "继续追加显示";
            }
            ChangePauseAdd(!isPauseAdd);
        }

        private void AutoSave_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isAutoSave)
            {
                ((ToolStripMenuItem)sender).Text = "自动保存内容";
            }
            else
            {
                ((ToolStripMenuItem)sender).Text = "关闭自动保存";
            }
            ChangeAntoSave(!isAutoSave);
        }

        private void Fill_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isBoxFill)
            {
                ((ToolStripMenuItem)sender).Text = "最大化显示";
            }
            else
            {
                ((ToolStripMenuItem)sender).Text = "恢复默认显示";
            }
            ChangeBoxFill(!isBoxFill);
        }


        private void ShowInNewWindow_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showForm!=null)
            {
                if (!showForm.IsDisposed)
                {
                    MessageBox.Show("已经在新窗口中显示", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            InitializeShowForm();
            PutOutShowForm(); 
        }


        private void InitializeShowForm()
        {
            showForm = new Form();
            showForm.WindowState = System.Windows.Forms.FormWindowState.Maximized; 
            showForm.Icon = MyCommonControl.Properties.Resources.full_page;
            showForm.FormClosing += showForm_FormClosing;

        }

        void showForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            showForm.Controls.Remove(richTextBox_dataContainer);
            this.Controls.Add(richTextBox_dataContainer);
            richTextBox_dataContainer.Dock = DockStyle.Fill;
        }

        private void PutOutShowForm()
        {
            showForm.Controls.Add(richTextBox_dataContainer);
            richTextBox_dataContainer.Dock = DockStyle.Fill;
            showForm.Show();
        }


        /// <summary>
        /// 向容器中添加数据
        /// </summary>
        /// <param name="yourStr">内容</param>
        /// <param name="fontColor">颜色</param>
        /// <param name="isNewLine">是否为新行</param>
        public void AddDate(string yourStr, Color fontColor, bool isNewLine)
        {
            if (isPauseAdd)
            {
                puaseLines.Add(new ValuePair<string, Color>(yourStr, fontColor));
                if (puaseLines.Count>maxLine)
                {
                    ValuePair<string, Color>[] tempLine = new ValuePair<string, Color>[maxLine/2];
                    puaseLines.CopyTo((maxLine+1) / 2, tempLine, 0, maxLine / 2);
                    puaseLines = new List<ValuePair<string, Color>>(tempLine);
                }
            }
            else
            {
                if (isAlwaysGoBottom)
                {
                    MyCommonHelper.MyCommonTool.setRichTextBoxContent(ref this.richTextBox_dataContainer, yourStr, fontColor, isNewLine);
                }
                else
                {
                    MyCommonTool.setRichTextBoxContent(ref this.richTextBox_dataContainer, yourStr, fontColor, isNewLine, true);
                }
                if(richTextBox_dataContainer.Lines.Length>maxLine)
                {
                    string[] tempLines = richTextBox_dataContainer.Lines;
                    int tempDropLineLen = tempLines.Length - maxLine / 2;
                    if (isAutoSave)
                    {
                        SaveDataRecord(tempLines, tempDropLineLen);
                    }
                    Array.Copy(tempLines, tempDropLineLen, tempLines, 0, maxLine / 2);
                    Array.Resize(ref tempLines, maxLine / 2);
                    MyCommonTool.SetControlFreeze(richTextBox_dataContainer);
                    richTextBox_dataContainer.Lines = tempLines;
                    richTextBox_dataContainer.SelectAll();
                    richTextBox_dataContainer.SelectionColor = Color.Gray;
                    richTextBox_dataContainer.DeselectAll();
                    MyCommonTool.SetControlUnfreeze(richTextBox_dataContainer);
                }
                
            }
        }

        private void ChangeAntoSave(bool isSave)
        {
            isAutoSave = isSave;
        }

        private void ChangePauseAdd(bool isPause)
        {
            isPauseAdd = isPause;
        }

        private void ChangeAlwaysGoBottom(bool isFreeze)
        {
            isAlwaysGoBottom = isFreeze;
            if(isAlwaysGoBottom)
            {
                richTextBox_dataContainer.HideSelection = false;
            }
            else
            {
                richTextBox_dataContainer.HideSelection = true;
            }
        }

        private void ChangeBoxFill(bool isFill)
        {
            isBoxFill = isFill;
            if(isBoxFill)
            {
                this.Dock = DockStyle.Fill;
                //this.BringToFront();

            }
            else
            {
                this.Dock = DockStyle.None;
                //this.Parent.Controls.SetChildIndex(this, this.Parent.Controls.GetChildIndex(this.Parent));
                //this.Parent.Controls.SetChildIndex(this, 0);
                //this.SendToBack();
            }
        }

        private void SaveDataRecord(string[] DataRecordLines )
        {
            SaveDataRecord(DataRecordLines, DataRecordLines.Length, null);
        }

        private void SaveDataRecord(string[] DataRecordLines, int lineLen)
        {
            SaveDataRecord(DataRecordLines, lineLen, null);
        }

        /// <summary>
        /// 使用追加的方式保存文件
        /// </summary>
        /// <param name="DataRecordLines">要保存的数据</param>
        /// <param name="lineLen">保存的数量，0表示全部保存</param>
        /// <param name="yourPath">保存的路径，null表示使用默认路径</param>
        private void SaveDataRecord(string[] DataRecordLines, int lineLen, string yourPath)
        {
            FileStream fs;
            if (yourPath == null)
            {
                yourPath = defaultSavePath;
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
            if (lineLen > DataRecordLines.Length || lineLen<=0)
            {
                foreach (string tempData in DataRecordLines)
                {
                    sw.WriteLine(tempData);
                }
            }
            else
            {
                for (int i=0; i<lineLen;i++)
                {
                    sw.WriteLine(DataRecordLines[i]);
                }
            }
            sw.Close();
            fs.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (isAutoSave)
            {
                SaveDataRecord(richTextBox_dataContainer.Lines, 0);
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        

        /*
         * 使用API选择丢弃更新，为最佳手段
         * 如果所有更新都通过AppendText完成，【方法2】优先
         * 若不确定使用者会如何添加文本，【方法3】优先
         * 【方法1】最后不推荐使用
        private void trb_addRecord_TextChanged(object sender, EventArgs e)
        {
            //老方法会闪屏【方法1】
            //trb_addRecord.Focus();
            //trb_addRecord.SelectionStart = trb_addRecord.Text.Length;
            //trb_addRecord.ScrollToCaret();

            //若没有其他填充逻辑可以直接创建填以下充函数【方法2】
            //rtb_info.AppendText(tempStr + "\n");
            //rtb_info.Focus();
            //Application.DoEvents();

            //定位到尾行【方法3】
            trb_addRecord.SelectionStart = trb_addRecord.Text.Length;
            //设置trb_addRecord.HideSelection = false;，即可以不用使用.Focus()
            //trb_addRecord.Focus();
            Application.DoEvents();

        }
         * */

    }
}
