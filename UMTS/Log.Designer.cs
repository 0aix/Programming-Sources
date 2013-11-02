namespace UMTS
{
    partial class Log
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Log));
            this.Logfile = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.UMTS = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // Logfile
            // 
            this.Logfile.BackColor = System.Drawing.Color.White;
            this.Logfile.Location = new System.Drawing.Point(12, 12);
            this.Logfile.MaxLength = 32627;
            this.Logfile.Multiline = true;
            this.Logfile.Name = "Logfile";
            this.Logfile.ReadOnly = true;
            this.Logfile.Size = new System.Drawing.Size(268, 242);
            this.Logfile.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UMTS
            // 
            this.UMTS.BalloonTipText = "Double-click here to open the UMTS Transaction Log";
            this.UMTS.BalloonTipTitle = "UMTS";
            this.UMTS.Icon = ((System.Drawing.Icon)(resources.GetObject("UMTS.Icon")));
            this.UMTS.Text = "notifyIcon1";
            this.UMTS.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UMTS_MouseDoubleClick);
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.Logfile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Log";
            this.Text = "Transaction Log - [0]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Logfile;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon UMTS;
    }
}