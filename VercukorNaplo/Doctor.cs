using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VercukorNaplo
{
    public partial class Doctor : Form
    {
        private bool chatLoading = false;
        private bool patientLoading = false;
        private bool diagramLoading = false;

        private readonly Panel pnlAtlag = new Panel();
        private readonly Panel pnlMinimum = new Panel();
        private readonly Panel pnlMaximum = new Panel();
        private readonly Label lblAtlag = new Label();
        private readonly Label lblMinimum = new Label();
        private readonly Label lblMaximum = new Label();

        private string[] honapNevek =
        {
            "Január", "Február", "Március", "Április", "Május", "Június",
            "Július", "Augusztus", "Szeptember", "Október", "November", "December"
        };

        public Doctor()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            LetrehozStatisztikaKartyak();
            cmbEv.SelectedIndexChanged += cmbEv_SelectedIndexChanged;
            cmbHonap.SelectedIndexChanged += cmbHonap_SelectedIndexChanged;
            this.Resize += Doctor_Resize;
            this.FormClosing += Doctor_FormClosing;
        }

        private bool kijelentkezes = false;

        private void Doctor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!kijelentkezes)
                Application.Exit();
        }

        private void Doctor_Load(object sender, EventArgs e)
        {
            BeallitKinezet();

            cmbnapszak.Items.Clear();
            cmbnapszak.Items.Add("Éhgyomri");
            cmbnapszak.Items.Add("Étkezés előtt");
            cmbnapszak.Items.Add("Étkezés után");
            cmbnapszak.Items.Add("Lefekvés előtt");
            cmbnapszak.Items.Add("Egyéb");
            cmbnapszak.SelectedIndex = 0;

            dgvmeresek.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvmeresek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvmeresek.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            pnlChat.Visible = false;
            rtbChat.ReadOnly = true;
            txtMessage.Multiline = true;

            LoadPatients();
            LoadChatPatients();
            FrissitOlvasatlanUzenetek();
            MeretezdDiagramot();
            MeretezdStatisztikaKartyak();
        }

        private void LetrehozStatisztikaKartyak()
        {
            Panel[] panelek = { pnlAtlag, pnlMinimum, pnlMaximum };
            Label[] labelek = { lblAtlag, lblMinimum, lblMaximum };

            foreach (Panel panel in panelek)
            {
                panel.Size = new Size(165, 44);
                panel.BackColor = Color.White;
                panel.BorderStyle = BorderStyle.None;
                this.Controls.Add(panel);
            }

            foreach (Label label in labelek)
            {
                label.Dock = DockStyle.Fill;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }

            pnlAtlag.Controls.Add(lblAtlag);
            pnlMinimum.Controls.Add(lblMinimum);
            pnlMaximum.Controls.Add(lblMaximum);

            lblAtlag.Text = "Havi átlag\n-";
            lblMinimum.Text = "Minimum\n-";
            lblMaximum.Text = "Maximum\n-";

            pnlAtlag.BringToFront();
            pnlMinimum.BringToFront();
            pnlMaximum.BringToFront();
        }

        private void MeretezdStatisztikaKartyak()
        {
            int szelesseg = 165;
            int tavolsag = 12;
            int jobbMargo = 20;
            int felsoPozicio = cmbEv.Top - 14;

            pnlMaximum.Left = this.ClientSize.Width - jobbMargo - szelesseg;
            pnlMinimum.Left = pnlMaximum.Left - tavolsag - szelesseg;
            pnlAtlag.Left = pnlMinimum.Left - tavolsag - szelesseg;

            pnlAtlag.Top = felsoPozicio;
            pnlMinimum.Top = felsoPozicio;
            pnlMaximum.Top = felsoPozicio;
        }

        private void BeallitKinezet()
        {
            Color kek = Color.FromArgb(47, 128, 237);
            Color hatter = Color.FromArgb(244, 247, 250);
            Color sotet = Color.FromArgb(45, 55, 72);

            this.BackColor = hatter;
            this.Font = new Font("Segoe UI", 10);
            this.Text = "Vércukornapló - Orvos";

            Button[] gombok = { button1, btnMentes, btnChat, btnSendMessage, btnCloseChat };

            foreach (Button gomb in gombok)
            {
                gomb.BackColor = kek;
                gomb.ForeColor = Color.White;
                gomb.FlatStyle = FlatStyle.Flat;
                gomb.FlatAppearance.BorderSize = 0;
                gomb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                gomb.Cursor = Cursors.Hand;
            }

            btnCloseChat.BackColor = Color.FromArgb(220, 53, 69);

            button1.Height = 34;
            btnMentes.Height = 34;
            btnChat.Height = 34;
            btnSendMessage.Height = 34;

            TextBox[] mezok = { txtErtek, txtGyorsInzulin, txtBazisInzulin, txtSzenhidrat, txtMessage };

            foreach (TextBox mezo in mezok)
            {
                mezo.BackColor = Color.White;
                mezo.ForeColor = sotet;
                mezo.BorderStyle = BorderStyle.FixedSingle;
                mezo.Font = new Font("Segoe UI", 10);
            }

            ComboBox[] kombok = { cmbPatient, cmbnapszak, cmbEv, cmbHonap, cmbChatPatient };

            foreach (ComboBox combo in kombok)
            {
                combo.BackColor = Color.White;
                combo.ForeColor = sotet;
                combo.FlatStyle = FlatStyle.Flat;
                combo.Font = new Font("Segoe UI", 10);
            }

            lblPatient.ForeColor = sotet;
            lblPatient.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            lblAtlag.ForeColor = kek;
            lblMinimum.ForeColor = kek;
            lblMaximum.ForeColor = kek;

            dgvmeresek.EnableHeadersVisualStyles = false;
            dgvmeresek.BackgroundColor = Color.White;
            dgvmeresek.BorderStyle = BorderStyle.None;
            dgvmeresek.GridColor = Color.FromArgb(220, 225, 230);
            dgvmeresek.ColumnHeadersDefaultCellStyle.BackColor = kek;
            dgvmeresek.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvmeresek.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvmeresek.ColumnHeadersDefaultCellStyle.SelectionBackColor = kek;
            dgvmeresek.DefaultCellStyle.BackColor = Color.White;
            dgvmeresek.DefaultCellStyle.ForeColor = sotet;
            dgvmeresek.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 255);
            dgvmeresek.DefaultCellStyle.SelectionForeColor = sotet;
            dgvmeresek.RowHeadersVisible = false;
            dgvmeresek.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvmeresek.MultiSelect = false;

            pnlChat.BackColor = Color.White;
            pnlChat.BorderStyle = BorderStyle.FixedSingle;

            btnCloseChat.Left = pnlChat.ClientSize.Width - btnCloseChat.Width - 4;
            btnCloseChat.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbChatPatient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            rtbChat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbChat.BackColor = Color.FromArgb(248, 250, 252);
            rtbChat.ForeColor = sotet;
            rtbChat.BorderStyle = BorderStyle.FixedSingle;
            rtbChat.Font = new Font("Segoe UI", 10);

            txtMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnSendMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            chartVercukor.BackColor = Color.White;
        }

        private void MeretezdDiagramot()
        {
            Control kontener = chartVercukor.Parent;

            if (kontener == null)
                return;

            Control szulo = kontener.Parent;

            if (szulo != null)
            {
                kontener.Left = 20;
                kontener.Width = Math.Max(100, szulo.ClientSize.Width - 40);
                kontener.Height = Math.Max(250, szulo.ClientSize.Height - kontener.Top - 20);
                kontener.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }

            chartVercukor.Dock = DockStyle.Fill;
        }

        private void Doctor_Resize(object sender, EventArgs e)
        {
            MeretezdDiagramot();
            MeretezdStatisztikaKartyak();
        }

        private int GetSelectedPatientId()
        {
            if (cmbPatient.SelectedValue == null || cmbPatient.SelectedValue is DataRowView)
                return 0;

            return Convert.ToInt32(cmbPatient.SelectedValue);
        }

        private void LoadPatients()
        {
            patientLoading = true;
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT f.id, f.felhasznalonev FROM doctor_patient dp JOIN felhasznalok f ON dp.patient_id=f.id WHERE dp.doctor_id=@doctorId AND f.szerepkor='user' ORDER BY f.felhasznalonev", db.GetConnection());
                da.SelectCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbPatient.DataSource = null;
                cmbPatient.DisplayMember = "felhasznalonev";
                cmbPatient.ValueMember = "id";
                cmbPatient.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A páciensek betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
                patientLoading = false;
            }

            if (GetSelectedPatientId() > 0)
                FrissitPatientFelulelet();
            else
                UritPatientFelulelet();
        }

        private void cmbPatient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (patientLoading)
                return;

            FrissitPatientFelulelet();

            if (pnlChat.Visible)
            {
                int patientId = GetSelectedPatientId();

                if (patientId > 0)
                {
                    LoadChatPatients(patientId);
                    LoadConversation(patientId);
                }
            }
        }

        private void FrissitPatientFelulelet()
        {
            int patientId = GetSelectedPatientId();

            if (patientId == 0)
            {
                UritPatientFelulelet();
                return;
            }

            LoadDoctorMeresek(patientId);
            LoadDiagramSzurok();
        }

        private void UritPatientFelulelet()
        {
            dgvmeresek.DataSource = null;
            cmbEv.Items.Clear();
            cmbHonap.Items.Clear();
            chartVercukor.Series.Clear();
            chartVercukor.ChartAreas.Clear();
            chartVercukor.Titles.Clear();
            lblAtlag.Text = "Havi átlag\n-";
            lblMinimum.Text = "Minimum\n-";
            lblMaximum.Text = "Maximum\n-";
        }

        private void LoadDoctorMeresek(int patientId)
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT m.id, m.datum, m.ido, m.ertek, m.napszak, m.gyors_inzulin, m.bazis_inzulin, m.szenhidrat FROM meresek m JOIN doctor_patient dp ON dp.patient_id=m.felhasznalo_id WHERE dp.doctor_id=@doctorId AND m.felhasznalo_id=@patientId ORDER BY m.datum DESC, m.ido DESC", db.GetConnection());
                da.SelectCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
                da.SelectCommand.Parameters.AddWithValue("@patientId", patientId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvmeresek.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A mérések betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            if (dgvmeresek.Columns.Contains("id"))
                dgvmeresek.Columns["id"].Visible = false;

            if (dgvmeresek.Columns.Contains("datum"))
                dgvmeresek.Columns["datum"].DisplayIndex = 0;

            if (dgvmeresek.Columns.Contains("ido"))
                dgvmeresek.Columns["ido"].DisplayIndex = 1;

            if (dgvmeresek.Columns.Contains("ertek"))
                dgvmeresek.Columns["ertek"].DisplayIndex = 2;

            if (dgvmeresek.Columns.Contains("gyors_inzulin"))
                dgvmeresek.Columns["gyors_inzulin"].DisplayIndex = 3;

            if (dgvmeresek.Columns.Contains("bazis_inzulin"))
                dgvmeresek.Columns["bazis_inzulin"].DisplayIndex = 4;

            if (dgvmeresek.Columns.Contains("szenhidrat"))
                dgvmeresek.Columns["szenhidrat"].DisplayIndex = 5;

            if (dgvmeresek.Columns.Contains("napszak"))
                dgvmeresek.Columns["napszak"].DisplayIndex = 6;
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
            int patientId = GetSelectedPatientId();

            if (patientId == 0)
            {
                MessageBox.Show("Válasszon pácienst!");
                return;
            }

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

            if (!TryParseOptional(txtSzenhidrat.Text, out double szenhidrat) || szenhidrat < 0)
            {
                MessageBox.Show("A szénhidrát értéke nem megfelelő!");
                return;
            }

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand check = new SQLiteCommand("SELECT COUNT(*) FROM doctor_patient WHERE doctor_id=@doctorId AND patient_id=@patientId", db.GetConnection());
                check.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
                check.Parameters.AddWithValue("@patientId", patientId);

                if (Convert.ToInt32(check.ExecuteScalar()) == 0)
                {
                    MessageBox.Show("Ehhez a pácienshez nincs hozzáférése.");
                    return;
                }

                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO meresek (datum, ido, ertek, napszak, gyors_inzulin, bazis_inzulin, szenhidrat, felhasznalo_id) VALUES (@datum, @ido, @ertek, @napszak, @gyors, @bazis, @ch, @userId)", db.GetConnection());
                cmd.Parameters.AddWithValue("@datum", dtpdatum.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ido", dtptime.Value.ToString("HH:mm"));
                cmd.Parameters.AddWithValue("@ertek", ertek);
                cmd.Parameters.AddWithValue("@napszak", cmbnapszak.Text);
                cmd.Parameters.AddWithValue("@gyors", gyors);
                cmd.Parameters.AddWithValue("@bazis", bazis);
                cmd.Parameters.AddWithValue("@ch", szenhidrat);
                cmd.Parameters.AddWithValue("@userId", patientId);
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

            LoadDoctorMeresek(patientId);
            LoadDiagramSzurok();
        }

        private void LoadDiagramSzurok()
        {
            int patientId = GetSelectedPatientId();

            if (patientId == 0)
                return;

            diagramLoading = true;
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT DISTINCT substr(datum, 1, 4) AS ev FROM meresek WHERE felhasznalo_id=@patientId ORDER BY ev DESC", db.GetConnection());
                cmd.Parameters.AddWithValue("@patientId", patientId);

                SQLiteDataReader reader = cmd.ExecuteReader();

                cmbEv.Items.Clear();
                cmbHonap.Items.Clear();

                while (reader.Read())
                    cmbEv.Items.Add(reader["ev"].ToString());

                reader.Close();

                if (cmbEv.Items.Count > 0)
                    cmbEv.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Az évek betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
                diagramLoading = false;
            }

            if (cmbEv.Items.Count > 0)
                LoadHonapok();
            else
            {
                chartVercukor.Series.Clear();
                chartVercukor.ChartAreas.Clear();
                chartVercukor.Titles.Clear();
                lblAtlag.Text = "Havi átlag\nNincs adat";
                lblMinimum.Text = "Minimum\nNincs adat";
                lblMaximum.Text = "Maximum\nNincs adat";
            }
        }

        private void LoadHonapok()
        {
            int patientId = GetSelectedPatientId();

            if (patientId == 0 || cmbEv.SelectedItem == null)
                return;

            diagramLoading = true;
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT DISTINCT substr(datum, 6, 2) AS honap FROM meresek WHERE felhasznalo_id=@patientId AND substr(datum, 1, 4)=@ev ORDER BY honap ASC", db.GetConnection());
                cmd.Parameters.AddWithValue("@patientId", patientId);
                cmd.Parameters.AddWithValue("@ev", cmbEv.SelectedItem.ToString());

                SQLiteDataReader reader = cmd.ExecuteReader();

                cmbHonap.Items.Clear();

                while (reader.Read())
                {
                    int honap = Convert.ToInt32(reader["honap"]);
                    cmbHonap.Items.Add(honap.ToString("00") + " - " + honapNevek[honap - 1]);
                }

                reader.Close();

                if (cmbHonap.Items.Count > 0)
                    cmbHonap.SelectedIndex = cmbHonap.Items.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A hónapok betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
                diagramLoading = false;
            }

            LoadVercukorChart();
        }

        private void LoadVercukorChart()
        {
            int patientId = GetSelectedPatientId();

            if (patientId == 0 || cmbEv.SelectedItem == null || cmbHonap.SelectedItem == null)
                return;

            int ev = Convert.ToInt32(cmbEv.SelectedItem);
            int honap = Convert.ToInt32(cmbHonap.SelectedItem.ToString().Substring(0, 2));

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT datum, ido, ertek FROM meresek WHERE felhasznalo_id=@patientId AND substr(datum, 1, 4)=@ev AND substr(datum, 6, 2)=@honap ORDER BY datum ASC, ido ASC", db.GetConnection());
                cmd.Parameters.AddWithValue("@patientId", patientId);
                cmd.Parameters.AddWithValue("@ev", ev.ToString());
                cmd.Parameters.AddWithValue("@honap", honap.ToString("00"));

                SQLiteDataReader reader = cmd.ExecuteReader();

                chartVercukor.Series.Clear();
                chartVercukor.ChartAreas.Clear();
                chartVercukor.Legends.Clear();
                chartVercukor.Titles.Clear();
                chartVercukor.BackColor = Color.White;

                ChartArea area = new ChartArea("MainArea");
                area.BackColor = Color.White;
                area.Position = new ElementPosition(3, 9, 94, 84);
                area.InnerPlotPosition = new ElementPosition(7, 7, 91, 78);
                area.AxisX.Title = ev + ". " + honapNevek[honap - 1];
                area.AxisY.Title = "Vércukorszint (mmol/L)";
                area.AxisX.TitleFont = new Font("Segoe UI", 12, FontStyle.Bold);
                area.AxisY.TitleFont = new Font("Segoe UI", 12, FontStyle.Bold);
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 10);
                area.AxisX.IsLabelAutoFit = false;
                area.AxisY.IsLabelAutoFit = false;
                area.AxisX.LabelStyle.Format = "dd.";
                area.AxisX.IntervalType = DateTimeIntervalType.Days;
                area.AxisX.Interval = 1;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.Enabled = true;
                area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

                DateTime honapEleje = new DateTime(ev, honap, 1);
                DateTime kovetkezoHonap = honapEleje.AddMonths(1);

                area.AxisX.Minimum = honapEleje.ToOADate();
                area.AxisX.Maximum = kovetkezoHonap.AddSeconds(-1).ToOADate();

                chartVercukor.ChartAreas.Add(area);

                Series series = new Series("Vércukorszint");
                series.ChartType = SeriesChartType.Line;
                series.XValueType = ChartValueType.DateTime;
                series.BorderWidth = 3;
                series.Color = Color.FromArgb(47, 128, 237);
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 10;
                series.MarkerColor = Color.White;
                series.MarkerBorderColor = Color.FromArgb(47, 128, 237);
                series.MarkerBorderWidth = 2;
                series.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                series.LabelForeColor = Color.Black;
                series.SmartLabelStyle.Enabled = true;

                double minErtek = double.MaxValue;
                double maxErtek = double.MinValue;

                while (reader.Read())
                {
                    string datum = reader["datum"].ToString() ?? "";
                    string ido = reader["ido"].ToString() ?? "";

                    if (!double.TryParse(reader["ertek"].ToString(), out double ertek))
                        continue;

                    if (!DateTime.TryParse(datum + " " + ido, out DateTime datumIdo))
                        continue;

                    DataPoint point = new DataPoint();
                    point.XValue = datumIdo.ToOADate();
                    point.YValues = new double[] { ertek };
                    point.Label = ertek.ToString("0.0");
                    point.ToolTip = datumIdo.ToString("yyyy.MM.dd. HH:mm") + "\n" + ertek.ToString("0.0") + " mmol/L";

                    series.Points.Add(point);

                    if (ertek < minErtek)
                        minErtek = ertek;

                    if (ertek > maxErtek)
                        maxErtek = ertek;
                }

                reader.Close();

                chartVercukor.Series.Add(series);

                string patientName = cmbPatient.Text;

                Title title = new Title(patientName + " - Vércukorszint alakulása");
                title.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                title.ForeColor = Color.Black;
                chartVercukor.Titles.Add(title);

                if (series.Points.Count > 0)
                {
                    area.AxisY.Minimum = Math.Max(0, Math.Floor(minErtek - 1));
                    area.AxisY.Maximum = Math.Ceiling(maxErtek + 1);
                    area.AxisY.Interval = 1;

                    if (series.Points.Count > 25)
                    {
                        foreach (DataPoint point in series.Points)
                            point.Label = "";
                    }
                }
                else
                {
                    area.AxisY.Minimum = 0;
                    area.AxisY.Maximum = 10;
                    area.AxisY.Interval = 1;

                    Title nincsAdat = new Title("Ebben a hónapban nincs mérési adat");
                    nincsAdat.Font = new Font("Segoe UI", 11, FontStyle.Italic);
                    nincsAdat.ForeColor = Color.Gray;
                    chartVercukor.Titles.Add(nincsAdat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A diagram betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            FrissitHaviStatisztika();
        }

        private void FrissitHaviStatisztika()
        {
            int patientId = GetSelectedPatientId();

            if (patientId == 0 || cmbEv.SelectedItem == null || cmbHonap.SelectedItem == null)
            {
                lblAtlag.Text = "Havi átlag\n-";
                lblMinimum.Text = "Minimum\n-";
                lblMaximum.Text = "Maximum\n-";
                return;
            }

            int ev = Convert.ToInt32(cmbEv.SelectedItem);
            int honap = Convert.ToInt32(cmbHonap.SelectedItem.ToString().Substring(0, 2));

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT AVG(ertek), MIN(ertek), MAX(ertek) FROM meresek WHERE felhasznalo_id=@patientId AND substr(datum, 1, 4)=@ev AND substr(datum, 6, 2)=@honap", db.GetConnection());
                cmd.Parameters.AddWithValue("@patientId", patientId);
                cmd.Parameters.AddWithValue("@ev", ev.ToString());
                cmd.Parameters.AddWithValue("@honap", honap.ToString("00"));

                SQLiteDataReader reader = cmd.ExecuteReader();

                if (reader.Read() && !reader.IsDBNull(0))
                {
                    double atlag = Convert.ToDouble(reader[0]);
                    double minimum = Convert.ToDouble(reader[1]);
                    double maximum = Convert.ToDouble(reader[2]);

                    lblAtlag.Text = "Havi átlag\n" + atlag.ToString("0.0") + " mmol/L";
                    lblMinimum.Text = "Minimum\n" + minimum.ToString("0.0") + " mmol/L";
                    lblMaximum.Text = "Maximum\n" + maximum.ToString("0.0") + " mmol/L";
                }
                else
                {
                    lblAtlag.Text = "Havi átlag\nNincs adat";
                    lblMinimum.Text = "Minimum\nNincs adat";
                    lblMaximum.Text = "Maximum\nNincs adat";
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A statisztika betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void cmbEv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!diagramLoading)
                LoadHonapok();
        }

        private void cmbHonap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!diagramLoading)
                LoadVercukorChart();
        }

        private void FrissitOlvasatlanUzenetek()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM uzenetek WHERE fogado_id=@id AND olvasott=0", db.GetConnection());
                cmd.Parameters.AddWithValue("@id", LoggedUser.Id);

                int olvasatlan = Convert.ToInt32(cmd.ExecuteScalar());

                if (olvasatlan > 0)
                    btnChat.Text = "Üzenetek (" + olvasatlan + " új)";
                else
                    btnChat.Text = "Üzenetek";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Az üzenetek ellenőrzése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void LoadChatPatients(int selectedPatientId = 0)
        {
            if (selectedPatientId == 0 && cmbChatPatient.SelectedValue != null && !(cmbChatPatient.SelectedValue is DataRowView))
                selectedPatientId = Convert.ToInt32(cmbChatPatient.SelectedValue);

            chatLoading = true;
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter(@"SELECT f.id, f.felhasznalonev,
                CASE WHEN COUNT(u.id)>0 THEN f.felhasznalonev || ' (' || COUNT(u.id) || ' új)' ELSE f.felhasznalonev END AS megjelenites
                FROM doctor_patient dp
                JOIN felhasznalok f ON dp.patient_id=f.id
                LEFT JOIN uzenetek u ON u.kuldo_id=f.id AND u.fogado_id=@doctorId AND u.olvasott=0
                WHERE dp.doctor_id=@doctorId AND f.szerepkor='user'
                GROUP BY f.id, f.felhasznalonev
                ORDER BY f.felhasznalonev", db.GetConnection());

                da.SelectCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbChatPatient.DataSource = null;
                cmbChatPatient.DisplayMember = "megjelenites";
                cmbChatPatient.ValueMember = "id";
                cmbChatPatient.DataSource = dt;

                if (selectedPatientId > 0)
                    cmbChatPatient.SelectedValue = selectedPatientId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A páciensek betöltése sikertelen!\n" + ex.Message);
            }
            finally
            {
                db.CloseConnection();
                chatLoading = false;
            }
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            pnlChat.Visible = true;
            pnlChat.BringToFront();

            int patientId = GetSelectedPatientId();

            LoadChatPatients(patientId);

            if (cmbChatPatient.SelectedValue != null && !(cmbChatPatient.SelectedValue is DataRowView))
            {
                patientId = Convert.ToInt32(cmbChatPatient.SelectedValue);
                LoadConversation(patientId);
            }
            else
            {
                rtbChat.Text = "Nincs hozzárendelt páciens.";
            }
        }

        private void cmbChatPatient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (chatLoading || cmbChatPatient.SelectedValue == null || cmbChatPatient.SelectedValue is DataRowView)
                return;

            int patientId = Convert.ToInt32(cmbChatPatient.SelectedValue);
            LoadConversation(patientId);
        }

        private void LoadConversation(int patientId)
        {
            rtbChat.Clear();
            string patientName = "";
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand relationshipCommand = new SQLiteCommand("SELECT f.felhasznalonev FROM doctor_patient dp JOIN felhasznalok f ON dp.patient_id=f.id WHERE dp.doctor_id=@doctorId AND dp.patient_id=@patientId LIMIT 1", db.GetConnection());
                relationshipCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
                relationshipCommand.Parameters.AddWithValue("@patientId", patientId);

                object result = relationshipCommand.ExecuteScalar();

                if (result == null)
                {
                    rtbChat.Text = "Ehhez a pácienshez nincs hozzáférése.";
                    return;
                }

                patientName = result.ToString() ?? "";

                SQLiteCommand cmd = new SQLiteCommand("SELECT id, kuldo_id, fogado_id, uzenet, kuldes_ideje FROM uzenetek WHERE (kuldo_id=@doctorId AND fogado_id=@patientId) OR (kuldo_id=@patientId AND fogado_id=@doctorId) ORDER BY kuldes_ideje ASC, id ASC", db.GetConnection());
                cmd.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
                cmd.Parameters.AddWithValue("@patientId", patientId);

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int senderId = Convert.ToInt32(reader["kuldo_id"]);
                    string message = reader["uzenet"].ToString() ?? "";
                    string date = reader["kuldes_ideje"].ToString() ?? "";

                    if (senderId == LoggedUser.Id)
                        rtbChat.AppendText("Ön " + date + Environment.NewLine);
                    else
                        rtbChat.AppendText(patientName + " " + date + Environment.NewLine);

                    rtbChat.AppendText(message + Environment.NewLine + Environment.NewLine);
                }

                reader.Close();

                SQLiteCommand readCommand = new SQLiteCommand("UPDATE uzenetek SET olvasott=1 WHERE kuldo_id=@patientId AND fogado_id=@doctorId AND olvasott=0", db.GetConnection());
                readCommand.Parameters.AddWithValue("@patientId", patientId);
                readCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
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

            FrissitOlvasatlanUzenetek();
            LoadChatPatients(patientId);
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (cmbChatPatient.SelectedValue == null || cmbChatPatient.SelectedValue is DataRowView)
            {
                MessageBox.Show("Válasszon pácienst!");
                return;
            }

            int patientId = Convert.ToInt32(cmbChatPatient.SelectedValue);
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

                SQLiteCommand checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM doctor_patient WHERE doctor_id=@doctorId AND patient_id=@patientId", db.GetConnection());
                checkCommand.Parameters.AddWithValue("@doctorId", LoggedUser.Id);
                checkCommand.Parameters.AddWithValue("@patientId", patientId);

                if (Convert.ToInt32(checkCommand.ExecuteScalar()) == 0)
                {
                    MessageBox.Show("Ehhez a pácienshez nincs hozzáférése.");
                    return;
                }

                SQLiteCommand insertCommand = new SQLiteCommand("INSERT INTO uzenetek (kuldo_id, fogado_id, uzenet, kuldes_ideje, olvasott) VALUES (@senderId, @receiverId, @message, @date, 0)", db.GetConnection());
                insertCommand.Parameters.AddWithValue("@senderId", LoggedUser.Id);
                insertCommand.Parameters.AddWithValue("@receiverId", patientId);
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

            LoadConversation(patientId);
            txtMessage.Focus();
        }

        private void btnCloseChat_Click(object sender, EventArgs e)
        {
            pnlChat.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kijelentkezes = true;

            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}