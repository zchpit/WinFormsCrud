namespace WinFormsCrud
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbUser = new Label();
            tbUser = new TextBox();
            lblPassword = new Label();
            gbLogin = new GroupBox();
            lbError = new Label();
            btnLogout = new Button();
            btnLogin = new Button();
            tbPassword = new TextBox();
            gbLogin.SuspendLayout();
            SuspendLayout();
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.Location = new Point(27, 22);
            lbUser.Name = "lbUser";
            lbUser.Size = new Size(30, 15);
            lbUser.TabIndex = 0;
            lbUser.Text = "User";
            // 
            // tbUser
            // 
            tbUser.Location = new Point(63, 19);
            tbUser.Name = "tbUser";
            tbUser.Size = new Size(106, 23);
            tbUser.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(175, 22);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password";
            // 
            // gbLogin
            // 
            gbLogin.Controls.Add(lbError);
            gbLogin.Controls.Add(btnLogin);
            gbLogin.Controls.Add(tbPassword);
            gbLogin.Controls.Add(lblPassword);
            gbLogin.Controls.Add(lbUser);
            gbLogin.Controls.Add(tbUser);
            gbLogin.Location = new Point(12, 12);
            gbLogin.Name = "gbLogin";
            gbLogin.Size = new Size(642, 66);
            gbLogin.TabIndex = 5;
            gbLogin.TabStop = false;
            gbLogin.Text = "groupBox1";
            // 
            // lbError
            // 
            lbError.AutoSize = true;
            lbError.Location = new Point(344, 24);
            lbError.Name = "lbError";
            lbError.Size = new Size(62, 15);
            lbError.TabIndex = 7;
            lbError.Text = "login error";
            lbError.Visible = false;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(660, 27);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(121, 33);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Visible = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(511, 15);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(120, 33);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(238, 19);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(100, 23);
            tbPassword.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 468);
            Controls.Add(gbLogin);
            Controls.Add(btnLogout);
            Name = "Form1";
            Text = "SimpleTestForm";
            gbLogin.ResumeLayout(false);
            gbLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lbUser;
        private TextBox tbUser;
        private Label lblPassword;
        private GroupBox gbLogin;
        private Label lbError;
        private Button btnLogout;
        private Button btnLogin;
        private TextBox tbPassword;
    }
}
