namespace VercukorNaplo
{
    partial class Assistant
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
            cmbDoctorAssign = new ComboBox();
            cmbPatientAssign = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnHozzarendeles = new Button();
            btnKapcsolatTorlese = new Button();
            label4 = new Label();
            label5 = new Label();
            cmbPatientView = new ComboBox();
            cmbDoctorView = new ComboBox();
            dgvMeresek = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvMeresek).BeginInit();
            SuspendLayout();
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(0, 0);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(106, 29);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Kijelentkezés";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click_1;
            // 
            // cmbDoctorAssign
            // 
            cmbDoctorAssign.FormattingEnabled = true;
            cmbDoctorAssign.Location = new Point(88, 423);
            cmbDoctorAssign.Name = "cmbDoctorAssign";
            cmbDoctorAssign.Size = new Size(151, 28);
            cmbDoctorAssign.TabIndex = 1;
            // 
            // cmbPatientAssign
            // 
            cmbPatientAssign.FormattingEnabled = true;
            cmbPatientAssign.Location = new Point(88, 470);
            cmbPatientAssign.Name = "cmbPatientAssign";
            cmbPatientAssign.Size = new Size(151, 28);
            cmbPatientAssign.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 423);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 3;
            label1.Text = "Orvos:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 470);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 4;
            label2.Text = "Páciens:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 391);
            label3.Name = "label3";
            label3.Size = new Size(167, 20);
            label3.TabIndex = 5;
            label3.Text = " Orvos-páciens kezelés  ";
            // 
            // btnHozzarendeles
            // 
            btnHozzarendeles.Location = new Point(19, 519);
            btnHozzarendeles.Name = "btnHozzarendeles";
            btnHozzarendeles.Size = new Size(125, 29);
            btnHozzarendeles.TabIndex = 6;
            btnHozzarendeles.Text = "Hozzárendelés";
            btnHozzarendeles.UseVisualStyleBackColor = true;
            btnHozzarendeles.Click += btnHozzarendeles_Click;
            // 
            // btnKapcsolatTorlese
            // 
            btnKapcsolatTorlese.Location = new Point(150, 519);
            btnKapcsolatTorlese.Name = "btnKapcsolatTorlese";
            btnKapcsolatTorlese.Size = new Size(175, 29);
            btnKapcsolatTorlese.TabIndex = 7;
            btnKapcsolatTorlese.Text = "Kapcsolat törlése";
            btnKapcsolatTorlese.UseVisualStyleBackColor = true;
            btnKapcsolatTorlese.Click += btnKapcsolatTorles_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 113);
            label4.Name = "label4";
            label4.Size = new Size(60, 20);
            label4.TabIndex = 11;
            label4.Text = "Páciens:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 66);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 10;
            label5.Text = "Orvos:";
            // 
            // cmbPatientView
            // 
            cmbPatientView.FormattingEnabled = true;
            cmbPatientView.Location = new Point(88, 113);
            cmbPatientView.Name = "cmbPatientView";
            cmbPatientView.Size = new Size(151, 28);
            cmbPatientView.TabIndex = 9;
            cmbPatientView.SelectionChangeCommitted += cmbPatientView_SelectionChangeCommitted;
            // 
            // cmbDoctorView
            // 
            cmbDoctorView.FormattingEnabled = true;
            cmbDoctorView.Location = new Point(88, 66);
            cmbDoctorView.Name = "cmbDoctorView";
            cmbDoctorView.Size = new Size(151, 28);
            cmbDoctorView.TabIndex = 8;
            cmbDoctorView.SelectionChangeCommitted += cmbDoctorView_SelectionChangeCommitted;
            // 
            // dgvMeresek
            // 
            dgvMeresek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMeresek.Location = new Point(12, 174);
            dgvMeresek.Name = "dgvMeresek";
            dgvMeresek.RowHeadersWidth = 51;
            dgvMeresek.Size = new Size(968, 188);
            dgvMeresek.TabIndex = 12;
            // 
            // Assistant
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1115, 590);
            Controls.Add(dgvMeresek);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(cmbPatientView);
            Controls.Add(cmbDoctorView);
            Controls.Add(btnKapcsolatTorlese);
            Controls.Add(btnHozzarendeles);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbPatientAssign);
            Controls.Add(cmbDoctorAssign);
            Controls.Add(btnLogout);
            Name = "Assistant";
            Text = "Assistant";
            Load += Assistant_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMeresek).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogout;
        private ComboBox cmbDoctorAssign;
        private ComboBox cmbPatientAssign;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnHozzarendeles;
        private Button btnKapcsolatTorlese;
        private Label label4;
        private Label label5;
        private ComboBox cmbPatientView;
        private ComboBox cmbDoctorView;
        private DataGridView dgvMeresek;
    }
}