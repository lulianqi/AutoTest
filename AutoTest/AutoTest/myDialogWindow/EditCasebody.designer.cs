namespace AutoTest.myDialogWindow
{
    partial class EditCasebody
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCasebody));
            this.bt_dw2_ok = new System.Windows.Forms.Button();
            this.bt_dw2_cancel = new System.Windows.Forms.Button();
            this.rtb_CaseContent = new System.Windows.Forms.RichTextBox();
            this.lb_dw2_step = new System.Windows.Forms.Label();
            this.tb_dw2_Id = new System.Windows.Forms.TextBox();
            this.tb_dw2_Target = new System.Windows.Forms.TextBox();
            this.lb_dw2_target = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bt_dw2_ok
            // 
            this.bt_dw2_ok.Location = new System.Drawing.Point(693, 516);
            this.bt_dw2_ok.Name = "bt_dw2_ok";
            this.bt_dw2_ok.Size = new System.Drawing.Size(42, 23);
            this.bt_dw2_ok.TabIndex = 8;
            this.bt_dw2_ok.Text = "确定";
            this.bt_dw2_ok.UseVisualStyleBackColor = true;
            this.bt_dw2_ok.Click += new System.EventHandler(this.bt_dw2_ok_Click);
            // 
            // bt_dw2_cancel
            // 
            this.bt_dw2_cancel.Location = new System.Drawing.Point(648, 516);
            this.bt_dw2_cancel.Name = "bt_dw2_cancel";
            this.bt_dw2_cancel.Size = new System.Drawing.Size(42, 23);
            this.bt_dw2_cancel.TabIndex = 7;
            this.bt_dw2_cancel.Text = "取消";
            this.bt_dw2_cancel.UseVisualStyleBackColor = true;
            this.bt_dw2_cancel.Click += new System.EventHandler(this.bt_dw2_cancel_Click);
            // 
            // rtb_CaseContent
            // 
            this.rtb_CaseContent.Location = new System.Drawing.Point(7, 25);
            this.rtb_CaseContent.Name = "rtb_CaseContent";
            this.rtb_CaseContent.Size = new System.Drawing.Size(729, 485);
            this.rtb_CaseContent.TabIndex = 6;
            this.rtb_CaseContent.Text = "";
            // 
            // lb_dw2_step
            // 
            this.lb_dw2_step.AutoSize = true;
            this.lb_dw2_step.Location = new System.Drawing.Point(7, 521);
            this.lb_dw2_step.Name = "lb_dw2_step";
            this.lb_dw2_step.Size = new System.Drawing.Size(23, 12);
            this.lb_dw2_step.TabIndex = 9;
            this.lb_dw2_step.Text = "id=";
            // 
            // tb_dw2_Id
            // 
            this.tb_dw2_Id.Enabled = false;
            this.tb_dw2_Id.Location = new System.Drawing.Point(32, 517);
            this.tb_dw2_Id.Name = "tb_dw2_Id";
            this.tb_dw2_Id.Size = new System.Drawing.Size(114, 21);
            this.tb_dw2_Id.TabIndex = 10;
            this.tb_dw2_Id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_dw2_Step_KeyPress);
            // 
            // tb_dw2_Target
            // 
            this.tb_dw2_Target.Enabled = false;
            this.tb_dw2_Target.Location = new System.Drawing.Point(194, 517);
            this.tb_dw2_Target.Name = "tb_dw2_Target";
            this.tb_dw2_Target.Size = new System.Drawing.Size(448, 21);
            this.tb_dw2_Target.TabIndex = 12;
            // 
            // lb_dw2_target
            // 
            this.lb_dw2_target.AutoSize = true;
            this.lb_dw2_target.Location = new System.Drawing.Point(149, 521);
            this.lb_dw2_target.Name = "lb_dw2_target";
            this.lb_dw2_target.Size = new System.Drawing.Size(47, 12);
            this.lb_dw2_target.TabIndex = 11;
            this.lb_dw2_target.Text = "target=";
            // 
            // EditCasebody
            // 
            this.ClientSize = new System.Drawing.Size(744, 583);
            this.Controls.Add(this.tb_dw2_Target);
            this.Controls.Add(this.lb_dw2_target);
            this.Controls.Add(this.tb_dw2_Id);
            this.Controls.Add(this.lb_dw2_step);
            this.Controls.Add(this.bt_dw2_ok);
            this.Controls.Add(this.bt_dw2_cancel);
            this.Controls.Add(this.rtb_CaseContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditCasebody";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditCasebody";
            this.Load += new System.EventHandler(this.EditCasebody_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_dw2_ok;
        private System.Windows.Forms.Button bt_dw2_cancel;
        private System.Windows.Forms.RichTextBox rtb_CaseContent;
        private System.Windows.Forms.Label lb_dw2_step;
        private System.Windows.Forms.TextBox tb_dw2_Id;
        private System.Windows.Forms.TextBox tb_dw2_Target;
        private System.Windows.Forms.Label lb_dw2_target;
    }
}