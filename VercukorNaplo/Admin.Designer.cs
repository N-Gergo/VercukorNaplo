namespace VercukorNaplo
{
    partial class Admin
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
            btnLogout = new Button();
            dgvmeresek = new DataGridView();
            dgvUsers = new DataGridView();
            btnTorles = new Button();
            btnSzerepkor = new Button();
            cmbSzerepkor = new ComboBox();
            txtSzuresNev = new TextBox();
            label1 = new Label();
            btnMeresTorles = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvmeresek).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(12, 22);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(111, 29);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Kijelentkezés";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // dgvmeresek
            // 
            dgvmeresek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvmeresek.Location = new Point(12, 199);
            dgvmeresek.Name = "dgvmeresek";
            dgvmeresek.RowHeadersWidth = 51;
            dgvmeresek.Size = new Size(915, 306);
            dgvmeresek.TabIndex = 1;
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(933, 199);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.Size = new Size(453, 207);
            dgvUsers.TabIndex = 2;
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;
            // 
            // btnTorles
            // 
            btnTorles.Location = new Point(960, 149);
            btnTorles.Name = "btnTorles";
            btnTorles.Size = new Size(152, 29);
            btnTorles.TabIndex = 3;
            btnTorles.Text = "Felhasználó törlése";
            btnTorles.UseVisualStyleBackColor = true;
            btnTorles.Click += btnTorles_Click;
            // 
            // btnSzerepkor
            // 
            btnSzerepkor.Location = new Point(943, 105);
            btnSzerepkor.Name = "btnSzerepkor";
            btnSzerepkor.Size = new Size(182, 29);
            btnSzerepkor.TabIndex = 4;
            btnSzerepkor.Text = "Szerepkör módosítása";
            btnSzerepkor.UseVisualStyleBackColor = true;
            btnSzerepkor.Click += btnSzerepkor_Click;
            // 
            // cmbSzerepkor
            // 
            cmbSzerepkor.FormattingEnabled = true;
            cmbSzerepkor.Location = new Point(1131, 105);
            cmbSzerepkor.Name = "cmbSzerepkor";
            cmbSzerepkor.Size = new Size(151, 28);
            cmbSzerepkor.TabIndex = 5;
            // 
            // txtSzuresNev
            // 
            txtSzuresNev.Location = new Point(168, 166);
            txtSzuresNev.Name = "txtSzuresNev";
            txtSzuresNev.Size = new Size(125, 27);
            txtSzuresNev.TabIndex = 6;
            txtSzuresNev.TextChanged += txtSzuresNev_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 169);
            label1.Name = "label1";
            label1.Size = new Size(128, 20);
            label1.TabIndex = 7;
            label1.Text = "Szűrés név szerint:";
            // 
            // btnMeresTorles
            // 
            btnMeresTorles.Location = new Point(419, 160);
            btnMeresTorles.Name = "btnMeresTorles";
            btnMeresTorles.Size = new Size(152, 29);
            btnMeresTorles.TabIndex = 8;
            btnMeresTorles.Text = "Mérés törlése";
            btnMeresTorles.UseVisualStyleBackColor = true;
            btnMeresTorles.Click += btnMeresTorles_Click;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1398, 517);
            Controls.Add(btnMeresTorles);
            Controls.Add(label1);
            Controls.Add(txtSzuresNev);
            Controls.Add(cmbSzerepkor);
            Controls.Add(btnSzerepkor);
            Controls.Add(btnTorles);
            Controls.Add(dgvUsers);
            Controls.Add(dgvmeresek);
            Controls.Add(btnLogout);
            Name = "Admin";
            Text = "Admin";
            Load += Admin_Load;
            ((System.ComponentModel.ISupportInitialize)dgvmeresek).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogout;
        private DataGridView dgvmeresek;
        private DataGridView dgvUsers;
        private Button btnTorles;
        private Button btnSzerepkor;
        private ComboBox cmbSzerepkor;
        private TextBox txtSzuresNev;
        private Label label1;
        private Button btnMeresTorles;
    }
}