using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace VercukorNaplo
{
    public partial class Admin : Form
    {
        private readonly Label lblMeresekCim = new Label();
        private readonly Label lblUsersCim = new Label();

        public Admin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            LetrehozCimek();
            this.Resize += Admin_Resize;
            this.FormClosing += Admin_FormClosing;
        }

        private bool kijelentkezes = false;

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!kijelentkezes)
                Application.Exit();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            BeallitKinezet();

            cmbSzerepkor.Items.Clear();
            cmbSzerepkor.Items.Add("user");
            cmbSzerepkor.Items.Add("doctor");
            cmbSzerepkor.Items.Add("assistant");
            cmbSzerepkor.SelectedIndex = 0;
            cmbSzerepkor.DropDownStyle = ComboBoxStyle.DropDownList;

            BeallitTabla(dgvmeresek);
            BeallitTabla(dgvUsers);

            LoadUsers();
            LoadAllMeresek();
            MeretezdElrendezes();
        }

        private void LetrehozCimek()
        {
            lblMeresekCim.AutoSize = true;
            lblMeresekCim.Text = "Mérések kezelése";

            lblUsersCim.AutoSize = true;
            lblUsersCim.Text = "Felhasználók kezelése";

            this.Controls.Add(lblMeresekCim);
            this.Controls.Add(lblUsersCim);

            lblMeresekCim.BringToFront();
            lblUsersCim.BringToFront();
        }

        private void BeallitKinezet()
        {
            Color kek = Color.FromArgb(47, 128, 237);
            Color hatter = Color.FromArgb(244, 247, 250);
            Color sotet = Color.FromArgb(45, 55, 72);
            Color piros = Color.FromArgb(220, 53, 69);

            this.BackColor = hatter;
            this.Font = new Font("Segoe UI", 10);
            this.Text = "Vércukornapló - Adminisztrátor";
            this.MinimumSize = new Size(1100, 650);
            this.AutoScroll = true;

            lblMeresekCim.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblMeresekCim.ForeColor = sotet;

            lblUsersCim.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblUsersCim.ForeColor = sotet;

            Button[] kekGombok = { btnLogout, btnSzerepkor };

            foreach (Button gomb in kekGombok)
            {
                gomb.BackColor = kek;
                gomb.ForeColor = Color.White;
                gomb.FlatStyle = FlatStyle.Flat;
                gomb.FlatAppearance.BorderSize = 0;
                gomb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                gomb.Cursor = Cursors.Hand;
                gomb.Height = 34;
            }

            Button[] pirosGombok = { btnTorles, btnMeresTorles };

            foreach (Button gomb in pirosGombok)
            {
                gomb.BackColor = piros;
                gomb.ForeColor = Color.White;
                gomb.FlatStyle = FlatStyle.Flat;
                gomb.FlatAppearance.BorderSize = 0;
                gomb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                gomb.Cursor = Cursors.Hand;
                gomb.Height = 34;
            }

            txtSzuresNev.BackColor = Color.White;
            txtSzuresNev.ForeColor = sotet;
            txtSzuresNev.BorderStyle = BorderStyle.FixedSingle;
            txtSzuresNev.Font = new Font("Segoe UI", 10);

            cmbSzerepkor.BackColor = Color.White;
            cmbSzerepkor.ForeColor = sotet;
            cmbSzerepkor.FlatStyle = FlatStyle.Flat;
            cmbSzerepkor.Font = new Font("Segoe UI", 10);
        }

        private void BeallitTabla(DataGridView tabla)
        {
            Color kek = Color.FromArgb(47, 128, 237);
            Color sotet = Color.FromArgb(45, 55, 72);

            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tabla.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            tabla.ReadOnly = true;
            tabla.AllowUserToAddRows = false;
            tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabla.MultiSelect = false;
            tabla.ScrollBars = ScrollBars.Both;
            tabla.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            tabla.EnableHeadersVisualStyles = false;
            tabla.BackgroundColor = Color.White;
            tabla.BorderStyle = BorderStyle.None;
            tabla.GridColor = Color.FromArgb(220, 225, 230);
            tabla.RowHeadersVisible = false;

            tabla.ColumnHeadersDefaultCellStyle.BackColor = kek;
            tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tabla.ColumnHeadersDefaultCellStyle.SelectionBackColor = kek;

            tabla.DefaultCellStyle.BackColor = Color.White;
            tabla.DefaultCellStyle.ForeColor = sotet;
            tabla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 255);
            tabla.DefaultCellStyle.SelectionForeColor = sotet;
        }

        private void Admin_Resize(object sender, EventArgs e)
        {
            MeretezdElrendezes();
        }

        private void MeretezdElrendezes()
        {
            int margo = 20;
            int tavolsag = 35;

            btnLogout.Location = new Point(margo, 15);

            int teljesSzelesseg = Math.Max(1050, this.ClientSize.Width - margo * 2);

            if (this.ClientSize.Width >= 1400)
            {
                int balSzelesseg = (int)(teljesSzelesseg * 0.64);
                int jobbSzelesseg = teljesSzelesseg - balSzelesseg - tavolsag;
                int jobbX = margo + balSzelesseg + tavolsag;

                lblMeresekCim.Location = new Point(margo, 80);

                label1.Location = new Point(margo, 145);
                txtSzuresNev.SetBounds(margo + 180, 140, 200, 28);
                btnMeresTorles.SetBounds(margo + 400, 137, 160, 34);

                int tablaMagassag = Math.Min(500, Math.Max(300, this.ClientSize.Height - 245));

                dgvmeresek.SetBounds(margo, 195, balSzelesseg, tablaMagassag);

                lblUsersCim.Location = new Point(jobbX, 80);

                btnSzerepkor.SetBounds(jobbX, 135, 190, 34);
                cmbSzerepkor.SetBounds(jobbX + 200, 138, 160, 28);

                btnTorles.SetBounds(jobbX, 180, 170, 34);

                dgvUsers.SetBounds(jobbX, 230, jobbSzelesseg, Math.Min(460, tablaMagassag));
            }
            else
            {
                lblMeresekCim.Location = new Point(margo, 80);

                label1.Location = new Point(margo, 145);
                txtSzuresNev.SetBounds(margo + 180, 140, 200, 28);
                btnMeresTorles.SetBounds(margo + 400, 137, 160, 34);

                int meresTablaMagassag = 330;

                dgvmeresek.SetBounds(margo, 195, teljesSzelesseg, meresTablaMagassag);

                int alsoY = dgvmeresek.Bottom + 45;

                lblUsersCim.Location = new Point(margo, alsoY);

                btnSzerepkor.SetBounds(margo, alsoY + 55, 190, 34);
                cmbSzerepkor.SetBounds(margo + 200, alsoY + 58, 160, 28);
                btnTorles.SetBounds(margo + 380, alsoY + 55, 170, 34);

                dgvUsers.SetBounds(margo, alsoY + 105, teljesSzelesseg, 300);
            }
        }

        private void LoadUsers()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, felhasznalonev, email, szerepkor FROM felhasznalok WHERE szerepkor<>'admin' ORDER BY felhasznalonev", db.GetConnection());

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A felhasználók betöltése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
            }

            if (dgvUsers.Columns.Contains("id"))
                dgvUsers.Columns["id"].Visible = false;

            if (dgvUsers.Columns.Contains("felhasznalonev"))
                dgvUsers.Columns["felhasznalonev"].HeaderText = "Felhasználónév";

            if (dgvUsers.Columns.Contains("email"))
                dgvUsers.Columns["email"].HeaderText = "E-mail";

            if (dgvUsers.Columns.Contains("szerepkor"))
                dgvUsers.Columns["szerepkor"].HeaderText = "Szerepkör";
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
                return;

            object value = dgvUsers.SelectedRows[0].Cells["szerepkor"].Value;

            if (value == null)
                return;

            string role = value.ToString() ?? "";

            if (cmbSzerepkor.Items.Contains(role))
                cmbSzerepkor.SelectedItem = role;
        }

        private void btnSzerepkor_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Válasszon ki egy felhasználót!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbSzerepkor.SelectedItem == null)
            {
                MessageBox.Show("Válasszon szerepkört!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["id"].Value);
            string regiRole = dgvUsers.SelectedRows[0].Cells["szerepkor"].Value?.ToString() ?? "";
            string ujRole = cmbSzerepkor.SelectedItem.ToString() ?? "";

            if (ujRole != "user" && ujRole != "doctor" && ujRole != "assistant")
            {
                MessageBox.Show("Érvénytelen szerepkör!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (regiRole == ujRole)
            {
                MessageBox.Show("A felhasználó már ebben a szerepkörben van.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Db db = new Db();

            try
            {
                db.OpenConnection();

                using (SQLiteTransaction transaction = db.GetConnection().BeginTransaction())
                {
                    if (regiRole == "doctor" && ujRole != "doctor")
                    {
                        SQLiteCommand kapcsolat = new SQLiteCommand("DELETE FROM doctor_patient WHERE doctor_id=@id", db.GetConnection(), transaction);
                        kapcsolat.Parameters.AddWithValue("@id", id);
                        kapcsolat.ExecuteNonQuery();
                    }

                    if (regiRole == "user" && ujRole != "user")
                    {
                        SQLiteCommand kapcsolat = new SQLiteCommand("DELETE FROM doctor_patient WHERE patient_id=@id", db.GetConnection(), transaction);
                        kapcsolat.Parameters.AddWithValue("@id", id);
                        kapcsolat.ExecuteNonQuery();
                    }

                    SQLiteCommand cmd = new SQLiteCommand("UPDATE felhasznalok SET szerepkor=@role WHERE id=@id AND szerepkor<>'admin'", db.GetConnection(), transaction);
                    cmd.Parameters.AddWithValue("@role", ujRole);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                MessageBox.Show("A szerepkör sikeresen módosítva.", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A szerepkör módosítása sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
            }

            LoadUsers();
            LoadAllMeresek();
        }

        private void btnTorles_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Válasszon ki egy felhasználót!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["id"].Value);
            string felhasznalonev = dgvUsers.SelectedRows[0].Cells["felhasznalonev"].Value?.ToString() ?? "";

            DialogResult answer = MessageBox.Show(
                "Biztosan törölni szeretné ezt a felhasználót?\n\n" +
                felhasznalonev +
                "\n\nA hozzá tartozó mérések, kapcsolatok és üzenetek is törlődnek.",
                "Megerősítés",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
                return;

            Db db = new Db();

            try
            {
                db.OpenConnection();

                using (SQLiteTransaction transaction = db.GetConnection().BeginTransaction())
                {
                    SQLiteCommand uzenetek = new SQLiteCommand("DELETE FROM uzenetek WHERE kuldo_id=@id OR fogado_id=@id", db.GetConnection(), transaction);
                    uzenetek.Parameters.AddWithValue("@id", id);
                    uzenetek.ExecuteNonQuery();

                    SQLiteCommand kapcsolatok = new SQLiteCommand("DELETE FROM doctor_patient WHERE doctor_id=@id OR patient_id=@id", db.GetConnection(), transaction);
                    kapcsolatok.Parameters.AddWithValue("@id", id);
                    kapcsolatok.ExecuteNonQuery();

                    SQLiteCommand meresek = new SQLiteCommand("DELETE FROM meresek WHERE felhasznalo_id=@id", db.GetConnection(), transaction);
                    meresek.Parameters.AddWithValue("@id", id);
                    meresek.ExecuteNonQuery();

                    SQLiteCommand user = new SQLiteCommand("DELETE FROM felhasznalok WHERE id=@id AND szerepkor<>'admin'", db.GetConnection(), transaction);
                    user.Parameters.AddWithValue("@id", id);
                    user.ExecuteNonQuery();

                    transaction.Commit();
                }

                MessageBox.Show("A felhasználó sikeresen törölve.", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A felhasználó törlése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
            }

            LoadUsers();
            LoadAllMeresek();
        }

        private void LoadAllMeresek()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter(
                    "SELECT m.id, m.datum, m.ido, m.ertek, m.gyors_inzulin, m.bazis_inzulin, m.szenhidrat, m.napszak, f.felhasznalonev " +
                    "FROM meresek m JOIN felhasznalok f ON m.felhasznalo_id=f.id " +
                    "ORDER BY m.datum DESC, m.ido DESC",
                    db.GetConnection());

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvmeresek.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A mérések betöltése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
            }

            if (dgvmeresek.Columns.Contains("id"))
                dgvmeresek.Columns["id"].Visible = false;

            if (dgvmeresek.Columns.Contains("datum"))
                dgvmeresek.Columns["datum"].HeaderText = "Dátum";

            if (dgvmeresek.Columns.Contains("ido"))
                dgvmeresek.Columns["ido"].HeaderText = "Idő";

            if (dgvmeresek.Columns.Contains("ertek"))
            {
                dgvmeresek.Columns["ertek"].HeaderText = "Érték";
                dgvmeresek.Columns["ertek"].DefaultCellStyle.Format = "0.0";
            }

            if (dgvmeresek.Columns.Contains("gyors_inzulin"))
            {
                dgvmeresek.Columns["gyors_inzulin"].HeaderText = "Gyors inzulin";
                dgvmeresek.Columns["gyors_inzulin"].DefaultCellStyle.Format = "0.#";
            }

            if (dgvmeresek.Columns.Contains("bazis_inzulin"))
            {
                dgvmeresek.Columns["bazis_inzulin"].HeaderText = "Bázis inzulin";
                dgvmeresek.Columns["bazis_inzulin"].DefaultCellStyle.Format = "0.#";
            }

            if (dgvmeresek.Columns.Contains("szenhidrat"))
            {
                dgvmeresek.Columns["szenhidrat"].HeaderText = "Szénhidrát";
                dgvmeresek.Columns["szenhidrat"].DefaultCellStyle.Format = "0.#";
            }

            if (dgvmeresek.Columns.Contains("napszak"))
                dgvmeresek.Columns["napszak"].HeaderText = "Napszak";

            if (dgvmeresek.Columns.Contains("felhasznalonev"))
                dgvmeresek.Columns["felhasznalonev"].HeaderText = "Felhasználó";
        }

        private void txtSzuresNev_TextChanged(object sender, EventArgs e)
        {
            string filter = txtSzuresNev.Text.Trim();
            DataTable dt = dgvmeresek.DataSource as DataTable;

            if (dt == null)
                return;

            if (string.IsNullOrWhiteSpace(filter))
            {
                dt.DefaultView.RowFilter = "";
                return;
            }

            filter = filter.Replace("'", "''");
            dt.DefaultView.RowFilter = "felhasznalonev LIKE '%" + filter + "%'";
        }

        private void btnMeresTorles_Click(object sender, EventArgs e)
        {
            if (dgvmeresek.SelectedRows.Count == 0)
            {
                MessageBox.Show("Válasszon ki egy mérést!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int meresId = Convert.ToInt32(dgvmeresek.SelectedRows[0].Cells["id"].Value);

            DialogResult answer = MessageBox.Show(
                "Biztosan törölni szeretné a kiválasztott mérést?",
                "Megerősítés",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
                return;

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("DELETE FROM meresek WHERE id=@id", db.GetConnection());
                cmd.Parameters.AddWithValue("@id", meresId);
                cmd.ExecuteNonQuery();

                MessageBox.Show("A mérés sikeresen törölve.", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A mérés törlése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
            }

            LoadAllMeresek();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            kijelentkezes = true;

            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}