using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using MetadataExtractor;
using SWE2_Projekt.Models;
using System.Linq;

namespace SWE2_Projekt
{
    public class DataAccessLayer : IDataAccessLayer
    {
        //PictureModel PictureModel = new PictureModel();
        private static string configfile = Path.GetFullPath("../../../config.txt");
        private static string PicFolderPath = Path.GetFullPath("../../../images");
        private string _connectionstring;
        private SqlCommand command;
        private int[] PicIDs = new int[30];
        List<PictureModel> PictureModelList;
        List<PhotographerModel> PhotographerModelList;
        List<IPTCModel> IPTCModelList;
        Dictionary<int, List<string>> EXIF;
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
                        if (segment.ToLower().Contains(".png") || segment.ToLower().Contains(".jpg") || segment.ToLower().Contains(".jpeg"))
                        {
                            title = segment;

                            command = new SqlCommand("INSERT INTO Bilder(Titel) VALUES(@Titel)" + "SELECT CAST(SCOPE_IDENTITY() AS INT) AS Scope_IDENTITY", connection);
                            command.Parameters.AddWithValue("@Titel", title);

                            using (SqlDataReader rd = command.ExecuteReader())
                            {
                                while (rd.Read())
                                {
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

        public List<PictureModel> ReturnAllPictureModels()
        {
            PictureModelList = new List<PictureModel>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                int ID = 0;
                string Title = "";
                int Photographer = 0;
                int EXIF = 0;
                int IPTC = 0;

                command = new SqlCommand("SELECT * FROM Bilder", connection);

                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            ID = rd.GetInt32(0);
                        }
                        if (!rd.IsDBNull(1))
                        {
                            Title = rd.GetString(1);
                        }
                        if (!rd.IsDBNull(2))
                        {
                            Photographer = rd.GetInt32(2);
                        }
                        if (!rd.IsDBNull(3))
                        {
                            EXIF = rd.GetInt32(3);
                        }
                        if (!rd.IsDBNull(4))
                        {
                            IPTC = rd.GetInt32(4);
                        }

                        PictureModel auxModel = new PictureModel(ID, Title, Photographer, EXIF, IPTC);
                        PictureModelList.Add(auxModel);
                    }
                }
                connection.Close();
            }
            return PictureModelList;
        }

        public List<PhotographerModel> ReturnAllPhotographerModels()
        {
            PhotographerModelList = new List<PhotographerModel>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                int ID = 0;
                string FirstName = "";
                string LastName = "";
                string Birthday = "";
                string Note = "";

                command = new SqlCommand("SELECT * FROM FotografInnen", connection);

                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            ID = rd.GetInt32(0);
                        }
                        if (!rd.IsDBNull(1))
                        {
                            FirstName = rd.GetString(1);
                        }
                        if (!rd.IsDBNull(2))
                        {
                            LastName = rd.GetString(2);
                        }
                        if (!rd.IsDBNull(3))
                        {
                            Birthday = Convert.ToString(rd.GetDateTime(3));
                        }
                        if (!rd.IsDBNull(4))
                        {
                            Note = rd.GetString(4);
                        }

