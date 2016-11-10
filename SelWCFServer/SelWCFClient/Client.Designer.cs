namespace SelWCFClient
{
    partial class Client
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
            this.richTextBox_info = new System.Windows.Forms.RichTextBox();
            this.button_sayHello = new System.Windows.Forms.Button();
            this.button_sayHello2 = new System.Windows.Forms.Button();
            this.button_sayBye = new System.Windows.Forms.Button();
            this.button_isWho = new System.Windows.Forms.Button();
            this.comboBox_binding = new System.Windows.Forms.ComboBox();
            this.button_CallBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox_info
            // 
            this.richTextBox_info.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBox_info.HideSelection = false;
            this.richTextBox_info.Location = new System.Drawing.Point(205, 0);
            this.richTextBox_info.Name = "richTextBox_info";
            this.richTextBox_info.Size = new System.Drawing.Size(794, 461);
            this.richTextBox_info.TabIndex = 2;
            this.richTextBox_info.Text = "";
            // 
            // button_sayHello
            // 
            this.button_sayHello.Location = new System.Drawing.Point(12, 12);
            this.button_sayHello.Name = "button_sayHello";
            this.button_sayHello.Size = new System.Drawing.Size(75, 23);
            this.button_sayHello.TabIndex = 3;
            this.button_sayHello.Text = "SayHello";
            this.button_sayHello.UseVisualStyleBackColor = true;
            this.button_sayHello.Click += new System.EventHandler(this.button_sayHello_Click);
            // 
            // button_sayHello2
            // 
            this.button_sayHello2.Location = new System.Drawing.Point(12, 53);
            this.button_sayHello2.Name = "button_sayHello2";
            this.button_sayHello2.Size = new System.Drawing.Size(75, 23);
            this.button_sayHello2.TabIndex = 4;
            this.button_sayHello2.Text = "SayHello";
            this.button_sayHello2.UseVisualStyleBackColor = true;
            this.button_sayHello2.Click += new System.EventHandler(this.button_sayHello2_Click);
            // 
            // button_sayBye
            // 
            this.button_sayBye.Location = new System.Drawing.Point(12, 102);
            this.button_sayBye.Name = "button_sayBye";
            this.button_sayBye.Size = new System.Drawing.Size(75, 23);
            this.button_sayBye.TabIndex = 5;
            this.button_sayBye.Text = "SayBye";
            this.button_sayBye.UseVisualStyleBackColor = true;
            this.button_sayBye.Click += new System.EventHandler(this.button_sayBye_Click);
            // 
            // button_isWho
            // 
            this.button_isWho.Location = new System.Drawing.Point(12, 147);
            this.button_isWho.Name = "button_isWho";
            this.button_isWho.Size = new System.Drawing.Size(75, 23);
            this.button_isWho.TabIndex = 6;
            this.button_isWho.Text = "Who";
            this.button_isWho.UseVisualStyleBackColor = true;
            this.button_isWho.Click += new System.EventHandler(this.button_isWho_Click);
            // 
            // comboBox_binding
            // 
            this.comboBox_binding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_binding.FormattingEnabled = true;
            this.comboBox_binding.Location = new System.Drawing.Point(3, 429);
            this.comboBox_binding.Name = "comboBox_binding";
            this.comboBox_binding.Size = new System.Drawing.Size(196, 20);
            this.comboBox_binding.TabIndex = 7;
            this.comboBox_binding.SelectedIndexChanged += new System.EventHandler(this.comboBox_binding_SelectedIndexChanged);
            // 
            // button_CallBack
            // 
            this.button_CallBack.Location = new System.Drawing.Point(12, 305);
            this.button_CallBack.Name = "button_CallBack";
            this.button_CallBack.Size = new System.Drawing.Size(75, 23);
            this.button_CallBack.TabIndex = 8;
            this.button_CallBack.Text = "CallBack";
            this.button_CallBack.UseVisualStyleBackColor = true;
            this.button_CallBack.Click += new System.EventHandler(this.button_CallBack_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 461);
            this.Controls.Add(this.button_CallBack);
            this.Controls.Add(this.comboBox_binding);
            this.Controls.Add(this.button_isWho);
            this.Controls.Add(this.button_sayBye);
            this.Controls.Add(this.button_sayHello2);
            this.Controls.Add(this.button_sayHello);
            this.Controls.Add(this.richTextBox_info);
            this.Name = "Client";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_info;
        private System.Windows.Forms.Button button_sayHello;
        private System.Windows.Forms.Button button_sayHello2;
        private System.Windows.Forms.Button button_sayBye;
        private System.Windows.Forms.Button button_isWho;
        private System.Windows.Forms.ComboBox comboBox_binding;
        private System.Windows.Forms.Button button_CallBack;
    }
}

