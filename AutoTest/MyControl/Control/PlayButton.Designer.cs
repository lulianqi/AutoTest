namespace MyCommonControl
{
    partial class PlayButton
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayButton));
            this.imageList_ForPlayButton = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox_Set = new System.Windows.Forms.PictureBox();
            this.pictureBox_Pause = new System.Windows.Forms.PictureBox();
            this.pictureBox_Play = new System.Windows.Forms.PictureBox();
            this.pictureBox_outReport = new System.Windows.Forms.PictureBox();
            this.pictureBox_Remove = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Pause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Play)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_outReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Remove)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList_ForPlayButton
            // 
            this.imageList_ForPlayButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_ForPlayButton.ImageStream")));
            this.imageList_ForPlayButton.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_ForPlayButton.Images.SetKeyName(0, "pause.png");
            this.imageList_ForPlayButton.Images.SetKeyName(1, "pause_.png");
            this.imageList_ForPlayButton.Images.SetKeyName(2, "play.png");
            this.imageList_ForPlayButton.Images.SetKeyName(3, "stop.png");
            this.imageList_ForPlayButton.Images.SetKeyName(4, "outReport.png");
            this.imageList_ForPlayButton.Images.SetKeyName(5, "outReport_.png");
            this.imageList_ForPlayButton.Images.SetKeyName(6, "remove.png");
            this.imageList_ForPlayButton.Images.SetKeyName(7, "remove_.png");
            this.imageList_ForPlayButton.Images.SetKeyName(8, "set.png");
            this.imageList_ForPlayButton.Images.SetKeyName(9, "set_.png");
            // 
            // pictureBox_Set
            // 
            this.pictureBox_Set.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Set.Location = new System.Drawing.Point(72, 0);
            this.pictureBox_Set.Name = "pictureBox_Set";
            this.pictureBox_Set.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_Set.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Set.TabIndex = 8;
            this.pictureBox_Set.TabStop = false;
            this.pictureBox_Set.Click += new System.EventHandler(this.pictureBox_Set_Click);
            this.pictureBox_Set.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_Set.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_Pause
            // 
            this.pictureBox_Pause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Pause.Location = new System.Drawing.Point(36, 0);
            this.pictureBox_Pause.Name = "pictureBox_Pause";
            this.pictureBox_Pause.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_Pause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Pause.TabIndex = 7;
            this.pictureBox_Pause.TabStop = false;
            this.pictureBox_Pause.Click += new System.EventHandler(this.pictureBox_Pause_Click);
            this.pictureBox_Pause.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_Pause.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_Play
            // 
            this.pictureBox_Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Play.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Play.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Play.Name = "pictureBox_Play";
            this.pictureBox_Play.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_Play.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Play.TabIndex = 6;
            this.pictureBox_Play.TabStop = false;
            this.pictureBox_Play.Click += new System.EventHandler(this.pictureBox_Play_Click);
            this.pictureBox_Play.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_Play.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_outReport
            // 
            this.pictureBox_outReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_outReport.Location = new System.Drawing.Point(108, 0);
            this.pictureBox_outReport.Name = "pictureBox_outReport";
            this.pictureBox_outReport.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_outReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_outReport.TabIndex = 9;
            this.pictureBox_outReport.TabStop = false;
            this.pictureBox_outReport.Click += new System.EventHandler(this.pictureBox_outReport_Click);
            this.pictureBox_outReport.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_outReport.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_Remove
            // 
            this.pictureBox_Remove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Remove.Location = new System.Drawing.Point(144, 0);
            this.pictureBox_Remove.Name = "pictureBox_Remove";
            this.pictureBox_Remove.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_Remove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Remove.TabIndex = 10;
            this.pictureBox_Remove.TabStop = false;
            this.pictureBox_Remove.Click += new System.EventHandler(this.pictureBox_Remove_Click);
            this.pictureBox_Remove.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_Remove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // PlayButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_Remove);
            this.Controls.Add(this.pictureBox_outReport);
            this.Controls.Add(this.pictureBox_Set);
            this.Controls.Add(this.pictureBox_Pause);
            this.Controls.Add(this.pictureBox_Play);
            this.Name = "PlayButton";
            this.Size = new System.Drawing.Size(195, 36);
            this.Resize += new System.EventHandler(this.PlayButton_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Pause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Play)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_outReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Remove)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Pause;
        private System.Windows.Forms.PictureBox pictureBox_Play;
        private System.Windows.Forms.ImageList imageList_ForPlayButton;
        private System.Windows.Forms.PictureBox pictureBox_Set;
        private System.Windows.Forms.PictureBox pictureBox_outReport;
        private System.Windows.Forms.PictureBox pictureBox_Remove;
    }
}
