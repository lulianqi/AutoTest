namespace SYDControls
{
    partial class ProgressForm
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
            this.progress = new SYDControls.SYDCircleProgress();
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.BackColor = System.Drawing.Color.Transparent;
            this.progress.BaseColor = System.Drawing.Color.LightSeaGreen;
            this.progress.Location = new System.Drawing.Point(43, 21);
            this.progress.Name = "progress";
            this.progress.RotationSpeed = 50;
            this.progress.Size = new System.Drawing.Size(100, 100);
            this.progress.SpokeThickness = 8;
            this.progress.TabIndex = 1;
            this.progress.Text = "sydCircleProgress1";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(186, 142);
            this.Controls.Add(this.progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProgressForm";
            this.TransparencyKey = System.Drawing.Color.White;
            this.ResumeLayout(false);

        }

        #endregion

        private SYDControls.SYDCircleProgress progress;



    }
}