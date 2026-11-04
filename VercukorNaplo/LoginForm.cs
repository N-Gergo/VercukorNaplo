using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace VercukorNaplo
{
    public partial class LoginForm : Form
    {
        private readonly Panel pnlLogin = new Panel();
        private readonly Label lblTitle = new Label();
        private readonly Label lblSubtitle = new Label();

        public LoginForm()
        {
            InitializeComponent();
            LetrehozKinezet();
            this.Resize += LoginForm_Resize;
        }

        private void LetrehozKinezet()
        {
            Color kek = Color.FromArgb(47, 128, 237);
            Color hatter = Color.FromArgb(244, 247, 250);
            Color sotet = Color.FromArgb(45, 55, 72);

            this.BackColor = hatter;
            this.Font = new Font("Segoe UI", 10);
            this.Text = "Vércukornapló - Bejelentkezés";
            this.MinimumSize = new Size(900, 650);
            this.WindowState = FormWindowState.Maximized;

            pnlLogin.Size = new Size(430, 430);
            pnlLogin.BackColor = Color.White;
            this.Controls.Add(pnlLogin);

            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(380, 45);
            lblTitle.Text = "Vércukornapló";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.ForeColor = sotet;
            pnlLogin.Controls.Add(lblTitle);

            lblSubtitle.AutoSize = false;
            lblSubtitle.Size = new Size(380, 30);
            lblSubtitle.Text = "Bejelentkezés";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            lblSubtitle.Font = new Font("Segoe UI", 12);
            lblSubtitle.ForeColor = Color.FromArgb(100, 110, 120);
            pnlLogin.Controls.Add(lblSubtitle);

            pnlLogin.Controls.Add(label1);
            pnlLogin.Controls.Add(txtUsername);
            pnlLogin.Controls.Add(label2);
            pnlLogin.Controls.Add(txtPassword);
            pnlLogin.Controls.Add(btnLogin);
            pnlLogin.Controls.Add(btnRegister);

            label1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            label1.ForeColor = sotet;

            label2.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            label2.ForeColor = sotet;

            txtUsername.BackColor = Color.White;
            txtUsername.ForeColor = sotet;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 11);

            txtPassword.BackColor = Color.White;
            txtPassword.ForeColor = sotet;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11);

            btnLogin.BackColor = kek;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;

            btnRegister.BackColor = Color.White;
            btnRegister.ForeColor = kek;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 1;
            btnRegister.FlatAppearance.BorderColor = kek;
            btnRegister.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRegister.Cursor = Cursors.Hand;

            MeretezdLogin();
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            MeretezdLogin();
        }

        private void MeretezdLogin()
        {
            pnlLogin.Left = (this.ClientSize.Width - pnlLogin.Width) / 2;
            pnlLogin.Top = Math.Max(40, (this.ClientSize.Height - pnlLogin.Height) / 2 - 20);

            lblTitle.Location = new Point(25, 30);
            lblSubtitle.Location = new Point(25, 78);

            label1.Location = new Point(45, 135);
            txtUsername.SetBounds(45, 162, 340, 32);

            label2.Location = new Point(45, 215);
            txtPassword.SetBounds(45, 242, 340, 32);

            btnLogin.SetBounds(45, 305, 340, 38);
            btnRegister.SetBounds(45, 355, 340, 38);
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