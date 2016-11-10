namespace AutoTest.myDialogWindow
{
    partial class RunnerSet
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
            this.lb_infor_5 = new System.Windows.Forms.Label();
            this.tb_waitTime = new System.Windows.Forms.TextBox();
            this.lb_infor_6 = new System.Windows.Forms.Label();
            this.tb_runnerName = new System.Windows.Forms.TextBox();
            this.lb_infor_4 = new System.Windows.Forms.Label();
            this.lb_infor_3 = new System.Windows.Forms.Label();
            this.lb_infor_2 = new System.Windows.Forms.Label();
            this.cb_cList = new System.Windows.Forms.ComboBox();
            this.cb_pList = new System.Windows.Forms.ComboBox();
            this.lb_sw_ok = new System.Windows.Forms.Button();
            this.bt_sw_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_infor_5
            // 
            this.lb_infor_5.AutoSize = true;
            this.lb_infor_5.Location = new System.Drawing.Point(288, 30);
            this.lb_infor_5.Name = "lb_infor_5";
            this.lb_infor_5.Size = new System.Drawing.Size(77, 12);
            this.lb_infor_5.TabIndex = 41;
            this.lb_infor_5.Text = "额外等待时间";
            // 
            // tb_waitTime
            // 
            this.tb_waitTime.Location = new System.Drawing.Point(381, 26);
            this.tb_waitTime.Name = "tb_waitTime";
            this.tb_waitTime.Size = new System.Drawing.Size(91, 21);
            this.tb_waitTime.TabIndex = 40;
            this.tb_waitTime.Text = "0";
            this.tb_waitTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_waitTime_KeyPress);
            // 
            // lb_infor_6
            // 
            this.lb_infor_6.AutoSize = true;
            this.lb_infor_6.Location = new System.Drawing.Point(16, 35);
            this.lb_infor_6.Name = "lb_infor_6";
            this.lb_infor_6.Size = new System.Drawing.Size(65, 12);
            this.lb_infor_6.TabIndex = 39;
            this.lb_infor_6.Text = "任务标识名";
            // 
            // tb_runnerName
            // 
            this.tb_runnerName.Location = new System.Drawing.Point(100, 30);
            this.tb_runnerName.Name = "tb_runnerName";
            this.tb_runnerName.Size = new System.Drawing.Size(152, 21);
            this.tb_runnerName.TabIndex = 38;
            this.tb_runnerName.Text = "NU";
            // 
            // lb_infor_4
            // 
            this.lb_infor_4.AutoSize = true;
            this.lb_infor_4.Location = new System.Drawing.Point(16, 60);
            this.lb_infor_4.Name = "lb_infor_4";
            this.lb_infor_4.Size = new System.Drawing.Size(77, 12);
            this.lb_infor_4.TabIndex = 37;
            this.lb_infor_4.Text = "请选择起始项";
            // 
            // lb_infor_3
            // 
            this.lb_infor_3.AutoSize = true;
            this.lb_infor_3.Location = new System.Drawing.Point(301, 60);
            this.lb_infor_3.Name = "lb_infor_3";
            this.lb_infor_3.Size = new System.Drawing.Size(59, 12);
            this.lb_infor_3.TabIndex = 36;
            this.lb_infor_3.Text = "CASE ID：";
            // 
            // lb_infor_2
            // 
            this.lb_infor_2.AutoSize = true;
            this.lb_infor_2.Location = new System.Drawing.Point(103, 61);
            this.lb_infor_2.Name = "lb_infor_2";
            this.lb_infor_2.Size = new System.Drawing.Size(77, 12);
            this.lb_infor_2.TabIndex = 35;
            this.lb_infor_2.Text = "PROJECT ID：";
            // 
            // cb_cList
            // 
            this.cb_cList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cList.FormattingEnabled = true;
            this.cb_cList.Location = new System.Drawing.Point(363, 57);
            this.cb_cList.Name = "cb_cList";
            this.cb_cList.Size = new System.Drawing.Size(110, 20);
            this.cb_cList.TabIndex = 34;
            // 
            // cb_pList
            // 
            this.cb_pList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_pList.FormattingEnabled = true;
            this.cb_pList.Location = new System.Drawing.Point(181, 57);
            this.cb_pList.Name = "cb_pList";
            this.cb_pList.Size = new System.Drawing.Size(110, 20);
            this.cb_pList.TabIndex = 33;
            this.cb_pList.SelectedIndexChanged += new System.EventHandler(this.cb_pList_SelectedIndexChanged);
            // 
            // lb_sw_ok
            // 
            this.lb_sw_ok.Location = new System.Drawing.Point(394, 92);
            this.lb_sw_ok.Name = "lb_sw_ok";
            this.lb_sw_ok.Size = new System.Drawing.Size(75, 23);
            this.lb_sw_ok.TabIndex = 43;
            this.lb_sw_ok.Text = "确定";
            this.lb_sw_ok.UseVisualStyleBackColor = true;
            this.lb_sw_ok.Click += new System.EventHandler(this.lb_sw_ok_Click);
            // 
            // bt_sw_cancel
            // 
            this.bt_sw_cancel.Location = new System.Drawing.Point(303, 92);
            this.bt_sw_cancel.Name = "bt_sw_cancel";
            this.bt_sw_cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_sw_cancel.TabIndex = 42;
            this.bt_sw_cancel.Text = "取消";
            this.bt_sw_cancel.UseVisualStyleBackColor = true;
            this.bt_sw_cancel.Click += new System.EventHandler(this.bt_sw_cancel_Click);
            // 
            // RunnerSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 145);
            this.Controls.Add(this.lb_sw_ok);
            this.Controls.Add(this.bt_sw_cancel);
            this.Controls.Add(this.lb_infor_5);
            this.Controls.Add(this.tb_waitTime);
            this.Controls.Add(this.lb_infor_6);
            this.Controls.Add(this.tb_runnerName);
            this.Controls.Add(this.lb_infor_4);
            this.Controls.Add(this.lb_infor_3);
            this.Controls.Add(this.lb_infor_2);
            this.Controls.Add(this.cb_cList);
            this.Controls.Add(this.cb_pList);
            this.Name = "RunnerSet";
            this.Text = "RunnerSet";
            this.Load += new System.EventHandler(this.RunnerSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_infor_5;
        private System.Windows.Forms.TextBox tb_waitTime;
        private System.Windows.Forms.Label lb_infor_6;
        private System.Windows.Forms.TextBox tb_runnerName;
        private System.Windows.Forms.Label lb_infor_4;
        private System.Windows.Forms.Label lb_infor_3;
        private System.Windows.Forms.Label lb_infor_2;
        private System.Windows.Forms.ComboBox cb_cList;
        private System.Windows.Forms.ComboBox cb_pList;
        private System.Windows.Forms.Button lb_sw_ok;
        private System.Windows.Forms.Button bt_sw_cancel;
    }
}