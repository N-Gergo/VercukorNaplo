using System.Data.SQLite;

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
            {
                btnLogin.PerformClick();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegistrateForm reg = new RegistrateForm();
            reg.Show();
            this.Hide();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //ellenõrzés
            if (string.IsNullOrWhiteSpace(txtUsername.Text) && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Felhasználónév és jelszó megadása kötelezõ!",
                    "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("A felhasználónév mezõ nem lehet üres!",
                    "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("A jelszó mezõ nem lehet üres!",
                    "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Db db = new Db();
            db.OpenConnection();

            SQLiteCommand cmdId = new SQLiteCommand(
                "SELECT id FROM felhasznalok WHERE felhasznalonev=@u AND jelszo=@p",
                db.GetConnection()
            );

            cmdId.Parameters.AddWithValue("@u", txtUsername.Text);
            cmdId.Parameters.AddWithValue("@p", txtPassword.Text);

            object idResult = cmdId.ExecuteScalar();

            if (idResult == null)
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                db.CloseConnection();
                return;
            }

            LoggedUser.Id = Convert.ToInt32(idResult);

            SQLiteCommand cmdRole = new SQLiteCommand(
                "SELECT szerepkor FROM felhasznalok WHERE id=@id",
                db.GetConnection()
            );

            cmdRole.Parameters.AddWithValue("@id", LoggedUser.Id);
            


            string role = cmdRole.ExecuteScalar().ToString();
            LoggedUser.Role = role;

            db.CloseConnection();

            // Szerepkör alapján form megnyitása
            if (role == "admin")
            {
                Admin af = new Admin();
                af.Show();
                this.Hide();
            }
            else if (role == "assistant")
            {
                Assistant assistant = new Assistant();
                assistant.Show();
                this.Hide();
            }
            else if (role == "doctor")
            {
                Doctor df = new Doctor();
                df.Show();
                this.Hide();
            }
            else
            {
                User uf = new User();
                uf.Show();
                this.Hide();
            }
        }

        
    }
}
