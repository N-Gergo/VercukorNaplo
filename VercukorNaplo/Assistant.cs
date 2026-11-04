using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace VercukorNaplo
{
    public partial class Assistant : Form
    {
        private bool isLoading = false;

        public Assistant()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            btnMentes.Click += btnMentes_Click;
            this.Resize += Assistant_Resize;
            this.FormClosing += Assistant_FormClosing;
        }

        private bool kijelentkezes = false;

        private void Assistant_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!kijelentkezes)
                Application.Exit();
        }

        private void Assistant_Load(object sender, EventArgs e)
        {
            BeallitKinezet();

            cmbDoctorView.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatientView.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDoctorAssign.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatientAssign.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbnapszak.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbnapszak.Items.Clear();
            cmbnapszak.Items.Add("Éhgyomri");
            cmbnapszak.Items.Add("Étkezés előtt");
            cmbnapszak.Items.Add("Étkezés után");
            cmbnapszak.Items.Add("Lefekvés előtt");
            cmbnapszak.Items.Add("Egyéb");
            cmbnapszak.SelectedIndex = 0;

            dgvMeresek.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMeresek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMeresek.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvMeresek.ReadOnly = true;
            dgvMeresek.AllowUserToAddRows = false;
            dgvMeresek.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMeresek.MultiSelect = false;
            dgvMeresek.ScrollBars = ScrollBars.Both;
            dgvMeresek.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            LoadDoctorsForView();
            LoadDoctorsForAssign();
            LoadAllPatientsForAssign();

            if (cmbDoctorView.SelectedValue != null && !(cmbDoctorView.SelectedValue is DataRowView))
                LoadPatientsForDoctor(Convert.ToInt32(cmbDoctorView.SelectedValue));
            else
            {
                cmbPatientView.DataSource = null;
                dgvMeresek.DataSource = null;
            }

            MeretezdElrendezes();
        }

        private void BeallitKinezet()
        {
            Color kek = Color.FromArgb(47, 128, 237);
            Color hatter = Color.FromArgb(244, 247, 250);
            Color sotet = Color.FromArgb(45, 55, 72);

            this.BackColor = hatter;
            this.Font = new Font("Segoe UI", 10);
            this.Text = "Vércukornapló - Asszisztens";
            this.MinimumSize = new Size(1100, 650);
            this.AutoScroll = true;

            Button[] kekGombok = { btnLogout, btnMentes, btnHozzarendeles };

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

            btnKapcsolatTorlese.BackColor = Color.FromArgb(220, 53, 69);
            btnKapcsolatTorlese.ForeColor = Color.White;
            btnKapcsolatTorlese.FlatStyle = FlatStyle.Flat;
            btnKapcsolatTorlese.FlatAppearance.BorderSize = 0;
            btnKapcsolatTorlese.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnKapcsolatTorlese.Cursor = Cursors.Hand;
            btnKapcsolatTorlese.Height = 34;

            TextBox[] mezok = { txtErtek, txtGyorsInzulin, txtBazisInzulin, txtSzenhidrat };

            foreach (TextBox mezo in mezok)
            {
                mezo.BackColor = Color.White;
                mezo.ForeColor = sotet;
                mezo.BorderStyle = BorderStyle.FixedSingle;
                mezo.Font = new Font("Segoe UI", 10);
            }

            ComboBox[] kombok =
            {
                cmbDoctorView,
                cmbPatientView,
                cmbDoctorAssign,
                cmbPatientAssign,
                cmbnapszak
            };

            foreach (ComboBox combo in kombok)
            {
                combo.BackColor = Color.White;
                combo.ForeColor = sotet;
                combo.FlatStyle = FlatStyle.Flat;
                combo.Font = new Font("Segoe UI", 10);
            }

            label3.Text = "Orvos–páciens kezelés";
            label3.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            label3.ForeColor = sotet;

            label9.Text = "Gyors inzulin:";
            label8.Text = "Bázis inzulin:";

            dgvMeresek.EnableHeadersVisualStyles = false;
            dgvMeresek.BackgroundColor = Color.White;
            dgvMeresek.BorderStyle = BorderStyle.None;
            dgvMeresek.GridColor = Color.FromArgb(220, 225, 230);

            dgvMeresek.ColumnHeadersDefaultCellStyle.BackColor = kek;
            dgvMeresek.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMeresek.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvMeresek.ColumnHeadersDefaultCellStyle.SelectionBackColor = kek;

            dgvMeresek.DefaultCellStyle.BackColor = Color.White;
            dgvMeresek.DefaultCellStyle.ForeColor = sotet;
            dgvMeresek.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 255);
            dgvMeresek.DefaultCellStyle.SelectionForeColor = sotet;

            dgvMeresek.RowHeadersVisible = false;
        }

        private void Assistant_Resize(object sender, EventArgs e)
        {
            MeretezdElrendezes();
        }

        private int TablaMagassag()
        {
            int sorMagassag = 25;
            int magassag = dgvMeresek.ColumnHeadersHeight + dgvMeresek.Rows.Count * sorMagassag + 3;

            if (magassag < 130)
                magassag = 130;

            if (magassag > 240)
                magassag = 240;

            return magassag;
        }

        private void MeretezdElrendezes()
        {
            btnLogout.Location = new Point(20, 15);

            label5.Location = new Point(28, 95);
            cmbDoctorView.SetBounds(90, 90, 180, 28);

            label4.Location = new Point(300, 95);
            cmbPatientView.SetBounds(370, 90, 180, 28);

            int labelY = 155;
            int inputY = 180;

            label12.Location = new Point(45, labelY);
            label11.Location = new Point(170, labelY);
            label10.Location = new Point(270, labelY);
            label9.Location = new Point(375, labelY);
            label8.Location = new Point(510, labelY);
            label6.Location = new Point(650, labelY);
            label7.Location = new Point(785, labelY);

            dtpdatum.SetBounds(20, inputY, 115, 27);
            dtptime.SetBounds(141, inputY, 84, 27);
            txtErtek.SetBounds(232, inputY, 125, 27);
            txtGyorsInzulin.SetBounds(366, inputY, 125, 27);
            txtBazisInzulin.SetBounds(497, inputY, 125, 27);
            txtSzenhidrat.SetBounds(628, inputY, 125, 27);
            cmbnapszak.SetBounds(759, inputY - 1, 151, 28);
            btnMentes.SetBounds(920, inputY - 2, 100, 34);

            int tablaSzelesseg = Math.Min(1100, Math.Max(900, this.ClientSize.Width - 650));
            int tablaMagassag = TablaMagassag();

            dgvMeresek.SetBounds(20, 225, tablaSzelesseg, tablaMagassag);

            if (this.ClientSize.Width >= 1450)
            {
                int jobbX = dgvMeresek.Right + 60;

                label3.Location = new Point(jobbX, 95);

                label1.Location = new Point(jobbX, 145);
                cmbDoctorAssign.SetBounds(jobbX + 70, 140, 190, 28);

                label2.Location = new Point(jobbX, 190);
                cmbPatientAssign.SetBounds(jobbX + 70, 185, 190, 28);

                btnHozzarendeles.SetBounds(jobbX, 240, 140, 34);
                btnKapcsolatTorlese.SetBounds(jobbX + 150, 240, 175, 34);
            }
            else
            {
                int alsoY = dgvMeresek.Bottom + 35;

                label3.Location = new Point(20, alsoY);

                label1.Location = new Point(20, alsoY + 50);
                cmbDoctorAssign.SetBounds(90, alsoY + 45, 190, 28);

                label2.Location = new Point(320, alsoY + 50);
                cmbPatientAssign.SetBounds(390, alsoY + 45, 190, 28);

                btnHozzarendeles.SetBounds(20, alsoY + 95, 140, 34);
                btnKapcsolatTorlese.SetBounds(170, alsoY + 95, 175, 34);
            }
        }

        private void LoadDoctorsForView()
        {
            isLoading = true;
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, felhasznalonev FROM felhasznalok WHERE szerepkor='doctor' ORDER BY felhasznalonev", db.GetConnection());

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbDoctorView.DataSource = null;
                cmbDoctorView.DisplayMember = "felhasznalonev";
                cmbDoctorView.ValueMember = "id";
                cmbDoctorView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Az orvosok betöltése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
                isLoading = false;
            }
        }

        private void LoadPatientsForDoctor(int doctorId)
        {
            bool oldLoading = isLoading;
            isLoading = true;

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT f.id, f.felhasznalonev FROM doctor_patient dp JOIN felhasznalok f ON dp.patient_id=f.id WHERE dp.doctor_id=@doctorId AND f.szerepkor='user' ORDER BY f.felhasznalonev", db.GetConnection());
                da.SelectCommand.Parameters.AddWithValue("@doctorId", doctorId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbPatientView.DataSource = null;
                cmbPatientView.DisplayMember = "felhasznalonev";
                cmbPatientView.ValueMember = "id";
                cmbPatientView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A páciensek betöltése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
                isLoading = oldLoading;
            }

            if (cmbPatientView.SelectedValue != null && !(cmbPatientView.SelectedValue is DataRowView))
                LoadPatientMeasurements(Convert.ToInt32(cmbPatientView.SelectedValue));
            else
            {
                dgvMeresek.DataSource = null;
                MeretezdElrendezes();
            }
        }

        private void LoadPatientMeasurements(int patientId)
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, datum, ido, ertek, gyors_inzulin, bazis_inzulin, szenhidrat, napszak FROM meresek WHERE felhasznalo_id=@patientId ORDER BY datum DESC, ido DESC", db.GetConnection());
                da.SelectCommand.Parameters.AddWithValue("@patientId", patientId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvMeresek.DataSource = dt;

                if (dgvMeresek.Columns.Contains("id"))
                    dgvMeresek.Columns["id"].Visible = false;

                if (dgvMeresek.Columns.Contains("datum"))
                    dgvMeresek.Columns["datum"].HeaderText = "Dátum";

                if (dgvMeresek.Columns.Contains("ido"))
                    dgvMeresek.Columns["ido"].HeaderText = "Idő";

                if (dgvMeresek.Columns.Contains("ertek"))
                {
                    dgvMeresek.Columns["ertek"].HeaderText = "Érték";
                    dgvMeresek.Columns["ertek"].DefaultCellStyle.Format = "0.0";
                }

                if (dgvMeresek.Columns.Contains("gyors_inzulin"))
                {
                    dgvMeresek.Columns["gyors_inzulin"].HeaderText = "Gyors inzulin";
                    dgvMeresek.Columns["gyors_inzulin"].DefaultCellStyle.Format = "0.#";
                }

                if (dgvMeresek.Columns.Contains("bazis_inzulin"))
                {
                    dgvMeresek.Columns["bazis_inzulin"].HeaderText = "Bázis inzulin";
                    dgvMeresek.Columns["bazis_inzulin"].DefaultCellStyle.Format = "0.#";
                }

                if (dgvMeresek.Columns.Contains("szenhidrat"))
                {
                    dgvMeresek.Columns["szenhidrat"].HeaderText = "Szénhidrát";
                    dgvMeresek.Columns["szenhidrat"].DefaultCellStyle.Format = "0.#";
                }

                if (dgvMeresek.Columns.Contains("napszak"))
                    dgvMeresek.Columns["napszak"].HeaderText = "Napszak";
            }
            catch (Exception ex)
            {
                MessageBox.Show("A mérések betöltése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
            }

            MeretezdElrendezes();
        }

        private bool TryParseSzam(string text, out double value)
        {
            if (double.TryParse(text, out value))
                return true;

            return double.TryParse(text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out value);
        }

        private bool TryParseOptional(string text, out double value)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                value = 0;
                return true;
            }

            return TryParseSzam(text.Trim(), out value);
        }

        private void btnMentes_Click(object sender, EventArgs e)
        {
            if (cmbDoctorView.SelectedValue == null || cmbDoctorView.SelectedValue is DataRowView ||
                cmbPatientView.SelectedValue == null || cmbPatientView.SelectedValue is DataRowView)
            {
                MessageBox.Show("Válasszon orvost és pácienst!");
                return;
            }

            int doctorId = Convert.ToInt32(cmbDoctorView.SelectedValue);
            int patientId = Convert.ToInt32(cmbPatientView.SelectedValue);

            if (!TryParseSzam(txtErtek.Text.Trim(), out double ertek) || ertek <= 0)
            {
                MessageBox.Show("Adjon meg érvényes vércukorértéket!");
                return;
            }

            if (!TryParseOptional(txtGyorsInzulin.Text, out double gyors) || gyors < 0)
            {
                MessageBox.Show("A gyors inzulin értéke nem megfelelő!");
                return;
            }

            if (!TryParseOptional(txtBazisInzulin.Text, out double bazis) || bazis < 0)
            {
                MessageBox.Show("A bázis inzulin értéke nem megfelelő!");
                return;
            }

            if (!TryParseOptional(txtSzenhidrat.Text, out double ch) || ch < 0)
            {
                MessageBox.Show("A szénhidrát értéke nem megfelelő!");
                return;
            }

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand check = new SQLiteCommand("SELECT COUNT(*) FROM doctor_patient WHERE doctor_id=@doctorId AND patient_id=@patientId", db.GetConnection());
                check.Parameters.AddWithValue("@doctorId", doctorId);
                check.Parameters.AddWithValue("@patientId", patientId);

                if (Convert.ToInt32(check.ExecuteScalar()) == 0)
                {
                    MessageBox.Show("A kiválasztott páciens nem tartozik ehhez az orvoshoz.");
                    return;
                }

                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO meresek (datum, ido, ertek, napszak, gyors_inzulin, bazis_inzulin, szenhidrat, felhasznalo_id) VALUES (@datum, @ido, @ertek, @napszak, @gyors, @bazis, @ch, @patientId)", db.GetConnection());

                cmd.Parameters.AddWithValue("@datum", dtpdatum.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ido", dtptime.Value.ToString("HH:mm"));
                cmd.Parameters.AddWithValue("@ertek", ertek);
                cmd.Parameters.AddWithValue("@napszak", cmbnapszak.Text);
                cmd.Parameters.AddWithValue("@gyors", gyors);
                cmd.Parameters.AddWithValue("@bazis", bazis);
                cmd.Parameters.AddWithValue("@ch", ch);
                cmd.Parameters.AddWithValue("@patientId", patientId);

                cmd.ExecuteNonQuery();

                MessageBox.Show("A mérés sikeresen rögzítve.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("A mérés mentése sikertelen!\n" + ex.Message);
                return;
            }
            finally
            {
                db.CloseConnection();
            }

            txtErtek.Clear();
            txtGyorsInzulin.Clear();
            txtBazisInzulin.Clear();
            txtSzenhidrat.Clear();

            LoadPatientMeasurements(patientId);
        }

        private void cmbDoctorView_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (isLoading || cmbDoctorView.SelectedValue == null || cmbDoctorView.SelectedValue is DataRowView)
                return;

            int doctorId = Convert.ToInt32(cmbDoctorView.SelectedValue);

            LoadPatientsForDoctor(doctorId);
        }

        private void cmbPatientView_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (isLoading || cmbPatientView.SelectedValue == null || cmbPatientView.SelectedValue is DataRowView)
            {
                dgvMeresek.DataSource = null;
                MeretezdElrendezes();
                return;
            }

            int patientId = Convert.ToInt32(cmbPatientView.SelectedValue);

            LoadPatientMeasurements(patientId);
        }

        private void LoadDoctorsForAssign()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, felhasznalonev FROM felhasznalok WHERE szerepkor='doctor' ORDER BY felhasznalonev", db.GetConnection());

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbDoctorAssign.DataSource = null;
                cmbDoctorAssign.DisplayMember = "felhasznalonev";
                cmbDoctorAssign.ValueMember = "id";
                cmbDoctorAssign.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Az orvosok betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void LoadAllPatientsForAssign()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, felhasznalonev FROM felhasznalok WHERE szerepkor='user' ORDER BY felhasznalonev", db.GetConnection());

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbPatientAssign.DataSource = null;
                cmbPatientAssign.DisplayMember = "felhasznalonev";
                cmbPatientAssign.ValueMember = "id";
                cmbPatientAssign.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A páciensek betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void btnHozzarendeles_Click(object sender, EventArgs e)
        {
            if (cmbDoctorAssign.SelectedValue == null || cmbDoctorAssign.SelectedValue is DataRowView)
            {
                MessageBox.Show("Nincs kiválasztva orvos!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPatientAssign.SelectedValue == null || cmbPatientAssign.SelectedValue is DataRowView)
            {
                MessageBox.Show("Nincs kiválasztva páciens!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int doctorId = Convert.ToInt32(cmbDoctorAssign.SelectedValue);
            int patientId = Convert.ToInt32(cmbPatientAssign.SelectedValue);

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand checkCmd = new SQLiteCommand("SELECT doctor_id FROM doctor_patient WHERE patient_id=@patientId LIMIT 1", db.GetConnection());
                checkCmd.Parameters.AddWithValue("@patientId", patientId);

                object result = checkCmd.ExecuteScalar();

                if (result != null)
                {
                    int existingDoctorId = Convert.ToInt32(result);

                    if (existingDoctorId == doctorId)
                    {
                        MessageBox.Show("Ez a páciens már ehhez az orvoshoz van hozzárendelve!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Ehhez a pácienshez már tartozik egy másik kezelőorvos!\nElőször törölje a meglévő kapcsolatot.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    return;
                }

                SQLiteCommand insertCmd = new SQLiteCommand("INSERT INTO doctor_patient (doctor_id, patient_id) VALUES (@doctorId, @patientId)", db.GetConnection());
                insertCmd.Parameters.AddWithValue("@doctorId", doctorId);
                insertCmd.Parameters.AddWithValue("@patientId", patientId);
                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Az orvos és a páciens hozzárendelése sikeres!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a hozzárendelés során!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
            }

            RefreshViewAfterChange(doctorId, patientId);
        }

        private void btnKapcsolatTorles_Click(object sender, EventArgs e)
        {
            if (cmbDoctorAssign.SelectedValue == null || cmbDoctorAssign.SelectedValue is DataRowView ||
                cmbPatientAssign.SelectedValue == null || cmbPatientAssign.SelectedValue is DataRowView)
            {
                MessageBox.Show("Válasszon ki egy orvost és egy pácienst!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int doctorId = Convert.ToInt32(cmbDoctorAssign.SelectedValue);
            int patientId = Convert.ToInt32(cmbPatientAssign.SelectedValue);

            DialogResult answer = MessageBox.Show("Biztosan törölni szeretné az orvos és a páciens kapcsolatát?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
                return;

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand deleteCmd = new SQLiteCommand("DELETE FROM doctor_patient WHERE doctor_id=@doctorId AND patient_id=@patientId", db.GetConnection());
                deleteCmd.Parameters.AddWithValue("@doctorId", doctorId);
                deleteCmd.Parameters.AddWithValue("@patientId", patientId);

                int affectedRows = deleteCmd.ExecuteNonQuery();

                if (affectedRows == 0)
                {
                    MessageBox.Show("A kiválasztott orvos és páciens között nincs kapcsolat.", "Nincs találat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("A kapcsolat sikeresen törölve!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a kapcsolat törlése során!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
            }

            RefreshViewAfterChange(doctorId, null);
        }

        private void RefreshViewAfterChange(int doctorId, int? patientId)
        {
            cmbDoctorView.SelectedValue = doctorId;

            LoadPatientsForDoctor(doctorId);

            if (patientId.HasValue)
            {
                cmbPatientView.SelectedValue = patientId.Value;

                if (cmbPatientView.SelectedValue != null && !(cmbPatientView.SelectedValue is DataRowView))
                    LoadPatientMeasurements(patientId.Value);
            }
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            kijelentkezes = true;

            LoginForm lf = new LoginForm();
            lf.Show();
            this.Close();
        }
    }
}