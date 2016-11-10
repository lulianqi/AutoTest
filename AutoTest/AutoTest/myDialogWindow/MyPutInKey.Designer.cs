namespace AutoTest.myDialogWindow
{
    partial class MyPutInKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPutInKey));
            this.tb_key = new System.Windows.Forms.TextBox();
            this.bt_ok = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.lb_info1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_key
            // 
            this.tb_key.Location = new System.Drawing.Point(101, 28);
            this.tb_key.Name = "tb_key";
            this.tb_key.Size = new System.Drawing.Size(255, 21);
            this.tb_key.TabIndex = 0;
            // 
            // bt_ok
            // 
            this.bt_ok.Location = new System.Drawing.Point(281, 55);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Size = new System.Drawing.Size(75, 23);
            this.bt_ok.TabIndex = 1;
            this.bt_ok.Text = "确认";
            this.bt_ok.UseVisualStyleBackColor = true;
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Location = new System.Drawing.Point(200, 55);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_cancel.TabIndex = 2;
            this.bt_cancel.Text = "取消";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // lb_info1
            // 
            this.lb_info1.AutoSize = true;
            this.lb_info1.Location = new System.Drawing.Point(26, 33);
            this.lb_info1.Name = "lb_info1";
            this.lb_info1.Size = new System.Drawing.Size(65, 12);
            this.lb_info1.TabIndex = 3;
            this.lb_info1.Text = "访问密码：";
            // 
            // MyPutInKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 109);
            this.Controls.Add(this.lb_info1);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.bt_ok);
            this.Controls.Add(this.tb_key);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyPutInKey";
            this.Text = "MyPutInKey";
            this.Load += new System.EventHandler(this.MyPutInKey_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_key;
        private System.Windows.Forms.Button bt_ok;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.Label lb_info1;
    }
}