﻿namespace UMTS
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.uid = new System.Windows.Forms.TextBox();
            this.pid = new System.Windows.Forms.TextBox();
            this.rid = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Round:";
            // 
            // uid
            // 
            this.uid.Location = new System.Drawing.Point(76, 6);
            this.uid.MaxLength = 15;
            this.uid.Name = "uid";
            this.uid.Size = new System.Drawing.Size(101, 20);
            this.uid.TabIndex = 3;
            this.uid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uid_KeyDown);
            // 
            // pid
            // 
            this.pid.Location = new System.Drawing.Point(76, 35);
            this.pid.MaxLength = 15;
            this.pid.Name = "pid";
            this.pid.PasswordChar = '*';
            this.pid.Size = new System.Drawing.Size(101, 20);
            this.pid.TabIndex = 4;
            this.pid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pid_KeyDown);
            // 
            // rid
            // 
            this.rid.Location = new System.Drawing.Point(76, 64);
            this.rid.MaxLength = 15;
            this.rid.Name = "rid";
            this.rid.Size = new System.Drawing.Size(101, 20);
            this.rid.TabIndex = 5;
            this.rid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rid_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(72, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "&Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 129);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rid);
            this.Controls.Add(this.pid);
            this.Controls.Add(this.uid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Text = "Login to the UMTS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox uid;
        private System.Windows.Forms.TextBox pid;
        private System.Windows.Forms.TextBox rid;
        private System.Windows.Forms.Button button1;
    }
}