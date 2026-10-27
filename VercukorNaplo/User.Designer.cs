namespace VercukorNaplo
{
    partial class User
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            btnLogout = new Button();
            dgvMeresek = new DataGridView();
            txtErtek = new TextBox();
            txtGyorsInzulin = new TextBox();
            txtBazisInzulin = new TextBox();
            txtSzenhidrat = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            cmbnapszak = new ComboBox();
            dtpdatum = new DateTimePicker();
            dtptime = new DateTimePicker();
            btnMentes = new Button();
            lblDoctorName = new Label();
            chartVercukor = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btnChat = new Button();
            pnlChat = new Panel();
            btnCloseChat = new Button();
            btnSendMessage = new Button();
            txtMessage = new TextBox();
            rtbChat = new RichTextBox();
            lblChatDoctor = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMeresek).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartVercukor).BeginInit();
            pnlChat.SuspendLayout();
            SuspendLayout();
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(12, 12);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(110, 29);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Kijelentkezés";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // dgvMeresek
            // 
            dgvMeresek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMeresek.Location = new Point(12, 189);
            dgvMeresek.Name = "dgvMeresek";
            dgvMeresek.RowHeadersWidth = 51;
            dgvMeresek.Size = new Size(1035, 249);
            dgvMeresek.TabIndex = 1;
            // 
            // txtErtek
            // 
            txtErtek.Location = new Point(224, 154);
            txtErtek.Name = "txtErtek";
            txtErtek.Size = new Size(125, 27);
            txtErtek.TabIndex = 4;
            // 
            // txtGyorsInzulin
            // 
            txtGyorsInzulin.Location = new Point(358, 154);
            txtGyorsInzulin.Name = "txtGyorsInzulin";
            txtGyorsInzulin.Size = new Size(125, 27);
            txtGyorsInzulin.TabIndex = 5;
            // 
            // txtBazisInzulin
            // 
            txtBazisInzulin.Location = new Point(489, 154);
            txtBazisInzulin.Name = "txtBazisInzulin";
            txtBazisInzulin.Size = new Size(125, 27);
            txtBazisInzulin.TabIndex = 6;
            // 
            // txtSzenhidrat
            // 
            txtSzenhidrat.Location = new Point(620, 154);
            txtSzenhidrat.Name = "txtSzenhidrat";
            txtSzenhidrat.Size = new Size(125, 27);
            txtSzenhidrat.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 131);
            label1.Name = "label1";
            label1.Size = new Size(57, 20);
            label1.TabIndex = 9;
            label1.Text = "Dátum:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(162, 133);
            label2.Name = "label2";
            label2.Size = new Size(34, 20);
            label2.TabIndex = 10;
            label2.Text = "Idő:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(262, 131);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.TabIndex = 11;
            label3.Text = "Érték:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(368, 131);
            label4.Name = "label4";
            label4.Size = new Size(98, 20);
            label4.TabIndex = 12;
            label4.Text = "Gyors_Inzulin:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(502, 131);
            label5.Name = "label5";
            label5.Size = new Size(95, 20);
            label5.TabIndex = 13;
            label5.Text = "Bázis_Inzulin:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(642, 131);
            label6.Name = "label6";
            label6.Size = new Size(82, 20);
            label6.TabIndex = 14;
            label6.Text = "Szénhidrát:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(777, 131);
            label7.Name = "label7";
            label7.Size = new Size(68, 20);
            label7.TabIndex = 15;
            label7.Text = "Napszak:";
            // 
            // cmbnapszak
            // 
            cmbnapszak.FormattingEnabled = true;
            cmbnapszak.Location = new Point(751, 153);
            cmbnapszak.Name = "cmbnapszak";
            cmbnapszak.Size = new Size(151, 28);
            cmbnapszak.TabIndex = 16;
            // 
            // dtpdatum
            // 
            dtpdatum.Format = DateTimePickerFormat.Short;
            dtpdatum.Location = new Point(12, 154);
            dtpdatum.Name = "dtpdatum";
            dtpdatum.Size = new Size(115, 27);
            dtpdatum.TabIndex = 17;
            // 
            // dtptime
            // 
            dtptime.Format = DateTimePickerFormat.Time;
            dtptime.Location = new Point(133, 154);
            dtptime.Name = "dtptime";
            dtptime.ShowUpDown = true;
            dtptime.Size = new Size(84, 27);
            dtptime.TabIndex = 18;
            // 
            // btnMentes
            // 
            btnMentes.Location = new Point(908, 152);
            btnMentes.Name = "btnMentes";
            btnMentes.Size = new Size(94, 29);
            btnMentes.TabIndex = 19;
            btnMentes.Text = "Felvitel";
            btnMentes.UseVisualStyleBackColor = true;
            btnMentes.Click += btnMentes_Click;
            // 
            // lblDoctorName
            // 
            lblDoctorName.AutoSize = true;
            lblDoctorName.Location = new Point(22, 60);
            lblDoctorName.Name = "lblDoctorName";
            lblDoctorName.Size = new Size(0, 20);
            lblDoctorName.TabIndex = 21;
            // 
            // chartVercukor
            // 
            chartArea1.Name = "ChartArea1";
            chartVercukor.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartVercukor.Legends.Add(legend1);
            chartVercukor.Location = new Point(419, 454);
            chartVercukor.Name = "chartVercukor";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartVercukor.Series.Add(series1);
            chartVercukor.Size = new Size(790, 375);
            chartVercukor.TabIndex = 23;
            chartVercukor.Text = "chart1";
            // 
            // btnChat
            // 
            btnChat.Location = new Point(341, 42);
            btnChat.Name = "btnChat";
            btnChat.Size = new Size(94, 29);
            btnChat.TabIndex = 24;
            btnChat.Text = "Üzenetek";
            btnChat.TextAlign = ContentAlignment.MiddleLeft;
            btnChat.UseVisualStyleBackColor = true;
            btnChat.Click += btnChat_Click;
            // 
            // pnlChat
            // 
            pnlChat.Controls.Add(btnCloseChat);
            pnlChat.Controls.Add(btnSendMessage);
            pnlChat.Controls.Add(txtMessage);
            pnlChat.Controls.Add(rtbChat);
            pnlChat.Controls.Add(lblChatDoctor);
            pnlChat.Location = new Point(1214, 12);
            pnlChat.Name = "pnlChat";
            pnlChat.Size = new Size(250, 426);
            pnlChat.TabIndex = 25;
            pnlChat.Visible = false;
            // 
            // btnCloseChat
            // 
            btnCloseChat.Location = new Point(223, 3);
            btnCloseChat.Name = "btnCloseChat";
            btnCloseChat.Size = new Size(24, 29);
            btnCloseChat.TabIndex = 26;
            btnCloseChat.Text = "X";
            btnCloseChat.UseVisualStyleBackColor = true;
            btnCloseChat.Click += btnCloseChat_Click;
            // 
            // btnSendMessage
            // 
            btnSendMessage.Location = new Point(3, 394);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(94, 29);
            btnSendMessage.TabIndex = 3;
            btnSendMessage.Text = "Küldés";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(3, 355);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(244, 34);
            txtMessage.TabIndex = 2;
            // 
            // rtbChat
            // 
            rtbChat.Location = new Point(3, 30);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(244, 323);
            rtbChat.TabIndex = 1;
            rtbChat.Text = "";
            // 
            // lblChatDoctor
            // 
            lblChatDoctor.AutoSize = true;
            lblChatDoctor.Location = new Point(99, 9);
            lblChatDoctor.Name = "lblChatDoctor";
            lblChatDoctor.Size = new Size(0, 20);
            lblChatDoctor.TabIndex = 0;
            lblChatDoctor.TextAlign = ContentAlignment.TopCenter;
            // 
            // User
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(pnlChat);
            Controls.Add(btnChat);
            Controls.Add(chartVercukor);
            Controls.Add(lblDoctorName);
            Controls.Add(btnMentes);
            Controls.Add(dtptime);
            Controls.Add(dtpdatum);
            Controls.Add(cmbnapszak);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtSzenhidrat);
            Controls.Add(txtBazisInzulin);
            Controls.Add(txtGyorsInzulin);
            Controls.Add(txtErtek);
            Controls.Add(dgvMeresek);
            Controls.Add(btnLogout);
            Name = "User";
            Text = "User";
            Load += User_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMeresek).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartVercukor).EndInit();
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogout;
        private DataGridView dgvMeresek;
        private TextBox txtMessage;
        private TextBox textBox2;
        private TextBox txtErtek;
        private TextBox txtGyorsInzulin;
        private TextBox txtBazisInzulin;
        private TextBox txtSzenhidrat;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox cmbnapszak;
        private DateTimePicker dtpdatum;
        private DateTimePicker dtptime;
        private Button btnMentes;
        private Label lblDoctorName;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVercukor;
        private Button btnChat;
        private Panel pnlChat;
        private RichTextBox rtbChat;
        private Label lblChatDoctor;
        private Button btnCloseChat;
        private Button btnSendMessage;
    }
}