                        PhotographerModel auxModel = new PhotographerModel(ID, FirstName, LastName, Birthday, Note);
                        PhotographerModelList.Add(auxModel);
                    }
                }
                connection.Close();
            }
            return PhotographerModelList;
        }

        public EXIFModel GetEXIFInfoByID(int id)
        {
            string Camera = "";
            string Resolution = "";
            string Date = "";
            string Place = "";
            string Country = "";
    
            EXIFModel auxEXIFModel = null;

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT * FROM EXIF WHERE ID_EXIF = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        // skipping rd.GetInt32(0) because that's the ID which we already have stored
                        if (!rd.IsDBNull(1))
                        {
                            Camera = rd.GetString(1);
                        }
                        if (!rd.IsDBNull(2))
                        {
                            Resolution = rd.GetString(2);
                        }
                        if (!rd.IsDBNull(3))
                        {
                            Date = Convert.ToString(rd.GetDateTime(3));
                        }
                        if (!rd.IsDBNull(4))
                        {
                            Place = rd.GetString(4);
                        }
                        if (!rd.IsDBNull(5))
                        {
                            Country = rd.GetString(5);
                        }
                        auxEXIFModel = new EXIFModel(id, Camera, Resolution, Date, Place, Country);
                    }
                }
                connection.Close();
            }
            return auxEXIFModel;
        }

        public IPTCModel GetIPTCInfoByID(int id)
        {
            string Title = "";
            string Creator = "";
            string Description = "";

            IPTCModel auxIPTCModel = null;

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT * FROM ITPC WHERE ID_ITPC = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        // skipping rd.GetInt32(0) because that's the ID which we already have stored
                        if (!rd.IsDBNull(1))
                        {
                            Title = rd.GetString(1);
                        }
                        if (!rd.IsDBNull(2))
                        {
                            Creator = rd.GetString(2);
                        }
                        if (!rd.IsDBNull(3))
                        {
                            Description = rd.GetString(3);
                        }

                        auxIPTCModel = new IPTCModel(id, Title, Creator, Description);
                    }
                }
                connection.Close();
            }
            return auxIPTCModel;
        }

        public Dictionary<int, List<string>> AllEXIFInfoFromOnePicture(string title)
        {
            EXIF = new Dictionary<int, List<string>>();
            List<string> helper = new List<string>();
            int fk_EXIF = -1;
            int EXIFID = -1;

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
                        if (!rd.IsDBNull(0))
                        {
                            fk_EXIF = rd.GetInt32(0);
                        }
                    }
                }

                command = new SqlCommand("SELECT * FROM EXIF WHERE ID_EXIF = @id", connection);
                command.Parameters.AddWithValue("@id", fk_EXIF);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            EXIFID = rd.GetInt32(0);
                        }
                        if (!rd.IsDBNull(1))
                        {
                            helper.Add(rd.GetString(1));
                        }
                        if (!rd.IsDBNull(2))
                        {
                            helper.Add(rd.GetString(2));
                        }
                        if (!rd.IsDBNull(3))
                        {
                            helper.Add(Convert.ToString(rd.GetDateTime(3)));
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
                    EXIF.Add(EXIFID, helper);
                }
                connection.Close();
            }
            return EXIF;
        }

        public List<IPTCModel> ReturnAllIPTCModels()
        {
            IPTCModelList = new List<IPTCModel>();
            int ID = 0;
            string Title = "";
            string Creator = "";
            string Description = "";

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT * FROM ITPC", connection);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        // Added this, then it worked
                        if (!rd.IsDBNull(0))
                        {
                            ID = rd.GetInt32(0);
                        }
                        if (!rd.IsDBNull(1))
                        {
                            Title = rd.GetString(1);
                        }
                        if (!rd.IsDBNull(2))
                        {
                            Creator = rd.GetString(2);
                        }
                        if (!rd.IsDBNull(3))
                        {
                            Description = rd.GetString(3);
                        }
                        IPTCModel auxModel = new IPTCModel(ID, Title, Creator, Description);
                        IPTCModelList.Add(auxModel);
                    }
                    
                }
                connection.Close();
            }
            return IPTCModelList;
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
                        if (segment.ToLower().Contains(".png") || segment.ToLower().Contains(".jpg") || segment.ToLower().Contains(".jpeg"))
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

                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
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
            string creator = "Unbekannt";
            string description = "Nicht gegeben";
            int ID = -1;

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();

                foreach (var image in System.IO.Directory.GetFiles(PicFolderPath))
                {
                    string[] segments = image.Split("\\");

                    foreach (string segment in segments)
                    {
                        if (segment.ToLower().Contains(".png") || segment.ToLower().Contains(".jpg") || segment.ToLower().Contains(".jpeg"))
                        {
                            title = segment;
                        }
                    }
                    command = new SqlCommand("INSERT INTO ITPC(Titel,Urheber,Beschreibung) VALUES(@Titel,@Creator,@Description)" + "SELECT CAST(SCOPE_IDENTITY() AS INT) AS Scope_IDENTITY", connection);
                    command.Parameters.AddWithValue("@Titel", title);
                    command.Parameters.AddWithValue("@Creator", creator);
                    command.Parameters.AddWithValue("@Description", description);

                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
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

                command = new SqlCommand("DELETE FROM [Bilder] WHERE [Titel] IS @Titel", connection);
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

                command = new SqlCommand("INSERT INTO FotografInnen(Vorname, Nachname, Geburtsdatum, Notiz) VALUES(@Vorname, @Nachname, @Geburtsdatum, @Notiz)", connection);
                command.Parameters.AddWithValue("@Vorname", Vorname);
                command.Parameters.AddWithValue("@Nachname", Nachname);
                command.Parameters.AddWithValue("@Geburtsdatum", Geburtsdatum);
                command.Parameters.AddWithValue("@Notiz", Notizen);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditPhotographer(int ID, List<string> Data)
        {
            string[] data = Data.ToArray();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("UPDATE FotografInnen SET Vorname=@Vorname, Nachname=@Nachname, Geburtsdatum=@Geburtsdatum, Notiz=@Notiz WHERE ID_FotografIn = @ID_FotografIn", connection);
                command.Parameters.AddWithValue("@Vorname", data[0]);
                command.Parameters.AddWithValue("@Nachname", data[1]);
                command.Parameters.AddWithValue("@Geburtsdatum", Convert.ToDateTime(data[2]));
                command.Parameters.AddWithValue("@Notiz", data[3]);
                command.Parameters.AddWithValue("@ID_FotografIn", ID);

                command.ExecuteNonQuery();
                connection.Close();
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

        public void EditEXIF(int ID, List<string> Data)
        {
            string[] data = Data.ToArray();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("INSERT INTO EXIF (Kameramodell, Auflösung, Datum, Ort, Land) VALUES (@Kameramodell, @Auflösung, @Datum, @Ort, @Land) WHERE ID_EXIF IS @ID_EXIF", connection);
                command.Parameters.AddWithValue("@Kameramodell", data[0]);
                command.Parameters.AddWithValue("@Auflösung", data[1]);
                command.Parameters.AddWithValue("@Datum", Convert.ToDateTime(data[2]));
                command.Parameters.AddWithValue("@Ort", data[3]);
                command.Parameters.AddWithValue("@Land", data[4]);
                command.Parameters.AddWithValue("@ID_EXIF", ID);

                command.ExecuteNonQuery();
                
                connection.Close();
            }
        }

        public void EditIPTC(int ID, List<string> Data)
        {
            string[] data = Data.ToArray();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("UPDATE ITPC SET Titel=@titel, Urheber=@Urheber, Beschreibung=@Beschreibung WHERE ID_ITPC = @ID_ITPC", connection);
                command.Parameters.AddWithValue("@titel", data[0]);
                command.Parameters.AddWithValue("@Urheber", data[1]);
                command.Parameters.AddWithValue("@Beschreibung", data[2]);
                command.Parameters.AddWithValue("@ID_ITPC", ID);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void AddTagToPicture(string PicTitle, string Tag)
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

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public Dictionary<string, int> getAllTagsWithPicCount()
        {
            Dictionary<string, int> allTags = new Dictionary<string, int>();
            List<string> tags = new List<string>();
            SqlCommand com;
            string tag = "";
            int id_tag = 0;
            int id_pic = 0;
            int count = 0;

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT fk_Tag_ID FROM Bild_Tag", connection);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            id_tag = rd.GetInt32(0);
                            com = new SqlCommand("SELECT Bezeichnung FROM Tags WHERE ID_Tag IS @ID_Tag", connection);
                            com.Parameters.AddWithValue("@ID_Tag", id_tag);

                            using (SqlDataReader r = command.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    if (!r.IsDBNull(0))
                                    {
                                        tag = r.GetString(0);
                                    }
                                }
                            }
                            tags.Add(tag);
                        }
                    }
                }

                var g = tags.GroupBy(i => i.ToString());
                foreach (var grp in g)
                {
                    if (!allTags.ContainsKey(grp.Key))
                    {
                        allTags.Add(grp.Key, grp.Count());
                    }
                    //Console.WriteLine("{0} {1}", grp.Key, grp.Count());
                }
            }
            return allTags;
        }

        public void DeleteTagofPicture(string PicTitle, string TagTitle)
        {
            int PicID = -1;
            int TagID = -1;
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT ID_Bild FROM Bilder WHERE Titel IS @Titel", connection);
                command.Parameters.AddWithValue("@Titel", PicTitle);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            PicID = rd.GetInt32(0);
                        }
                    }  
                }

                command = new SqlCommand("SELECT ID_Tag FROM Tags WHERE Bezeichnung IS @Bezeichnung", connection);
                command.Parameters.AddWithValue("@Bezeichnung", TagTitle);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            TagID = rd.GetInt32(0);
                        }
                    } 
                }

                command = new SqlCommand("DELETE FROM Bild_Tag WHERE fk_Bild_ID IS @fk_Bild_ID AND fk_Tag_ID IS @fk_Tag_ID", connection);
                command.Parameters.AddWithValue("@fk_Bild_ID", PicID);
                command.Parameters.AddWithValue("@fk_Tag_ID", TagID);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void AssignPhotographertoPicture(int PhotographerID, string Title)
        {
            int PicID = -1;
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT ID_Bild FROM Bilder WHERE Titel IS @Titel", connection);
                command.Parameters.AddWithValue("@Titel", Title);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            PicID = rd.GetInt32(0);
                        }
                    }
                }

                command = new SqlCommand("UPDATE Bilder SET fk_FotografIn_ID IS @fk_FotografIn_ID WHERE ID_Bild IS @ID_Bild", connection);
                command.Parameters.AddWithValue("@fk_FotografIn_ID", PhotographerID);
                command.Parameters.AddWithValue("@ID_Bild", PicID);
                
                connection.Close();
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
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            photographerID = rd.GetInt32(0);
                        }
                    }
                }

                if(photographerID != -1)
                {
                    command = new SqlCommand("SELECT Titel FROM Bilder WHERE fk_FotografIn_ID IS @fk_FotografIn_ID");
                    command.Parameters.AddWithValue("@fk_FotografIn_ID", photographerID);
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (!rd.IsDBNull(0))
                            {
                                PicturesOfPhotographers.Add(rd.GetString(0));
                            }
                        }
                    }
                }
                connection.Close();
            }
            return PicturesOfPhotographers;
        }

        public List<string> SearchForPicturesWithTag(string Tag)
        {
            int TagID = -1;
            List<int> PicIDList = new List<int>(); 
            var Pictures = new List<string>();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT ID_Tag FROM Tags WHERE Bezeichnung IS @Tag", connection);
                command.Parameters.AddWithValue("@Tag", Tag);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            TagID = rd.GetInt32(0);
                        }
                    }
                }

                command = new SqlCommand("SELECT fk_Bild_ID WHERE fk_Tag_ID IS @fk_Tag_ID", connection);
                command.Parameters.AddWithValue("@fk_Tag_ID", TagID);
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            PicIDList.Add(rd.GetInt32(0));
                        }
                    }
                }

                foreach(int ID in PicIDList)
                {
                    command = new SqlCommand("SELECT Titel FROM Bilder WHERE ID_Bild IS @ID_Bild", connection);
                    command.Parameters.AddWithValue("@ID_Bild", ID);
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (!rd.IsDBNull(0))
                            {
                                Pictures.Add(rd.GetString(0));
                            }
                        }
                    }
                }
                
                connection.Close();
            }
            return Pictures;
        }

        public List<PictureModel> SearchForPictures(string value)
        {
            value.ToLower();

            string FullName = "";
            List<int> PicIDList = new List<int>();
            List<int> PhotographerIDList = new List<int>();
            List<int> EXIFIDList = new List<int>();
            List<int> IPTCIDList = new List<int>();
            Dictionary<int, List<string>> AllData = new Dictionary<int, List<string>>();
            var Pictures = new List<PictureModel>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                Console.WriteLine("Opening PicDB Connection!");
                connection.Open();
                Console.WriteLine("Connected to PicDB!\n");

                command = new SqlCommand("SELECT ID_Bild, Titel FROM Bilder", connection); //Bilder
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0))
                        {
                            if (rd.GetString(1).ToLower().Contains(value))
                            {
                                PicIDList.Add(rd.GetInt32(0));
                            }
                        }
                    }
                }

                command = new SqlCommand("SELECT * FROM FotografInnen", connection); //FotografIn
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(1) || !rd.IsDBNull(2))
                        {
                            FullName = rd.GetString(1) + ' ';
                            FullName = rd.GetString(2);
                            FullName.ToLower();

                            if (FullName.Contains(value))
                            {
                                PhotographerIDList.Add(rd.GetInt32(0));
                            }
                        }

                        if (!rd.IsDBNull(3))
                        {
                            string date = rd.GetDateTime(3).ToString();
                            if (date.Contains(value))
                            {
                                PhotographerIDList.Add(rd.GetInt32(0));
                            }
                        }

                        if (!rd.IsDBNull(4))
                        {
                            if (rd.GetString(4).ToLower().Contains(value))
                            {
                                PhotographerIDList.Add(rd.GetInt32(0));
                            }
                        }
                    }
                }

                command = new SqlCommand("SELECT * FROM ITPC", connection); //IPTC
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(1))
                        {
                            if (rd.GetString(1).ToLower().Contains(value))
                            {
                                IPTCIDList.Add(rd.GetInt32(0));
                            }
                        }
                        if (!rd.IsDBNull(2))
                        {
                            if (rd.GetString(2).ToLower().Contains(value))
                            {
                                IPTCIDList.Add(rd.GetInt32(0));
                            }
                        }
                        if (!rd.IsDBNull(3))
                        {
                            if (rd.GetString(3).ToLower().Contains(value))
                            {
                                IPTCIDList.Add(rd.GetInt32(0));
                            }
                        }
                    }
                }

                command = new SqlCommand("SELECT * FROM EXIF", connection); //EXIF
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(1))
                        {
                            if (rd.GetString(1).ToLower().Contains(value))
                            {
                                EXIFIDList.Add(rd.GetInt32(0));
                            }
                        }
                        if (!rd.IsDBNull(2))
                        {
                            if (rd.GetString(2).ToLower().Contains(value))
                            {
                                EXIFIDList.Add(rd.GetInt32(0));
                            }
                        }
                        if (!rd.IsDBNull(3))
                        {
                            string date = rd.GetDateTime(3).ToString();
                            if (date.Contains(value))
                            {
                                EXIFIDList.Add(rd.GetInt32(0));
                            }
                        }
                        if (!rd.IsDBNull(4))
                        {
                            if (rd.GetString(4).ToLower().Contains(value))
                            {
                                EXIFIDList.Add(rd.GetInt32(0));
                            }
                        }
                        if (!rd.IsDBNull(5))
                        {
                            if (rd.GetString(5).ToLower().Contains(value))
                            {
                                EXIFIDList.Add(rd.GetInt32(0));
                            }
                        }
                    }
                }

                foreach(int ID in PicIDList)
                {
                    command = new SqlCommand("SELECT * FROM Bilder WHERE ID_Bild = @ID_Bild", connection);
                    command.Parameters.AddWithValue("@ID_Bild", ID);

                    int picID = 0;
                    string Title = "";
                    int Photographer = 0;
                    int EXIF = 0;
                    int IPTC = 0;


                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (!rd.IsDBNull(0))
                            {
                                picID = rd.GetInt32(0);
                            }
                            if (!rd.IsDBNull(1))
                            {
                                Title = rd.GetString(1);
                            }
                            if (!rd.IsDBNull(2))
                            {
                                Photographer = rd.GetInt32(2);
                            }
                            if (!rd.IsDBNull(3))
                            {
                                EXIF = rd.GetInt32(3);
                            }
                            if (!rd.IsDBNull(4))
                            {
                                IPTC = rd.GetInt32(4);
                            }

                            PictureModel auxModel = new PictureModel(ID, Title, Photographer, EXIF, IPTC);
                            Pictures.Add(auxModel);
                        }
                    }
                }

                foreach (int ID in PhotographerIDList)
                {
                    command = new SqlCommand("SELECT * FROM Bilder WHERE fk_FotografIn_ID = @fk_FotografIn_ID", connection);
                    command.Parameters.AddWithValue("@fk_FotografIn_ID", ID);

                    int picID = 0;
                    string Title = "";
                    int Photographer = 0;
                    int EXIF = 0;
                    int IPTC = 0;

                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (!rd.IsDBNull(0))
                            {
                                picID = rd.GetInt32(0);
                            }
                            if (!rd.IsDBNull(1))
                            {
                                Title = rd.GetString(1);
                            }
                            if (!rd.IsDBNull(2))
                            {
                                Photographer = rd.GetInt32(2);
                            }
                            if (!rd.IsDBNull(3))
                            {
                                EXIF = rd.GetInt32(3);
                            }
                            if (!rd.IsDBNull(4))
                            {
                                IPTC = rd.GetInt32(4);
                            }

                            PictureModel auxModel = new PictureModel(ID, Title, Photographer, EXIF, IPTC);
                            Pictures.Add(auxModel);
                        }
                    }
                }

                foreach(int ID in EXIFIDList)
                {
                    command = new SqlCommand("SELECT * FROM Bilder WHERE fk_EXIF_ID = @fk_EXIF_ID", connection);
                    command.Parameters.AddWithValue("@fk_EXIF_ID", ID);

                    int picID = 0;
                    string Title = "";
                    int Photographer = 0;
                    int EXIF = 0;
                    int IPTC = 0;

                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (!rd.IsDBNull(0))
                            {
                                picID = rd.GetInt32(0);
                            }
                            if (!rd.IsDBNull(1))
                            {
                                Title = rd.GetString(1);
                            }
                            if (!rd.IsDBNull(2))
                            {
                                Photographer = rd.GetInt32(2);
                            }
                            if (!rd.IsDBNull(3))
                            {
                                EXIF = rd.GetInt32(3);
                            }
                            if (!rd.IsDBNull(4))
                            {
                                IPTC = rd.GetInt32(4);
                            }

                            PictureModel auxModel = new PictureModel(ID, Title, Photographer, EXIF, IPTC);
                            Pictures.Add(auxModel);
                        }
                    }
                }

                foreach(int ID in IPTCIDList)
                {
                    command = new SqlCommand("SELECT * FROM Bilder WHERE fk_ITPC_ID = @fk_ITPC_ID", connection);
                    command.Parameters.AddWithValue("@fk_ITPC_ID", ID);

                    int picID = 0;
                    string Title = "";
                    int Photographer = 0;
                    int EXIF = 0;
                    int IPTC = 0;

                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (!rd.IsDBNull(0))
                            {
                                picID = rd.GetInt32(0);
                            }
                            if (!rd.IsDBNull(1))
                            {
                                Title = rd.GetString(1);
                            }
                            if (!rd.IsDBNull(2))
                            {
                                Photographer = rd.GetInt32(2);
                            }
                            if (!rd.IsDBNull(3))
                            {
                                EXIF = rd.GetInt32(3);
                            }
                            if (!rd.IsDBNull(4))
                            {
                                IPTC = rd.GetInt32(4);
                            }

                            PictureModel auxModel = new PictureModel(ID, Title, Photographer, EXIF, IPTC);
                            Pictures.Add(auxModel);
                        }
                    }
                }
                connection.Close();
            }
            return Pictures;
        }
    }

    // Source: https://stackoverflow.com/questions/194863/random-date-in-c-sharp
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

            pair.SetValue(RandomKey, 0);
            pair.SetValue(RandomValue, 1);

            return pair;
        }
    }
}
