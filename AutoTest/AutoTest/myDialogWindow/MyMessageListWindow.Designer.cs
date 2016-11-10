using AutoTest.myControl;
using MyCommonControl;
namespace AutoTest.myDialogWindow
{
    partial class MyMessageListWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyMessageListWindow));
            this.lb_windowInfo = new System.Windows.Forms.Label();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.pictureBox_refresh = new System.Windows.Forms.PictureBox();
            this.pictureBox_delAll = new System.Windows.Forms.PictureBox();
            this.listView_infoList = new ListViewExDB();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_refresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_delAll)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_windowInfo
            // 
            this.lb_windowInfo.AutoSize = true;
            this.lb_windowInfo.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_windowInfo.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lb_windowInfo.Location = new System.Drawing.Point(14, 14);
            this.lb_windowInfo.Name = "lb_windowInfo";
            this.lb_windowInfo.Size = new System.Drawing.Size(47, 13);
            this.lb_windowInfo.TabIndex = 12;
            this.lb_windowInfo.Text = "Infor";
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_close.Image = global::AutoTest.Properties.Resources._2013112005093916_easyicon_net_512;
            this.pictureBox_close.Location = new System.Drawing.Point(1100, 7);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_close.TabIndex = 13;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            // 
            // pictureBox_refresh
            // 
            this.pictureBox_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_refresh.Image = global::AutoTest.Properties.Resources._20141211063254953_easyicon_net_256;
            this.pictureBox_refresh.Location = new System.Drawing.Point(1044, 7);
            this.pictureBox_refresh.Name = "pictureBox_refresh";
            this.pictureBox_refresh.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_refresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_refresh.TabIndex = 16;
            this.pictureBox_refresh.TabStop = false;
            this.pictureBox_refresh.Click += new System.EventHandler(this.pictureBox_refresh_Click);
            // 
            // pictureBox_delAll
            // 
            this.pictureBox_delAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_delAll.Image = global::AutoTest.Properties.Resources._20131120052552411_easyicon_net_118;
            this.pictureBox_delAll.Location = new System.Drawing.Point(1072, 7);
            this.pictureBox_delAll.Name = "pictureBox_delAll";
            this.pictureBox_delAll.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_delAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_delAll.TabIndex = 15;
            this.pictureBox_delAll.TabStop = false;
            this.pictureBox_delAll.Click += new System.EventHandler(this.pictureBox_delAll_Click);
            // 
            // listView_infoList
            // 
            this.listView_infoList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView_infoList.FullRowSelect = true;
            this.listView_infoList.Location = new System.Drawing.Point(7, 35);
            this.listView_infoList.Name = "listView_infoList";
            this.listView_infoList.Size = new System.Drawing.Size(1119, 557);
            this.listView_infoList.TabIndex = 14;
            this.listView_infoList.UseCompatibleStateImageBehavior = false;
            this.listView_infoList.View = System.Windows.Forms.View.Details;
            this.listView_infoList.DoubleClick += new System.EventHandler(this.listView_infoList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Key";
            this.columnHeader1.Width = 146;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 959;
            // 
            // MyMessageListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 616);
            this.Controls.Add(this.pictureBox_refresh);
            this.Controls.Add(this.pictureBox_delAll);
            this.Controls.Add(this.listView_infoList);
            this.Controls.Add(this.pictureBox_close);
            this.Controls.Add(this.lb_windowInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyMessageListWindow";
            this.Text = "MyMessageListWindow";
            this.Load += new System.EventHandler(this.MyMessageListWindow_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyMessageListWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MyMessageListWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MyMessageListWindow_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_refresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_delAll)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_windowInfo;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private ListViewExDB listView_infoList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.PictureBox pictureBox_refresh;
        private System.Windows.Forms.PictureBox pictureBox_delAll;
    }
}