namespace VercukorNaplo
{
    partial class Doctor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            button1 = new Button();
            lblPatient = new Label();
            cmbPatient = new ComboBox();
            dgvmeresek = new DataGridView();
            pnlChat = new Panel();
            btnCloseChat = new Button();
            btnSendMessage = new Button();
            txtMessage = new TextBox();
            rtbChat = new RichTextBox();
            cmbChatPatient = new ComboBox();
            btnChat = new Button();
            btnMentes = new Button();
            dtptime = new DateTimePicker();
            dtpdatum = new DateTimePicker();
            cmbnapszak = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtSzenhidrat = new TextBox();
            txtBazisInzulin = new TextBox();
            txtGyorsInzulin = new TextBox();
            txtErtek = new TextBox();
            label8 = new Label();
            label9 = new Label();
            cmbEv = new ComboBox();
            cmbHonap = new ComboBox();
            pnlDiagram = new Panel();
            chartVercukor = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)dgvmeresek).BeginInit();
            pnlChat.SuspendLayout();
            pnlDiagram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartVercukor).BeginInit();
            SuspendLayout();

            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(110, 29);
            button1.TabIndex = 0;
            button1.Text = "Kijelentkezés";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;

            lblPatient.AutoSize = true;
            lblPatient.Location = new Point(150, 18);
            lblPatient.Name = "lblPatient";
            lblPatient.Size = new Size(63, 20);
            lblPatient.TabIndex = 1;
            lblPatient.Text = "Páciens:";

            cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatient.FormattingEnabled = true;
            cmbPatient.Location = new Point(220, 14);
            cmbPatient.Name = "cmbPatient";
            cmbPatient.Size = new Size(180, 28);
            cmbPatient.TabIndex = 2;
            cmbPatient.SelectionChangeCommitted += cmbPatient_SelectionChangeCommitted;

            btnChat.Location = new Point(430, 12);
            btnChat.Name = "btnChat";
            btnChat.Size = new Size(145, 34);
            btnChat.TabIndex = 3;
            btnChat.Text = "Üzenetek";
            btnChat.TextAlign = ContentAlignment.MiddleLeft;
            btnChat.UseVisualStyleBackColor = true;
            btnChat.Click += btnChat_Click;

            label1.AutoSize = true;
            label1.Location = new Point(37, 131);
            label1.Name = "label1";
            label1.Size = new Size(57, 20);
            label1.Text = "Dátum:";

            label2.AutoSize = true;
            label2.Location = new Point(162, 133);
            label2.Name = "label2";
            label2.Size = new Size(34, 20);
            label2.Text = "Idő:";

            label3.AutoSize = true;
            label3.Location = new Point(262, 131);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.Text = "Érték:";

            label4.AutoSize = true;
            label4.Location = new Point(368, 131);
            label4.Name = "label4";
            label4.Size = new Size(98, 20);
            label4.Text = "Gyors_Inzulin:";

            label5.AutoSize = true;
            label5.Location = new Point(502, 131);
            label5.Name = "label5";
            label5.Size = new Size(95, 20);
            label5.Text = "Bázis_Inzulin:";

            label6.AutoSize = true;
            label6.Location = new Point(642, 131);
            label6.Name = "label6";
            label6.Size = new Size(82, 20);
            label6.Text = "Szénhidrát:";

            label7.AutoSize = true;
            label7.Location = new Point(777, 131);
            label7.Name = "label7";
            label7.Size = new Size(68, 20);
            label7.Text = "Napszak:";

            dtpdatum.Format = DateTimePickerFormat.Short;
            dtpdatum.Location = new Point(12, 154);
            dtpdatum.Name = "dtpdatum";
            dtpdatum.Size = new Size(115, 27);
            dtpdatum.TabIndex = 10;

            dtptime.Format = DateTimePickerFormat.Time;
            dtptime.Location = new Point(133, 154);
            dtptime.Name = "dtptime";
            dtptime.ShowUpDown = true;
            dtptime.Size = new Size(84, 27);
            dtptime.TabIndex = 11;

            txtErtek.Location = new Point(224, 154);
            txtErtek.Name = "txtErtek";
            txtErtek.Size = new Size(125, 27);
            txtErtek.TabIndex = 12;

            txtGyorsInzulin.Location = new Point(358, 154);
            txtGyorsInzulin.Name = "txtGyorsInzulin";
            txtGyorsInzulin.Size = new Size(125, 27);
            txtGyorsInzulin.TabIndex = 13;

            txtBazisInzulin.Location = new Point(489, 154);
            txtBazisInzulin.Name = "txtBazisInzulin";
            txtBazisInzulin.Size = new Size(125, 27);
            txtBazisInzulin.TabIndex = 14;

            txtSzenhidrat.Location = new Point(620, 154);
            txtSzenhidrat.Name = "txtSzenhidrat";
            txtSzenhidrat.Size = new Size(125, 27);
            txtSzenhidrat.TabIndex = 15;

            cmbnapszak.FormattingEnabled = true;
            cmbnapszak.Location = new Point(751, 153);
            cmbnapszak.Name = "cmbnapszak";
            cmbnapszak.Size = new Size(151, 28);
            cmbnapszak.TabIndex = 16;

            btnMentes.Location = new Point(908, 152);
            btnMentes.Name = "btnMentes";
            btnMentes.Size = new Size(94, 29);
            btnMentes.TabIndex = 17;
            btnMentes.Text = "Felvitel";
            btnMentes.UseVisualStyleBackColor = true;
            btnMentes.Click += btnMentes_Click;

            dgvmeresek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvmeresek.Location = new Point(12, 189);
            dgvmeresek.Name = "dgvmeresek";
            dgvmeresek.RowHeadersWidth = 51;
            dgvmeresek.Size = new Size(1035, 249);
            dgvmeresek.TabIndex = 18;

            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(437, 469);
            label8.Name = "label8";
            label8.Size = new Size(36, 28);
            label8.Text = "Év:";

            cmbEv.FormattingEnabled = true;
            cmbEv.Location = new Point(479, 473);
            cmbEv.Name = "cmbEv";
            cmbEv.Size = new Size(151, 28);
            cmbEv.TabIndex = 19;

            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.Location = new Point(654, 469);
            label9.Name = "label9";
            label9.Size = new Size(75, 28);
            label9.Text = "Hónap:";

            cmbHonap.FormattingEnabled = true;
            cmbHonap.Location = new Point(735, 473);
            cmbHonap.Name = "cmbHonap";
            cmbHonap.Size = new Size(151, 28);
            cmbHonap.TabIndex = 20;

            pnlDiagram.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlDiagram.Controls.Add(chartVercukor);
            pnlDiagram.Location = new Point(185, 507);
            pnlDiagram.Name = "pnlDiagram";
            pnlDiagram.Size = new Size(1104, 424);
            pnlDiagram.TabIndex = 21;

            chartArea1.Name = "ChartArea1";
            chartVercukor.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartVercukor.Legends.Add(legend1);
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartVercukor.Series.Add(series1);
            chartVercukor.Dock = DockStyle.Fill;
            chartVercukor.Location = new Point(0, 0);
            chartVercukor.Name = "chartVercukor";
            chartVercukor.Size = new Size(1104, 424);
            chartVercukor.TabIndex = 0;

            pnlChat.Controls.Add(btnCloseChat);
            pnlChat.Controls.Add(btnSendMessage);
            pnlChat.Controls.Add(txtMessage);
            pnlChat.Controls.Add(rtbChat);
            pnlChat.Controls.Add(cmbChatPatient);
            pnlChat.Location = new Point(1214, 12);
            pnlChat.Name = "pnlChat";
            pnlChat.Size = new Size(320, 426);
            pnlChat.TabIndex = 22;
            pnlChat.Visible = false;

            cmbChatPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChatPatient.FormattingEnabled = true;
            cmbChatPatient.Location = new Point(70, 3);
            cmbChatPatient.Name = "cmbChatPatient";
            cmbChatPatient.Size = new Size(190, 28);
            cmbChatPatient.TabIndex = 0;
            cmbChatPatient.SelectionChangeCommitted += cmbChatPatient_SelectionChangeCommitted;

            btnCloseChat.Location = new Point(293, 3);
            btnCloseChat.Name = "btnCloseChat";
            btnCloseChat.Size = new Size(24, 29);
            btnCloseChat.TabIndex = 1;
            btnCloseChat.Text = "X";
            btnCloseChat.UseVisualStyleBackColor = true;
            btnCloseChat.Click += btnCloseChat_Click;

            rtbChat.Location = new Point(3, 36);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(314, 317);
            rtbChat.TabIndex = 2;
            rtbChat.Text = "";

            txtMessage.Location = new Point(3, 355);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(314, 34);
            txtMessage.TabIndex = 3;

            btnSendMessage.Location = new Point(3, 394);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(94, 29);
            btnSendMessage.TabIndex = 4;
            btnSendMessage.Text = "Küldés";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(pnlChat);
            Controls.Add(pnlDiagram);
            Controls.Add(cmbHonap);
            Controls.Add(label9);
            Controls.Add(cmbEv);
            Controls.Add(label8);
            Controls.Add(dgvmeresek);
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
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnChat);
            Controls.Add(cmbPatient);
            Controls.Add(lblPatient);
            Controls.Add(button1);
            Name = "Doctor";
            Text = "Orvosi felület";
            Load += Doctor_Load;
            ((System.ComponentModel.ISupportInitialize)dgvmeresek).EndInit();
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            pnlDiagram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartVercukor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button button1;
        private Label lblPatient;
        private ComboBox cmbPatient;
        private DataGridView dgvmeresek;
        private Panel pnlChat;
        private TextBox txtMessage;
        private RichTextBox rtbChat;
        private ComboBox cmbChatPatient;
        private Button btnCloseChat;
        private Button btnSendMessage;
        private Button btnChat;
        private Button btnMentes;
        private DateTimePicker dtptime;
        private DateTimePicker dtpdatum;
        private ComboBox cmbnapszak;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtSzenhidrat;
        private TextBox txtBazisInzulin;
        private TextBox txtGyorsInzulin;
        private TextBox txtErtek;
        private Label label8;
        private Label label9;
        private ComboBox cmbEv;
        private ComboBox cmbHonap;
        private Panel pnlDiagram;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVercukor;
    }
}