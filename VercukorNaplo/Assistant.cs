using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace VercukorNaplo
{
    public partial class Assistant : Form
    {
        
        private bool isLoading = false;

        public Assistant()
        {
            InitializeComponent();
           
        }

       

        private void Assistant_Load(object sender, EventArgs e)
        {
            
            cmbDoctorView.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatientView.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbDoctorAssign.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatientAssign.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvMeresek.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvMeresek.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvMeresek.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvMeresek.ReadOnly = true;
            dgvMeresek.AllowUserToAddRows = false;
            dgvMeresek.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            LoadDoctorsForView();
            LoadDoctorsForAssign();
            LoadAllPatientsForAssign();

       
            if (cmbDoctorView.SelectedValue != null)
            {
                int doctorId =
                    Convert.ToInt32(cmbDoctorView.SelectedValue);

                LoadPatientsForDoctor(doctorId);
            }
            else
            {
                cmbPatientView.DataSource = null;
                dgvMeresek.DataSource = null;
            }
        }

        

        private void LoadDoctorsForView()
        {
            isLoading = true;

            Db db = new Db();

            try
            {
                db.OpenConnection();

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                    @"SELECT id, felhasznalonev
                      FROM felhasznalok
                      WHERE szerepkor = 'doctor'
                      ORDER BY felhasznalonev",
                    db.GetConnection()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbDoctorView.DataSource = null;

                    cmbDoctorView.DisplayMember = "felhasznalonev";
                    cmbDoctorView.ValueMember = "id";
                    cmbDoctorView.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Az orvosok betöltése sikertelen!\n" + ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                    @"SELECT f.id, f.felhasznalonev
                      FROM doctor_patient dp
                      JOIN felhasznalok f
                          ON dp.patient_id = f.id
                      WHERE dp.doctor_id = @doctorId
                      AND f.szerepkor = 'user'
                      ORDER BY f.felhasznalonev",
                    db.GetConnection()))
                {
                    da.SelectCommand.Parameters.AddWithValue(
                        "@doctorId",
                        doctorId
                    );

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbPatientView.DataSource = null;

                    cmbPatientView.DisplayMember = "felhasznalonev";
                    cmbPatientView.ValueMember = "id";
                    cmbPatientView.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "A páciensek betöltése sikertelen!\n" + ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                db.CloseConnection();
                isLoading = oldLoading;
            }

            
            if (cmbPatientView.SelectedValue != null)
            {
                int patientId =
                    Convert.ToInt32(cmbPatientView.SelectedValue);

                LoadPatientMeasurements(patientId);
            }
            else
            {
                dgvMeresek.DataSource = null;
            }
        }

        

        private void LoadPatientMeasurements(int patientId)
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                    @"SELECT
                        id,
                        datum,
                        ido,
                        ertek,
                        gyors_inzulin,
                        bazis_inzulin,
                        szenhidrat,
                        napszak
                      FROM meresek
                      WHERE felhasznalo_id = @patientId
                      ORDER BY datum DESC, ido DESC",
                    db.GetConnection()))
                {
                    da.SelectCommand.Parameters.AddWithValue(
                        "@patientId",
                        patientId
                    );

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMeresek.DataSource = dt;
                }

                if (dgvMeresek.Columns["id"] != null)
                {
                    dgvMeresek.Columns["id"].Visible = false;
                }

               
                if (dgvMeresek.Columns["datum"] != null)
                    dgvMeresek.Columns["datum"].HeaderText = "Dátum";

                if (dgvMeresek.Columns["ido"] != null)
                    dgvMeresek.Columns["ido"].HeaderText = "Idő";

                if (dgvMeresek.Columns["ertek"] != null)
                    dgvMeresek.Columns["ertek"].HeaderText = "Érték";

                if (dgvMeresek.Columns["gyors_inzulin"] != null)
                    dgvMeresek.Columns["gyors_inzulin"].HeaderText =
                        "Gyors inzulin";

                if (dgvMeresek.Columns["bazis_inzulin"] != null)
                    dgvMeresek.Columns["bazis_inzulin"].HeaderText =
                        "Bázis inzulin";

                if (dgvMeresek.Columns["szenhidrat"] != null)
                    dgvMeresek.Columns["szenhidrat"].HeaderText =
                        "Szénhidrát";

                if (dgvMeresek.Columns["napszak"] != null)
                    dgvMeresek.Columns["napszak"].HeaderText =
                        "Napszak";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "A mérések betöltése sikertelen!\n" + ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                db.CloseConnection();
            }
        }



        private void cmbDoctorView_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbDoctorView.SelectedValue == null)
                return;

            int doctorId = Convert.ToInt32(cmbDoctorView.SelectedValue);

            LoadPatientsForDoctor(doctorId);
        }

        private void cmbPatientView_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbPatientView.SelectedValue == null)
            {
                dgvMeresek.DataSource = null;
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

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                    @"SELECT id, felhasznalonev
                      FROM felhasznalok
                      WHERE szerepkor = 'doctor'
                      ORDER BY felhasznalonev",
                    db.GetConnection()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbDoctorAssign.DataSource = null;

                    cmbDoctorAssign.DisplayMember = "felhasznalonev";
                    cmbDoctorAssign.ValueMember = "id";
                    cmbDoctorAssign.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Az orvosok betöltése sikertelen!\n" + ex.Message
                );
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

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(
                    @"SELECT id, felhasznalonev
                      FROM felhasznalok
                      WHERE szerepkor = 'user'
                      ORDER BY felhasznalonev",
                    db.GetConnection()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbPatientAssign.DataSource = null;

                    cmbPatientAssign.DisplayMember = "felhasznalonev";
                    cmbPatientAssign.ValueMember = "id";
                    cmbPatientAssign.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "A páciensek betöltése sikertelen!\n" + ex.Message
                );
            }
            finally
            {
                db.CloseConnection();
            }
        }

      

        private void btnHozzarendeles_Click(
            object sender,
            EventArgs e)
        {
            if (cmbDoctorAssign.SelectedValue == null)
            {
                MessageBox.Show(
                    "Nincs kiválasztva orvos!",
                    "Figyelmeztetés",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cmbPatientAssign.SelectedValue == null)
            {
                MessageBox.Show(
                    "Nincs kiválasztva páciens!",
                    "Figyelmeztetés",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            int doctorId =
                Convert.ToInt32(cmbDoctorAssign.SelectedValue);

            int patientId =
                Convert.ToInt32(cmbPatientAssign.SelectedValue);

            Db db = new Db();

            try
            {
                db.OpenConnection();

                
                using (SQLiteCommand checkCmd = new SQLiteCommand(
                    @"SELECT doctor_id
                      FROM doctor_patient
                      WHERE patient_id = @patientId
                      LIMIT 1",
                    db.GetConnection()))
                {
                    checkCmd.Parameters.AddWithValue(
                        "@patientId",
                        patientId
                    );

                    object result = checkCmd.ExecuteScalar();

                    if (result != null)
                    {
                        int existingDoctorId =
                            Convert.ToInt32(result);

                        if (existingDoctorId == doctorId)
                        {
                            MessageBox.Show(
                                "Ez a páciens már ehhez az orvoshoz van hozzárendelve!",
                                "Figyelmeztetés",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                        else
                        {
                            MessageBox.Show(
                                "Ehhez a pácienshez már tartozik egy másik kezelőorvos!\n" +
                                "Először törölje a meglévő kapcsolatot.",
                                "Figyelmeztetés",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }

                        return;
                    }
                }

                using (SQLiteCommand insertCmd = new SQLiteCommand(
                    @"INSERT INTO doctor_patient
                      (doctor_id, patient_id)
                      VALUES (@doctorId, @patientId)",
                    db.GetConnection()))
                {
                    insertCmd.Parameters.AddWithValue(
                        "@doctorId",
                        doctorId
                    );

                    insertCmd.Parameters.AddWithValue(
                        "@patientId",
                        patientId
                    );

                    insertCmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "Az orvos és a páciens hozzárendelése sikeres!",
                    "Siker",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Hiba történt a hozzárendelés során!\n" +
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

            RefreshViewAfterChange(doctorId, patientId);
        }

        

        private void btnKapcsolatTorles_Click(
            object sender,
            EventArgs e)
        {
            if (cmbDoctorAssign.SelectedValue == null ||
                cmbPatientAssign.SelectedValue == null)
            {
                MessageBox.Show(
                    "Válasszon ki egy orvost és egy pácienst!",
                    "Figyelmeztetés",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            int doctorId =
                Convert.ToInt32(cmbDoctorAssign.SelectedValue);

            int patientId =
                Convert.ToInt32(cmbPatientAssign.SelectedValue);

            DialogResult answer = MessageBox.Show(
                "Biztosan törölni szeretné az orvos és a páciens kapcsolatát?",
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

                using (SQLiteCommand deleteCmd = new SQLiteCommand(
                    @"DELETE FROM doctor_patient
                      WHERE doctor_id = @doctorId
                      AND patient_id = @patientId",
                    db.GetConnection()))
                {
                    deleteCmd.Parameters.AddWithValue(
                        "@doctorId",
                        doctorId
                    );

                    deleteCmd.Parameters.AddWithValue(
                        "@patientId",
                        patientId
                    );

                    int affectedRows =
                        deleteCmd.ExecuteNonQuery();

                    if (affectedRows == 0)
                    {
                        MessageBox.Show(
                            "A kiválasztott orvos és páciens között nincs kapcsolat.",
                            "Nincs találat",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        return;
                    }
                }

                MessageBox.Show(
                    "A kapcsolat sikeresen törölve!",
                    "Siker",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Hiba történt a kapcsolat törlése során!\n" +
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

            RefreshViewAfterChange(doctorId, null);
        }



        private void RefreshViewAfterChange(
    int doctorId,
    int? patientId)
        {
            cmbDoctorView.SelectedValue = doctorId;

            LoadPatientsForDoctor(doctorId);

            if (patientId.HasValue &&
                cmbPatientView.SelectedValue != null)
            {
                cmbPatientView.SelectedValue = patientId.Value;

                LoadPatientMeasurements(patientId.Value);
            }
        }



        private void btnLogout_Click_1(
            object sender,
            EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.Show();

            this.Close();
        }
    }
}