namespace MyCommonControl
{
    partial class DataRecordBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataRecordBox));
            this.richTextBox_dataContainer = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip_RecBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FreezeText_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoSave_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Fill_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_dataAddSave = new System.Windows.Forms.PictureBox();
            this.pictureBox_dataAddclean = new System.Windows.Forms.PictureBox();
            this.pictureBox_AlwaysGoBottom = new System.Windows.Forms.PictureBox();
            this.toolTip_DataRecordBox = new System.Windows.Forms.ToolTip(this.components);
            this.imageListForButton = new System.Windows.Forms.ImageList(this.components);
            this.ShowInNewWindow_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_RecBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddclean)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AlwaysGoBottom)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox_dataContainer
            // 
            this.richTextBox_dataContainer.BackColor = System.Drawing.Color.Azure;
            this.richTextBox_dataContainer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_dataContainer.ContextMenuStrip = this.contextMenuStrip_RecBox;
            this.richTextBox_dataContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_dataContainer.HideSelection = false;
            this.richTextBox_dataContainer.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_dataContainer.Name = "richTextBox_dataContainer";
            this.richTextBox_dataContainer.Size = new System.Drawing.Size(652, 182);
            this.richTextBox_dataContainer.TabIndex = 0;
            this.richTextBox_dataContainer.Text = "";
            // 
            // contextMenuStrip_RecBox
            // 
            this.contextMenuStrip_RecBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FreezeText_ToolStripMenuItem,
            this.AutoSave_ToolStripMenuItem,
            this.Fill_ToolStripMenuItem,
            this.ShowInNewWindow_ToolStripMenuItem});
            this.contextMenuStrip_RecBox.Name = "contextMenuStrip_RecBox";
            this.contextMenuStrip_RecBox.Size = new System.Drawing.Size(153, 114);
            // 
            // FreezeText_ToolStripMenuItem
            // 
            this.FreezeText_ToolStripMenuItem.Name = "FreezeText_ToolStripMenuItem";
            this.FreezeText_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FreezeText_ToolStripMenuItem.Text = "冻结追加显示";
            this.FreezeText_ToolStripMenuItem.Click += new System.EventHandler(this.FreezeText_ToolStripMenuItem_Click);
            // 
            // AutoSave_ToolStripMenuItem
            // 
            this.AutoSave_ToolStripMenuItem.Name = "AutoSave_ToolStripMenuItem";
            this.AutoSave_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AutoSave_ToolStripMenuItem.Text = "自动保存内容";
            this.AutoSave_ToolStripMenuItem.Click += new System.EventHandler(this.AutoSave_ToolStripMenuItem_Click);
            // 
            // Fill_ToolStripMenuItem
            // 
            this.Fill_ToolStripMenuItem.Name = "Fill_ToolStripMenuItem";
            this.Fill_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.Fill_ToolStripMenuItem.Text = "最大化显示";
            this.Fill_ToolStripMenuItem.Click += new System.EventHandler(this.Fill_ToolStripMenuItem_Click);
            // 
            // pictureBox_dataAddSave
            // 
            this.pictureBox_dataAddSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_dataAddSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_dataAddSave.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_dataAddSave.Image")));
            this.pictureBox_dataAddSave.Location = new System.Drawing.Point(601, 2);
            this.pictureBox_dataAddSave.Name = "pictureBox_dataAddSave";
            this.pictureBox_dataAddSave.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_dataAddSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_dataAddSave.TabIndex = 11;
            this.pictureBox_dataAddSave.TabStop = false;
            this.toolTip_DataRecordBox.SetToolTip(this.pictureBox_dataAddSave, "保存当前数据");
            this.pictureBox_dataAddSave.Click += new System.EventHandler(this.pictureBox_dataAddSave_Click);
            this.pictureBox_dataAddSave.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_dataAddSave.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_dataAddclean
            // 
            this.pictureBox_dataAddclean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_dataAddclean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_dataAddclean.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_dataAddclean.Image")));
            this.pictureBox_dataAddclean.Location = new System.Drawing.Point(627, 2);
            this.pictureBox_dataAddclean.Name = "pictureBox_dataAddclean";
            this.pictureBox_dataAddclean.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_dataAddclean.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_dataAddclean.TabIndex = 12;
            this.pictureBox_dataAddclean.TabStop = false;
            this.toolTip_DataRecordBox.SetToolTip(this.pictureBox_dataAddclean, "清除当前显示");
            this.pictureBox_dataAddclean.Click += new System.EventHandler(this.pictureBox_dataAddclean_Click);
            this.pictureBox_dataAddclean.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_dataAddclean.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_AlwaysGoBottom
            // 
            this.pictureBox_AlwaysGoBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_AlwaysGoBottom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_AlwaysGoBottom.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_AlwaysGoBottom.Image")));
            this.pictureBox_AlwaysGoBottom.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_AlwaysGoBottom.InitialImage")));
            this.pictureBox_AlwaysGoBottom.Location = new System.Drawing.Point(575, 2);
            this.pictureBox_AlwaysGoBottom.Name = "pictureBox_AlwaysGoBottom";
            this.pictureBox_AlwaysGoBottom.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_AlwaysGoBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_AlwaysGoBottom.TabIndex = 13;
            this.pictureBox_AlwaysGoBottom.TabStop = false;
            this.toolTip_DataRecordBox.SetToolTip(this.pictureBox_AlwaysGoBottom, "始终定位到尾部");
            this.pictureBox_AlwaysGoBottom.Click += new System.EventHandler(this.pictureBox_pictureBox_AlwaysGoBottom_Click);
            this.pictureBox_AlwaysGoBottom.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_AlwaysGoBottom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // imageListForButton
            // 
            this.imageListForButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForButton.ImageStream")));
            this.imageListForButton.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForButton.Images.SetKeyName(0, "bt_GoBottom_off");
            this.imageListForButton.Images.SetKeyName(1, "bt_GoBottom_on");
            this.imageListForButton.Images.SetKeyName(2, "bt_heat_0.png");
            this.imageListForButton.Images.SetKeyName(3, "bt_heat_1.png");
            this.imageListForButton.Images.SetKeyName(4, "bt_heat_3.png");
            this.imageListForButton.Images.SetKeyName(5, "bt_heat_2.png");
            // 
            // ShowInNewWindow_ToolStripMenuItem
            // 
            this.ShowInNewWindow_ToolStripMenuItem.Name = "ShowInNewWindow_ToolStripMenuItem";
            this.ShowInNewWindow_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ShowInNewWindow_ToolStripMenuItem.Text = "新窗口中查看";
            this.ShowInNewWindow_ToolStripMenuItem.Click += new System.EventHandler(this.ShowInNewWindow_ToolStripMenuItem_Click);
            // 
            // DataRecordBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.Controls.Add(this.pictureBox_AlwaysGoBottom);
            //this.Controls.Add(this.pictureBox_dataAddSave);
            //this.Controls.Add(this.pictureBox_dataAddclean);
            richTextBox_dataContainer.Controls.Add(this.pictureBox_AlwaysGoBottom);
            richTextBox_dataContainer.Controls.Add(this.pictureBox_dataAddSave);
            richTextBox_dataContainer.Controls.Add(this.pictureBox_dataAddclean);
            this.Controls.Add(this.richTextBox_dataContainer);
            this.Name = "DataRecordBox";
            this.Size = new System.Drawing.Size(652, 182);
            this.contextMenuStrip_RecBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddclean)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AlwaysGoBottom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_dataContainer;
        private System.Windows.Forms.PictureBox pictureBox_dataAddSave;
        private System.Windows.Forms.PictureBox pictureBox_dataAddclean;
        private System.Windows.Forms.PictureBox pictureBox_AlwaysGoBottom;
        private System.Windows.Forms.ToolTip toolTip_DataRecordBox;
        public System.Windows.Forms.ImageList imageListForButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_RecBox;
        private System.Windows.Forms.ToolStripMenuItem FreezeText_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoSave_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Fill_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowInNewWindow_ToolStripMenuItem;
    }
}
