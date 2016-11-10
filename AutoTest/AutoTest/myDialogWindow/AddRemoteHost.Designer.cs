namespace AutoTest.myDialogWindow
{
    partial class AddRemoteHost
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
            this.tb_hostAddress = new System.Windows.Forms.TextBox();
            this.label_info1 = new System.Windows.Forms.Label();
            this.bt_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_hostAddress
            // 
            this.tb_hostAddress.Location = new System.Drawing.Point(89, 36);
            this.tb_hostAddress.Name = "tb_hostAddress";
            this.tb_hostAddress.Size = new System.Drawing.Size(360, 21);
            this.tb_hostAddress.TabIndex = 14;
            this.tb_hostAddress.Text = "http://localhost:8087/SelService";
            // 
            // label_info1
            // 
            this.label_info1.AutoSize = true;
            this.label_info1.Location = new System.Drawing.Point(12, 40);
            this.label_info1.Name = "label_info1";
            this.label_info1.Size = new System.Drawing.Size(53, 12);
            this.label_info1.TabIndex = 15;
            this.label_info1.Text = "主机地址";
            // 
            // bt_ok
            // 
            this.bt_ok.Location = new System.Drawing.Point(374, 63);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Size = new System.Drawing.Size(75, 23);
            this.bt_ok.TabIndex = 16;
            this.bt_ok.Text = "确认";
            this.bt_ok.UseVisualStyleBackColor = true;
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // AddRemoteHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 95);
            this.Controls.Add(this.bt_ok);
            this.Controls.Add(this.label_info1);
            this.Controls.Add(this.tb_hostAddress);
            this.IsShowHideBox = false;
            this.MaximizeBox = false;
            this.Name = "AddRemoteHost";
            this.Text = "AddRemoteHost";
            this.WindowName = "AddRemoteHost";
            this.Controls.SetChildIndex(this.tb_hostAddress, 0);
            this.Controls.SetChildIndex(this.label_info1, 0);
            this.Controls.SetChildIndex(this.bt_ok, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_hostAddress;
        private System.Windows.Forms.Label label_info1;
        private System.Windows.Forms.Button bt_ok;
    }
}