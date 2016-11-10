namespace SelWCFServer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_startService = new System.Windows.Forms.Button();
            this.richTextBox_info = new System.Windows.Forms.RichTextBox();
            this.bt_stopService = new System.Windows.Forms.Button();
            this.button_duaStop = new System.Windows.Forms.Button();
            this.button_duaStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_startService
            // 
            this.bt_startService.Location = new System.Drawing.Point(12, 12);
            this.bt_startService.Name = "bt_startService";
            this.bt_startService.Size = new System.Drawing.Size(130, 23);
            this.bt_startService.TabIndex = 0;
            this.bt_startService.Text = "StartService";
            this.bt_startService.UseVisualStyleBackColor = true;
            this.bt_startService.Click += new System.EventHandler(this.bt_startService_Click);
            // 
            // richTextBox_info
            // 
            this.richTextBox_info.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBox_info.HideSelection = false;
            this.richTextBox_info.Location = new System.Drawing.Point(166, 0);
            this.richTextBox_info.Name = "richTextBox_info";
            this.richTextBox_info.Size = new System.Drawing.Size(742, 317);
            this.richTextBox_info.TabIndex = 1;
            this.richTextBox_info.Text = "";
            // 
            // bt_stopService
            // 
            this.bt_stopService.Location = new System.Drawing.Point(12, 53);
            this.bt_stopService.Name = "bt_stopService";
            this.bt_stopService.Size = new System.Drawing.Size(130, 23);
            this.bt_stopService.TabIndex = 2;
            this.bt_stopService.Text = "StopService";
            this.bt_stopService.UseVisualStyleBackColor = true;
            this.bt_stopService.Click += new System.EventHandler(this.bt_stopService_Click);
            // 
            // button_duaStop
            // 
            this.button_duaStop.Location = new System.Drawing.Point(12, 145);
            this.button_duaStop.Name = "button_duaStop";
            this.button_duaStop.Size = new System.Drawing.Size(130, 23);
            this.button_duaStop.TabIndex = 4;
            this.button_duaStop.Text = "StopServiceDua";
            this.button_duaStop.UseVisualStyleBackColor = true;
            this.button_duaStop.Click += new System.EventHandler(this.button_duaStop_Click);
            // 
            // button_duaStart
            // 
            this.button_duaStart.Location = new System.Drawing.Point(12, 104);
            this.button_duaStart.Name = "button_duaStart";
            this.button_duaStart.Size = new System.Drawing.Size(130, 23);
            this.button_duaStart.TabIndex = 3;
            this.button_duaStart.Text = "StartServiceDua";
            this.button_duaStart.UseVisualStyleBackColor = true;
            this.button_duaStart.Click += new System.EventHandler(this.button_duaStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 317);
            this.Controls.Add(this.button_duaStop);
            this.Controls.Add(this.button_duaStart);
            this.Controls.Add(this.bt_stopService);
            this.Controls.Add(this.richTextBox_info);
            this.Controls.Add(this.bt_startService);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_startService;
        private System.Windows.Forms.RichTextBox richTextBox_info;
        private System.Windows.Forms.Button bt_stopService;
        private System.Windows.Forms.Button button_duaStop;
        private System.Windows.Forms.Button button_duaStart;
    }
}

