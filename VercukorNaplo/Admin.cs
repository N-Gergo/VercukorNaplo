using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace VercukorNaplo
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadAllMeresek();

            // Szerepkörök
            cmbSzerepkor.Items.Clear();
            cmbSzerepkor.Items.Add("user");
            cmbSzerepkor.Items.Add("doctor");
            cmbSzerepkor.Items.Add("assistant");
            cmbSzerepkor.SelectedIndex = 0;

            // Felhasználók táblázata
            dgvUsers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvUsers.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvUsers.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvUsers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvUsers.MultiSelect = false;

            // Mérések táblázata
            dgvmeresek.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvmeresek.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvmeresek.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvmeresek.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvmeresek.MultiSelect = false;
        }

        // KIJELENTKEZÉS
        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        // FELHASZNÁLÓK BETÖLTÉSE
        private void LoadUsers()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter(
                    @"SELECT id, felhasznalonev, szerepkor
                      FROM felhasznalok
                      WHERE szerepkor <> 'admin'
                      ORDER BY id ASC",
                    db.GetConnection()
                );

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "A felhasználók betöltése sikertelen!\n" +
                    ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                db.CloseConnection();
            }

            if (dgvUsers.Columns.Contains("id"))
            {
                dgvUsers.Columns["id"].Visible = false;
            }
        }

        // KIVÁLASZTOTT FELHASZNÁLÓ SZEREPKÖRÉNEK MEGJELENÍTÉSE
        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
                return;

            object value =
                dgvUsers.SelectedRows[0].Cells["szerepkor"].Value;

            if (value == null)
                return;

            string role = value.ToString() ?? "";

            if (cmbSzerepkor.Items.Contains(role))
            {
                cmbSzerepkor.SelectedItem = role;
            }
        }

        // SZEREPKÖR MÓDOSÍTÁSA
        private void btnSzerepkor_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Válasszon ki egy felhasználót!",
                    "Figyelmeztetés",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cmbSzerepkor.SelectedItem == null)
            {
                MessageBox.Show(
                    "Válasszon szerepkört!",
                    "Figyelmeztetés",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            int id = Convert.ToInt32(
                dgvUsers.SelectedRows[0].Cells["id"].Value
            );

            string newRole =
                cmbSzerepkor.SelectedItem.ToString() ?? "";

            if (newRole != "user" &&
                newRole != "doctor" &&
                newRole != "assistant")
            {
                MessageBox.Show(
                    "Érvénytelen szerepkör!",
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand(
                    @"UPDATE felhasznalok
                      SET szerepkor = @role
                      WHERE id = @id
                      AND szerepkor <> 'admin'",
                    db.GetConnection()
                );

                cmd.Parameters.AddWithValue("@role", newRole);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "A szerepkör sikeresen módosítva.",
                    "Siker",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "A szerepkör módosítása sikertelen!\n" +
                    ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                db.CloseConnection();
            }

            LoadUsers();
        }

        // FELHASZNÁLÓ TÖRLÉSE
        private void btnTorles_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Válasszon ki egy felhasználót!",
                    "Figyelmeztetés",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            int id = Convert.ToInt32(
                dgvUsers.SelectedRows[0].Cells["id"].Value
            );

            DialogResult answer = MessageBox.Show(
                "Biztosan törölni szeretné a kiválasztott felhasználót?",
                "Megerősítés",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (answer != DialogResult.Yes)
                return;

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand(
                    @"DELETE FROM felhasznalok
                      WHERE id = @id
                      AND szerepkor <> 'admin'",
                    db.GetConnection()
                );

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "A felhasználó sikeresen törölve.",
                    "Siker",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "A felhasználó törlése sikertelen!\n" +
                    ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                db.CloseConnection();
            }

            LoadUsers();
            LoadAllMeresek();
        }

        // ÖSSZES MÉRÉS BETÖLTÉSE
        private void LoadAllMeresek()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter(
                    @"SELECT
                        m.id,
                        m.datum,
                        m.ido,
                        m.ertek,
                        m.napszak,
                        m.gyors_inzulin,
                        m.bazis_inzulin,
                        m.szenhidrat,
                        f.felhasznalonev
                      FROM meresek m
                      JOIN felhasznalok f
                          ON m.felhasznalo_id = f.id
                      ORDER BY m.datum DESC, m.ido DESC",
                    db.GetConnection()
                );

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvmeresek.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "A mérések betöltése sikertelen!\n" +
                    ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                db.CloseConnection();
            }

            if (dgvmeresek.Columns.Contains("id"))
            {
                dgvmeresek.Columns["id"].Visible = false;
            }
        }

        // MÉRÉSEK SZŰRÉSE FELHASZNÁLÓNÉV ALAPJÁN
        private void txtSzuresNev_TextChanged(object sender, EventArgs e)
        {
            string filter =
                txtSzuresNev.Text.Trim().Replace("'", "''");

            DataTable? dt =
                dgvmeresek.DataSource as DataTable;

            if (dt == null)
                return;

            if (string.IsNullOrWhiteSpace(filter))
            {
                dt.DefaultView.RowFilter = "";
            }
            else
            {
                dt.DefaultView.RowFilter =
                    $"felhasznalonev LIKE '%{filter}%'";
            }
        }

        // MÉRÉS TÖRLÉSE
        private void btnMeresTorles_Click(object sender, EventArgs e)
        {
            if (dgvmeresek.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Válasszon ki egy mérést!",
                    "Figyelmeztetés",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            int meresId = Convert.ToInt32(
                dgvmeresek.SelectedRows[0].Cells["id"].Value
            );

            DialogResult answer = MessageBox.Show(
                "Biztosan törölni szeretné a kiválasztott mérést?",
                "Megerősítés",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (answer != DialogResult.Yes)
                return;

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand(
                    "DELETE FROM meresek WHERE id = @id",
                    db.GetConnection()
                );

                cmd.Parameters.AddWithValue("@id", meresId);

                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "A mérés sikeresen törölve.",
                    "Siker",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "A mérés törlése sikertelen!\n" +
                    ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                db.CloseConnection();
            }

            LoadAllMeresek();
        }
    }
}