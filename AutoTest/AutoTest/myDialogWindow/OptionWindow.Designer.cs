namespace AutoTest.myDialogWindow
{
    partial class OptionWindow
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
            this.bt_ow_other = new System.Windows.Forms.TextBox();
            this.lb_sw_infor3 = new System.Windows.Forms.Label();
            this.tb_ow_maxline = new System.Windows.Forms.TextBox();
            this.lb_sw_infor2 = new System.Windows.Forms.Label();
            this.tb_ow_waittime = new System.Windows.Forms.TextBox();
            this.lb_sw_infor1 = new System.Windows.Forms.Label();
            this.lb_sw_ok = new System.Windows.Forms.Button();
            this.bt_sw_cancel = new System.Windows.Forms.Button();
            this.lb_sw_infor4 = new System.Windows.Forms.Label();
            this.cb_ow_postDes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bt_ow_other
            // 
            this.bt_ow_other.BackColor = System.Drawing.Color.AliceBlue;
            this.bt_ow_other.Location = new System.Drawing.Point(98, 55);
            this.bt_ow_other.Name = "bt_ow_other";
            this.bt_ow_other.Size = new System.Drawing.Size(248, 21);
            this.bt_ow_other.TabIndex = 30;
            // 
            // lb_sw_infor3
            // 
            this.lb_sw_infor3.AutoSize = true;
            this.lb_sw_infor3.BackColor = System.Drawing.Color.Transparent;
            this.lb_sw_infor3.Location = new System.Drawing.Point(10, 58);
            this.lb_sw_infor3.Name = "lb_sw_infor3";
            this.lb_sw_infor3.Size = new System.Drawing.Size(83, 12);
            this.lb_sw_infor3.TabIndex = 29;
            this.lb_sw_infor3.Text = "文件存储路径:";
            // 
            // tb_ow_maxline
            // 
            this.tb_ow_maxline.BackColor = System.Drawing.Color.AliceBlue;
            this.tb_ow_maxline.Location = new System.Drawing.Point(98, 34);
            this.tb_ow_maxline.Name = "tb_ow_maxline";
            this.tb_ow_maxline.Size = new System.Drawing.Size(248, 21);
            this.tb_ow_maxline.TabIndex = 28;
            // 
            // lb_sw_infor2
            // 
            this.lb_sw_infor2.AutoSize = true;
            this.lb_sw_infor2.BackColor = System.Drawing.Color.Transparent;
            this.lb_sw_infor2.Location = new System.Drawing.Point(10, 37);
            this.lb_sw_infor2.Name = "lb_sw_infor2";
            this.lb_sw_infor2.Size = new System.Drawing.Size(83, 12);
            this.lb_sw_infor2.TabIndex = 27;
            this.lb_sw_infor2.Text = "最大记录行数:";
            // 
            // tb_ow_waittime
            // 
            this.tb_ow_waittime.BackColor = System.Drawing.Color.AliceBlue;
            this.tb_ow_waittime.Location = new System.Drawing.Point(98, 13);
            this.tb_ow_waittime.Name = "tb_ow_waittime";
            this.tb_ow_waittime.Size = new System.Drawing.Size(248, 21);
            this.tb_ow_waittime.TabIndex = 26;
            // 
            // lb_sw_infor1
            // 
            this.lb_sw_infor1.AutoSize = true;
            this.lb_sw_infor1.BackColor = System.Drawing.Color.Transparent;
            this.lb_sw_infor1.Location = new System.Drawing.Point(10, 16);
            this.lb_sw_infor1.Name = "lb_sw_infor1";
            this.lb_sw_infor1.Size = new System.Drawing.Size(83, 12);
            this.lb_sw_infor1.TabIndex = 25;
            this.lb_sw_infor1.Text = "执行等待时间:";
            // 
            // lb_sw_ok
            // 
            this.lb_sw_ok.Location = new System.Drawing.Point(269, 100);
            this.lb_sw_ok.Name = "lb_sw_ok";
            this.lb_sw_ok.Size = new System.Drawing.Size(75, 23);
            this.lb_sw_ok.TabIndex = 32;
            this.lb_sw_ok.Text = "确定";
            this.lb_sw_ok.UseVisualStyleBackColor = true;
            this.lb_sw_ok.Click += new System.EventHandler(this.lb_sw_ok_Click);
            // 
            // bt_sw_cancel
            // 
            this.bt_sw_cancel.Location = new System.Drawing.Point(178, 100);
            this.bt_sw_cancel.Name = "bt_sw_cancel";
            this.bt_sw_cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_sw_cancel.TabIndex = 31;
            this.bt_sw_cancel.Text = "取消";
            this.bt_sw_cancel.UseVisualStyleBackColor = true;
            this.bt_sw_cancel.Click += new System.EventHandler(this.bt_sw_cancel_Click);
            // 
            // lb_sw_infor4
            // 
            this.lb_sw_infor4.AutoSize = true;
            this.lb_sw_infor4.BackColor = System.Drawing.Color.Transparent;
            this.lb_sw_infor4.Location = new System.Drawing.Point(10, 79);
            this.lb_sw_infor4.Name = "lb_sw_infor4";
            this.lb_sw_infor4.Size = new System.Drawing.Size(83, 12);
            this.lb_sw_infor4.TabIndex = 33;
            this.lb_sw_infor4.Text = "POST数据位置:";
            // 
            // cb_ow_postDes
            // 
            this.cb_ow_postDes.AutoCompleteCustomSource.AddRange(new string[] {
            "HEAD",
            "BODY"});
            this.cb_ow_postDes.BackColor = System.Drawing.Color.AliceBlue;
            this.cb_ow_postDes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ow_postDes.FormattingEnabled = true;
            this.cb_ow_postDes.Items.AddRange(new object[] {
            "body",
            "Head"});
            this.cb_ow_postDes.Location = new System.Drawing.Point(98, 76);
            this.cb_ow_postDes.Name = "cb_ow_postDes";
            this.cb_ow_postDes.Size = new System.Drawing.Size(248, 20);
            this.cb_ow_postDes.TabIndex = 34;
            // 
            // OptionWindow
            // 
            this.ClientSize = new System.Drawing.Size(358, 169);
            this.Controls.Add(this.cb_ow_postDes);
            this.Controls.Add(this.lb_sw_infor4);
            this.Controls.Add(this.lb_sw_ok);
            this.Controls.Add(this.bt_sw_cancel);
            this.Controls.Add(this.bt_ow_other);
            this.Controls.Add(this.lb_sw_infor3);
            this.Controls.Add(this.tb_ow_maxline);
            this.Controls.Add(this.lb_sw_infor2);
            this.Controls.Add(this.tb_ow_waittime);
            this.Controls.Add(this.lb_sw_infor1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditCasebody";
            this.Load += new System.EventHandler(this.EditCasebody_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox bt_ow_other;
        private System.Windows.Forms.Label lb_sw_infor3;
        private System.Windows.Forms.TextBox tb_ow_maxline;
        private System.Windows.Forms.Label lb_sw_infor2;
        private System.Windows.Forms.TextBox tb_ow_waittime;
        private System.Windows.Forms.Label lb_sw_infor1;
        private System.Windows.Forms.Button lb_sw_ok;
        private System.Windows.Forms.Button bt_sw_cancel;
        private System.Windows.Forms.Label lb_sw_infor4;
        private System.Windows.Forms.ComboBox cb_ow_postDes;

    }
}