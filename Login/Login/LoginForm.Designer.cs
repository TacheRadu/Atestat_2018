namespace Login
{
    partial class LoginForm
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
            this.submit = new System.Windows.Forms.Button();
            this.Support = new System.Windows.Forms.Button();
            this.user = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // submit
            // 
            this.submit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.submit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(20)))), ((int)(((byte)(89)))));
            this.submit.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.submit.Location = new System.Drawing.Point(365, 216);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(178, 27);
            this.submit.TabIndex = 2;
            this.submit.Text = "Login";
            this.submit.UseVisualStyleBackColor = false;
            this.submit.Click += new System.EventHandler(this.submission);
            // 
            // Support
            // 
            this.Support.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Support.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(20)))), ((int)(((byte)(89)))));
            this.Support.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Support.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Support.Location = new System.Drawing.Point(29, 276);
            this.Support.Name = "Support";
            this.Support.Size = new System.Drawing.Size(75, 62);
            this.Support.TabIndex = 4;
            this.Support.Text = "Support";
            this.Support.UseVisualStyleBackColor = false;
            this.Support.Click += new System.EventHandler(this.Support_Click);
            // 
            // user
            // 
            this.user.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(20)))), ((int)(((byte)(89)))));
            this.user.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user.ForeColor = System.Drawing.Color.White;
            this.user.Location = new System.Drawing.Point(275, 143);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(125, 23);
            this.user.TabIndex = 5;
            this.user.Text = "Username";
            this.user.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.user.Enter += new System.EventHandler(this.userFocus);
            this.user.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userEnter);
            // 
            // pass
            // 
            this.pass.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(20)))), ((int)(((byte)(89)))));
            this.pass.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pass.ForeColor = System.Drawing.Color.White;
            this.pass.Location = new System.Drawing.Point(520, 143);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(125, 23);
            this.pass.TabIndex = 6;
            this.pass.Text = "Password";
            this.pass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pass.Enter += new System.EventHandler(this.passFocus);
            this.pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passEnter);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(20)))), ((int)(((byte)(89)))));
            this.panel1.BackgroundImage = global::Login.Properties.Resources.download__2_;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(330, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 86);
            this.panel1.TabIndex = 7;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(54)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(914, 359);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.user);
            this.Controls.Add(this.Support);
            this.Controls.Add(this.submit);
            this.DoubleBuffered = true;
            this.Location = new System.Drawing.Point(458, 0);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Button Support;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Panel panel1;

    }
}

