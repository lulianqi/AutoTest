namespace AutoTest.myDialogWindow
{
    partial class AddRunner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_openFile = new System.Windows.Forms.Button();
            this.tb_caseFilePath = new System.Windows.Forms.TextBox();
            this.bt_loadFile = new System.Windows.Forms.Button();
            this.lb_infor_1 = new System.Windows.Forms.Label();
            this.openFileDialog_caseFile = new System.Windows.Forms.OpenFileDialog();
            this.rtb_info = new System.Windows.Forms.RichTextBox();
            this.cb_pList = new System.Windows.Forms.ComboBox();
            this.cb_cList = new System.Windows.Forms.ComboBox();
            this.lb_infor_2 = new System.Windows.Forms.Label();
            this.lb_infor_3 = new System.Windows.Forms.Label();
            this.lb_infor_4 = new System.Windows.Forms.Label();
            this.tb_runnerName = new System.Windows.Forms.TextBox();
            this.lb_infor_6 = new System.Windows.Forms.Label();
            this.lb_infor_5 = new System.Windows.Forms.Label();
            this.tb_waitTime = new System.Windows.Forms.TextBox();
            this.pictureBox_AddRunner = new System.Windows.Forms.PictureBox();
            this.pictureBox_Cancel = new System.Windows.Forms.PictureBox();
            this.lb_infor_7 = new System.Windows.Forms.Label();
            this.tb_cloneNum = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AddRunner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Cancel)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_openFile
            // 
            this.bt_openFile.BackColor = System.Drawing.Color.White;
            this.bt_openFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_openFile.Location = new System.Drawing.Point(528, 54);
            this.bt_openFile.Name = "bt_openFile";
            this.bt_openFile.Size = new System.Drawing.Size(43, 21);
            this.bt_openFile.TabIndex = 21;
            this.bt_openFile.Text = "浏览";
            this.bt_openFile.UseVisualStyleBackColor = false;
            this.bt_openFile.Click += new System.EventHandler(this.bt_openFile_Click);
            // 
            // tb_caseFilePath
            // 
            this.tb_caseFilePath.BackColor = System.Drawing.Color.Beige;
            this.tb_caseFilePath.Location = new System.Drawing.Point(10, 54);
            this.tb_caseFilePath.Name = "tb_caseFilePath";
            this.tb_caseFilePath.Size = new System.Drawing.Size(520, 21);
            this.tb_caseFilePath.TabIndex = 20;
            // 
            // bt_loadFile
            // 
            this.bt_loadFile.Location = new System.Drawing.Point(10, 92);
            this.bt_loadFile.Name = "bt_loadFile";
            this.bt_loadFile.Size = new System.Drawing.Size(73, 23);
            this.bt_loadFile.TabIndex = 19;
            this.bt_loadFile.Text = "加载文件";
            this.bt_loadFile.UseVisualStyleBackColor = true;
            this.bt_loadFile.Click += new System.EventHandler(this.bt_loadFile_Click);
            // 
            // lb_infor_1
            // 
            this.lb_infor_1.AutoSize = true;
            this.lb_infor_1.Location = new System.Drawing.Point(13, 30);
            this.lb_infor_1.Name = "lb_infor_1";
            this.lb_infor_1.Size = new System.Drawing.Size(65, 12);
            this.lb_infor_1.TabIndex = 22;
            this.lb_infor_1.Text = "请选择文件";
            // 
            // openFileDialog_caseFile
            // 
            this.openFileDialog_caseFile.Filter = "测试用例|*.xml";
            // 
            // rtb_info
            // 
            this.rtb_info.BackColor = System.Drawing.Color.Snow;
            this.rtb_info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_info.HideSelection = false;
            this.rtb_info.Location = new System.Drawing.Point(99, 92);
            this.rtb_info.Name = "rtb_info";
            this.rtb_info.Size = new System.Drawing.Size(469, 138);
            this.rtb_info.TabIndex = 23;
            this.rtb_info.Text = "";
            // 
            // cb_pList
            // 
            this.cb_pList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_pList.FormattingEnabled = true;
            this.cb_pList.Location = new System.Drawing.Point(174, 271);
            this.cb_pList.Name = "cb_pList";
            this.cb_pList.Size = new System.Drawing.Size(110, 20);
            this.cb_pList.TabIndex = 24;
            this.cb_pList.SelectedIndexChanged += new System.EventHandler(this.cb_pList_SelectedIndexChanged);
            // 
            // cb_cList
            // 
            this.cb_cList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cList.FormattingEnabled = true;
            this.cb_cList.Location = new System.Drawing.Point(356, 271);
            this.cb_cList.Name = "cb_cList";
            this.cb_cList.Size = new System.Drawing.Size(110, 20);
            this.cb_cList.TabIndex = 25;
            this.cb_cList.SelectedIndexChanged += new System.EventHandler(this.cb_pList_SelectedIndexChanged);
            // 
            // lb_infor_2
            // 
            this.lb_infor_2.AutoSize = true;
            this.lb_infor_2.Location = new System.Drawing.Point(96, 275);
            this.lb_infor_2.Name = "lb_infor_2";
            this.lb_infor_2.Size = new System.Drawing.Size(77, 12);
            this.lb_infor_2.TabIndex = 26;
            this.lb_infor_2.Text = "PROJECT ID：";
            // 
            // lb_infor_3
            // 
            this.lb_infor_3.AutoSize = true;
            this.lb_infor_3.Location = new System.Drawing.Point(294, 274);
            this.lb_infor_3.Name = "lb_infor_3";
            this.lb_infor_3.Size = new System.Drawing.Size(59, 12);
            this.lb_infor_3.TabIndex = 27;
            this.lb_infor_3.Text = "CASE ID：";
            // 
            // lb_infor_4
            // 
            this.lb_infor_4.AutoSize = true;
            this.lb_infor_4.Location = new System.Drawing.Point(9, 274);
            this.lb_infor_4.Name = "lb_infor_4";
            this.lb_infor_4.Size = new System.Drawing.Size(77, 12);
            this.lb_infor_4.TabIndex = 28;
            this.lb_infor_4.Text = "请选择起始项";
            // 
            // tb_runnerName
            // 
            this.tb_runnerName.Location = new System.Drawing.Point(93, 244);
            this.tb_runnerName.Name = "tb_runnerName";
            this.tb_runnerName.Size = new System.Drawing.Size(152, 21);
            this.tb_runnerName.TabIndex = 29;
            this.tb_runnerName.Text = "NU";
            // 
            // lb_infor_6
            // 
            this.lb_infor_6.AutoSize = true;
            this.lb_infor_6.Location = new System.Drawing.Point(9, 249);
            this.lb_infor_6.Name = "lb_infor_6";
            this.lb_infor_6.Size = new System.Drawing.Size(65, 12);
            this.lb_infor_6.TabIndex = 30;
            this.lb_infor_6.Text = "任务标识名";
            // 
            // lb_infor_5
            // 
            this.lb_infor_5.AutoSize = true;
            this.lb_infor_5.Location = new System.Drawing.Point(12, 304);
            this.lb_infor_5.Name = "lb_infor_5";
            this.lb_infor_5.Size = new System.Drawing.Size(77, 12);
            this.lb_infor_5.TabIndex = 32;
            this.lb_infor_5.Text = "额外等待时间";
            // 
            // tb_waitTime
            // 
            this.tb_waitTime.Location = new System.Drawing.Point(105, 300);
            this.tb_waitTime.Name = "tb_waitTime";
            this.tb_waitTime.Size = new System.Drawing.Size(91, 21);
            this.tb_waitTime.TabIndex = 31;
            this.tb_waitTime.Text = "0";
            this.tb_waitTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_waitTime_KeyPress);
            // 
            // pictureBox_AddRunner
            // 
            this.pictureBox_AddRunner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_AddRunner.Location = new System.Drawing.Point(476, 276);
            this.pictureBox_AddRunner.Name = "pictureBox_AddRunner";
            this.pictureBox_AddRunner.Size = new System.Drawing.Size(45, 41);
            this.pictureBox_AddRunner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_AddRunner.TabIndex = 33;
            this.pictureBox_AddRunner.TabStop = false;
            this.pictureBox_AddRunner.Click += new System.EventHandler(this.pictureBox_AddRunner_Click);
            this.pictureBox_AddRunner.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_AddRunner.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_Cancel
            // 
            this.pictureBox_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Cancel.Image = global::AutoTest.Properties.Resources._1187340;
            this.pictureBox_Cancel.Location = new System.Drawing.Point(528, 277);
            this.pictureBox_Cancel.Name = "pictureBox_Cancel";
            this.pictureBox_Cancel.Size = new System.Drawing.Size(45, 41);
            this.pictureBox_Cancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Cancel.TabIndex = 34;
            this.pictureBox_Cancel.TabStop = false;
            this.pictureBox_Cancel.Click += new System.EventHandler(this.pictureBox_Cancel_Click);
            this.pictureBox_Cancel.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_Cancel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // lb_infor_7
            // 
            this.lb_infor_7.AutoSize = true;
            this.lb_infor_7.Location = new System.Drawing.Point(244, 304);
            this.lb_infor_7.Name = "lb_infor_7";
            this.lb_infor_7.Size = new System.Drawing.Size(113, 12);
            this.lb_infor_7.TabIndex = 36;
            this.lb_infor_7.Text = "额外的克隆用户数量";
            // 
            // tb_cloneNum
            // 
            this.tb_cloneNum.Location = new System.Drawing.Point(374, 299);
            this.tb_cloneNum.Name = "tb_cloneNum";
            this.tb_cloneNum.Size = new System.Drawing.Size(91, 21);
            this.tb_cloneNum.TabIndex = 35;
            this.tb_cloneNum.Text = "0";
            this.tb_cloneNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_waitTime_KeyPress);
            // 
            // AddRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 330);
            this.Controls.Add(this.lb_infor_7);
            this.Controls.Add(this.tb_cloneNum);
            this.Controls.Add(this.pictureBox_Cancel);
            this.Controls.Add(this.pictureBox_AddRunner);
            this.Controls.Add(this.lb_infor_5);
            this.Controls.Add(this.tb_waitTime);
            this.Controls.Add(this.lb_infor_6);
            this.Controls.Add(this.tb_runnerName);
            this.Controls.Add(this.lb_infor_4);
            this.Controls.Add(this.lb_infor_3);
            this.Controls.Add(this.lb_infor_2);
            this.Controls.Add(this.cb_cList);
            this.Controls.Add(this.cb_pList);
            this.Controls.Add(this.rtb_info);
            this.Controls.Add(this.lb_infor_1);
            this.Controls.Add(this.bt_openFile);
            this.Controls.Add(this.tb_caseFilePath);
            this.Controls.Add(this.bt_loadFile);
            this.IsShowHideBox = false;
            this.MaximizeBox = false;
            this.Name = "AddRunner";
            this.Text = "AddRunner";
            this.WindowName = "AddRunner";
            this.Load += new System.EventHandler(this.AddRunner_Load);
            this.Controls.SetChildIndex(this.bt_loadFile, 0);
            this.Controls.SetChildIndex(this.tb_caseFilePath, 0);
            this.Controls.SetChildIndex(this.bt_openFile, 0);
            this.Controls.SetChildIndex(this.lb_infor_1, 0);
            this.Controls.SetChildIndex(this.rtb_info, 0);
            this.Controls.SetChildIndex(this.cb_pList, 0);
            this.Controls.SetChildIndex(this.cb_cList, 0);
            this.Controls.SetChildIndex(this.lb_infor_2, 0);
            this.Controls.SetChildIndex(this.lb_infor_3, 0);
            this.Controls.SetChildIndex(this.lb_infor_4, 0);
            this.Controls.SetChildIndex(this.tb_runnerName, 0);
            this.Controls.SetChildIndex(this.lb_infor_6, 0);
            this.Controls.SetChildIndex(this.tb_waitTime, 0);
            this.Controls.SetChildIndex(this.lb_infor_5, 0);
            this.Controls.SetChildIndex(this.pictureBox_AddRunner, 0);
            this.Controls.SetChildIndex(this.pictureBox_Cancel, 0);
            this.Controls.SetChildIndex(this.tb_cloneNum, 0);
            this.Controls.SetChildIndex(this.lb_infor_7, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AddRunner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Cancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_openFile;
        private System.Windows.Forms.TextBox tb_caseFilePath;
        private System.Windows.Forms.Button bt_loadFile;
        private System.Windows.Forms.Label lb_infor_1;
        private System.Windows.Forms.OpenFileDialog openFileDialog_caseFile;
        private System.Windows.Forms.RichTextBox rtb_info;
        private System.Windows.Forms.ComboBox cb_pList;
        private System.Windows.Forms.ComboBox cb_cList;
        private System.Windows.Forms.Label lb_infor_2;
        private System.Windows.Forms.Label lb_infor_3;
        private System.Windows.Forms.Label lb_infor_4;
        private System.Windows.Forms.TextBox tb_runnerName;
        private System.Windows.Forms.Label lb_infor_6;
        private System.Windows.Forms.Label lb_infor_5;
        private System.Windows.Forms.TextBox tb_waitTime;
        private System.Windows.Forms.PictureBox pictureBox_AddRunner;
        private System.Windows.Forms.PictureBox pictureBox_Cancel;
        private System.Windows.Forms.Label lb_infor_7;
        private System.Windows.Forms.TextBox tb_cloneNum;
    }
}