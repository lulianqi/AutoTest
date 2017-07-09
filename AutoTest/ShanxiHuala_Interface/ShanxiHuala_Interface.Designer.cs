namespace ShanxiHuala_Interface
{
    partial class ShanxiHuala_Interface
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
            this.bt_oauth = new System.Windows.Forms.Button();
            this.tb_access_token = new System.Windows.Forms.TextBox();
            this.tb_sendTime = new System.Windows.Forms.TextBox();
            this.tb_url = new System.Windows.Forms.TextBox();
            this.bt_send = new System.Windows.Forms.Button();
            this.rtb_sendBody = new System.Windows.Forms.RichTextBox();
            this.rtb_response = new System.Windows.Forms.RichTextBox();
            this.tb_host = new System.Windows.Forms.TextBox();
            this.cb_httpMethod = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bt_oauth
            // 
            this.bt_oauth.Location = new System.Drawing.Point(12, 12);
            this.bt_oauth.Name = "bt_oauth";
            this.bt_oauth.Size = new System.Drawing.Size(75, 23);
            this.bt_oauth.TabIndex = 0;
            this.bt_oauth.Text = "oauth";
            this.bt_oauth.UseVisualStyleBackColor = true;
            this.bt_oauth.Click += new System.EventHandler(this.bt_oauth_Click);
            // 
            // tb_access_token
            // 
            this.tb_access_token.Location = new System.Drawing.Point(104, 12);
            this.tb_access_token.Name = "tb_access_token";
            this.tb_access_token.Size = new System.Drawing.Size(464, 21);
            this.tb_access_token.TabIndex = 1;
            // 
            // tb_sendTime
            // 
            this.tb_sendTime.Location = new System.Drawing.Point(596, 12);
            this.tb_sendTime.Name = "tb_sendTime";
            this.tb_sendTime.Size = new System.Drawing.Size(132, 21);
            this.tb_sendTime.TabIndex = 2;
            // 
            // tb_url
            // 
            this.tb_url.AutoCompleteCustomSource.AddRange(new string[] {
            "/oauth/ap/shop/info",
            "/oauth/ap/category/publicCategory",
            "/oauth/ap/category/shopCategory",
            "/oauth/ap/goods/public",
            "/oauth/ap/goods/shop",
            "/oauth/ap/order/onlineOrder",
            "/oauth/ap/order/terminalOrder",
            "/oauth/ap/shop/statusControl",
            "/oauth/ap/order/onlineCurrentData",
            "/oauth/ap/order/statistics",
            "/oauth/ap/order/weekData",
            "/oauth/ap/shop/drawBalance",
            "/oauth/ap/goods/edit",
            "/oauth/ap/shop/drawMoney",
            "/oauth/ap/shop/incomeStatistics",
            "/oauth/ap/shop/accountStatement"});
            this.tb_url.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_url.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tb_url.Location = new System.Drawing.Point(202, 46);
            this.tb_url.Name = "tb_url";
            this.tb_url.Size = new System.Drawing.Size(438, 21);
            this.tb_url.TabIndex = 3;
            // 
            // bt_send
            // 
            this.bt_send.Location = new System.Drawing.Point(12, 290);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(79, 23);
            this.bt_send.TabIndex = 4;
            this.bt_send.Text = "send";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.bt_send_Click);
            // 
            // rtb_sendBody
            // 
            this.rtb_sendBody.Location = new System.Drawing.Point(12, 78);
            this.rtb_sendBody.Name = "rtb_sendBody";
            this.rtb_sendBody.Size = new System.Drawing.Size(716, 206);
            this.rtb_sendBody.TabIndex = 5;
            this.rtb_sendBody.Text = "";
            // 
            // rtb_response
            // 
            this.rtb_response.Location = new System.Drawing.Point(8, 319);
            this.rtb_response.Name = "rtb_response";
            this.rtb_response.Size = new System.Drawing.Size(720, 227);
            this.rtb_response.TabIndex = 6;
            this.rtb_response.Text = "";
            // 
            // tb_host
            // 
            this.tb_host.Location = new System.Drawing.Point(12, 46);
            this.tb_host.Name = "tb_host";
            this.tb_host.Size = new System.Drawing.Size(184, 21);
            this.tb_host.TabIndex = 7;
            // 
            // cb_httpMethod
            // 
            this.cb_httpMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_httpMethod.FormattingEnabled = true;
            this.cb_httpMethod.Items.AddRange(new object[] {
            "GET",
            "POST"});
            this.cb_httpMethod.Location = new System.Drawing.Point(655, 46);
            this.cb_httpMethod.Name = "cb_httpMethod";
            this.cb_httpMethod.Size = new System.Drawing.Size(73, 20);
            this.cb_httpMethod.TabIndex = 8;
            // 
            // ShanxiHuala_Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 558);
            this.Controls.Add(this.cb_httpMethod);
            this.Controls.Add(this.tb_host);
            this.Controls.Add(this.rtb_response);
            this.Controls.Add(this.rtb_sendBody);
            this.Controls.Add(this.bt_send);
            this.Controls.Add(this.tb_url);
            this.Controls.Add(this.tb_sendTime);
            this.Controls.Add(this.tb_access_token);
            this.Controls.Add(this.bt_oauth);
            this.Name = "ShanxiHuala_Interface";
            this.Text = "ShanxiHuala_Interface";
            this.Load += new System.EventHandler(this.ShanxiHuala_Interface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_oauth;
        private System.Windows.Forms.TextBox tb_access_token;
        private System.Windows.Forms.TextBox tb_sendTime;
        private System.Windows.Forms.TextBox tb_url;
        private System.Windows.Forms.Button bt_send;
        private System.Windows.Forms.RichTextBox rtb_sendBody;
        private System.Windows.Forms.RichTextBox rtb_response;
        private System.Windows.Forms.TextBox tb_host;
        private System.Windows.Forms.ComboBox cb_httpMethod;
    }
}

