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
                con.Open();
        }

        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        public void InitializeDatabase()
        {
            bool ujAdatbazis = !File.Exists("vercukor.db");

            if (ujAdatbazis)
                SQLiteConnection.CreateFile("vercukor.db");

            OpenConnection();
            CreateTables();
            EnsureEmailColumn();

            if (ujAdatbazis)
                CreateTestData();

            CloseConnection();
        }

        private void CreateTables()
        {
            string sql = @"
CREATE TABLE IF NOT EXISTS felhasznalok (
id INTEGER PRIMARY KEY AUTOINCREMENT,
felhasznalonev TEXT NOT NULL UNIQUE,
email TEXT,
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

        private void EnsureEmailColumn()
        {
            bool vanEmail = false;

            SQLiteCommand check = new SQLiteCommand("PRAGMA table_info(felhasznalok)", con);
            SQLiteDataReader reader = check.ExecuteReader();

            while (reader.Read())
            {
                if (reader["name"].ToString() == "email")
                {
                    vanEmail = true;
                    break;
                }
            }

            reader.Close();

            if (!vanEmail)
            {
                SQLiteCommand alter = new SQLiteCommand("ALTER TABLE felhasznalok ADD COLUMN email TEXT", con);
                alter.ExecuteNonQuery();
            }

            SQLiteCommand index = new SQLiteCommand("CREATE UNIQUE INDEX IF NOT EXISTS idx_felhasznalok_email ON felhasznalok(email) WHERE email IS NOT NULL AND TRIM(email) <> ''", con);
            index.ExecuteNonQuery();
        }

        private void CreateTestData()
        {
            InsertUser("admin", "admin@example.com", "123456", "admin");

            InsertUser("teszta1", "teszta1@example.com", "123456", "assistant");
            InsertUser("teszta2", "teszta2@example.com", "123456", "assistant");
            InsertUser("teszta3", "teszta3@example.com", "123456", "assistant");

            InsertUser("tesztd1", "tesztd1@example.com", "123456", "doctor");
            InsertUser("tesztd2", "tesztd2@example.com", "123456", "doctor");
            InsertUser("tesztd3", "tesztd3@example.com", "123456", "doctor");
            InsertUser("tesztd4", "tesztd4@example.com", "123456", "doctor");
            InsertUser("tesztd5", "tesztd5@example.com", "123456", "doctor");

            int p1 = InsertUser("tesztp1", "tesztp1@example.com", "123456", "user");
            int p2 = InsertUser("tesztp2", "tesztp2@example.com", "123456", "user");
            int p3 = InsertUser("tesztp3", "tesztp3@example.com", "123456", "user");
            int p4 = InsertUser("tesztp4", "tesztp4@example.com", "123456", "user");
            int p5 = InsertUser("tesztp5", "tesztp5@example.com", "123456", "user");
            int p6 = InsertUser("tesztp6", "tesztp6@example.com", "123456", "user");
            int p7 = InsertUser("tesztp7", "tesztp7@example.com", "123456", "user");
            int p8 = InsertUser("tesztp8", "tesztp8@example.com", "123456", "user");
            int p9 = InsertUser("tesztp9", "tesztp9@example.com", "123456", "user");
            int p10 = InsertUser("tesztp10", "tesztp10@example.com", "123456", "user");

            int[] patients = { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 };

            for (int i = 0; i < patients.Length; i++)
            {
                double elteres = i * 0.03;

                InsertMeres(patients[i], "2026-10-02", "07:30", 5.2 + elteres, "Éhgyomri", 0, 0, 25);
                InsertMeres(patients[i], "2026-10-05", "12:00", 3.5 + elteres, "Étkezés előtt", 0, 0, 40);
                InsertMeres(patients[i], "2026-10-08", "14:00", 9.7 + elteres, "Étkezés után", 5, 0, 60);
                InsertMeres(patients[i], "2026-10-11", "21:00", 6.4 + elteres, "Lefekvés előtt", 0, 12, 20);

                InsertMeres(patients[i], "2026-10-15", "06:30", 5.1 + elteres, "Éhgyomri", 0, 0, 20);
                InsertMeres(patients[i], "2026-10-15", "08:30", 3.7 + elteres, "Étkezés előtt", 0, 0, 30);
                InsertMeres(patients[i], "2026-10-15", "10:30", 6.8 + elteres, "Étkezés után", 4, 0, 45);
                InsertMeres(patients[i], "2026-10-15", "12:30", 8.2 + elteres, "Étkezés előtt", 4, 0, 50);
                InsertMeres(patients[i], "2026-10-15", "14:30", 10.6 + elteres, "Étkezés után", 5, 0, 65);
                InsertMeres(patients[i], "2026-10-15", "17:30", 7.1 + elteres, "Egyéb", 0, 0, 20);
                InsertMeres(patients[i], "2026-10-15", "20:00", 9.4 + elteres, "Étkezés után", 4, 0, 55);
                InsertMeres(patients[i], "2026-10-15", "22:30", 4.3 + elteres, "Lefekvés előtt", 0, 12, 15);

                InsertMeres(patients[i], "2026-10-19", "07:15", 3.4 + elteres, "Éhgyomri", 0, 0, 20);
                InsertMeres(patients[i], "2026-10-22", "13:30", 11.2 + elteres, "Étkezés után", 6, 0, 70);
                InsertMeres(patients[i], "2026-10-26", "18:00", 5.9 + elteres, "Étkezés előtt", 3, 0, 40);
                InsertMeres(patients[i], "2026-10-30", "21:30", 6.7 + elteres, "Lefekvés előtt", 0, 12, 20);
            }
        }

        private int InsertUser(string nev, string email, string jelszo, string szerepkor)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO felhasznalok (felhasznalonev, email, jelszo, szerepkor) VALUES (@nev, @email, @jelszo, @szerepkor); SELECT last_insert_rowid();", con);
            cmd.Parameters.AddWithValue("@nev", nev);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@jelszo", jelszo);
            cmd.Parameters.AddWithValue("@szerepkor", szerepkor);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void InsertMeres(int userId, string datum, string ido, double ertek, string napszak, double gyors, double bazis, double ch)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO meresek (datum, ido, ertek, napszak, gyors_inzulin, bazis_inzulin, szenhidrat, felhasznalo_id) VALUES (@datum, @ido, @ertek, @napszak, @gyors, @bazis, @ch, @user)", con);
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