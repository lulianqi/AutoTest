namespace AutoTest.myDialogWindow
{
    partial class RemoteRunner
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
            this.RemoteRunnerView = new AutoTest.myControl.ListView_RemoteRunnerView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // RemoteRunnerView
            // 
            this.RemoteRunnerView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.RemoteRunnerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RemoteRunnerView.Location = new System.Drawing.Point(0, 0);
            this.RemoteRunnerView.Name = "RemoteRunnerView";
            this.RemoteRunnerView.Size = new System.Drawing.Size(704, 320);
            this.RemoteRunnerView.TabIndex = 13;
            this.RemoteRunnerView.UseCompatibleStateImageBehavior = false;
            this.RemoteRunnerView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "NowCell";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "RunDetails";
            this.columnHeader3.Width = 177;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Time";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Result";
            this.columnHeader5.Width = 63;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "RunProcesse";
            this.columnHeader6.Width = 115;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "State";
            this.columnHeader7.Width = 53;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "RunnerControl";
            this.columnHeader8.Width = 91;
            // 
            // RemoteRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 320);
            this.Controls.Add(this.RemoteRunnerView);
            this.Name = "RemoteRunner";
            this.Text = "RemoteRunner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemoteRunner_FormClosed);
            this.Load += new System.EventHandler(this.RemoteRunner_Load);
            this.Click += new System.EventHandler(this.RemoteRunner_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private myControl.ListView_RemoteRunnerView RemoteRunnerView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
    }
}