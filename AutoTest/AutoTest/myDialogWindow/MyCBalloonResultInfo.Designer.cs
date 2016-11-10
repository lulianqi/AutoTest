namespace AutoTest.myDialogWindow
{
    partial class MyCBalloonResultInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyCBalloonResultInfo));
            this.lb_startTime = new System.Windows.Forms.Label();
            this.lb_spanTime = new System.Windows.Forms.Label();
            this.lb_caseId = new System.Windows.Forms.Label();
            this.lb_index = new System.Windows.Forms.Label();
            this.lb_remark = new System.Windows.Forms.Label();
            this.lb_ret = new System.Windows.Forms.Label();
            this.gb_result = new System.Windows.Forms.GroupBox();
            this.rtb_testResult = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.gb_remark = new System.Windows.Forms.GroupBox();
            this.gb_result.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            this.gb_remark.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_startTime
            // 
            this.lb_startTime.AutoSize = true;
            this.lb_startTime.Location = new System.Drawing.Point(43, 69);
            this.lb_startTime.Name = "lb_startTime";
            this.lb_startTime.Size = new System.Drawing.Size(59, 12);
            this.lb_startTime.TabIndex = 0;
            this.lb_startTime.Text = "startTime";
            // 
            // lb_spanTime
            // 
            this.lb_spanTime.AutoSize = true;
            this.lb_spanTime.Location = new System.Drawing.Point(43, 86);
            this.lb_spanTime.Name = "lb_spanTime";
            this.lb_spanTime.Size = new System.Drawing.Size(53, 12);
            this.lb_spanTime.TabIndex = 1;
            this.lb_spanTime.Text = "spanTime";
            // 
            // lb_caseId
            // 
            this.lb_caseId.AutoSize = true;
            this.lb_caseId.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_caseId.ForeColor = System.Drawing.Color.LightCoral;
            this.lb_caseId.Location = new System.Drawing.Point(174, 37);
            this.lb_caseId.Name = "lb_caseId";
            this.lb_caseId.Size = new System.Drawing.Size(62, 16);
            this.lb_caseId.TabIndex = 3;
            this.lb_caseId.Text = "caseId";
            // 
            // lb_index
            // 
            this.lb_index.AutoSize = true;
            this.lb_index.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_index.ForeColor = System.Drawing.Color.LightCoral;
            this.lb_index.Location = new System.Drawing.Point(43, 37);
            this.lb_index.Name = "lb_index";
            this.lb_index.Size = new System.Drawing.Size(53, 16);
            this.lb_index.TabIndex = 2;
            this.lb_index.Text = "index";
            // 
            // lb_remark
            // 
            this.lb_remark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_remark.Location = new System.Drawing.Point(3, 17);
            this.lb_remark.Name = "lb_remark";
            this.lb_remark.Size = new System.Drawing.Size(130, 48);
            this.lb_remark.TabIndex = 5;
            this.lb_remark.Text = "remark";
            // 
            // lb_ret
            // 
            this.lb_ret.AutoSize = true;
            this.lb_ret.Location = new System.Drawing.Point(43, 103);
            this.lb_ret.Name = "lb_ret";
            this.lb_ret.Size = new System.Drawing.Size(23, 12);
            this.lb_ret.TabIndex = 4;
            this.lb_ret.Text = "ret";
            // 
            // gb_result
            // 
            this.gb_result.Controls.Add(this.rtb_testResult);
            this.gb_result.Location = new System.Drawing.Point(176, 65);
            this.gb_result.Name = "gb_result";
            this.gb_result.Size = new System.Drawing.Size(276, 121);
            this.gb_result.TabIndex = 6;
            this.gb_result.TabStop = false;
            this.gb_result.Text = "result";
            // 
            // rtb_testResult
            // 
            this.rtb_testResult.BackColor = System.Drawing.Color.White;
            this.rtb_testResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_testResult.Location = new System.Drawing.Point(3, 17);
            this.rtb_testResult.Name = "rtb_testResult";
            this.rtb_testResult.ReadOnly = true;
            this.rtb_testResult.Size = new System.Drawing.Size(270, 101);
            this.rtb_testResult.TabIndex = 0;
            this.rtb_testResult.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(411, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_close.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_close.Image")));
            this.pictureBox_close.Location = new System.Drawing.Point(434, 31);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_close.TabIndex = 8;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            // 
            // gb_remark
            // 
            this.gb_remark.Controls.Add(this.lb_remark);
            this.gb_remark.Location = new System.Drawing.Point(34, 118);
            this.gb_remark.Name = "gb_remark";
            this.gb_remark.Size = new System.Drawing.Size(136, 68);
            this.gb_remark.TabIndex = 10;
            this.gb_remark.TabStop = false;
            this.gb_remark.Text = "备注";
            // 
            // MyCBalloonResultInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(487, 219);
            this.Controls.Add(this.gb_remark);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox_close);
            this.Controls.Add(this.gb_result);
            this.Controls.Add(this.lb_ret);
            this.Controls.Add(this.lb_caseId);
            this.Controls.Add(this.lb_index);
            this.Controls.Add(this.lb_spanTime);
            this.Controls.Add(this.lb_startTime);
            this.Name = "MyCBalloonResultInfo";
            this.Text = "MyCBalloonResultInfo";
            this.Load += new System.EventHandler(this.MyCBalloonResultInfo_Load);
            this.gb_result.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            this.gb_remark.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_startTime;
        private System.Windows.Forms.Label lb_spanTime;
        private System.Windows.Forms.Label lb_caseId;
        private System.Windows.Forms.Label lb_index;
        private System.Windows.Forms.Label lb_remark;
        private System.Windows.Forms.Label lb_ret;
        private System.Windows.Forms.GroupBox gb_result;
        private System.Windows.Forms.RichTextBox rtb_testResult;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.GroupBox gb_remark;
    }
}