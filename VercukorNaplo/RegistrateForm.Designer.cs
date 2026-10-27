namespace VercukorNaplo
{
    partial class RegistrateForm
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
            btnRegister = new Button();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            txtPasswordAgain = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(376, 267);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(121, 29);
            btnRegister.TabIndex = 0;
            btnRegister.Text = "Regisztráció";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += button1_Click;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(372, 81);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(125, 27);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(372, 140);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(125, 27);
            txtPassword.TabIndex = 2;
            // 
            // txtPasswordAgain
            // 
            txtPasswordAgain.Location = new Point(372, 197);
            txtPasswordAgain.Name = "txtPasswordAgain";
            txtPasswordAgain.Size = new Size(125, 27);
            txtPasswordAgain.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(254, 84);
            label1.Name = "label1";
            label1.Size = new Size(112, 20);
            label1.TabIndex = 4;
            label1.Text = "Felhasználónév:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(282, 147);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 5;
            label2.Text = "Jelszó:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(282, 200);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 6;
            label3.Text = "Jelszó újra:";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(376, 312);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(121, 29);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Bejelentkezés";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // RegistrateForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogin);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPasswordAgain);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(btnRegister);
            Name = "RegistrateForm";
            Text = "RegistrateForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRegister;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtPasswordAgain;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnLogin;
    }
}