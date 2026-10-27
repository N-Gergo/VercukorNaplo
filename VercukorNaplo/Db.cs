using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace VercukorNaplo
{
    class Db
    {
        private SQLiteConnection con = new SQLiteConnection("Data Source=vercukor.db");

        public SQLiteConnection GetConnection()
        {
            return con;
        }

        public void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        public void InitializeDatabase()
        {
            bool ujAdatbazis = !File.Exists("vercukor.db");

            if (ujAdatbazis)
            {
                SQLiteConnection.CreateFile("vercukor.db");
            }

            OpenConnection();

            CreateTables();

            if (ujAdatbazis)
            {
                CreateTestData();
            }

            CloseConnection();
        }

        private void CreateTables()
        {
            string sql = @"
CREATE TABLE IF NOT EXISTS felhasznalok (
id INTEGER PRIMARY KEY AUTOINCREMENT,
felhasznalonev TEXT NOT NULL UNIQUE,
jelszo TEXT NOT NULL,
szerepkor TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS doctor_patient (
id INTEGER PRIMARY KEY AUTOINCREMENT,
doctor_id INTEGER NOT NULL,
patient_id INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS meresek (
id INTEGER PRIMARY KEY AUTOINCREMENT,
datum TEXT NOT NULL,
ido TEXT NOT NULL,
ertek REAL NOT NULL,
napszak TEXT,
gyors_inzulin REAL,
bazis_inzulin REAL,
szenhidrat REAL,
felhasznalo_id INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS uzenetek (
id INTEGER PRIMARY KEY AUTOINCREMENT,
kuldo_id INTEGER NOT NULL,
fogado_id INTEGER NOT NULL,
uzenet TEXT NOT NULL,
kuldes_ideje TEXT NOT NULL,
olvasott INTEGER NOT NULL DEFAULT 0
);";

            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        private void CreateTestData()
        {
            InsertUser("admin", "123456", "admin");

            InsertUser("teszta1", "123456", "assistant");
            InsertUser("teszta2", "123456", "assistant");
            InsertUser("teszta3", "123456", "assistant");

            InsertUser("tesztd1", "123456", "doctor");
            InsertUser("tesztd2", "123456", "doctor");
            InsertUser("tesztd3", "123456", "doctor");
            InsertUser("tesztd4", "123456", "doctor");
            InsertUser("tesztd5", "123456", "doctor");

            int p1 = InsertUser("tesztp1", "123456", "user");
            int p2 = InsertUser("tesztp2", "123456", "user");
            int p3 = InsertUser("tesztp3", "123456", "user");
            int p4 = InsertUser("tesztp4", "123456", "user");
            int p5 = InsertUser("tesztp5", "123456", "user");
            int p6 = InsertUser("tesztp6", "123456", "user");
            int p7 = InsertUser("tesztp7", "123456", "user");
            int p8 = InsertUser("tesztp8", "123456", "user");
            int p9 = InsertUser("tesztp9", "123456", "user");
            int p10 = InsertUser("tesztp10", "123456", "user");

            int[] patients = { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 };

            for (int i = 0; i < patients.Length; i++)
            {
                InsertMeres(patients[i], DateTime.Today.AddDays(-3).ToString("yyyy-MM-dd"), "07:30", 5.1 + i * 0.1, "Éhgyomri", 0, 0, 25);
                InsertMeres(patients[i], DateTime.Today.AddDays(-2).ToString("yyyy-MM-dd"), "12:00", 6.2 + i * 0.1, "Étkezés előtt", 4, 0, 50);
                InsertMeres(patients[i], DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"), "14:00", 7.4 + i * 0.1, "Étkezés után", 5, 0, 60);
                InsertMeres(patients[i], DateTime.Today.ToString("yyyy-MM-dd"), "21:00", 6.5 + i * 0.1, "Lefekvés előtt", 0, 12, 20);
            }
        }

        private int InsertUser(string nev, string jelszo, string szerepkor)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO felhasznalok (felhasznalonev, jelszo, szerepkor) VALUES (@nev, @jelszo, @szerepkor); SELECT last_insert_rowid();",
                con
            );

            cmd.Parameters.AddWithValue("@nev", nev);
            cmd.Parameters.AddWithValue("@jelszo", jelszo);
            cmd.Parameters.AddWithValue("@szerepkor", szerepkor);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void InsertMeres(int userId, string datum, string ido, double ertek, string napszak, double gyors, double bazis, double ch)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO meresek (datum, ido, ertek, napszak, gyors_inzulin, bazis_inzulin, szenhidrat, felhasznalo_id) VALUES (@datum, @ido, @ertek, @napszak, @gyors, @bazis, @ch, @user)",
                con
            );

            cmd.Parameters.AddWithValue("@datum", datum);
            cmd.Parameters.AddWithValue("@ido", ido);
            cmd.Parameters.AddWithValue("@ertek", ertek);
            cmd.Parameters.AddWithValue("@napszak", napszak);
            cmd.Parameters.AddWithValue("@gyors", gyors);
            cmd.Parameters.AddWithValue("@bazis", bazis);
            cmd.Parameters.AddWithValue("@ch", ch);
            cmd.Parameters.AddWithValue("@user", userId);

            cmd.ExecuteNonQuery();
        }
    }
}