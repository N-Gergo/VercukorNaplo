using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VercukorNaplo
{
    public partial class User : Form
    {
        private int assignedDoctorId = 0;

        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
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

            pnlChat.Visible = false;
            rtbChat.ReadOnly = true;
            txtMessage.Multiline = true;

           

            LoadMeresek();
        }

        private void LoadMeresek()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter(
                    "SELECT id, datum, ido, ertek, napszak, gyors_inzulin, bazis_inzulin, szenhidrat " +
                    "FROM meresek WHERE felhasznalo_id = @id ORDER BY datum DESC, ido DESC",
                    db.GetConnection()
                );

                da.SelectCommand.Parameters.AddWithValue("@id", LoggedUser.Id);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvMeresek.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A mérések betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            if (dgvMeresek.Columns.Contains("id"))
                dgvMeresek.Columns["id"].Visible = false;

            if (dgvMeresek.Columns.Contains("datum"))
                dgvMeresek.Columns["datum"].DisplayIndex = 0;

            if (dgvMeresek.Columns.Contains("ido"))
                dgvMeresek.Columns["ido"].DisplayIndex = 1;

            if (dgvMeresek.Columns.Contains("ertek"))
                dgvMeresek.Columns["ertek"].DisplayIndex = 2;

            if (dgvMeresek.Columns.Contains("gyors_inzulin"))
                dgvMeresek.Columns["gyors_inzulin"].DisplayIndex = 3;

            if (dgvMeresek.Columns.Contains("bazis_inzulin"))
                dgvMeresek.Columns["bazis_inzulin"].DisplayIndex = 4;

            if (dgvMeresek.Columns.Contains("szenhidrat"))
                dgvMeresek.Columns["szenhidrat"].DisplayIndex = 5;

            if (dgvMeresek.Columns.Contains("napszak"))
                dgvMeresek.Columns["napszak"].DisplayIndex = 6;

            LoadVercukorChart();
        }

        private void btnMentes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtErtek.Text))
            {
                MessageBox.Show("Adja meg a vércukorértéket!");
                return;
            }

            Db db = new Db();

            try
            {
                db.OpenConnection();

                string sql = "INSERT INTO meresek (datum, ido, ertek, napszak, gyors_inzulin, bazis_inzulin, szenhidrat, felhasznalo_id) " +
                             "VALUES (@datum, @ido, @ertek, @napszak, @gyors, @bazis, @ch, @fid)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@datum", dtpdatum.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ido", dtptime.Value.ToString("HH:mm"));
                    cmd.Parameters.AddWithValue("@ertek", txtErtek.Text.Trim());
                    cmd.Parameters.AddWithValue("@napszak", cmbnapszak.Text);
                    cmd.Parameters.AddWithValue("@gyors", txtGyorsInzulin.Text.Trim());
                    cmd.Parameters.AddWithValue("@bazis", txtBazisInzulin.Text.Trim());
                    cmd.Parameters.AddWithValue("@ch", txtSzenhidrat.Text.Trim());
                    cmd.Parameters.AddWithValue("@fid", LoggedUser.Id);

                    cmd.ExecuteNonQuery();
                }

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

            LoadMeresek();
        }

        private void LoadAssignedDoctor()
        {
            assignedDoctorId = 0;

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand(
                    @"SELECT f.id, f.felhasznalonev
                      FROM doctor_patient dp
                      JOIN felhasznalok f ON dp.doctor_id = f.id
                      WHERE dp.patient_id = @pid
                      AND f.szerepkor = 'doctor'
                      LIMIT 1",
                    db.GetConnection()
                );

                cmd.Parameters.AddWithValue("@pid", LoggedUser.Id);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        assignedDoctorId = Convert.ToInt32(reader["id"]);
                        string doctorName = reader["felhasznalonev"].ToString() ?? "";
                        lblDoctorName.Text = "Hozzárendelt orvos: Dr." + doctorName;
                    }
                    else
                    {
                        lblDoctorName.Text = "Még nincs kezelőorvosa hozzárendelve.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A kezelőorvos betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void LoadVercukorChart()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand(
                    @"SELECT datum, ido, ertek
                      FROM meresek
                      WHERE felhasznalo_id = @userId
                      ORDER BY datum ASC, ido ASC",
                    db.GetConnection()
                );

                cmd.Parameters.AddWithValue("@userId", LoggedUser.Id);

                SQLiteDataReader reader = cmd.ExecuteReader();

                chartVercukor.Series.Clear();
                chartVercukor.ChartAreas.Clear();
                chartVercukor.Legends.Clear();
                chartVercukor.Titles.Clear();

                ChartArea area = new ChartArea("MainArea");

                area.AxisX.Title = "Dátum / idő";
                area.AxisY.Title = "Vércukorszint";
                area.AxisX.Interval = 1;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.LineColor = Color.LightGray;
                area.AxisY.Minimum = 0;

                chartVercukor.ChartAreas.Add(area);

                Series series = new Series("Vércukorszint");

                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 8;
                series.IsXValueIndexed = true;

                int pontIndex = 0;

                while (reader.Read())
                {
                    string datum = reader["datum"].ToString() ?? "";
                    string ido = reader["ido"].ToString() ?? "";

                    if (!double.TryParse(reader["ertek"].ToString(), out double ertek))
                        continue;

                    string felirat;

                    if (DateTime.TryParse(datum + " " + ido, out DateTime datumIdo))
                        felirat = datumIdo.ToString("MM.dd") + "\n" + datumIdo.ToString("HH:mm");
                    else
                        felirat = datum + "\n" + ido;

                    DataPoint point = new DataPoint();

                    point.XValue = pontIndex;
                    point.YValues = new double[] { ertek };
                    point.AxisLabel = felirat;
                    point.Label = ertek.ToString("0.0");

                    series.Points.Add(point);

                    pontIndex++;
                }

                reader.Close();

                chartVercukor.Series.Add(series);
                chartVercukor.Titles.Add(new Title("Vércukorszint alakulása"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("A diagram betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            LoadAssignedDoctor();

            if (assignedDoctorId == 0)
            {
                MessageBox.Show("Önhöz jelenleg nincs kezelőorvos hozzárendelve.");
                return;
            }

            pnlChat.Visible = true;
            pnlChat.BringToFront();

            LoadConversation();
        }

        private void LoadConversation()
        {
            if (assignedDoctorId == 0)
                return;

            rtbChat.Clear();

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

                checkCommand.Parameters.AddWithValue("@doctorId", assignedDoctorId);
                checkCommand.Parameters.AddWithValue("@patientId", LoggedUser.Id);

                long count = Convert.ToInt64(checkCommand.ExecuteScalar());

                if (count == 0)
                {
                    rtbChat.Text = "A kezelőorvosi kapcsolat már nem létezik.";
                    return;
                }

                SQLiteCommand cmd = new SQLiteCommand(
                    @"SELECT id, kuldo_id, fogado_id, uzenet, kuldes_ideje
                      FROM uzenetek
                      WHERE
                      (kuldo_id = @patientId AND fogado_id = @doctorId)
                      OR
                      (kuldo_id = @doctorId AND fogado_id = @patientId)
                      ORDER BY kuldes_ideje ASC, id ASC",
                    db.GetConnection()
                );

                cmd.Parameters.AddWithValue("@patientId", LoggedUser.Id);
                cmd.Parameters.AddWithValue("@doctorId", assignedDoctorId);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int senderId = Convert.ToInt32(reader["kuldo_id"]);
                        string message = reader["uzenet"].ToString() ?? "";
                        string date = reader["kuldes_ideje"].ToString() ?? "";

                        if (senderId == LoggedUser.Id)
                            rtbChat.AppendText("Ön " + date + Environment.NewLine);
                        else
                            rtbChat.AppendText("Orvos " + date + Environment.NewLine);

                        rtbChat.AppendText(message + Environment.NewLine + Environment.NewLine);
                    }
                }

                SQLiteCommand readCommand = new SQLiteCommand(
                    @"UPDATE uzenetek
                      SET olvasott = 1
                      WHERE kuldo_id = @doctorId
                      AND fogado_id = @patientId
                      AND olvasott = 0",
                    db.GetConnection()
                );

                readCommand.Parameters.AddWithValue("@doctorId", assignedDoctorId);
                readCommand.Parameters.AddWithValue("@patientId", LoggedUser.Id);

                readCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A beszélgetés betöltése sikertelen!\n" + ex.Message);
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
            if (assignedDoctorId == 0)
            {
                MessageBox.Show("Nincs hozzárendelt kezelőorvos!");
                return;
            }

            string message = txtMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Írjon be egy üzenetet!");
                return;
            }

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

                checkCommand.Parameters.AddWithValue("@doctorId", assignedDoctorId);
                checkCommand.Parameters.AddWithValue("@patientId", LoggedUser.Id);

                long count = Convert.ToInt64(checkCommand.ExecuteScalar());

                if (count == 0)
                {
                    MessageBox.Show("A kezelőorvosi kapcsolat már nem létezik.");
                    return;
                }

                SQLiteCommand insertCommand = new SQLiteCommand(
                    @"INSERT INTO uzenetek
                      (kuldo_id, fogado_id, uzenet, kuldes_ideje, olvasott)
                      VALUES
                      (@senderId, @receiverId, @message, @date, 0)",
                    db.GetConnection()
                );

                insertCommand.Parameters.AddWithValue("@senderId", LoggedUser.Id);
                insertCommand.Parameters.AddWithValue("@receiverId", assignedDoctorId);
                insertCommand.Parameters.AddWithValue("@message", message);
                insertCommand.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                insertCommand.ExecuteNonQuery();

                txtMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Az üzenet küldése sikertelen!\n" + ex.Message);
                return;
            }
            finally
            {
                db.CloseConnection();
            }

            LoadConversation();
            txtMessage.Focus();
        }

        private void btnCloseChat_Click(object sender, EventArgs e)
        {
            pnlChat.Visible = false;
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}