using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using MetadataExtractor;


namespace SWE2_Projekt
{
    public class DataAccessLayer
    {
        private static string configfile = Path.GetFullPath("../../../config.txt");
        private static string PicFolderPath = Path.GetFullPath("../../../images");
        private string _connectionstring;
        private SqlCommand command;
        private int[] PicIDs = new int[30];
        List<string> PictureNames;
        List<string> EXIF;
        List<string> IPTC;
        Dictionary<int, List<string>> AllPhotographers;

        public DataAccessLayer()
        {
            var file = File.ReadAllText(configfile, Encoding.UTF8);
            _connectionstring = file.ToString();
        }

        public void DeleteAllData()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");
                //SqlCommand command;

                command = new SqlCommand("DELETE FROM FotografInnen", connection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM ITPC", connection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM EXIF", connection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM Tags", connection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM Bilder", connection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM Bild_Tag", connection);
                command.ExecuteNonQuery();

                Console.WriteLine("Deleted all entries.\n\n");

                connection.Close();
            }
        }

        public void InsertAllPictures()
        {
            int idx = 0;
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                foreach (var image in System.IO.Directory.GetFiles(PicFolderPath))
                {
                    string[] segments = image.Split("\\");
                    string title;
                    foreach (string segment in segments)
                    {
                        if (segment.Contains("."))
                        {
                            title = segment;

                            command = new SqlCommand("INSERT INTO Bilder(Titel) VALUES(@Titel)" + "SELECT CAST(SCOPE_IDENTITY() AS INT) AS Scope_IDENTITY", connection);
                            command.Parameters.AddWithValue("@Titel", title);

                            using (SqlDataReader rd = command.ExecuteReader())
                            {
                                while (rd.Read())
                                {
                                    // Added this, then it worked
                                    if (!rd.IsDBNull(0))
                                    {
                                        PicIDs[idx] = rd.GetInt32(0);
                                    }
                                }
                            }
                        }
                    }
                    idx += 1;
                }
                connection.Close();
            }
        }

        public List<string> returnAllPicNames()
        {
            PictureNames = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT Titel FROM Bilder", connection);

                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        // Added this, then it worked
                        if (!rd.IsDBNull(0))
                        {
                            PictureNames.Add(rd.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return PictureNames;
        }

        public List<string> AllEXIFInfoFromOnePic(string title)
        {
            EXIF = new List<string>();
            int fk_EXIF = -1;

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT fk_EXIF_ID FROM Bilder WHERE Titel = @title", connection);
                command.Parameters.AddWithValue("@title", title);

                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        // Added this, then it worked
                        if (!rd.IsDBNull(0))
                        {
                            fk_EXIF = rd.GetInt32(0);
                        }
                    }
                }
                //Console.WriteLine(fk_EXIF);

                command = new SqlCommand("SELECT Kameramodell, Auflösung, Datum, Ort, Land FROM EXIF WHERE ID_EXIF = @id", connection);
                command.Parameters.AddWithValue("@id", fk_EXIF);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        // Added this, then it worked
                        if (!rd.IsDBNull(0))
                        {
                            EXIF.Add(rd.GetString(0));
                        }
                        if (!rd.IsDBNull(1))
                        {
                            EXIF.Add(rd.GetString(1));
                        }
                        if(!rd.IsDBNull(2))
                        {
                            EXIF.Add(rd.GetDateTime(2).ToString());
                        }
                        if (!rd.IsDBNull(3))
                        {
                            EXIF.Add(rd.GetString(3));
                        }
                        if(!rd.IsDBNull(4))
                        {
                            EXIF.Add(rd.GetString(4));
                        }
                    }
                }
                connection.Close();
            }
            return EXIF;
        }

        public List<string> AllIPTCInfoFromOnePic(string title)
        {
            IPTC = new List<string>();
            int fk_IPTC = -1;
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT fk_ITPC_ID FROM Bilder WHERE Titel = @title", connection);
                command.Parameters.AddWithValue("@title", title);

                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        // Added this, then it worked
                        if (!rd.IsDBNull(0))
                        {
                            fk_IPTC = rd.GetInt32(0);
                        }
                    }
                }
                //Console.WriteLine(fk_EXIF);

                command = new SqlCommand("SELECT titel, Urheber, Beschreibung FROM ITPC WHERE ID_ITPC = @id", connection);
                command.Parameters.AddWithValue("@id", fk_IPTC);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        // Added this, then it worked
                        if (!rd.IsDBNull(0))
                        {
                            IPTC.Add(rd.GetString(0));
                        }
                        if (!rd.IsDBNull(1))
                        {
                            IPTC.Add(rd.GetString(1));
                        }
                        else
                        {
                            IPTC.Add("");
                        }
                        if (!rd.IsDBNull(2))
                        {
                            IPTC.Add(rd.GetString(2));
                        }
                        else
                        {
                            IPTC.Add("");
                        }
                    }
                }
                connection.Close();
            }
            return IPTC;
        }

        public void InsertAllEXIFData()
        { 
            int idx = 0;
            string title = "";
            int ID = -1;

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                foreach (var image in System.IO.Directory.GetFiles(PicFolderPath))
                {
                    string[] segments = image.Split("\\");
                    
                    foreach (string segment in segments)
                    {
                        if (segment.Contains("."))
                        {
                            title = segment;
                        }
                    }
                    
                    RandomDateTime date = new RandomDateTime();
                    SelectRandomValues rand = new SelectRandomValues();

                    string[] RandomOrtLand = rand.GetRandomCityAndCountry();

                    command = new SqlCommand("INSERT INTO EXIF(Kameramodell, Auflösung, Datum, Ort, Land) VALUES(@Kameramodell, @Auflösung, @Datum, @Ort, @Land)" + "SELECT CAST(SCOPE_IDENTITY() AS INT) AS Scope_IDENTITY", connection);

                    command.Parameters.AddWithValue("@Kameramodell", rand.GetRandomCamera());
                    command.Parameters.AddWithValue("@Auflösung", "240x240");
                    command.Parameters.AddWithValue("@Datum", date.Next());
                    command.Parameters.AddWithValue("@Ort", RandomOrtLand[0]);
                    command.Parameters.AddWithValue("@Land", RandomOrtLand[1]);

                    //var ID = command.ExecuteScalar();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            // Added this, then it worked
                            if (!rd.IsDBNull(0))
                            {
                                ID = rd.GetInt32(0);
                            }
                        }
                    }

                    command = new SqlCommand("UPDATE [Bilder] SET fk_EXIF_ID = @fk_EXIF_ID WHERE ID_Bild = @ID_Bild", connection);
                    command.Parameters.AddWithValue("@fk_EXIF_ID", ID);
                    command.Parameters.AddWithValue("@ID_Bild", PicIDs[idx]);

                    command.ExecuteNonQuery();
                    idx += 1;
                }
                connection.Close();
            }
        }

        public void InsertAllIPTCData()
        {
            int idx = 0;
            string title = "";
            int ID = -1;

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();

                foreach (var image in System.IO.Directory.GetFiles(PicFolderPath))
                {
                    string[] segments = image.Split("\\");

                    foreach (string segment in segments)
                    {
                        if (segment.Contains("."))
                        {
                            title = segment;
                        }
                    }
                    command = new SqlCommand("INSERT INTO ITPC(Titel) VALUES(@Titel)" + "SELECT CAST(SCOPE_IDENTITY() AS INT) AS Scope_IDENTITY", connection);
                    command.Parameters.AddWithValue("@Titel", title);

                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            // Added this, then it worked
                            if (!rd.IsDBNull(0))
                            {
                                ID = rd.GetInt32(0);
                            }
                        }
                    }

                    command = new SqlCommand("UPDATE Bilder SET fk_ITPC_ID = @fk_ITPC_ID WHERE ID_Bild = @ID_Bild", connection);
                    command.Parameters.AddWithValue("@fk_ITPC_ID", ID);
                    command.Parameters.AddWithValue("@ID_Bild", PicIDs[idx]);

                    command.ExecuteNonQuery();

                    idx += 1;
                }
                connection.Close();
            }
        }

        public void DeletePicture(string titel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("DELETE FROM [Bilder] WHERE [Titel] IS @Titel ", connection);
                command.Parameters.AddWithValue("@Titel", titel);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AddPhotographer(string Vorname, string Nachname, DateTime Geburtsdatum, string Notizen)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("INSERT INTO PhotografInnen(Vorname, Nachname, Geburtsdatum, Notiz) VALUES(@Vorname, @Nachname, @Geburtsdatum, @Notiz)", connection);
                command.Parameters.AddWithValue("@Vorname", Vorname);
                command.Parameters.AddWithValue("@Nachname", Nachname);
                command.Parameters.AddWithValue("@Geburtsdatum", Geburtsdatum);
                command.Parameters.AddWithValue("@Notiz", Notizen);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditPhotagrapher()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }

        public void DeletePhotographer(string Vorname, string Nachname)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("DELETE FROM [FotografInnen] WHERE [Vorname] IS @Vorname AND [Nachname] IS @Nachname", connection);
                command.Parameters.AddWithValue("@Vorname", Vorname);
                command.Parameters.AddWithValue("@Nachname", Nachname);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Dictionary<int, List<string>> GetAllPhotographers()
        {
            AllPhotographers = new Dictionary<int, List<string>>();
            List<string> helper = new List<string>();
            int helperID = -1;

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT * FROM FotografInnen", connection);

                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            helperID = rd.GetInt32(0);
                        }
                        if (!rd.IsDBNull(1))
                        {
                            helper.Add(rd.GetString(1));
                        }
                        else
                        {
                            helper.Add("");
                        }
                        if (!rd.IsDBNull(2))
                        {
                            helper.Add(rd.GetString(2));
                        }
                        if (!rd.IsDBNull(3))
                        {
                            helper.Add(rd.GetDateTime(3).ToString());
                        }
                        if (!rd.IsDBNull(4))
                        {
                            helper.Add(rd.GetString(4));
                        }
                        else
                        {
                            helper.Add("");
                        }
                    }
                    AllPhotographers.Add(helperID, helper);
                }
                connection.Close();
            }
            return AllPhotographers;
        }

        public void EditEXIF()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }

        public void EditIPTC()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }

        public void AddTagsToPic(string PicTitle, string Tag)
        {
            int tagID = -1;
            int picID = -1;
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT ID_Tag FROM Tags WHERE Bezeichnung IS @bezeichnung", connection);
                command.Parameters.AddWithValue("@bezeichnung", Tag);

                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            tagID = rd.GetInt32(0);
                        } else
                        {
                            command = new SqlCommand("INSERT INTO Tags (Bezeichnung) VALUES (@bezeichnung)" + "SELECT CAST(SCOPE_IDENTITY() AS INT) AS Scope_IDENTITY", connection);
                            command.Parameters.AddWithValue("@bezeichnung", Tag);
                            using(SqlDataReader r = command.ExecuteReader())
                            {
                                if (!r.IsDBNull(0))
                                {
                                    tagID = r.GetInt32(0);
                                }
                            }
                        }
                    }
                }

                command = new SqlCommand("SELECT ID_Bild FROM Bilder WHERE Titel IS @titel");
                command.Parameters.AddWithValue("@titel", PicTitle);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    if (!rd.IsDBNull(0))
                    {
                        picID = rd.GetInt32(0);
                    }
                }

                command = new SqlCommand("INSERT INTO Bild_Tag (fk_Bild_ID, fk_Tag_ID) VALUES (@fk_Bild_ID, @fk_Tag_ID)", connection);
                command.Parameters.AddWithValue("@fk_Bild_ID", picID);
                command.Parameters.AddWithValue("@fk_Tag_ID", tagID);
            }
        }

        public void EditTags()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }

        public void DeleteTags()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }

        public void AssignPhotographertoPicture()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }

        public List<string> ListPicturesOfPhotographer(string Vorname, string Nachname)
        {
            int photographerID = -1;
            List<string> PicturesOfPhotographers = new List<string>(); ;
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT ID_FotografIn FROM FotografInnen WHERE Vorname IS @vorname AND Nachname IS @nachname", connection);
                command.Parameters.AddWithValue("@vorname", Vorname);
                command.Parameters.AddWithValue("@nachname", Nachname);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    if (!rd.IsDBNull(0))
                    {
                        photographerID = rd.GetInt32(0);
                    }
                }

                if(photographerID != -1)
                {
                    command = new SqlCommand("SELECT Titel FROM Bilder WHERE fk_FotografIn_ID IS @fk_FotografIn_ID");
                    command.Parameters.AddWithValue("@fk_FotografIn_ID", photographerID);
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            PicturesOfPhotographers.Add(rd.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return PicturesOfPhotographers;
        }

        public void SearchForPicturesWithTag(string Tag)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }

        public void SearchForPicturesWithEXIFinfo(string exif_info)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }

        public void SearchForPicturesWithIPTCinfo(string itpc_info)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");
                
            }
        }
    }

    class RandomDateTime
    {
        DateTime start;
        Random gen;
        int range;

        public RandomDateTime()
        {
            start = new DateTime(1995, 1, 1);
            gen = new Random();
            range = (DateTime.Today - start).Days;
        }

        public DateTime Next()
        {
            return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }
    }

    // Source: https://stackoverflow.com/questions/194863/random-date-in-c-sharp
    class SelectRandomValues
    {
        private List<string> Kameramodelle = new List<string> { "Canon EOS 650", "Sony Alpha 58", "Nikon D3200", "Canon T80", "Sony A7R IV", "Sony Alpha 350", "Nikon Z 6", "Sony Mavica", "Leica S", "Panasonic Lumix DC-GH5", "Canon EOS 200D" };
        private Dictionary<string, string> Ort_Land = new Dictionary<string, string> {
            { "Wien", "Österreich" },
            { "Amstetten", "Österreich" },
            { "Berlin", "Deutschland" },
            { "London", "England" },
            { "Hamburg", "Deutschland" },
            { "St. Pölten", "Österreich" },
            { "Venedig", "Italien" },
            { "Köln", "Deutschland" },
            { "Paris", "Frankreich" },
        };
        int index;
        string Kameraname;
        string RandomKey;
        string RandomValue;
        Random random;
        List<string> keyList;
        private string[] Ort_Land_gew = { "" };

        public SelectRandomValues()
        {
            random = new Random();
        }
        
        public string GetRandomCamera()
        {
            index = random.Next(Kameramodelle.Count);
            Kameraname = Kameramodelle[index];

            return Kameraname;
        }

        public string[] GetRandomCityAndCountry()
        {
            string[] pair = new string[2];
            keyList = new List<string>(Ort_Land.Keys);
            RandomKey = keyList[random.Next(keyList.Count)];
            Ort_Land.TryGetValue(RandomKey, out RandomValue);
            //Console.WriteLine(RandomKey);
            //Console.WriteLine(RandomValue);
            pair.SetValue(RandomKey, 0);
            pair.SetValue(RandomValue, 1);
            //Console.WriteLine(pair[0]);
            //Console.WriteLine(pair[1]);

            return pair;
        }
    }
}
