using System;
using System.Data.SQLite;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace VercukorNaplo
{
    public partial class RegistrateForm : Form
    {
        private readonly Panel pnlRegister = new Panel();
        private readonly Label lblTitle = new Label();
        private readonly Label lblSubtitle = new Label();

        public RegistrateForm()
        {
            InitializeComponent();
            LetrehozKinezet();
            this.Resize += RegistrateForm_Resize;
            txtPasswordAgain.KeyDown += txtPasswordAgain_KeyDown;
            this.FormClosing += RegistrateForm_FormClosing;
        }

        private void RegistrateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void LetrehozKinezet()
        {
            Color kek = Color.FromArgb(47, 128, 237);
            Color hatter = Color.FromArgb(244, 247, 250);
            Color sotet = Color.FromArgb(45, 55, 72);

            this.BackColor = hatter;
            this.Font = new Font("Segoe UI", 10);
            this.Text = "Vércukornapló - Regisztráció";
            this.MinimumSize = new Size(900, 700);
            this.WindowState = FormWindowState.Maximized;

            pnlRegister.Size = new Size(470, 570);
            pnlRegister.BackColor = Color.White;
            this.Controls.Add(pnlRegister);

            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(420, 45);
            lblTitle.Text = "Vércukornapló";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.ForeColor = sotet;
            pnlRegister.Controls.Add(lblTitle);

            lblSubtitle.AutoSize = false;
            lblSubtitle.Size = new Size(420, 30);
            lblSubtitle.Text = "Regisztráció";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            lblSubtitle.Font = new Font("Segoe UI", 12);
            lblSubtitle.ForeColor = Color.FromArgb(100, 110, 120);
            pnlRegister.Controls.Add(lblSubtitle);

            foreach (Control control in this.Controls)
            {
                if (control is Label label && label != lblTitle && label != lblSubtitle)
                {
                    label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    label.ForeColor = sotet;
                }
            }

            Label lblUsername = FindLabel("Felhasználónév:");
            Label lblEmail = FindLabel("E-mail:");
            Label lblPassword = FindLabel("Jelszó:");
            Label lblPasswordAgain = FindLabel("Jelszó újra:");

            if (lblUsername != null) pnlRegister.Controls.Add(lblUsername);
            if (lblEmail != null) pnlRegister.Controls.Add(lblEmail);
            if (lblPassword != null) pnlRegister.Controls.Add(lblPassword);
            if (lblPasswordAgain != null) pnlRegister.Controls.Add(lblPasswordAgain);

            pnlRegister.Controls.Add(txtUsername);
            pnlRegister.Controls.Add(txtEmail);
            pnlRegister.Controls.Add(txtPassword);
            pnlRegister.Controls.Add(txtPasswordAgain);
            pnlRegister.Controls.Add(btnRegister);
            pnlRegister.Controls.Add(btnLogin);

            TextBox[] mezok =
            {
                txtUsername,
                txtEmail,
                txtPassword,
                txtPasswordAgain
            };

            foreach (TextBox mezo in mezok)
            {
                mezo.BackColor = Color.White;
                mezo.ForeColor = sotet;
                mezo.BorderStyle = BorderStyle.FixedSingle;
                mezo.Font = new Font("Segoe UI", 11);
            }

            txtPassword.PasswordChar = '*';
            txtPasswordAgain.PasswordChar = '*';

            btnRegister.BackColor = kek;
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.Text = "Regisztráció";

            btnLogin.BackColor = Color.White;
            btnLogin.ForeColor = kek;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 1;
            btnLogin.FlatAppearance.BorderColor = kek;
            btnLogin.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;

            MeretezdRegisztracio();
        }

        private Label FindLabel(string text)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label label && label.Text.Trim() == text)
                    return label;
            }

            return null;
        }

        private void RegistrateForm_Resize(object sender, EventArgs e)
        {
            MeretezdRegisztracio();
        }

        private void MeretezdRegisztracio()
        {
            pnlRegister.Left = (this.ClientSize.Width - pnlRegister.Width) / 2;
            pnlRegister.Top = Math.Max(30, (this.ClientSize.Height - pnlRegister.Height) / 2 - 20);

            lblTitle.Location = new Point(25, 25);
            lblSubtitle.Location = new Point(25, 75);

            Label lblUsername = FindLabelInPanel("Felhasználónév:");
            Label lblEmail = FindLabelInPanel("E-mail:");
            Label lblPassword = FindLabelInPanel("Jelszó:");
            Label lblPasswordAgain = FindLabelInPanel("Jelszó újra:");

            if (lblUsername != null)
                lblUsername.Location = new Point(50, 130);

            txtUsername.SetBounds(50, 157, 370, 32);

            if (lblEmail != null)
                lblEmail.Location = new Point(50, 205);

            txtEmail.SetBounds(50, 232, 370, 32);

            if (lblPassword != null)
                lblPassword.Location = new Point(50, 280);

            txtPassword.SetBounds(50, 307, 370, 32);

            if (lblPasswordAgain != null)
                lblPasswordAgain.Location = new Point(50, 355);

            txtPasswordAgain.SetBounds(50, 382, 370, 32);

            btnRegister.SetBounds(50, 450, 370, 40);
            btnLogin.SetBounds(50, 505, 370, 40);
        }

        private Label FindLabelInPanel(string text)
        {
            foreach (Control control in pnlRegister.Controls)
            {
                if (control is Label label && label.Text.Trim() == text)
                    return label;
            }

            return null;
        }

        private void txtPasswordAgain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnRegister.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim().ToLowerInvariant();
            string password = txtPassword.Text.Trim();
            string passwordAgain = txtPasswordAgain.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordAgain))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!");
                return;
            }

            try
            {
                MailAddress mailAddress = new MailAddress(email);

                if (!mailAddress.Address.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Érvénytelen e-mail-cím!");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Érvénytelen e-mail-cím!");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("A jelszónak legalább 6 karakter hosszúnak kell lennie!");
                return;
            }

            if (password != passwordAgain)
            {
                MessageBox.Show("A két jelszó nem egyezik!");
                return;
            }

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand usernameCheck = new SQLiteCommand("SELECT COUNT(*) FROM felhasznalok WHERE felhasznalonev=@username", db.GetConnection());
                usernameCheck.Parameters.AddWithValue("@username", username);

                if (Convert.ToInt32(usernameCheck.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Ez a felhasználónév már foglalt!");
                    return;
                }

                SQLiteCommand emailCheck = new SQLiteCommand("SELECT COUNT(*) FROM felhasznalok WHERE LOWER(email)=LOWER(@email)", db.GetConnection());
                emailCheck.Parameters.AddWithValue("@email", email);

                if (Convert.ToInt32(emailCheck.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Ezzel az e-mail-címmel már regisztráltak!");
                    return;
                }

                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO felhasznalok (felhasznalonev, email, jelszo, szerepkor) VALUES (@username, @email, @password, 'user')", db.GetConnection());
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A regisztráció sikertelen!\n" + ex.Message);
                return;
            }
            finally
            {
                db.CloseConnection();
            }

            MessageBox.Show("Sikeres regisztráció!");

            LoginForm lf = new LoginForm();
            lf.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.Show();
            this.Hide();
        }
    }
}