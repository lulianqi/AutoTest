using MyCommonControl;
namespace AutoTest.myDialogWindow
{
    partial class myCaseParameter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(myCaseParameter));
            this.listView_CaseParameter = new MyCommonControl.ListViewExDB();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tb_keyAdd = new System.Windows.Forms.TextBox();
            this.tb_valueAdd = new System.Windows.Forms.TextBox();
            this.lb_info_keyValue = new System.Windows.Forms.Label();
            this.lb_info_parameter = new System.Windows.Forms.Label();
            this.pictureBox_set = new System.Windows.Forms.PictureBox();
            this.pictureBox_next = new System.Windows.Forms.PictureBox();
            this.pictureBox_refresh = new System.Windows.Forms.PictureBox();
            this.pictureBox_add = new System.Windows.Forms.PictureBox();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.toolTip_info = new System.Windows.Forms.ToolTip(this.components);
            this.lb_info_dataSouce = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_next)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_refresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_add)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            this.SuspendLayout();
            // 
            // listView_CaseParameter
            // 
            this.listView_CaseParameter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView_CaseParameter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listView_CaseParameter.FullRowSelect = true;
            this.listView_CaseParameter.Location = new System.Drawing.Point(6, 31);
            this.listView_CaseParameter.Name = "listView_CaseParameter";
            this.listView_CaseParameter.Size = new System.Drawing.Size(348, 247);
            this.listView_CaseParameter.TabIndex = 0;
            this.listView_CaseParameter.UseCompatibleStateImageBehavior = false;
            this.listView_CaseParameter.View = System.Windows.Forms.View.Details;
            this.listView_CaseParameter.SelectedIndexChanged += new System.EventHandler(this.listView_CaseParameter_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "name";
            this.columnHeader1.Width = 93;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "value";
            this.columnHeader2.Width = 248;
            // 
            // tb_keyAdd
            // 
            this.tb_keyAdd.Location = new System.Drawing.Point(6, 287);
            this.tb_keyAdd.Name = "tb_keyAdd";
            this.tb_keyAdd.Size = new System.Drawing.Size(97, 21);
            this.tb_keyAdd.TabIndex = 2;
            // 
            // tb_valueAdd
            // 
            this.tb_valueAdd.Location = new System.Drawing.Point(109, 287);
            this.tb_valueAdd.Name = "tb_valueAdd";
            this.tb_valueAdd.Size = new System.Drawing.Size(220, 21);
            this.tb_valueAdd.TabIndex = 3;
            // 
            // lb_info_keyValue
            // 
            this.lb_info_keyValue.AutoSize = true;
            this.lb_info_keyValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.lb_info_keyValue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lb_info_keyValue.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_info_keyValue.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lb_info_keyValue.Location = new System.Drawing.Point(8, 9);
            this.lb_info_keyValue.Name = "lb_info_keyValue";
            this.lb_info_keyValue.Size = new System.Drawing.Size(71, 13);
            this.lb_info_keyValue.TabIndex = 11;
            this.lb_info_keyValue.Text = "KeyValue";
            this.lb_info_keyValue.Click += new System.EventHandler(this.lb_info_runTimeParameter_Click);
            this.lb_info_keyValue.MouseLeave += new System.EventHandler(this.lb_info_MouseLeave);
            this.lb_info_keyValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lb_info_MouseMove);
            // 
            // lb_info_parameter
            // 
            this.lb_info_parameter.AutoSize = true;
            this.lb_info_parameter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lb_info_parameter.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_info_parameter.ForeColor = System.Drawing.Color.DarkGray;
            this.lb_info_parameter.Location = new System.Drawing.Point(85, 9);
            this.lb_info_parameter.Name = "lb_info_parameter";
            this.lb_info_parameter.Size = new System.Drawing.Size(79, 13);
            this.lb_info_parameter.TabIndex = 12;
            this.lb_info_parameter.Text = "Parameter";
            this.lb_info_parameter.Click += new System.EventHandler(this.lb_info_runTimeParameter_Click);
            this.lb_info_parameter.MouseLeave += new System.EventHandler(this.lb_info_MouseLeave);
            this.lb_info_parameter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lb_info_MouseMove);
            // 
            // pictureBox_set
            // 
            this.pictureBox_set.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_set.Image = global::AutoTest.Properties.Resources._1168178;
            this.pictureBox_set.Location = new System.Drawing.Point(306, 286);
            this.pictureBox_set.Name = "pictureBox_set";
            this.pictureBox_set.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_set.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_set.TabIndex = 15;
            this.pictureBox_set.TabStop = false;
            this.toolTip_info.SetToolTip(this.pictureBox_set, "编辑该数据");
            this.pictureBox_set.Click += new System.EventHandler(this.pictureBox_set_Click);
            // 
            // pictureBox_next
            // 
            this.pictureBox_next.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_next.Image = global::AutoTest.Properties.Resources._20150621012209230_easyicon_net_128;
            this.pictureBox_next.Location = new System.Drawing.Point(282, 286);
            this.pictureBox_next.Name = "pictureBox_next";
            this.pictureBox_next.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_next.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_next.TabIndex = 14;
            this.pictureBox_next.TabStop = false;
            this.toolTip_info.SetToolTip(this.pictureBox_next, "取下一次运行值");
            this.pictureBox_next.Click += new System.EventHandler(this.pictureBox_next_Click);
            // 
            // pictureBox_refresh
            // 
            this.pictureBox_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_refresh.Image = global::AutoTest.Properties.Resources._20141211063254953_easyicon_net_256;
            this.pictureBox_refresh.Location = new System.Drawing.Point(258, 286);
            this.pictureBox_refresh.Name = "pictureBox_refresh";
            this.pictureBox_refresh.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_refresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_refresh.TabIndex = 13;
            this.pictureBox_refresh.TabStop = false;
            this.toolTip_info.SetToolTip(this.pictureBox_refresh, "刷新所有数据");
            this.pictureBox_refresh.Click += new System.EventHandler(this.pictureBox_refresh_Click);
            // 
            // pictureBox_add
            // 
            this.pictureBox_add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_add.Image = global::AutoTest.Properties.Resources._20140924023908574_easyicon_net_128;
            this.pictureBox_add.Location = new System.Drawing.Point(330, 286);
            this.pictureBox_add.Name = "pictureBox_add";
            this.pictureBox_add.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_add.TabIndex = 10;
            this.pictureBox_add.TabStop = false;
            this.toolTip_info.SetToolTip(this.pictureBox_add, "修改&添加数据");
            this.pictureBox_add.Click += new System.EventHandler(this.pictureBox_add_Click);
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_close.Image = global::AutoTest.Properties.Resources._2013112005093916_easyicon_net_512;
            this.pictureBox_close.Location = new System.Drawing.Point(331, 5);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_close.TabIndex = 9;
            this.pictureBox_close.TabStop = false;
            this.toolTip_info.SetToolTip(this.pictureBox_close, "关闭");
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            // 
            // lb_info_dataSouce
            // 
            this.lb_info_dataSouce.AutoSize = true;
            this.lb_info_dataSouce.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lb_info_dataSouce.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_info_dataSouce.ForeColor = System.Drawing.Color.DarkGray;
            this.lb_info_dataSouce.Location = new System.Drawing.Point(168, 9);
            this.lb_info_dataSouce.Name = "lb_info_dataSouce";
            this.lb_info_dataSouce.Size = new System.Drawing.Size(79, 13);
            this.lb_info_dataSouce.TabIndex = 16;
            this.lb_info_dataSouce.Text = "DataSouce";
            this.lb_info_dataSouce.Click += new System.EventHandler(this.lb_info_runTimeParameter_Click);
            this.lb_info_dataSouce.MouseLeave += new System.EventHandler(this.lb_info_MouseLeave);
            this.lb_info_dataSouce.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lb_info_MouseMove);
            // 
            // myCaseParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 317);
            this.Controls.Add(this.lb_info_dataSouce);
            this.Controls.Add(this.lb_info_parameter);
            this.Controls.Add(this.lb_info_keyValue);
            this.Controls.Add(this.pictureBox_close);
            this.Controls.Add(this.tb_valueAdd);
            this.Controls.Add(this.tb_keyAdd);
            this.Controls.Add(this.listView_CaseParameter);
            this.Controls.Add(this.pictureBox_set);
            this.Controls.Add(this.pictureBox_next);
            this.Controls.Add(this.pictureBox_refresh);
            this.Controls.Add(this.pictureBox_add);
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(362, 317);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(362, 317);
            this.Name = "myCaseParameter";
            this.Text = "myCaseParameter";
            this.Load += new System.EventHandler(this.myCaseParameter_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.myCaseParameter_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.myCaseParameter_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.myCaseParameter_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_next)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_refresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_add)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewExDB listView_CaseParameter;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox tb_keyAdd;
        private System.Windows.Forms.TextBox tb_valueAdd;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.PictureBox pictureBox_add;
        private System.Windows.Forms.Label lb_info_keyValue;
        private System.Windows.Forms.Label lb_info_parameter;
        private System.Windows.Forms.PictureBox pictureBox_refresh;
        private System.Windows.Forms.PictureBox pictureBox_next;
        private System.Windows.Forms.PictureBox pictureBox_set;
        private System.Windows.Forms.ToolTip toolTip_info;
        private System.Windows.Forms.Label lb_info_dataSouce;
    }
}