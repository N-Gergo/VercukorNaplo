using System;
using System.Data.SQLite;
using System.Net.Mail;
using System.Windows.Forms;

namespace VercukorNaplo
{
    public partial class RegistrateForm : Form
    {
        public RegistrateForm()
        {
            InitializeComponent();
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