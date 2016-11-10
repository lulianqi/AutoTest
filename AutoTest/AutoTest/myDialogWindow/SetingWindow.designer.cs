namespace AutoTest.myDialogWindow
{
    partial class SetingWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetingWindow));
            this.bt_sw_secret = new System.Windows.Forms.TextBox();
            this.lb_sw_infor3 = new System.Windows.Forms.Label();
            this.tb_sw_key = new System.Windows.Forms.TextBox();
            this.lb_sw_infor2 = new System.Windows.Forms.Label();
            this.tb_sw_url = new System.Windows.Forms.TextBox();
            this.lb_sw_infor1 = new System.Windows.Forms.Label();
            this.lb_sw_ok = new System.Windows.Forms.Button();
            this.bt_sw_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_sw_secret
            // 
            this.bt_sw_secret.BackColor = System.Drawing.Color.AliceBlue;
            this.bt_sw_secret.Location = new System.Drawing.Point(57, 55);
            this.bt_sw_secret.Name = "bt_sw_secret";
            this.bt_sw_secret.Size = new System.Drawing.Size(359, 21);
            this.bt_sw_secret.TabIndex = 30;
            // 
            // lb_sw_infor3
            // 
            this.lb_sw_infor3.AutoSize = true;
            this.lb_sw_infor3.BackColor = System.Drawing.Color.Transparent;
            this.lb_sw_infor3.Location = new System.Drawing.Point(10, 58);
            this.lb_sw_infor3.Name = "lb_sw_infor3";
            this.lb_sw_infor3.Size = new System.Drawing.Size(47, 12);
            this.lb_sw_infor3.TabIndex = 29;
            this.lb_sw_infor3.Text = "Secret:";
            // 
            // tb_sw_key
            // 
            this.tb_sw_key.BackColor = System.Drawing.Color.AliceBlue;
            this.tb_sw_key.Location = new System.Drawing.Point(57, 34);
            this.tb_sw_key.Name = "tb_sw_key";
            this.tb_sw_key.Size = new System.Drawing.Size(359, 21);
            this.tb_sw_key.TabIndex = 28;
            // 
            // lb_sw_infor2
            // 
            this.lb_sw_infor2.AutoSize = true;
            this.lb_sw_infor2.BackColor = System.Drawing.Color.Transparent;
            this.lb_sw_infor2.Location = new System.Drawing.Point(10, 37);
            this.lb_sw_infor2.Name = "lb_sw_infor2";
            this.lb_sw_infor2.Size = new System.Drawing.Size(29, 12);
            this.lb_sw_infor2.TabIndex = 27;
            this.lb_sw_infor2.Text = "Key:";
            // 
            // tb_sw_url
            // 
            this.tb_sw_url.BackColor = System.Drawing.Color.AliceBlue;
            this.tb_sw_url.Location = new System.Drawing.Point(57, 13);
            this.tb_sw_url.Name = "tb_sw_url";
            this.tb_sw_url.Size = new System.Drawing.Size(359, 21);
            this.tb_sw_url.TabIndex = 26;
            // 
            // lb_sw_infor1
            // 
            this.lb_sw_infor1.AutoSize = true;
            this.lb_sw_infor1.BackColor = System.Drawing.Color.Transparent;
            this.lb_sw_infor1.Location = new System.Drawing.Point(10, 16);
            this.lb_sw_infor1.Name = "lb_sw_infor1";
            this.lb_sw_infor1.Size = new System.Drawing.Size(29, 12);
            this.lb_sw_infor1.TabIndex = 25;
            this.lb_sw_infor1.Text = "Url:";
            // 
            // lb_sw_ok
            // 
            this.lb_sw_ok.Location = new System.Drawing.Point(341, 82);
            this.lb_sw_ok.Name = "lb_sw_ok";
            this.lb_sw_ok.Size = new System.Drawing.Size(75, 23);
            this.lb_sw_ok.TabIndex = 32;
            this.lb_sw_ok.Text = "确定";
            this.lb_sw_ok.UseVisualStyleBackColor = true;
            this.lb_sw_ok.Click += new System.EventHandler(this.lb_sw_ok_Click);
            // 
            // bt_sw_cancel
            // 
            this.bt_sw_cancel.Location = new System.Drawing.Point(250, 82);
            this.bt_sw_cancel.Name = "bt_sw_cancel";
            this.bt_sw_cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_sw_cancel.TabIndex = 31;
            this.bt_sw_cancel.Text = "取消";
            this.bt_sw_cancel.UseVisualStyleBackColor = true;
            this.bt_sw_cancel.Click += new System.EventHandler(this.bt_sw_cancel_Click);
            // 
            // SetingWindow
            // 
            this.ClientSize = new System.Drawing.Size(432, 144);
            this.Controls.Add(this.lb_sw_ok);
            this.Controls.Add(this.bt_sw_cancel);
            this.Controls.Add(this.bt_sw_secret);
            this.Controls.Add(this.lb_sw_infor3);
            this.Controls.Add(this.tb_sw_key);
            this.Controls.Add(this.lb_sw_infor2);
            this.Controls.Add(this.tb_sw_url);
            this.Controls.Add(this.lb_sw_infor1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetingWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditCasebody";
            this.Load += new System.EventHandler(this.EditCasebody_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox bt_sw_secret;
        private System.Windows.Forms.Label lb_sw_infor3;
        private System.Windows.Forms.TextBox tb_sw_key;
        private System.Windows.Forms.Label lb_sw_infor2;
        private System.Windows.Forms.TextBox tb_sw_url;
        private System.Windows.Forms.Label lb_sw_infor1;
        private System.Windows.Forms.Button lb_sw_ok;
        private System.Windows.Forms.Button bt_sw_cancel;

    }
}