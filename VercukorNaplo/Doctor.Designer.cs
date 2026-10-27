namespace VercukorNaplo
{
    partial class Doctor
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
            button1 = new Button();
            dgvmeresek = new DataGridView();
            pnlChat = new Panel();
            btnCloseChat = new Button();
            btnSendMessage = new Button();
            txtMessage = new TextBox();
            rtbChat = new RichTextBox();
            cmbChatPatient = new ComboBox();
            btnChat = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvmeresek).BeginInit();
            pnlChat.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(109, 29);
            button1.TabIndex = 0;
            button1.Text = "Kijelentkezés";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dgvmeresek
            // 
            dgvmeresek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvmeresek.Location = new Point(12, 164);
            dgvmeresek.Name = "dgvmeresek";
            dgvmeresek.RowHeadersWidth = 51;
            dgvmeresek.Size = new Size(894, 225);
            dgvmeresek.TabIndex = 1;
            // 
            // pnlChat
            // 
            pnlChat.Controls.Add(btnCloseChat);
            pnlChat.Controls.Add(btnSendMessage);
            pnlChat.Controls.Add(txtMessage);
            pnlChat.Controls.Add(rtbChat);
            pnlChat.Controls.Add(cmbChatPatient);
            pnlChat.Location = new Point(1035, 12);
            pnlChat.Name = "pnlChat";
            pnlChat.Size = new Size(275, 448);
            pnlChat.TabIndex = 3;
            pnlChat.Visible = false;
            // 
            // btnCloseChat
            // 
            btnCloseChat.Location = new Point(241, 3);
            btnCloseChat.Name = "btnCloseChat";
            btnCloseChat.Size = new Size(31, 29);
            btnCloseChat.TabIndex = 4;
            btnCloseChat.Text = "X";
            btnCloseChat.UseVisualStyleBackColor = true;
            btnCloseChat.Click += btnCloseChat_Click;
            // 
            // btnSendMessage
            // 
            btnSendMessage.Location = new Point(3, 416);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(94, 29);
            btnSendMessage.TabIndex = 3;
            btnSendMessage.Text = "Küldés";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(3, 376);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(269, 34);
            txtMessage.TabIndex = 2;
            // 
            // rtbChat
            // 
            rtbChat.Location = new Point(3, 36);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(269, 341);
            rtbChat.TabIndex = 1;
            rtbChat.Text = "";
            // 
            // cmbChatPatient
            // 
            cmbChatPatient.FormattingEnabled = true;
            cmbChatPatient.Location = new Point(70, 3);
            cmbChatPatient.Name = "cmbChatPatient";
            cmbChatPatient.Size = new Size(151, 28);
            cmbChatPatient.TabIndex = 0;
            cmbChatPatient.SelectionChangeCommitted += cmbChatPatient_SelectionChangeCommitted;
            // 
            // btnChat
            // 
            btnChat.Location = new Point(12, 67);
            btnChat.Name = "btnChat";
            btnChat.Size = new Size(94, 29);
            btnChat.TabIndex = 4;
            btnChat.Text = "Üzenetek";
            btnChat.UseVisualStyleBackColor = true;
            btnChat.Click += btnChat_Click;
            // 
            // Doctor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1310, 566);
            Controls.Add(btnChat);
            Controls.Add(pnlChat);
            Controls.Add(dgvmeresek);
            Controls.Add(button1);
            Name = "Doctor";
            Text = "Doctor";
            Load += Doctor_Load;
            ((System.ComponentModel.ISupportInitialize)dgvmeresek).EndInit();
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private DataGridView dgvmeresek;
        private Panel pnlChat;
        private TextBox txtMessage;
        private RichTextBox rtbChat;
        private ComboBox cmbChatPatient;
        private Button btnCloseChat;
        private Button btnSendMessage;
        private Button btnChat;
    }
}