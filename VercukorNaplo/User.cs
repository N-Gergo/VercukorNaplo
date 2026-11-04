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

        private bool kijelentkezes = false;
        public User()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            LetrehozStatisztikaKartyak();
            cmbEv.SelectedIndexChanged += cmbEv_SelectedIndexChanged;
            cmbHonap.SelectedIndexChanged += cmbHonap_SelectedIndexChanged;
            this.Resize += User_Resize;
            this.FormClosing += User_FormClosing;
        }

        private void User_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!kijelentkezes)
                Application.Exit();
        }

        private void User_Load(object sender, EventArgs e)
        {
            BeallitKinezet();

            cmbnapszak.Items.Clear();
            cmbnapszak.Items.Add("Éhgyomri");
            cmbnapszak.Items.Add("Étkezés előtt");
            cmbnapszak.Items.Add("Étkezés után");
            cmbnapszak.Items.Add("Lefekvés előtt");
            cmbnapszak.Items.Add("Egyéb");
            cmbnapszak.SelectedIndex = 0;

            chartVercukor.Dock = DockStyle.Fill;
            MeretezdDiagramot();

            dgvMeresek.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMeresek.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMeresek.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            pnlChat.Visible = false;
            rtbChat.ReadOnly = true;
            txtMessage.Multiline = true;
            btnChat.Width = 145;

            LoadMeresek();
            LoadDiagramSzurok();
            LoadAssignedDoctor();
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
            this.Text = "Vércukornapló";

            Button[] gombok = { btnMentes, btnChat, btnLogout, btnSendMessage, btnCloseChat };

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
            btnCloseChat.ForeColor = Color.White;
            btnCloseChat.FlatStyle = FlatStyle.Flat;
            btnCloseChat.FlatAppearance.BorderSize = 0;

            btnMentes.Height = 34;
            btnChat.Height = 34;
            btnLogout.Height = 34;
            btnSendMessage.Height = 34;

            TextBox[] mezok = { txtErtek, txtGyorsInzulin, txtBazisInzulin, txtSzenhidrat, txtMessage };

            foreach (TextBox mezo in mezok)
            {
                mezo.BackColor = Color.White;
                mezo.ForeColor = sotet;
                mezo.BorderStyle = BorderStyle.FixedSingle;
                mezo.Font = new Font("Segoe UI", 10);
            }

            cmbnapszak.BackColor = Color.White;
            cmbnapszak.ForeColor = sotet;
            cmbnapszak.FlatStyle = FlatStyle.Flat;
            cmbnapszak.Font = new Font("Segoe UI", 10);

            cmbEv.BackColor = Color.White;
            cmbEv.ForeColor = sotet;
            cmbEv.FlatStyle = FlatStyle.Flat;
            cmbEv.Font = new Font("Segoe UI", 10);

            cmbHonap.BackColor = Color.White;
            cmbHonap.ForeColor = sotet;
            cmbHonap.FlatStyle = FlatStyle.Flat;
            cmbHonap.Font = new Font("Segoe UI", 10);

            dtpdatum.Font = new Font("Segoe UI", 10);
            dtptime.Font = new Font("Segoe UI", 10);

            lblDoctorName.ForeColor = sotet;
            lblDoctorName.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            lblAtlag.ForeColor = kek;
            lblMinimum.ForeColor = kek;
            lblMaximum.ForeColor = kek;

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
            dgvMeresek.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMeresek.MultiSelect = false;

            pnlChat.BackColor = Color.White;
            pnlChat.BorderStyle = BorderStyle.FixedSingle;
            pnlChat.Width = 320;

            btnCloseChat.Left = pnlChat.ClientSize.Width - btnCloseChat.Width - 4;
            btnCloseChat.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            rtbChat.Left = 3;
            rtbChat.Width = pnlChat.ClientSize.Width - 6;
            rtbChat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbChat.BackColor = Color.FromArgb(248, 250, 252);
            rtbChat.ForeColor = sotet;
            rtbChat.BorderStyle = BorderStyle.FixedSingle;
            rtbChat.Font = new Font("Segoe UI", 10);

            txtMessage.Left = 3;
            txtMessage.Width = pnlChat.ClientSize.Width - 6;
            txtMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            btnSendMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            chartVercukor.BackColor = Color.White;
        }

        private void MeretezdDiagramot()
        {
            Control kontener = chartVercukor.Parent;

            if (kontener == null)
                return;

            if (kontener == this)
            {
                chartVercukor.Dock = DockStyle.None;
                chartVercukor.Left = 20;
                chartVercukor.Width = Math.Max(100, this.ClientSize.Width - 40);
                chartVercukor.Height = Math.Max(250, this.ClientSize.Height - chartVercukor.Top - 20);
                chartVercukor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            else
            {
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
        }

        private void User_Resize(object sender, EventArgs e)
        {
            MeretezdDiagramot();
            MeretezdStatisztikaKartyak();
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

        private void FrissitHaviStatisztika()
        {
            if (cmbEv.SelectedItem == null || cmbHonap.SelectedItem == null)
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

                SQLiteCommand cmd = new SQLiteCommand("SELECT AVG(ertek), MIN(ertek), MAX(ertek) FROM meresek WHERE felhasznalo_id=@userId AND substr(datum, 1, 4)=@ev AND substr(datum, 6, 2)=@honap", db.GetConnection());
                cmd.Parameters.AddWithValue("@userId", LoggedUser.Id);
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

        private void LoadMeresek()
        {
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, datum, ido, ertek, napszak, gyors_inzulin, bazis_inzulin, szenhidrat FROM meresek WHERE felhasznalo_id=@id ORDER BY datum DESC, ido DESC", db.GetConnection());
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

                string sql = "INSERT INTO meresek (datum, ido, ertek, napszak, gyors_inzulin, bazis_inzulin, szenhidrat, felhasznalo_id) VALUES (@datum, @ido, @ertek, @napszak, @gyors, @bazis, @ch, @fid)";

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
            LoadDiagramSzurok();
        }

        private void LoadAssignedDoctor()
        {
            assignedDoctorId = 0;
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT f.id, f.felhasznalonev FROM doctor_patient dp JOIN felhasznalok f ON dp.doctor_id=f.id WHERE dp.patient_id=@pid AND f.szerepkor='doctor' LIMIT 1", db.GetConnection());
                cmd.Parameters.AddWithValue("@pid", LoggedUser.Id);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        assignedDoctorId = Convert.ToInt32(reader["id"]);
                        string doctorName = reader["felhasznalonev"].ToString() ?? "";
                        lblDoctorName.Text = "Hozzárendelt orvos: Dr. " + doctorName;
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

        private void LoadDiagramSzurok()
        {
            diagramLoading = true;
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT DISTINCT substr(datum, 1, 4) AS ev FROM meresek WHERE felhasznalo_id=@userId ORDER BY ev DESC", db.GetConnection());
                cmd.Parameters.AddWithValue("@userId", LoggedUser.Id);

                SQLiteDataReader reader = cmd.ExecuteReader();
                cmbEv.Items.Clear();

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

            LoadHonapok();
        }

        private void LoadHonapok()
        {
            if (cmbEv.SelectedItem == null)
                return;

            diagramLoading = true;
            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT DISTINCT substr(datum, 6, 2) AS honap FROM meresek WHERE felhasznalo_id=@userId AND substr(datum, 1, 4)=@ev ORDER BY honap ASC", db.GetConnection());
                cmd.Parameters.AddWithValue("@userId", LoggedUser.Id);
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
            if (cmbEv.SelectedItem == null || cmbHonap.SelectedItem == null)
                return;

            int ev = Convert.ToInt32(cmbEv.SelectedItem);
            int honap = Convert.ToInt32(cmbHonap.SelectedItem.ToString().Substring(0, 2));

            Db db = new Db();

            try
            {
                db.OpenConnection();

                SQLiteCommand cmd = new SQLiteCommand("SELECT datum, ido, ertek FROM meresek WHERE felhasznalo_id=@userId AND substr(datum, 1, 4)=@ev AND substr(datum, 6, 2)=@honap ORDER BY datum ASC, ido ASC", db.GetConnection());
                cmd.Parameters.AddWithValue("@userId", LoggedUser.Id);
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
                area.AxisX.LabelStyle.Angle = 0;
                area.AxisX.IntervalType = DateTimeIntervalType.Days;
                area.AxisX.Interval = 1;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.Enabled = true;
                area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                area.AxisX.LineColor = Color.Gray;
                area.AxisY.LineColor = Color.Gray;
                area.AxisX.MajorTickMark.LineColor = Color.Gray;
                area.AxisY.MajorTickMark.LineColor = Color.Gray;

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

                Title title = new Title("Vércukorszint alakulása");
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

        private void btnChat_Click(object sender, EventArgs e)
        {
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

                SQLiteCommand checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM doctor_patient WHERE doctor_id=@doctorId AND patient_id=@patientId", db.GetConnection());
                checkCommand.Parameters.AddWithValue("@doctorId", assignedDoctorId);
                checkCommand.Parameters.AddWithValue("@patientId", LoggedUser.Id);

                long count = Convert.ToInt64(checkCommand.ExecuteScalar());

                if (count == 0)
                {
                    rtbChat.Text = "A kezelőorvosi kapcsolat már nem létezik.";
                    return;
                }

                SQLiteCommand cmd = new SQLiteCommand("SELECT id, kuldo_id, fogado_id, uzenet, kuldes_ideje FROM uzenetek WHERE (kuldo_id=@patientId AND fogado_id=@doctorId) OR (kuldo_id=@doctorId AND fogado_id=@patientId) ORDER BY kuldes_ideje ASC, id ASC", db.GetConnection());
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

                SQLiteCommand readCommand = new SQLiteCommand("UPDATE uzenetek SET olvasott=1 WHERE kuldo_id=@doctorId AND fogado_id=@patientId AND olvasott=0", db.GetConnection());
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
            FrissitOlvasatlanUzenetek();
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

                SQLiteCommand checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM doctor_patient WHERE doctor_id=@doctorId AND patient_id=@patientId", db.GetConnection());
                checkCommand.Parameters.AddWithValue("@doctorId", assignedDoctorId);
                checkCommand.Parameters.AddWithValue("@patientId", LoggedUser.Id);

                long count = Convert.ToInt64(checkCommand.ExecuteScalar());

                if (count == 0)
                {
                    MessageBox.Show("A kezelőorvosi kapcsolat már nem létezik.");
                    return;
                }

                SQLiteCommand insertCommand = new SQLiteCommand("INSERT INTO uzenetek (kuldo_id, fogado_id, uzenet, kuldes_ideje, olvasott) VALUES (@senderId, @receiverId, @message, @date, 0)", db.GetConnection());
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
            kijelentkezes = true;

            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}