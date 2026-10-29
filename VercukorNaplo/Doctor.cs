using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace VercukorNaplo
{
    public partial class Doctor : Form
    {
        private bool chatLoading = false;

        public Doctor()
        {
            InitializeComponent();
        }

        private void Doctor_Load(object sender, EventArgs e)
        {
            LoadDoctorMeresek();

            dgvmeresek.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvmeresek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvmeresek.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            pnlChat.Visible = false;
            cmbChatPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            rtbChat.ReadOnly = true;
            txtMessage.Multiline = true;

            LoadChatPatients();
        }

        private void LoadDoctorMeresek()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter(
                    @"SELECT m.id, m.datum, m.ido, m.ertek, m.napszak, m.gyors_inzulin, m.bazis_inzulin, m.szenhidrat, f.felhasznalonev
                      FROM meresek m
                      JOIN felhasznalok f ON m.felhasznalo_id = f.id
                      JOIN doctor_patient dp ON dp.patient_id = f.id
                      WHERE dp.doctor_id = @doc
                      ORDER BY m.datum DESC, m.ido DESC",
                    db.GetConnection()
                );

                da.SelectCommand.Parameters.AddWithValue("@doc", LoggedUser.Id);

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
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            pnlChat.Visible = true;
            pnlChat.BringToFront();

            LoadChatPatients();

            if (cmbChatPatient.SelectedValue != null && !(cmbChatPatient.SelectedValue is DataRowView))
            {
                int patientId = Convert.ToInt32(cmbChatPatient.SelectedValue);
                LoadConversation(patientId);
            }
        }

        private void btnCloseChat_Click(object sender, EventArgs e)
        {
            pnlChat.Visible = false;
        }

        private void LoadChatPatients()
        {
            chatLoading = true;
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter(
                    @"SELECT DISTINCT f.id, f.felhasznalonev
                      FROM doctor_patient dp
                      JOIN felhasznalok f ON dp.patient_id = f.id
                      WHERE dp.doctor_id = @doctorId
                      AND f.szerepkor = 'user'
                      ORDER BY f.felhasznalonev",
                    db.GetConnection()
                );

                da.SelectCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbChatPatient.DataSource = null;
                cmbChatPatient.DisplayMember = "felhasznalonev";
                cmbChatPatient.ValueMember = "id";
                cmbChatPatient.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A páciensek betöltése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
                chatLoading = false;
            }

            if (cmbChatPatient.SelectedValue != null && !(cmbChatPatient.SelectedValue is DataRowView))
            {
                int patientId = Convert.ToInt32(cmbChatPatient.SelectedValue);
                LoadConversation(patientId);
            }
            else
            {
                rtbChat.Clear();
                rtbChat.Text = "Nincs hozzárendelt páciens.";
            }
        }

        private void cmbChatPatient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (chatLoading)
                return;

            if (cmbChatPatient.SelectedValue == null)
                return;

            if (cmbChatPatient.SelectedValue is DataRowView)
                return;

            int patientId = Convert.ToInt32(cmbChatPatient.SelectedValue);
            LoadConversation(patientId);
        }

        private void LoadConversation(int patientId)
        {
            rtbChat.Clear();
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand relationshipCommand = new SQLiteCommand(
                    @"SELECT COUNT(*)
                      FROM doctor_patient
                      WHERE doctor_id = @doctorId
                      AND patient_id = @patientId",
                    db.GetConnection()
                );

                relationshipCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
                relationshipCommand.Parameters.AddWithValue("@patientId", patientId);

                long relationshipCount = Convert.ToInt64(relationshipCommand.ExecuteScalar());

                if (relationshipCount == 0)
                {
                    rtbChat.Text = "Ehhez a pácienshez nincs hozzáférése.";
                    return;
                }

                SQLiteCommand cmd = new SQLiteCommand(
                    @"SELECT id, kuldo_id, fogado_id, uzenet, kuldes_ideje
                      FROM uzenetek
                      WHERE (kuldo_id = @doctorId AND fogado_id = @patientId)
                      OR (kuldo_id = @patientId AND fogado_id = @doctorId)
                      ORDER BY kuldes_ideje ASC, id ASC",
                    db.GetConnection()
                );

                cmd.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
                cmd.Parameters.AddWithValue("@patientId", patientId);

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int senderId = Convert.ToInt32(reader["kuldo_id"]);
                    string message = reader["uzenet"].ToString() ?? "";
                    string date = reader["kuldes_ideje"].ToString() ?? "";

                    if (senderId == LoggedUser.Id)
                        rtbChat.AppendText("Ön");
                    else
                        rtbChat.AppendText(cmbChatPatient.Text);

                    rtbChat.AppendText("  " + date + Environment.NewLine);
                    rtbChat.AppendText(message + Environment.NewLine + Environment.NewLine);
                }

                reader.Close();

                SQLiteCommand readCommand = new SQLiteCommand(
                    @"UPDATE uzenetek
                      SET olvasott = 1
                      WHERE kuldo_id = @patientId
                      AND fogado_id = @doctorId
                      AND olvasott = 0",
                    db.GetConnection()
                );

                readCommand.Parameters.AddWithValue("@patientId", patientId);
                readCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
                readCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A beszélgetés betöltése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.CloseConnection();
            }

            rtbChat.SelectionStart = rtbChat.Text.Length;
            rtbChat.ScrollToCaret();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (cmbChatPatient.SelectedValue == null || cmbChatPatient.SelectedValue is DataRowView)
            {
                MessageBox.Show("Válasszon ki egy pácienst!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = txtMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Írjon be egy üzenetet!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int patientId = Convert.ToInt32(cmbChatPatient.SelectedValue);
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand checkCommand = new SQLiteCommand(
                    @"SELECT COUNT(*)
                      FROM doctor_patient
                      WHERE doctor_id = @doctorId
                      AND patient_id = @patientId",
                    db.GetConnection()
                );

                checkCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
                checkCommand.Parameters.AddWithValue("@patientId", patientId);

                long count = Convert.ToInt64(checkCommand.ExecuteScalar());

                if (count == 0)
                {
                    MessageBox.Show("Ennek a páciensnek nem Ön a kezelőorvosa!", "Hozzáférés megtagadva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SQLiteCommand insertCommand = new SQLiteCommand(
                    @"INSERT INTO uzenetek (kuldo_id, fogado_id, uzenet, kuldes_ideje, olvasott)
                      VALUES (@senderId, @receiverId, @message, @date, 0)",
                    db.GetConnection()
                );

                insertCommand.Parameters.AddWithValue("@senderId", LoggedUser.Id);
                insertCommand.Parameters.AddWithValue("@receiverId", patientId);
                insertCommand.Parameters.AddWithValue("@message", message);
                insertCommand.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                insertCommand.ExecuteNonQuery();
                txtMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Az üzenet küldése sikertelen!\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                db.CloseConnection();
            }

            LoadConversation(patientId);
            txtMessage.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}