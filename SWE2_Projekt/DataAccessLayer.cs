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
        private decimal[] PicIDs = new decimal[20];

        public DataAccessLayer()
        {
            var file = File.ReadAllText(configfile, Encoding.UTF8);
            _connectionstring = file.ToString();
            //Console.WriteLine(_connectionstring);
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
                            
                            command = new SqlCommand("INSERT INTO Bilder(Titel) VALUES(@Titel)" + "Select Scope_Identity()", connection);

                            //command.Parameters.AddWithValue("@ID_Bild", id);
                            command.Parameters.AddWithValue("@Titel", title);

                            var PicID = command.ExecuteScalar();
                            PicIDs.SetValue(PicID, idx);
                            //Console.WriteLine(PicID);
                        }
                    }
                    idx += 1;
                }
                connection.Close();
            }
        }

        public void InsertAllEXIFData()
        { 
            int idx = 0;
            string title = "";

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
                            //Console.WriteLine(title); 
                        }
                    }
                    
                    RandomDateTime date = new RandomDateTime();
                    SelectRandomValues rand = new SelectRandomValues();

                    string[] RandomOrtLand = rand.GetRandomCityAndCountry();

                    command = new SqlCommand("INSERT INTO EXIF(Kameramodell, Auflösung, Datum, Ort, Land) VALUES(@Kameramodell, @Auflösung, @Datum, @Ort, @Land)" + "Select Scope_Identity()", connection);

                    //command.Parameters.AddWithValue("@ID_EXIF", id);
                    command.Parameters.AddWithValue("@Kameramodell", rand.GetRandomCamera());
                    command.Parameters.AddWithValue("@Auflösung", "240x240");
                    command.Parameters.AddWithValue("@Datum", date.Next());
                    command.Parameters.AddWithValue("@Ort", RandomOrtLand[0]);
                    command.Parameters.AddWithValue("@Land", RandomOrtLand[1]);

                    var ID = command.ExecuteScalar();
                    Console.WriteLine(ID);

                    command = new SqlCommand("UPATE [Bilder] SET fk_EXIF_ID = @fk_EXIF_ID WHERE [ID_Bild] IS @ID_Bild", connection);
                    command.Parameters.AddWithValue("@fk_EXIF_ID", Convert.ToInt32(ID));
                    command.Parameters.AddWithValue("@ID_Bild", Convert.ToInt32(PicIDs[idx]));

                    command.ExecuteNonQuery();

                    idx += 1;
                }
                connection.Close();
            }
        }

        public void InsertAllITPCData()
        {
            int idx = 0;
            string title = "";

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

                    command = new SqlCommand("INSERT INTO ITPC(Titel) VALUES(@Titel)" + "Select Scope_Identity()", connection);
                    command.Parameters.AddWithValue("@Titel", title);

                    var ID = command.ExecuteScalar();
                    //Console.WriteLine(ID);

                    command = new SqlCommand("UPATE Bilder SET fk_ITPC_ID = @fk_ITPC_ID WHERE ID_Bild = @ID_Bild", connection);
                    command.Parameters.AddWithValue("@fk_ITPC_ID", Convert.ToInt32(ID));
                    command.Parameters.AddWithValue("@ID_Bild", Convert.ToInt32(PicIDs[idx]));

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

        public void DeletePhotographer()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }

        public void ListPhotagraphers()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
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

        public void EditITPC()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
        }
        public void AddTags()
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

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

        public void ListPicturesOfPhotographer(string Photographer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

            }
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

        public void SearchForPicturesWithITPCinfo(string itpc_info)
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
