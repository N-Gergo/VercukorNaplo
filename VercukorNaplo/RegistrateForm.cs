using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string password = txtPassword.Text.Trim();
            string passwordAgain = txtPasswordAgain.Text.Trim();


            if (string.IsNullOrWhiteSpace(username) &&
                string.IsNullOrWhiteSpace(password) &&
                string.IsNullOrWhiteSpace(passwordAgain))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!");
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("A felhasználónév mező nem lehet üres!");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("A jelszó mező nem lehet üres!");
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordAgain))
            {
                MessageBox.Show("A jelszó megerősítése mező nem lehet üres!");
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
            db.OpenConnection();

            SQLiteCommand cmd = new SQLiteCommand(
                 @"INSERT INTO felhasznalok
                     (felhasznalonev, jelszo, szerepkor)
                        VALUES (@username, @password, 'user')",
                    db.GetConnection()
            );

            cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);

            cmd.ExecuteNonQuery();
            db.CloseConnection();

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
