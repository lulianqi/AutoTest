namespace ActiveMQTest
{
    partial class ActiveMQ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActiveMQ));
            this.bt_publish = new System.Windows.Forms.Button();
            this.cb_TopicQueues = new System.Windows.Forms.ComboBox();
            this.bt_Discounect = new System.Windows.Forms.Button();
            this.lb_info_clientId = new System.Windows.Forms.Label();
            this.lb_info_address = new System.Windows.Forms.Label();
            this.ck_isDurable = new System.Windows.Forms.CheckBox();
            this.bt_Subscribe = new System.Windows.Forms.Button();
            this.bt_Connect = new System.Windows.Forms.Button();
            this.lb_info_userNmae = new System.Windows.Forms.Label();
            this.lb_info_passWord = new System.Windows.Forms.Label();
            this.cb_sendTopicQueues = new System.Windows.Forms.ComboBox();
            this.rtb_dataToSend = new System.Windows.Forms.RichTextBox();
            this.cb_sendTextByte = new System.Windows.Forms.ComboBox();
            this.Drb_messageReceive = new MyCommonControl.DataRecordBox();
            this.tb_sendTopic = new MyCommonControl.Control.TextBoxWithWatermak();
            this.lv_pathList = new MyCommonControl.ListViewWithButton();
            this.columnHeader_topicOrQueue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_del = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tb_Password = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_userName = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_clientId = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_ConsumerTopic = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_uri = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_ConsumerName = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_sendCount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bt_publish
            // 
            this.bt_publish.Location = new System.Drawing.Point(523, 389);
            this.bt_publish.Name = "bt_publish";
            this.bt_publish.Size = new System.Drawing.Size(83, 23);
            this.bt_publish.TabIndex = 42;
            this.bt_publish.Text = "Publish";
            this.bt_publish.UseVisualStyleBackColor = true;
            this.bt_publish.Click += new System.EventHandler(this.bt_publish_Click);
            // 
            // cb_TopicQueues
            // 
            this.cb_TopicQueues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TopicQueues.FormattingEnabled = true;
            this.cb_TopicQueues.Items.AddRange(new object[] {
            "Topic",
            "Queues"});
            this.cb_TopicQueues.Location = new System.Drawing.Point(5, 69);
            this.cb_TopicQueues.Name = "cb_TopicQueues";
            this.cb_TopicQueues.Size = new System.Drawing.Size(70, 20);
            this.cb_TopicQueues.TabIndex = 40;
            // 
            // bt_Discounect
            // 
            this.bt_Discounect.Location = new System.Drawing.Point(732, 10);
            this.bt_Discounect.Name = "bt_Discounect";
            this.bt_Discounect.Size = new System.Drawing.Size(83, 23);
            this.bt_Discounect.TabIndex = 38;
            this.bt_Discounect.Text = "Discounect";
            this.bt_Discounect.UseVisualStyleBackColor = true;
            this.bt_Discounect.Click += new System.EventHandler(this.bt_Discounect_Click);
            // 
            // lb_info_clientId
            // 
            this.lb_info_clientId.AutoSize = true;
            this.lb_info_clientId.Location = new System.Drawing.Point(5, 44);
            this.lb_info_clientId.Name = "lb_info_clientId";
            this.lb_info_clientId.Size = new System.Drawing.Size(59, 12);
            this.lb_info_clientId.TabIndex = 37;
            this.lb_info_clientId.Text = "ClientId:";
            // 
            // lb_info_address
            // 
            this.lb_info_address.AutoSize = true;
            this.lb_info_address.Location = new System.Drawing.Point(5, 16);
            this.lb_info_address.Name = "lb_info_address";
            this.lb_info_address.Size = new System.Drawing.Size(53, 12);
            this.lb_info_address.TabIndex = 36;
            this.lb_info_address.Text = "Address:";
            // 
            // ck_isDurable
            // 
            this.ck_isDurable.AutoSize = true;
            this.ck_isDurable.Location = new System.Drawing.Point(81, 71);
            this.ck_isDurable.Name = "ck_isDurable";
            this.ck_isDurable.Size = new System.Drawing.Size(78, 16);
            this.ck_isDurable.TabIndex = 35;
            this.ck_isDurable.Text = "IsDurable";
            this.ck_isDurable.UseVisualStyleBackColor = true;
            this.ck_isDurable.CheckedChanged += new System.EventHandler(this.ck_isDurable_CheckedChanged);
            // 
            // bt_Subscribe
            // 
            this.bt_Subscribe.Location = new System.Drawing.Point(732, 67);
            this.bt_Subscribe.Name = "bt_Subscribe";
            this.bt_Subscribe.Size = new System.Drawing.Size(75, 23);
            this.bt_Subscribe.TabIndex = 31;
            this.bt_Subscribe.Text = "Subscribe";
            this.bt_Subscribe.UseVisualStyleBackColor = true;
            this.bt_Subscribe.Click += new System.EventHandler(this.bt_Subscribe_Click);
            // 
            // bt_Connect
            // 
            this.bt_Connect.Location = new System.Drawing.Point(651, 10);
            this.bt_Connect.Name = "bt_Connect";
            this.bt_Connect.Size = new System.Drawing.Size(75, 23);
            this.bt_Connect.TabIndex = 30;
            this.bt_Connect.Text = "Connect";
            this.bt_Connect.UseVisualStyleBackColor = true;
            this.bt_Connect.Click += new System.EventHandler(this.bt_Connect_Click);
            // 
            // lb_info_userNmae
            // 
            this.lb_info_userNmae.AutoSize = true;
            this.lb_info_userNmae.Location = new System.Drawing.Point(433, 44);
            this.lb_info_userNmae.Name = "lb_info_userNmae";
            this.lb_info_userNmae.Size = new System.Drawing.Size(53, 12);
            this.lb_info_userNmae.TabIndex = 43;
            this.lb_info_userNmae.Text = "Username";
            // 
            // lb_info_passWord
            // 
            this.lb_info_passWord.AutoSize = true;
            this.lb_info_passWord.Location = new System.Drawing.Point(632, 44);
            this.lb_info_passWord.Name = "lb_info_passWord";
            this.lb_info_passWord.Size = new System.Drawing.Size(53, 12);
            this.lb_info_passWord.TabIndex = 44;
            this.lb_info_passWord.Text = "Password";
            // 
            // cb_sendTopicQueues
            // 
            this.cb_sendTopicQueues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_sendTopicQueues.FormattingEnabled = true;
            this.cb_sendTopicQueues.Items.AddRange(new object[] {
            "Topic",
            "Queues"});
            this.cb_sendTopicQueues.Location = new System.Drawing.Point(8, 391);
            this.cb_sendTopicQueues.Name = "cb_sendTopicQueues";
            this.cb_sendTopicQueues.Size = new System.Drawing.Size(70, 20);
            this.cb_sendTopicQueues.TabIndex = 50;
            // 
            // rtb_dataToSend
            // 
            this.rtb_dataToSend.BackColor = System.Drawing.Color.Lavender;
            this.rtb_dataToSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_dataToSend.Location = new System.Drawing.Point(3, 418);
            this.rtb_dataToSend.Name = "rtb_dataToSend";
            this.rtb_dataToSend.Size = new System.Drawing.Size(603, 68);
            this.rtb_dataToSend.TabIndex = 51;
            this.rtb_dataToSend.Text = "";
            // 
            // cb_sendTextByte
            // 
            this.cb_sendTextByte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_sendTextByte.FormattingEnabled = true;
            this.cb_sendTextByte.Items.AddRange(new object[] {
            "Text",
            "Bytes"});
            this.cb_sendTextByte.Location = new System.Drawing.Point(441, 390);
            this.cb_sendTextByte.Name = "cb_sendTextByte";
            this.cb_sendTextByte.Size = new System.Drawing.Size(70, 20);
            this.cb_sendTextByte.TabIndex = 52;
            // 
            // Drb_messageReceive
            // 
            this.Drb_messageReceive.CanFill = true;
            this.Drb_messageReceive.Location = new System.Drawing.Point(6, 98);
            this.Drb_messageReceive.MaxLine = 5000;
            this.Drb_messageReceive.MianDirectory = "DataRecord";
            this.Drb_messageReceive.Name = "Drb_messageReceive";
            this.Drb_messageReceive.Size = new System.Drawing.Size(600, 284);
            this.Drb_messageReceive.TabIndex = 47;
            // 
            // tb_sendTopic
            // 
            this.tb_sendTopic.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_sendTopic.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tb_sendTopic.Location = new System.Drawing.Point(83, 390);
            this.tb_sendTopic.Name = "tb_sendTopic";
            this.tb_sendTopic.Size = new System.Drawing.Size(352, 21);
            this.tb_sendTopic.TabIndex = 49;
            this.tb_sendTopic.WatermarkText = "发布消息 Topic或Queues";
            // 
            // lv_pathList
            // 
            this.lv_pathList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_pathList.BackColor = System.Drawing.SystemColors.Window;
            this.lv_pathList.ButtonIndex = 1;
            this.lv_pathList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_topicOrQueue,
            this.columnHeader_del});
            this.lv_pathList.Location = new System.Drawing.Point(612, 98);
            this.lv_pathList.MultiSelect = false;
            this.lv_pathList.Name = "lv_pathList";
            this.lv_pathList.Size = new System.Drawing.Size(204, 388);
            this.lv_pathList.TabIndex = 48;
            this.lv_pathList.UseCompatibleStateImageBehavior = false;
            this.lv_pathList.View = System.Windows.Forms.View.Details;
            this.lv_pathList.ButtonClickEvent += new System.EventHandler(this.lv_pathList_ButtonClickEvent);
            // 
            // columnHeader_topicOrQueue
            // 
            this.columnHeader_topicOrQueue.Text = "Topic/Queue";
            this.columnHeader_topicOrQueue.Width = 142;
            // 
            // columnHeader_del
            // 
            this.columnHeader_del.Text = "操作";
            this.columnHeader_del.Width = 56;
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(701, 40);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.Size = new System.Drawing.Size(114, 21);
            this.tb_Password.TabIndex = 46;
            this.tb_Password.WatermarkText = "密码（可选）";
            // 
            // tb_userName
            // 
            this.tb_userName.Location = new System.Drawing.Point(500, 40);
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(114, 21);
            this.tb_userName.TabIndex = 45;
            this.tb_userName.WatermarkText = "用户名（可选）";
            // 
            // tb_clientId
            // 
            this.tb_clientId.AutoCompleteCustomSource.AddRange(new string[] {
            "tcp://m2m.vanelife.com:11883",
            "tcp://127.0.0.1:1883"});
            this.tb_clientId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_clientId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tb_clientId.Location = new System.Drawing.Point(81, 39);
            this.tb_clientId.Name = "tb_clientId";
            this.tb_clientId.Size = new System.Drawing.Size(346, 21);
            this.tb_clientId.TabIndex = 34;
            this.tb_clientId.WatermarkText = "连接标识ID（可选，如果填写需要保证唯一性）";
            // 
            // tb_ConsumerTopic
            // 
            this.tb_ConsumerTopic.Location = new System.Drawing.Point(158, 69);
            this.tb_ConsumerTopic.Name = "tb_ConsumerTopic";
            this.tb_ConsumerTopic.Size = new System.Drawing.Size(568, 21);
            this.tb_ConsumerTopic.TabIndex = 33;
            this.tb_ConsumerTopic.WatermarkText = "订阅Consumer Topic或Queues";
            // 
            // tb_uri
            // 
            this.tb_uri.AutoCompleteCustomSource.AddRange(new string[] {
            "tcp://m2m.vanelife.com:11883",
            "tcp://127.0.0.1:1883",
            "tcp://192.168.78.110:61616",
            "tcp://172.0.0.1:61616",
            "tcp://192.168.200.150:61616",
            "tcp://192.168.110.29:61616",
            "tcp://192.168.200.23:61616"});
            this.tb_uri.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_uri.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tb_uri.Location = new System.Drawing.Point(81, 12);
            this.tb_uri.Name = "tb_uri";
            this.tb_uri.Size = new System.Drawing.Size(557, 21);
            this.tb_uri.TabIndex = 32;
            this.tb_uri.Text = "tcp://192.168.78.110:61616";
            this.tb_uri.WatermarkText = "链接URI";
            // 
            // tb_ConsumerName
            // 
            this.tb_ConsumerName.AutoCompleteCustomSource.AddRange(new string[] {
            "tcp://m2m.vanelife.com:11883",
            "tcp://127.0.0.1:1883"});
            this.tb_ConsumerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_ConsumerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tb_ConsumerName.Location = new System.Drawing.Point(158, 69);
            this.tb_ConsumerName.Name = "tb_ConsumerName";
            this.tb_ConsumerName.Size = new System.Drawing.Size(201, 21);
            this.tb_ConsumerName.TabIndex = 53;
            this.tb_ConsumerName.Visible = false;
            this.tb_ConsumerName.WatermarkText = "可靠连接的ConsumerName（可选）";
            // 
            // tb_sendCount
            // 
            this.tb_sendCount.Location = new System.Drawing.Point(612, 390);
            this.tb_sendCount.Name = "tb_sendCount";
            this.tb_sendCount.Size = new System.Drawing.Size(92, 21);
            this.tb_sendCount.TabIndex = 54;
            this.tb_sendCount.Text = "1";
            // 
            // ActiveMQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 492);
            this.Controls.Add(this.Drb_messageReceive);
            this.Controls.Add(this.cb_sendTextByte);
            this.Controls.Add(this.rtb_dataToSend);
            this.Controls.Add(this.cb_sendTopicQueues);
            this.Controls.Add(this.tb_sendTopic);
            this.Controls.Add(this.lv_pathList);
            this.Controls.Add(this.tb_Password);
            this.Controls.Add(this.lb_info_userNmae);
            this.Controls.Add(this.lb_info_passWord);
            this.Controls.Add(this.tb_userName);
            this.Controls.Add(this.bt_publish);
            this.Controls.Add(this.cb_TopicQueues);
            this.Controls.Add(this.bt_Discounect);
            this.Controls.Add(this.lb_info_clientId);
            this.Controls.Add(this.lb_info_address);
            this.Controls.Add(this.ck_isDurable);
            this.Controls.Add(this.tb_clientId);
            this.Controls.Add(this.tb_ConsumerTopic);
            this.Controls.Add(this.tb_uri);
            this.Controls.Add(this.bt_Subscribe);
            this.Controls.Add(this.bt_Connect);
            this.Controls.Add(this.tb_ConsumerName);
            this.Controls.Add(this.tb_sendCount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActiveMQ";
            this.Text = "ActiveMQ";
            this.Load += new System.EventHandler(this.ActiveMQ_Load);
            this.Resize += new System.EventHandler(this.ActiveMQ_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_publish;
        private System.Windows.Forms.ComboBox cb_TopicQueues;
        private System.Windows.Forms.Button bt_Discounect;
        private System.Windows.Forms.Label lb_info_clientId;
        private System.Windows.Forms.Label lb_info_address;
        private System.Windows.Forms.CheckBox ck_isDurable;
        private MyCommonControl.Control.TextBoxWithWatermak tb_clientId;
        private MyCommonControl.Control.TextBoxWithWatermak tb_ConsumerTopic;
        private MyCommonControl.Control.TextBoxWithWatermak tb_uri;
        private System.Windows.Forms.Button bt_Subscribe;
        private System.Windows.Forms.Button bt_Connect;
        private MyCommonControl.Control.TextBoxWithWatermak tb_Password;
        private System.Windows.Forms.Label lb_info_userNmae;
        private System.Windows.Forms.Label lb_info_passWord;
        private MyCommonControl.Control.TextBoxWithWatermak tb_userName;
        private MyCommonControl.DataRecordBox Drb_messageReceive;
        private MyCommonControl.ListViewWithButton lv_pathList;
        private System.Windows.Forms.ColumnHeader columnHeader_topicOrQueue;
        private System.Windows.Forms.ColumnHeader columnHeader_del;
        private System.Windows.Forms.ComboBox cb_sendTopicQueues;
        private MyCommonControl.Control.TextBoxWithWatermak tb_sendTopic;
        private System.Windows.Forms.RichTextBox rtb_dataToSend;
        private System.Windows.Forms.ComboBox cb_sendTextByte;
        private MyCommonControl.Control.TextBoxWithWatermak tb_ConsumerName;
        private System.Windows.Forms.TextBox tb_sendCount;
    }
}

