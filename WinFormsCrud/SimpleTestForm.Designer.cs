﻿namespace WinFormsCrud
{
    partial class SimpleTestForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleTestForm));
            lbUser = new Label();
            tbUser = new TextBox();
            lblPassword = new Label();
            gbLogin = new GroupBox();
            lbError = new Label();
            btnLogin = new Button();
            tbPassword = new TextBox();
            btnLogout = new Button();
            dgvCases = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            headerDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priorityDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            caseDtoBindingSource1 = new BindingSource(components);
            caseDtoBindingSource = new BindingSource(components);
            gbEditRow = new GroupBox();
            btnEdit = new Button();
            btnAdd = new Button();
            numericUpDown1 = new NumericUpDown();
            tbDescription = new TextBox();
            tbHeader = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            gbLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCases).BeginInit();
            ((System.ComponentModel.ISupportInitialize)caseDtoBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)caseDtoBindingSource).BeginInit();
            gbEditRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
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
            // 
            // lbError
            // 
            lbError.AutoSize = true;
            lbError.ForeColor = Color.Red;
            lbError.Location = new Point(344, 24);
            lbError.Name = "lbError";
            lbError.Size = new Size(163, 15);
            lbError.TabIndex = 7;
            lbError.Text = "wrong username or password";
            lbError.Visible = false;
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
            // dgvCases
            // 
            dgvCases.AllowUserToAddRows = false;
            dgvCases.AllowUserToDeleteRows = false;
            dgvCases.AutoGenerateColumns = false;
            dgvCases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCases.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, headerDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, priorityDataGridViewTextBoxColumn });
            dgvCases.DataSource = caseDtoBindingSource1;
            dgvCases.Location = new Point(260, 147);
            dgvCases.Name = "dgvCases";
            dgvCases.ReadOnly = true;
            dgvCases.Size = new Size(619, 284);
            dgvCases.TabIndex = 7;
            dgvCases.Visible = false;
            dgvCases.RowEnter += dgvCases_RowEnter;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // headerDataGridViewTextBoxColumn
            // 
            headerDataGridViewTextBoxColumn.DataPropertyName = "Header";
            headerDataGridViewTextBoxColumn.HeaderText = "Header";
            headerDataGridViewTextBoxColumn.Name = "headerDataGridViewTextBoxColumn";
            headerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priorityDataGridViewTextBoxColumn
            // 
            priorityDataGridViewTextBoxColumn.DataPropertyName = "Priority";
            priorityDataGridViewTextBoxColumn.HeaderText = "Priority";
            priorityDataGridViewTextBoxColumn.Name = "priorityDataGridViewTextBoxColumn";
            priorityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // caseDtoBindingSource1
            // 
            caseDtoBindingSource1.DataSource = typeof(Dto.CaseDto);
            // 
            // caseDtoBindingSource
            // 
            caseDtoBindingSource.DataSource = typeof(Dto.CaseDto);
            // 
            // gbEditRow
            // 
            gbEditRow.Controls.Add(btnEdit);
            gbEditRow.Controls.Add(btnAdd);
            gbEditRow.Controls.Add(numericUpDown1);
            gbEditRow.Controls.Add(tbDescription);
            gbEditRow.Controls.Add(tbHeader);
            gbEditRow.Controls.Add(label3);
            gbEditRow.Controls.Add(label2);
            gbEditRow.Controls.Add(label1);
            gbEditRow.Location = new Point(12, 147);
            gbEditRow.Name = "gbEditRow";
            gbEditRow.Size = new Size(242, 284);
            gbEditRow.TabIndex = 8;
            gbEditRow.TabStop = false;
            gbEditRow.Visible = false;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(157, 255);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 23);
            btnEdit.TabIndex = 8;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(72, 255);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(72, 219);
            numericUpDown1.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.ReadOnly = true;
            numericUpDown1.Size = new Size(160, 23);
            numericUpDown1.TabIndex = 6;
            numericUpDown1.TextAlign = HorizontalAlignment.Right;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // tbDescription
            // 
            tbDescription.Location = new Point(72, 57);
            tbDescription.Multiline = true;
            tbDescription.Name = "tbDescription";
            tbDescription.Size = new Size(160, 156);
            tbDescription.TabIndex = 4;
            // 
            // tbHeader
            // 
            tbHeader.Location = new Point(72, 28);
            tbHeader.Name = "tbHeader";
            tbHeader.Size = new Size(160, 23);
            tbHeader.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(0, 221);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 2;
            label3.Text = "Priority";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 127);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 1;
            label2.Text = "Description";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 31);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 0;
            label1.Text = "Header";
            // 
            // button1
            // 
            button1.Location = new Point(787, 27);
            button1.Name = "button1";
            button1.Size = new Size(92, 33);
            button1.TabIndex = 9;
            button1.Text = "Close App";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnClose_Click;
            // 
            // SimpleTestForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(892, 443);
            Controls.Add(button1);
            Controls.Add(gbEditRow);
            Controls.Add(dgvCases);
            Controls.Add(gbLogin);
            Controls.Add(btnLogout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SimpleTestForm";
            Text = "SimpleTestForm";
            gbLogin.ResumeLayout(false);
            gbLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCases).EndInit();
            ((System.ComponentModel.ISupportInitialize)caseDtoBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)caseDtoBindingSource).EndInit();
            gbEditRow.ResumeLayout(false);
            gbEditRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
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
        private DataGridView dgvCases;
        private BindingSource caseDtoBindingSource1;
        private BindingSource caseDtoBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn headerDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priorityDataGridViewTextBoxColumn;
        private GroupBox gbEditRow;
        private NumericUpDown numericUpDown1;
        private TextBox tbDescription;
        private TextBox tbHeader;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnEdit;
        private Button btnAdd;
        private Button button1;
    }
}
