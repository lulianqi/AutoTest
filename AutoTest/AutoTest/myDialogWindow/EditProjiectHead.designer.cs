namespace AutoTest.myDialogWindow
{
    partial class EditProjiectHead
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditProjiectHead));
            this.lb_dw1_infor1 = new System.Windows.Forms.Label();
            this.lb_dw1_infor2 = new System.Windows.Forms.Label();
            this.tb_dw1_ProjectName = new System.Windows.Forms.TextBox();
            this.rtb_dw1_ProjectRemark = new System.Windows.Forms.RichTextBox();
            this.bt_dw1_cancel = new System.Windows.Forms.Button();
            this.lb_dw1_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_dw1_infor1
            // 
            this.lb_dw1_infor1.AutoSize = true;
            this.lb_dw1_infor1.BackColor = System.Drawing.Color.Transparent;
            this.lb_dw1_infor1.Location = new System.Drawing.Point(17, 23);
            this.lb_dw1_infor1.Name = "lb_dw1_infor1";
            this.lb_dw1_infor1.Size = new System.Drawing.Size(65, 12);
            this.lb_dw1_infor1.TabIndex = 0;
            this.lb_dw1_infor1.Text = "工程名称：";
            // 
            // lb_dw1_infor2
            // 
            this.lb_dw1_infor2.AutoSize = true;
            this.lb_dw1_infor2.BackColor = System.Drawing.Color.Transparent;
            this.lb_dw1_infor2.Location = new System.Drawing.Point(17, 52);
            this.lb_dw1_infor2.Name = "lb_dw1_infor2";
            this.lb_dw1_infor2.Size = new System.Drawing.Size(65, 12);
            this.lb_dw1_infor2.TabIndex = 1;
            this.lb_dw1_infor2.Text = "工程备注：";
            // 
            // tb_dw1_ProjectName
            // 
            this.tb_dw1_ProjectName.Location = new System.Drawing.Point(79, 20);
            this.tb_dw1_ProjectName.Name = "tb_dw1_ProjectName";
            this.tb_dw1_ProjectName.Size = new System.Drawing.Size(317, 21);
            this.tb_dw1_ProjectName.TabIndex = 2;
            // 
            // rtb_dw1_ProjectRemark
            // 
            this.rtb_dw1_ProjectRemark.Location = new System.Drawing.Point(79, 47);
            this.rtb_dw1_ProjectRemark.Name = "rtb_dw1_ProjectRemark";
            this.rtb_dw1_ProjectRemark.Size = new System.Drawing.Size(317, 47);
            this.rtb_dw1_ProjectRemark.TabIndex = 3;
            this.rtb_dw1_ProjectRemark.Text = "";
            // 
            // bt_dw1_cancel
            // 
            this.bt_dw1_cancel.Location = new System.Drawing.Point(230, 100);
            this.bt_dw1_cancel.Name = "bt_dw1_cancel";
            this.bt_dw1_cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_dw1_cancel.TabIndex = 4;
            this.bt_dw1_cancel.Text = "取消";
            this.bt_dw1_cancel.UseVisualStyleBackColor = true;
            this.bt_dw1_cancel.Click += new System.EventHandler(this.bt_dw1_cancel_Click);
            // 
            // lb_dw1_ok
            // 
            this.lb_dw1_ok.Location = new System.Drawing.Point(321, 100);
            this.lb_dw1_ok.Name = "lb_dw1_ok";
            this.lb_dw1_ok.Size = new System.Drawing.Size(75, 23);
            this.lb_dw1_ok.TabIndex = 5;
            this.lb_dw1_ok.Text = "确定";
            this.lb_dw1_ok.UseVisualStyleBackColor = true;
            this.lb_dw1_ok.Click += new System.EventHandler(this.lb_dw1_ok_Click);
            // 
            // EditProjiectHead
            // 
            this.ClientSize = new System.Drawing.Size(413, 161);
            this.Controls.Add(this.lb_dw1_ok);
            this.Controls.Add(this.bt_dw1_cancel);
            this.Controls.Add(this.rtb_dw1_ProjectRemark);
            this.Controls.Add(this.tb_dw1_ProjectName);
            this.Controls.Add(this.lb_dw1_infor2);
            this.Controls.Add(this.lb_dw1_infor1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditProjiectHead";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditProjiectHead";
            this.Load += new System.EventHandler(this.EditProjiectHead_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_dw1_infor1;
        private System.Windows.Forms.Label lb_dw1_infor2;
        private System.Windows.Forms.TextBox tb_dw1_ProjectName;
        private System.Windows.Forms.RichTextBox rtb_dw1_ProjectRemark;
        private System.Windows.Forms.Button bt_dw1_cancel;
        private System.Windows.Forms.Button lb_dw1_ok;
    }
}