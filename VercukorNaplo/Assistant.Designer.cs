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
            btnMentes = new Button();
            cmbnapszak = new ComboBox();
            txtSzenhidrat = new TextBox();
            txtBazisInzulin = new TextBox();
            txtGyorsInzulin = new TextBox();
            txtErtek = new TextBox();
            dtptime = new DateTimePicker();
            dtpdatum = new DateTimePicker();
            label7 = new Label();
            label6 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMeresek).BeginInit();
            SuspendLayout();
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(0, 0);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(120, 29);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Kijelentkezés";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click_1;
            // 
            // cmbDoctorAssign
            // 
            cmbDoctorAssign.FormattingEnabled = true;
            cmbDoctorAssign.Location = new Point(88, 457);
            cmbDoctorAssign.Name = "cmbDoctorAssign";
            cmbDoctorAssign.Size = new Size(151, 28);
            cmbDoctorAssign.TabIndex = 1;
            // 
            // cmbPatientAssign
            // 
            cmbPatientAssign.FormattingEnabled = true;
            cmbPatientAssign.Location = new Point(88, 504);
            cmbPatientAssign.Name = "cmbPatientAssign";
            cmbPatientAssign.Size = new Size(151, 28);
            cmbPatientAssign.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 457);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 3;
            label1.Text = "Orvos:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 504);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 4;
            label2.Text = "Páciens:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 425);
            label3.Name = "label3";
            label3.Size = new Size(167, 20);
            label3.TabIndex = 5;
            label3.Text = " Orvos-páciens kezelés  ";
            // 
            // btnHozzarendeles
            // 
            btnHozzarendeles.Location = new Point(19, 553);
            btnHozzarendeles.Name = "btnHozzarendeles";
            btnHozzarendeles.Size = new Size(125, 29);
            btnHozzarendeles.TabIndex = 6;
            btnHozzarendeles.Text = "Hozzárendelés";
            btnHozzarendeles.UseVisualStyleBackColor = true;
            btnHozzarendeles.Click += btnHozzarendeles_Click;
            // 
            // btnKapcsolatTorlese
            // 
            btnKapcsolatTorlese.Location = new Point(150, 553);
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
            dgvMeresek.Location = new Point(12, 208);
            dgvMeresek.Name = "dgvMeresek";
            dgvMeresek.RowHeadersWidth = 51;
            dgvMeresek.Size = new Size(968, 188);
            dgvMeresek.TabIndex = 12;
            // 
            // btnMentes
            // 
            btnMentes.Location = new Point(912, 173);
            btnMentes.Name = "btnMentes";
            btnMentes.Size = new Size(94, 29);
            btnMentes.TabIndex = 39;
            btnMentes.Text = "Felvitel";
            btnMentes.UseVisualStyleBackColor = true;
            btnMentes.Click += btnMentes_Click;
            // 
            // cmbnapszak
            // 
            cmbnapszak.FormattingEnabled = true;
            cmbnapszak.Location = new Point(755, 174);
            cmbnapszak.Name = "cmbnapszak";
            cmbnapszak.Size = new Size(151, 28);
            cmbnapszak.TabIndex = 38;
            // 
            // txtSzenhidrat
            // 
            txtSzenhidrat.Location = new Point(624, 175);
            txtSzenhidrat.Name = "txtSzenhidrat";
            txtSzenhidrat.Size = new Size(125, 27);
            txtSzenhidrat.TabIndex = 37;
            // 
            // txtBazisInzulin
            // 
            txtBazisInzulin.Location = new Point(493, 175);
            txtBazisInzulin.Name = "txtBazisInzulin";
            txtBazisInzulin.Size = new Size(125, 27);
            txtBazisInzulin.TabIndex = 36;
            // 
            // txtGyorsInzulin
            // 
            txtGyorsInzulin.Location = new Point(362, 175);
            txtGyorsInzulin.Name = "txtGyorsInzulin";
            txtGyorsInzulin.Size = new Size(125, 27);
            txtGyorsInzulin.TabIndex = 35;
            // 
            // txtErtek
            // 
            txtErtek.Location = new Point(228, 175);
            txtErtek.Name = "txtErtek";
            txtErtek.Size = new Size(125, 27);
            txtErtek.TabIndex = 34;
            // 
            // dtptime
            // 
            dtptime.Format = DateTimePickerFormat.Time;
            dtptime.Location = new Point(137, 175);
            dtptime.Name = "dtptime";
            dtptime.ShowUpDown = true;
            dtptime.Size = new Size(84, 27);
            dtptime.TabIndex = 33;
            // 
            // dtpdatum
            // 
            dtpdatum.Format = DateTimePickerFormat.Short;
            dtpdatum.Location = new Point(16, 175);
            dtpdatum.Name = "dtpdatum";
            dtpdatum.Size = new Size(115, 27);
            dtpdatum.TabIndex = 32;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(781, 152);
            label7.Name = "label7";
            label7.Size = new Size(68, 20);
            label7.TabIndex = 40;
            label7.Text = "Napszak:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(646, 152);
            label6.Name = "label6";
            label6.Size = new Size(82, 20);
            label6.TabIndex = 41;
            label6.Text = "Szénhidrát:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(506, 152);
            label8.Name = "label8";
            label8.Size = new Size(95, 20);
            label8.TabIndex = 42;
            label8.Text = "Bázis_Inzulin:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(372, 152);
            label9.Name = "label9";
            label9.Size = new Size(98, 20);
            label9.TabIndex = 43;
            label9.Text = "Gyors_Inzulin:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(266, 152);
            label10.Name = "label10";
            label10.Size = new Size(45, 20);
            label10.TabIndex = 44;
            label10.Text = "Érték:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(166, 154);
            label11.Name = "label11";
            label11.Size = new Size(34, 20);
            label11.TabIndex = 45;
            label11.Text = "Idő:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(41, 152);
            label12.Name = "label12";
            label12.Size = new Size(57, 20);
            label12.TabIndex = 46;
            label12.Text = "Dátum:";
            // 
            // Assistant
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 590);
            Controls.Add(btnMentes);
            Controls.Add(cmbnapszak);
            Controls.Add(txtSzenhidrat);
            Controls.Add(txtBazisInzulin);
            Controls.Add(txtGyorsInzulin);
            Controls.Add(txtErtek);
            Controls.Add(dtptime);
            Controls.Add(dtpdatum);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(label11);
            Controls.Add(label12);
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
        private Button btnMentes;
        private ComboBox cmbnapszak;
        private TextBox txtSzenhidrat;
        private TextBox txtBazisInzulin;
        private TextBox txtGyorsInzulin;
        private TextBox txtErtek;
        private DateTimePicker dtptime;
        private DateTimePicker dtpdatum;
        private Label label7;
        private Label label6;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
    }
}