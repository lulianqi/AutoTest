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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShanxiHuala_Interface));
            this.bt_oauth = new System.Windows.Forms.Button();
            this.tb_access_token = new System.Windows.Forms.TextBox();
            this.tb_sendTime = new System.Windows.Forms.TextBox();
            this.tb_url = new System.Windows.Forms.TextBox();
            this.bt_send = new System.Windows.Forms.Button();
            this.rtb_sendBody = new System.Windows.Forms.RichTextBox();
            this.tb_host = new System.Windows.Forms.TextBox();
            this.cb_httpMethod = new System.Windows.Forms.ComboBox();
            this.listView_InterfaceList = new System.Windows.Forms.ListView();
            this.tb_sign = new System.Windows.Forms.TextBox();
            this.rtb_response = new MyCommonControl.DataRecordBox();
            this.ck_isSgin = new System.Windows.Forms.CheckBox();
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
            this.tb_access_token.Size = new System.Drawing.Size(407, 21);
            this.tb_access_token.TabIndex = 1;
            // 
            // tb_sendTime
            // 
            this.tb_sendTime.Location = new System.Drawing.Point(521, 12);
            this.tb_sendTime.Name = "tb_sendTime";
            this.tb_sendTime.Size = new System.Drawing.Size(111, 21);
            this.tb_sendTime.TabIndex = 2;
            // 
            // tb_url
            // 
            this.tb_url.AutoCompleteCustomSource.AddRange(new string[] {
            "/oauth/api/shop/info",
            "/oauth/api/category/publicCategory",
            "/oauth/api/category/shopCategory",
            "/oauth/api/goods/public",
            "/oauth/api/goods/shop",
            "/oauth/api/order/onlineOrder",
            "/oauth/api/order/terminalOrder",
            "/oauth/api/shop/statusControl",
            "/oauth/api/order/onlineCurrentData",
            "/oauth/api/order/statistics",
            "/oauth/api/order/weekData",
            "/oauth/api/shop/drawBalance",
            "/oauth/api/goods/edit",
            "/oauth/api/shop/drawMoney",
            "/oauth/ap/shop/incomeStatistics",
            "/oauth/ap/shop/accountStatement"});
            this.tb_url.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_url.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tb_url.Location = new System.Drawing.Point(202, 46);
            this.tb_url.Name = "tb_url";
            this.tb_url.Size = new System.Drawing.Size(604, 21);
            this.tb_url.TabIndex = 3;
            this.tb_url.TextChanged += new System.EventHandler(this.tb_url_TextChanged);
            // 
            // bt_send
            // 
            this.bt_send.Location = new System.Drawing.Point(236, 347);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(79, 23);
            this.bt_send.TabIndex = 4;
            this.bt_send.Text = "send";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.bt_send_Click);
            // 
            // rtb_sendBody
            // 
            this.rtb_sendBody.BackColor = System.Drawing.Color.LightCyan;
            this.rtb_sendBody.Location = new System.Drawing.Point(236, 73);
            this.rtb_sendBody.Name = "rtb_sendBody";
            this.rtb_sendBody.Size = new System.Drawing.Size(649, 268);
            this.rtb_sendBody.TabIndex = 5;
            this.rtb_sendBody.Text = "";
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
            this.cb_httpMethod.Location = new System.Drawing.Point(812, 46);
            this.cb_httpMethod.Name = "cb_httpMethod";
            this.cb_httpMethod.Size = new System.Drawing.Size(73, 20);
            this.cb_httpMethod.TabIndex = 8;
            // 
            // listView_InterfaceList
            // 
            this.listView_InterfaceList.HideSelection = false;
            this.listView_InterfaceList.Location = new System.Drawing.Point(12, 73);
            this.listView_InterfaceList.Name = "listView_InterfaceList";
            this.listView_InterfaceList.Size = new System.Drawing.Size(218, 492);
            this.listView_InterfaceList.TabIndex = 9;
            this.listView_InterfaceList.UseCompatibleStateImageBehavior = false;
            this.listView_InterfaceList.View = System.Windows.Forms.View.List;
            this.listView_InterfaceList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_InterfaceList_ItemSelectionChanged);
            // 
            // tb_sign
            // 
            this.tb_sign.Location = new System.Drawing.Point(638, 12);
            this.tb_sign.Name = "tb_sign";
            this.tb_sign.Size = new System.Drawing.Size(220, 21);
            this.tb_sign.TabIndex = 11;
            // 
            // rtb_response
            // 
            this.rtb_response.CanFill = true;
            this.rtb_response.IsShowIcon = true;
            this.rtb_response.Location = new System.Drawing.Point(237, 377);
            this.rtb_response.MaxLine = 5000;
            this.rtb_response.MianDirectory = "DataRecord";
            this.rtb_response.Name = "rtb_response";
            this.rtb_response.Size = new System.Drawing.Size(648, 182);
            this.rtb_response.TabIndex = 10;
            this.rtb_response.OnShowInNewWindowChange += new System.EventHandler<MyCommonControl.DataRecordBox.ShowInNewWindowEventArgs>(this.rtb_response_OnShowInNewWindowChange);
            // 
            // ck_isSgin
            // 
            this.ck_isSgin.AutoSize = true;
            this.ck_isSgin.Location = new System.Drawing.Point(865, 14);
            this.ck_isSgin.Name = "ck_isSgin";
            this.ck_isSgin.Size = new System.Drawing.Size(15, 14);
            this.ck_isSgin.TabIndex = 12;
            this.ck_isSgin.UseVisualStyleBackColor = true;
            // 
            // ShanxiHuala_Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 573);
            this.Controls.Add(this.ck_isSgin);
            this.Controls.Add(this.tb_sign);
            this.Controls.Add(this.rtb_response);
            this.Controls.Add(this.listView_InterfaceList);
            this.Controls.Add(this.cb_httpMethod);
            this.Controls.Add(this.tb_host);
            this.Controls.Add(this.rtb_sendBody);
            this.Controls.Add(this.bt_send);
            this.Controls.Add(this.tb_url);
            this.Controls.Add(this.tb_sendTime);
            this.Controls.Add(this.tb_access_token);
            this.Controls.Add(this.bt_oauth);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(908, 611);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(908, 611);
            this.Name = "ShanxiHuala_Interface";
            this.Text = "ShanxiHuala_Interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShanxiHuala_Interface_FormClosing);
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
        private System.Windows.Forms.TextBox tb_host;
        private System.Windows.Forms.ComboBox cb_httpMethod;
        private System.Windows.Forms.ListView listView_InterfaceList;
        private MyCommonControl.DataRecordBox rtb_response;
        private System.Windows.Forms.TextBox tb_sign;
        private System.Windows.Forms.CheckBox ck_isSgin;
    }
}

