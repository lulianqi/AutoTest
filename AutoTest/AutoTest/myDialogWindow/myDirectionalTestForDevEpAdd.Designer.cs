namespace AutoTest.myDialogWindow
{
    partial class myDirectionalTestForDevEpAdd
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(myDirectionalTestForDevEpAdd));
            this.richTextBox_info = new System.Windows.Forms.RichTextBox();
            this.richTextBox_resData = new System.Windows.Forms.RichTextBox();
            this.listView_AllDataBack = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tb_GwKey = new System.Windows.Forms.TextBox();
            this.bt_Login = new System.Windows.Forms.Button();
            this.bt_startTest = new System.Windows.Forms.Button();
            this.tb_EpId = new System.Windows.Forms.TextBox();
            this.tb_EpName = new System.Windows.Forms.TextBox();
            this.lb_info_1 = new System.Windows.Forms.Label();
            this.lb_info_2 = new System.Windows.Forms.Label();
            this.lb_info_3 = new System.Windows.Forms.Label();
            this.timer_wait = new System.Windows.Forms.Timer(this.components);
            this.lb_info_4 = new System.Windows.Forms.Label();
            this.comboBox_waitTime = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_testInfo_5 = new System.Windows.Forms.Label();
            this.lb_info_9 = new System.Windows.Forms.Label();
            this.lb_testInfo_4 = new System.Windows.Forms.Label();
            this.lb_info_8 = new System.Windows.Forms.Label();
            this.lb_testInfo_3 = new System.Windows.Forms.Label();
            this.lb_info_7 = new System.Windows.Forms.Label();
            this.lb_testInfo_2 = new System.Windows.Forms.Label();
            this.lb_info_6 = new System.Windows.Forms.Label();
            this.lb_testInfo_1 = new System.Windows.Forms.Label();
            this.lb_info_5 = new System.Windows.Forms.Label();
            this.bt_add = new System.Windows.Forms.Button();
            this.bt_del = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox_info
            // 
            this.richTextBox_info.BackColor = System.Drawing.Color.Azure;
            this.richTextBox_info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_info.HideSelection = false;
            this.richTextBox_info.Location = new System.Drawing.Point(314, 1);
            this.richTextBox_info.Name = "richTextBox_info";
            this.richTextBox_info.Size = new System.Drawing.Size(406, 354);
            this.richTextBox_info.TabIndex = 22;
            this.richTextBox_info.Text = "";
            this.richTextBox_info.TextChanged += new System.EventHandler(this.richTextBox_info_TextChanged);
            // 
            // richTextBox_resData
            // 
            this.richTextBox_resData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox_resData.HideSelection = false;
            this.richTextBox_resData.Location = new System.Drawing.Point(0, 356);
            this.richTextBox_resData.Name = "richTextBox_resData";
            this.richTextBox_resData.Size = new System.Drawing.Size(909, 99);
            this.richTextBox_resData.TabIndex = 23;
            this.richTextBox_resData.Text = "";
            this.richTextBox_resData.TextChanged += new System.EventHandler(this.richTextBox_resData_TextChanged);
            // 
            // listView_AllDataBack
            // 
            this.listView_AllDataBack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView_AllDataBack.Location = new System.Drawing.Point(0, 1);
            this.listView_AllDataBack.Name = "listView_AllDataBack";
            this.listView_AllDataBack.Size = new System.Drawing.Size(311, 354);
            this.listView_AllDataBack.TabIndex = 24;
            this.listView_AllDataBack.UseCompatibleStateImageBehavior = false;
            this.listView_AllDataBack.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "项目";
            this.columnHeader1.Width = 87;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "内容";
            this.columnHeader2.Width = 311;
            // 
            // tb_GwKey
            // 
            this.tb_GwKey.Location = new System.Drawing.Point(791, 12);
            this.tb_GwKey.Name = "tb_GwKey";
            this.tb_GwKey.Size = new System.Drawing.Size(117, 21);
            this.tb_GwKey.TabIndex = 25;
            this.tb_GwKey.Text = "vanelife";
            // 
            // bt_Login
            // 
            this.bt_Login.Location = new System.Drawing.Point(833, 41);
            this.bt_Login.Name = "bt_Login";
            this.bt_Login.Size = new System.Drawing.Size(75, 23);
            this.bt_Login.TabIndex = 26;
            this.bt_Login.Text = "登录";
            this.bt_Login.UseVisualStyleBackColor = true;
            this.bt_Login.Click += new System.EventHandler(this.bt_Login_Click);
            // 
            // bt_startTest
            // 
            this.bt_startTest.Location = new System.Drawing.Point(832, 191);
            this.bt_startTest.Name = "bt_startTest";
            this.bt_startTest.Size = new System.Drawing.Size(75, 23);
            this.bt_startTest.TabIndex = 28;
            this.bt_startTest.Text = "开始";
            this.bt_startTest.UseVisualStyleBackColor = true;
            this.bt_startTest.Click += new System.EventHandler(this.bt_startTest_Click);
            // 
            // tb_EpId
            // 
            this.tb_EpId.Location = new System.Drawing.Point(791, 84);
            this.tb_EpId.Name = "tb_EpId";
            this.tb_EpId.Size = new System.Drawing.Size(117, 21);
            this.tb_EpId.TabIndex = 27;
            // 
            // tb_EpName
            // 
            this.tb_EpName.Location = new System.Drawing.Point(791, 111);
            this.tb_EpName.Name = "tb_EpName";
            this.tb_EpName.Size = new System.Drawing.Size(117, 21);
            this.tb_EpName.TabIndex = 29;
            // 
            // lb_info_1
            // 
            this.lb_info_1.AutoSize = true;
            this.lb_info_1.Location = new System.Drawing.Point(720, 12);
            this.lb_info_1.Name = "lb_info_1";
            this.lb_info_1.Size = new System.Drawing.Size(65, 12);
            this.lb_info_1.TabIndex = 30;
            this.lb_info_1.Text = "网关密码：";
            // 
            // lb_info_2
            // 
            this.lb_info_2.AutoSize = true;
            this.lb_info_2.Location = new System.Drawing.Point(720, 87);
            this.lb_info_2.Name = "lb_info_2";
            this.lb_info_2.Size = new System.Drawing.Size(53, 12);
            this.lb_info_2.TabIndex = 31;
            this.lb_info_2.Text = "分控ID：";
            // 
            // lb_info_3
            // 
            this.lb_info_3.AutoSize = true;
            this.lb_info_3.Location = new System.Drawing.Point(719, 120);
            this.lb_info_3.Name = "lb_info_3";
            this.lb_info_3.Size = new System.Drawing.Size(65, 12);
            this.lb_info_3.TabIndex = 32;
            this.lb_info_3.Text = "分控名称：";
            // 
            // timer_wait
            // 
            this.timer_wait.Interval = 1000;
            this.timer_wait.Tick += new System.EventHandler(this.timer_wait_Tick);
            // 
            // lb_info_4
            // 
            this.lb_info_4.AutoSize = true;
            this.lb_info_4.Location = new System.Drawing.Point(720, 147);
            this.lb_info_4.Name = "lb_info_4";
            this.lb_info_4.Size = new System.Drawing.Size(65, 12);
            this.lb_info_4.TabIndex = 33;
            this.lb_info_4.Text = "操作延时：";
            // 
            // comboBox_waitTime
            // 
            this.comboBox_waitTime.FormattingEnabled = true;
            this.comboBox_waitTime.Items.AddRange(new object[] {
            "1秒",
            "2秒",
            "3秒",
            "4秒",
            "5秒",
            "6秒",
            "7秒",
            "8秒",
            "9秒",
            "10秒"});
            this.comboBox_waitTime.Location = new System.Drawing.Point(789, 142);
            this.comboBox_waitTime.Name = "comboBox_waitTime";
            this.comboBox_waitTime.Size = new System.Drawing.Size(119, 20);
            this.comboBox_waitTime.TabIndex = 34;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_testInfo_5);
            this.groupBox1.Controls.Add(this.lb_info_9);
            this.groupBox1.Controls.Add(this.lb_testInfo_4);
            this.groupBox1.Controls.Add(this.lb_info_8);
            this.groupBox1.Controls.Add(this.lb_testInfo_3);
            this.groupBox1.Controls.Add(this.lb_info_7);
            this.groupBox1.Controls.Add(this.lb_testInfo_2);
            this.groupBox1.Controls.Add(this.lb_info_6);
            this.groupBox1.Controls.Add(this.lb_testInfo_1);
            this.groupBox1.Controls.Add(this.lb_info_5);
            this.groupBox1.Location = new System.Drawing.Point(723, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 126);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "结果统计";
            // 
            // lb_testInfo_5
            // 
            this.lb_testInfo_5.AutoSize = true;
            this.lb_testInfo_5.Location = new System.Drawing.Point(97, 92);
            this.lb_testInfo_5.Name = "lb_testInfo_5";
            this.lb_testInfo_5.Size = new System.Drawing.Size(29, 12);
            this.lb_testInfo_5.TabIndex = 9;
            this.lb_testInfo_5.Text = "null";
            // 
            // lb_info_9
            // 
            this.lb_info_9.AutoSize = true;
            this.lb_info_9.Location = new System.Drawing.Point(26, 93);
            this.lb_info_9.Name = "lb_info_9";
            this.lb_info_9.Size = new System.Drawing.Size(65, 12);
            this.lb_info_9.TabIndex = 8;
            this.lb_info_9.Text = "超时重试：";
            // 
            // lb_testInfo_4
            // 
            this.lb_testInfo_4.AutoSize = true;
            this.lb_testInfo_4.Location = new System.Drawing.Point(97, 72);
            this.lb_testInfo_4.Name = "lb_testInfo_4";
            this.lb_testInfo_4.Size = new System.Drawing.Size(29, 12);
            this.lb_testInfo_4.TabIndex = 7;
            this.lb_testInfo_4.Text = "null";
            // 
            // lb_info_8
            // 
            this.lb_info_8.AutoSize = true;
            this.lb_info_8.Location = new System.Drawing.Point(26, 72);
            this.lb_info_8.Name = "lb_info_8";
            this.lb_info_8.Size = new System.Drawing.Size(65, 12);
            this.lb_info_8.TabIndex = 6;
            this.lb_info_8.Text = "删除失败：";
            // 
            // lb_testInfo_3
            // 
            this.lb_testInfo_3.AutoSize = true;
            this.lb_testInfo_3.Location = new System.Drawing.Point(97, 55);
            this.lb_testInfo_3.Name = "lb_testInfo_3";
            this.lb_testInfo_3.Size = new System.Drawing.Size(29, 12);
            this.lb_testInfo_3.TabIndex = 5;
            this.lb_testInfo_3.Text = "null";
            // 
            // lb_info_7
            // 
            this.lb_info_7.AutoSize = true;
            this.lb_info_7.Location = new System.Drawing.Point(26, 55);
            this.lb_info_7.Name = "lb_info_7";
            this.lb_info_7.Size = new System.Drawing.Size(65, 12);
            this.lb_info_7.TabIndex = 4;
            this.lb_info_7.Text = "成功删除：";
            // 
            // lb_testInfo_2
            // 
            this.lb_testInfo_2.AutoSize = true;
            this.lb_testInfo_2.Location = new System.Drawing.Point(97, 36);
            this.lb_testInfo_2.Name = "lb_testInfo_2";
            this.lb_testInfo_2.Size = new System.Drawing.Size(29, 12);
            this.lb_testInfo_2.TabIndex = 3;
            this.lb_testInfo_2.Text = "null";
            // 
            // lb_info_6
            // 
            this.lb_info_6.AutoSize = true;
            this.lb_info_6.Location = new System.Drawing.Point(26, 36);
            this.lb_info_6.Name = "lb_info_6";
            this.lb_info_6.Size = new System.Drawing.Size(65, 12);
            this.lb_info_6.TabIndex = 2;
            this.lb_info_6.Text = "添加失败：";
            // 
            // lb_testInfo_1
            // 
            this.lb_testInfo_1.AutoSize = true;
            this.lb_testInfo_1.Location = new System.Drawing.Point(97, 17);
            this.lb_testInfo_1.Name = "lb_testInfo_1";
            this.lb_testInfo_1.Size = new System.Drawing.Size(29, 12);
            this.lb_testInfo_1.TabIndex = 1;
            this.lb_testInfo_1.Text = "null";
            // 
            // lb_info_5
            // 
            this.lb_info_5.AutoSize = true;
            this.lb_info_5.Location = new System.Drawing.Point(26, 17);
            this.lb_info_5.Name = "lb_info_5";
            this.lb_info_5.Size = new System.Drawing.Size(65, 12);
            this.lb_info_5.TabIndex = 0;
            this.lb_info_5.Text = "成功添加：";
            // 
            // bt_add
            // 
            this.bt_add.Location = new System.Drawing.Point(737, 165);
            this.bt_add.Name = "bt_add";
            this.bt_add.Size = new System.Drawing.Size(75, 23);
            this.bt_add.TabIndex = 36;
            this.bt_add.Text = "添加";
            this.bt_add.UseVisualStyleBackColor = true;
            this.bt_add.Click += new System.EventHandler(this.bt_add_Click);
            // 
            // bt_del
            // 
            this.bt_del.Location = new System.Drawing.Point(833, 164);
            this.bt_del.Name = "bt_del";
            this.bt_del.Size = new System.Drawing.Size(75, 23);
            this.bt_del.TabIndex = 37;
            this.bt_del.Text = "删除";
            this.bt_del.UseVisualStyleBackColor = true;
            this.bt_del.Click += new System.EventHandler(this.bt_del_Click);
            // 
            // myDirectionalTestForDevEpAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 455);
            this.Controls.Add(this.bt_del);
            this.Controls.Add(this.bt_add);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox_waitTime);
            this.Controls.Add(this.lb_info_4);
            this.Controls.Add(this.lb_info_3);
            this.Controls.Add(this.lb_info_2);
            this.Controls.Add(this.lb_info_1);
            this.Controls.Add(this.tb_EpName);
            this.Controls.Add(this.bt_startTest);
            this.Controls.Add(this.tb_EpId);
            this.Controls.Add(this.bt_Login);
            this.Controls.Add(this.tb_GwKey);
            this.Controls.Add(this.listView_AllDataBack);
            this.Controls.Add(this.richTextBox_resData);
            this.Controls.Add(this.richTextBox_info);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(925, 494);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(925, 494);
            this.Name = "myDirectionalTestForDevEpAdd";
            this.Text = "myDirectionalTestForDevEpAdd";
            this.Load += new System.EventHandler(this.myDirectionalTestForDevEpAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_info;
        private System.Windows.Forms.RichTextBox richTextBox_resData;
        private System.Windows.Forms.ListView listView_AllDataBack;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox tb_GwKey;
        private System.Windows.Forms.Button bt_Login;
        private System.Windows.Forms.Button bt_startTest;
        private System.Windows.Forms.TextBox tb_EpId;
        private System.Windows.Forms.TextBox tb_EpName;
        private System.Windows.Forms.Label lb_info_1;
        private System.Windows.Forms.Label lb_info_2;
        private System.Windows.Forms.Label lb_info_3;
        private System.Windows.Forms.Timer timer_wait;
        private System.Windows.Forms.Label lb_info_4;
        private System.Windows.Forms.ComboBox comboBox_waitTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_testInfo_1;
        private System.Windows.Forms.Label lb_info_5;
        private System.Windows.Forms.Label lb_testInfo_4;
        private System.Windows.Forms.Label lb_info_8;
        private System.Windows.Forms.Label lb_testInfo_3;
        private System.Windows.Forms.Label lb_info_7;
        private System.Windows.Forms.Label lb_testInfo_2;
        private System.Windows.Forms.Label lb_info_6;
        private System.Windows.Forms.Button bt_add;
        private System.Windows.Forms.Button bt_del;
        private System.Windows.Forms.Label lb_testInfo_5;
        private System.Windows.Forms.Label lb_info_9;

    }
}