using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace VercukorNaplo
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.PerformClick();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegistrateForm reg = new RegistrateForm();
            reg.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Felhasználónév és jelszó megadása kötelezõ!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("A felhasználónév mezõ nem lehet üres!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("A jelszó mezõ nem lehet üres!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Db db = new Db();
            int olvasatlanUzenetek = 0;

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT id, szerepkor FROM felhasznalok WHERE felhasznalonev=@u AND jelszo=@p", db.GetConnection());
                cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@p", txtPassword.Text);

                SQLiteDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LoggedUser.Id = Convert.ToInt32(reader["id"]);
                LoggedUser.Role = reader["szerepkor"].ToString() ?? "";

                reader.Close();

                if (LoggedUser.Role == "doctor" || LoggedUser.Role == "user")
                {
                    SQLiteCommand unreadCmd = new SQLiteCommand("SELECT COUNT(*) FROM uzenetek WHERE fogado_id=@id AND olvasott=0", db.GetConnection());
                    unreadCmd.Parameters.AddWithValue("@id", LoggedUser.Id);
                    olvasatlanUzenetek = Convert.ToInt32(unreadCmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A bejelentkezés sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                db.CloseConnection();
            }

            if (LoggedUser.Role == "admin")
            {
                Admin af = new Admin();
                af.Show();
                this.Hide();
            }
            else if (LoggedUser.Role == "assistant")
            {
                Assistant assistant = new Assistant();
                assistant.Show();
                this.Hide();
            }
            else if (LoggedUser.Role == "doctor")
            {
                Doctor df = new Doctor();

                if (olvasatlanUzenetek > 0)
                {
                    df.Shown += (s, args) =>
                    {
                        MessageBox.Show(olvasatlanUzenetek + " olvasatlan üzenete van.", "Új üzenet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                }

                df.Show();
                this.Hide();
            }
            else if (LoggedUser.Role == "user")
            {
                User uf = new User();

                if (olvasatlanUzenetek > 0)
                {
                    uf.Shown += (s, args) =>
                    {
                        MessageBox.Show(olvasatlanUzenetek + " olvasatlan üzenete van.", "Új üzenet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                }

                uf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Ismeretlen felhasználói szerepkör!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}