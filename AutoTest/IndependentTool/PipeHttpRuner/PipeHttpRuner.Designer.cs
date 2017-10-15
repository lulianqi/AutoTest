namespace PipeHttpRuner
{
    partial class PipeHttpRuner
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
            MyCommonControl.Control.TextBoxWithWatermak tb_editHeadKey;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PipeHttpRuner));
            this.lb_pipeHost = new System.Windows.Forms.Label();
            this.lv_pipeList = new System.Windows.Forms.ListView();
            this.columnHeader_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_reConectCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lb_reConTime = new System.Windows.Forms.Label();
            this.lb_responseType = new System.Windows.Forms.Label();
            this.cb_responseType = new System.Windows.Forms.ComboBox();
            this.bt_addPile = new System.Windows.Forms.Button();
            this.bt_sendRequest = new System.Windows.Forms.Button();
            this.cb_isAsynSend = new System.Windows.Forms.CheckBox();
            this.lb_requestCount = new System.Windows.Forms.Label();
            this.bt_connectAllPile = new System.Windows.Forms.Button();
            this.tb_rawRequest = new System.Windows.Forms.TextBox();
            this.panel_editRequest = new System.Windows.Forms.Panel();
            this.lb_editHeads = new System.Windows.Forms.Label();
            this.bt_editAddHead = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader_heads = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cb_editRequstEdition = new System.Windows.Forms.ComboBox();
            this.cb_editRequstMethod = new System.Windows.Forms.ComboBox();
            this.lb_editStartLine = new System.Windows.Forms.Label();
            this.tb_editRequestBody = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_editHeadVaule = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_editSartLine = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_RequstCount = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_addTime = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_reConTime = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_pilePort = new MyCommonControl.Control.TextBoxWithWatermak();
            this.tb_pileHost = new MyCommonControl.Control.TextBoxWithWatermak();
            this.rtb_dataRecieve = new MyCommonControl.DataRecordBox();
            this.pictureBox_caseParameter = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            tb_editHeadKey = new MyCommonControl.Control.TextBoxWithWatermak();
            this.panel_editRequest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_caseParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_pipeHost
            // 
            this.lb_pipeHost.AutoSize = true;
            this.lb_pipeHost.Location = new System.Drawing.Point(1, 322);
            this.lb_pipeHost.Name = "lb_pipeHost";
            this.lb_pipeHost.Size = new System.Drawing.Size(65, 12);
            this.lb_pipeHost.TabIndex = 4;
            this.lb_pipeHost.Text = "pipe host:";
            // 
            // lv_pipeList
            // 
            this.lv_pipeList.BackColor = System.Drawing.Color.AliceBlue;
            this.lv_pipeList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_id,
            this.columnHeader_reConectCount});
            this.lv_pipeList.FullRowSelect = true;
            this.lv_pipeList.Location = new System.Drawing.Point(821, 3);
            this.lv_pipeList.Name = "lv_pipeList";
            this.lv_pipeList.Size = new System.Drawing.Size(139, 520);
            this.lv_pipeList.TabIndex = 5;
            this.lv_pipeList.UseCompatibleStateImageBehavior = false;
            this.lv_pipeList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_id
            // 
            this.columnHeader_id.Text = "ID";
            this.columnHeader_id.Width = 34;
            // 
            // columnHeader_reConectCount
            // 
            this.columnHeader_reConectCount.Text = "reConectCount";
            this.columnHeader_reConectCount.Width = 97;
            // 
            // lb_reConTime
            // 
            this.lb_reConTime.AutoSize = true;
            this.lb_reConTime.Location = new System.Drawing.Point(623, 345);
            this.lb_reConTime.Name = "lb_reConTime";
            this.lb_reConTime.Size = new System.Drawing.Size(65, 12);
            this.lb_reConTime.TabIndex = 7;
            this.lb_reConTime.Text = "ReConTime:";
            // 
            // lb_responseType
            // 
            this.lb_responseType.AutoSize = true;
            this.lb_responseType.Location = new System.Drawing.Point(623, 375);
            this.lb_responseType.Name = "lb_responseType";
            this.lb_responseType.Size = new System.Drawing.Size(59, 12);
            this.lb_responseType.TabIndex = 8;
            this.lb_responseType.Text = "Response:";
            // 
            // cb_responseType
            // 
            this.cb_responseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_responseType.FormattingEnabled = true;
            this.cb_responseType.Items.AddRange(new object[] {
            "Report",
            "Drop"});
            this.cb_responseType.Location = new System.Drawing.Point(689, 371);
            this.cb_responseType.Name = "cb_responseType";
            this.cb_responseType.Size = new System.Drawing.Size(129, 20);
            this.cb_responseType.TabIndex = 9;
            // 
            // bt_addPile
            // 
            this.bt_addPile.Location = new System.Drawing.Point(703, 403);
            this.bt_addPile.Name = "bt_addPile";
            this.bt_addPile.Size = new System.Drawing.Size(115, 23);
            this.bt_addPile.TabIndex = 11;
            this.bt_addPile.Text = "Add Pipe";
            this.bt_addPile.UseVisualStyleBackColor = true;
            this.bt_addPile.Click += new System.EventHandler(this.bt_addPile_Click);
            // 
            // bt_sendRequest
            // 
            this.bt_sendRequest.Location = new System.Drawing.Point(703, 492);
            this.bt_sendRequest.Name = "bt_sendRequest";
            this.bt_sendRequest.Size = new System.Drawing.Size(115, 23);
            this.bt_sendRequest.TabIndex = 12;
            this.bt_sendRequest.Text = "Send Request";
            this.bt_sendRequest.UseVisualStyleBackColor = true;
            this.bt_sendRequest.Click += new System.EventHandler(this.bt_sendRequest_Click);
            // 
            // cb_isAsynSend
            // 
            this.cb_isAsynSend.AutoSize = true;
            this.cb_isAsynSend.Location = new System.Drawing.Point(620, 496);
            this.cb_isAsynSend.Name = "cb_isAsynSend";
            this.cb_isAsynSend.Size = new System.Drawing.Size(84, 16);
            this.cb_isAsynSend.TabIndex = 13;
            this.cb_isAsynSend.Text = "isAsynSend";
            this.cb_isAsynSend.UseVisualStyleBackColor = true;
            // 
            // lb_requestCount
            // 
            this.lb_requestCount.AutoSize = true;
            this.lb_requestCount.Location = new System.Drawing.Point(623, 467);
            this.lb_requestCount.Name = "lb_requestCount";
            this.lb_requestCount.Size = new System.Drawing.Size(77, 12);
            this.lb_requestCount.TabIndex = 15;
            this.lb_requestCount.Text = "RequstCount:";
            // 
            // bt_connectAllPile
            // 
            this.bt_connectAllPile.Location = new System.Drawing.Point(703, 432);
            this.bt_connectAllPile.Name = "bt_connectAllPile";
            this.bt_connectAllPile.Size = new System.Drawing.Size(115, 23);
            this.bt_connectAllPile.TabIndex = 16;
            this.bt_connectAllPile.Text = "ConnectAllPile";
            this.bt_connectAllPile.UseVisualStyleBackColor = true;
            this.bt_connectAllPile.Click += new System.EventHandler(this.bt_connectAllPile_Click);
            // 
            // tb_rawRequest
            // 
            this.tb_rawRequest.BackColor = System.Drawing.Color.Azure;
            this.tb_rawRequest.Location = new System.Drawing.Point(3, 342);
            this.tb_rawRequest.Multiline = true;
            this.tb_rawRequest.Name = "tb_rawRequest";
            this.tb_rawRequest.Size = new System.Drawing.Size(611, 181);
            this.tb_rawRequest.TabIndex = 17;
            this.tb_rawRequest.Text = "GET http://www.baidu.com HTTP/1.1\r\nContent-Type: application/x-www-form-urlencode" +
    "d\r\nHost: www.baidu.com\r\nConnection: Keep-Alive\r\n\r\n";
            this.tb_rawRequest.Leave += new System.EventHandler(this.tb_rawRequest_Leave);
            // 
            // panel_editRequest
            // 
            this.panel_editRequest.BackColor = System.Drawing.Color.Lavender;
            this.panel_editRequest.Controls.Add(this.pictureBox1);
            this.panel_editRequest.Controls.Add(this.pictureBox_caseParameter);
            this.panel_editRequest.Controls.Add(this.tb_editRequestBody);
            this.panel_editRequest.Controls.Add(this.lb_editHeads);
            this.panel_editRequest.Controls.Add(this.tb_editHeadVaule);
            this.panel_editRequest.Controls.Add(tb_editHeadKey);
            this.panel_editRequest.Controls.Add(this.tb_editSartLine);
            this.panel_editRequest.Controls.Add(this.bt_editAddHead);
            this.panel_editRequest.Controls.Add(this.listView1);
            this.panel_editRequest.Controls.Add(this.cb_editRequstEdition);
            this.panel_editRequest.Controls.Add(this.cb_editRequstMethod);
            this.panel_editRequest.Controls.Add(this.lb_editStartLine);
            this.panel_editRequest.Location = new System.Drawing.Point(3, 342);
            this.panel_editRequest.Name = "panel_editRequest";
            this.panel_editRequest.Size = new System.Drawing.Size(611, 181);
            this.panel_editRequest.TabIndex = 18;
            // 
            // lb_editHeads
            // 
            this.lb_editHeads.AutoSize = true;
            this.lb_editHeads.Location = new System.Drawing.Point(3, 37);
            this.lb_editHeads.Name = "lb_editHeads";
            this.lb_editHeads.Size = new System.Drawing.Size(41, 12);
            this.lb_editHeads.TabIndex = 22;
            this.lb_editHeads.Text = "Heads:";
            // 
            // bt_editAddHead
            // 
            this.bt_editAddHead.Location = new System.Drawing.Point(291, 30);
            this.bt_editAddHead.Name = "bt_editAddHead";
            this.bt_editAddHead.Size = new System.Drawing.Size(64, 23);
            this.bt_editAddHead.TabIndex = 15;
            this.bt_editAddHead.Text = "AddHead>";
            this.bt_editAddHead.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_heads});
            this.listView1.Location = new System.Drawing.Point(361, 29);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(246, 149);
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_heads
            // 
            this.columnHeader_heads.Text = "Heads";
            this.columnHeader_heads.Width = 241;
            // 
            // cb_editRequstEdition
            // 
            this.cb_editRequstEdition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_editRequstEdition.FormattingEnabled = true;
            this.cb_editRequstEdition.Items.AddRange(new object[] {
            "Report",
            "Drop"});
            this.cb_editRequstEdition.Location = new System.Drawing.Point(471, 5);
            this.cb_editRequstEdition.Name = "cb_editRequstEdition";
            this.cb_editRequstEdition.Size = new System.Drawing.Size(78, 20);
            this.cb_editRequstEdition.TabIndex = 12;
            // 
            // cb_editRequstMethod
            // 
            this.cb_editRequstMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_editRequstMethod.FormattingEnabled = true;
            this.cb_editRequstMethod.Items.AddRange(new object[] {
            "Report",
            "Drop"});
            this.cb_editRequstMethod.Location = new System.Drawing.Point(76, 5);
            this.cb_editRequstMethod.Name = "cb_editRequstMethod";
            this.cb_editRequstMethod.Size = new System.Drawing.Size(78, 20);
            this.cb_editRequstMethod.TabIndex = 10;
            // 
            // lb_editStartLine
            // 
            this.lb_editStartLine.AutoSize = true;
            this.lb_editStartLine.Location = new System.Drawing.Point(3, 9);
            this.lb_editStartLine.Name = "lb_editStartLine";
            this.lb_editStartLine.Size = new System.Drawing.Size(71, 12);
            this.lb_editStartLine.TabIndex = 5;
            this.lb_editStartLine.Text = "Start Line:";
            // 
            // tb_editRequestBody
            // 
            this.tb_editRequestBody.Location = new System.Drawing.Point(5, 56);
            this.tb_editRequestBody.Multiline = true;
            this.tb_editRequestBody.Name = "tb_editRequestBody";
            this.tb_editRequestBody.Size = new System.Drawing.Size(350, 121);
            this.tb_editRequestBody.TabIndex = 23;
            this.tb_editRequestBody.WatermarkText = "Request Body";
            // 
            // tb_editHeadVaule
            // 
            this.tb_editHeadVaule.Location = new System.Drawing.Point(117, 31);
            this.tb_editHeadVaule.Name = "tb_editHeadVaule";
            this.tb_editHeadVaule.Size = new System.Drawing.Size(174, 21);
            this.tb_editHeadVaule.TabIndex = 21;
            this.tb_editHeadVaule.Text = "Keep-Alive";
            this.tb_editHeadVaule.WatermarkText = "head vaule";
            // 
            // tb_editHeadKey
            // 
            tb_editHeadKey.Location = new System.Drawing.Point(43, 31);
            tb_editHeadKey.Name = "tb_editHeadKey";
            tb_editHeadKey.Size = new System.Drawing.Size(72, 21);
            tb_editHeadKey.TabIndex = 20;
            tb_editHeadKey.Text = "Connection";
            tb_editHeadKey.WatermarkText = "head key";
            // 
            // tb_editSartLine
            // 
            this.tb_editSartLine.Location = new System.Drawing.Point(160, 5);
            this.tb_editSartLine.Name = "tb_editSartLine";
            this.tb_editSartLine.Size = new System.Drawing.Size(305, 21);
            this.tb_editSartLine.TabIndex = 19;
            this.tb_editSartLine.Text = "www.baidu.com";
            this.tb_editSartLine.WatermarkText = "http://www.baidu.com";
            // 
            // tb_RequstCount
            // 
            this.tb_RequstCount.Location = new System.Drawing.Point(703, 463);
            this.tb_RequstCount.Name = "tb_RequstCount";
            this.tb_RequstCount.Size = new System.Drawing.Size(115, 21);
            this.tb_RequstCount.TabIndex = 14;
            this.tb_RequstCount.Text = "1";
            this.tb_RequstCount.WatermarkText = "RequstCount/pipe";
            // 
            // tb_addTime
            // 
            this.tb_addTime.Location = new System.Drawing.Point(620, 404);
            this.tb_addTime.Name = "tb_addTime";
            this.tb_addTime.Size = new System.Drawing.Size(77, 21);
            this.tb_addTime.TabIndex = 10;
            this.tb_addTime.Text = "1";
            this.tb_addTime.WatermarkText = "add time";
            // 
            // tb_reConTime
            // 
            this.tb_reConTime.Location = new System.Drawing.Point(688, 342);
            this.tb_reConTime.Name = "tb_reConTime";
            this.tb_reConTime.Size = new System.Drawing.Size(130, 21);
            this.tb_reConTime.TabIndex = 6;
            this.tb_reConTime.Text = "0";
            this.tb_reConTime.WatermarkText = "0 is never reconnect";
            // 
            // tb_pilePort
            // 
            this.tb_pilePort.Location = new System.Drawing.Point(349, 318);
            this.tb_pilePort.Name = "tb_pilePort";
            this.tb_pilePort.Size = new System.Drawing.Size(61, 21);
            this.tb_pilePort.TabIndex = 3;
            this.tb_pilePort.Text = "80";
            this.tb_pilePort.WatermarkText = "pipe port";
            this.tb_pilePort.TextChanged += new System.EventHandler(this.tb_pilePort_TextChanged);
            // 
            // tb_pileHost
            // 
            this.tb_pileHost.Location = new System.Drawing.Point(73, 318);
            this.tb_pileHost.Name = "tb_pileHost";
            this.tb_pileHost.Size = new System.Drawing.Size(260, 21);
            this.tb_pileHost.TabIndex = 2;
            this.tb_pileHost.Text = "www.baidu.com";
            this.tb_pileHost.WatermarkText = "pipe adress ip or host";
            this.tb_pileHost.TextChanged += new System.EventHandler(this.tb_pileHost_TextChanged);
            // 
            // rtb_dataRecieve
            // 
            this.rtb_dataRecieve.CanFill = true;
            this.rtb_dataRecieve.IsShowIcon = true;
            this.rtb_dataRecieve.Location = new System.Drawing.Point(3, 3);
            this.rtb_dataRecieve.MaxLine = 5000;
            this.rtb_dataRecieve.MianDirectory = "DataRecord";
            this.rtb_dataRecieve.Name = "rtb_dataRecieve";
            this.rtb_dataRecieve.Size = new System.Drawing.Size(815, 311);
            this.rtb_dataRecieve.TabIndex = 0;
            // 
            // pictureBox_caseParameter
            // 
            this.pictureBox_caseParameter.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_caseParameter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_caseParameter.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_caseParameter.Image")));
            this.pictureBox_caseParameter.Location = new System.Drawing.Point(584, 3);
            this.pictureBox_caseParameter.Name = "pictureBox_caseParameter";
            this.pictureBox_caseParameter.Size = new System.Drawing.Size(23, 22);
            this.pictureBox_caseParameter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_caseParameter.TabIndex = 35;
            this.pictureBox_caseParameter.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(558, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(620, 431);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(23, 22);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 37;
            this.pictureBox2.TabStop = false;
            // 
            // PipeHttpRuner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 527);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel_editRequest);
            this.Controls.Add(this.tb_rawRequest);
            this.Controls.Add(this.bt_connectAllPile);
            this.Controls.Add(this.lb_requestCount);
            this.Controls.Add(this.tb_RequstCount);
            this.Controls.Add(this.cb_isAsynSend);
            this.Controls.Add(this.bt_sendRequest);
            this.Controls.Add(this.bt_addPile);
            this.Controls.Add(this.tb_addTime);
            this.Controls.Add(this.cb_responseType);
            this.Controls.Add(this.lb_responseType);
            this.Controls.Add(this.lb_reConTime);
            this.Controls.Add(this.tb_reConTime);
            this.Controls.Add(this.lv_pipeList);
            this.Controls.Add(this.lb_pipeHost);
            this.Controls.Add(this.tb_pilePort);
            this.Controls.Add(this.tb_pileHost);
            this.Controls.Add(this.rtb_dataRecieve);
            this.Name = "PipeHttpRuner";
            this.Text = "PipeHttp";
            this.Load += new System.EventHandler(this.PipeHttpRuner_Load);
            this.panel_editRequest.ResumeLayout(false);
            this.panel_editRequest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_caseParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyCommonControl.DataRecordBox rtb_dataRecieve;
        private MyCommonControl.Control.TextBoxWithWatermak tb_pileHost;
        private MyCommonControl.Control.TextBoxWithWatermak tb_pilePort;
        private System.Windows.Forms.Label lb_pipeHost;
        private System.Windows.Forms.ListView lv_pipeList;
        private System.Windows.Forms.ColumnHeader columnHeader_id;
        private System.Windows.Forms.ColumnHeader columnHeader_reConectCount;
        private MyCommonControl.Control.TextBoxWithWatermak tb_reConTime;
        private System.Windows.Forms.Label lb_reConTime;
        private System.Windows.Forms.Label lb_responseType;
        private System.Windows.Forms.ComboBox cb_responseType;
        private MyCommonControl.Control.TextBoxWithWatermak tb_addTime;
        private System.Windows.Forms.Button bt_addPile;
        private System.Windows.Forms.Button bt_sendRequest;
        private System.Windows.Forms.CheckBox cb_isAsynSend;
        private MyCommonControl.Control.TextBoxWithWatermak tb_RequstCount;
        private System.Windows.Forms.Label lb_requestCount;
        private System.Windows.Forms.Button bt_connectAllPile;
        private System.Windows.Forms.TextBox tb_rawRequest;
        private System.Windows.Forms.Panel panel_editRequest;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader_heads;
        private System.Windows.Forms.ComboBox cb_editRequstEdition;
        private System.Windows.Forms.ComboBox cb_editRequstMethod;
        private System.Windows.Forms.Label lb_editStartLine;
        private System.Windows.Forms.Button bt_editAddHead;
        private System.Windows.Forms.Label lb_editHeads;
        private MyCommonControl.Control.TextBoxWithWatermak tb_editHeadVaule;
        private MyCommonControl.Control.TextBoxWithWatermak tb_editSartLine;
        private MyCommonControl.Control.TextBoxWithWatermak tb_editRequestBody;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox_caseParameter;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

