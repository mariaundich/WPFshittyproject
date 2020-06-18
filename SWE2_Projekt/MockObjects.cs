using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SWE2_Projekt;
using SWE2_Projekt.Models;
using System.Collections.ObjectModel;

namespace SWE2_Projekt
{
    // To Do: interface für DAL und BL schreiben, für den Mock ableiten und alle
    // abgeleiteten Funktionen statt SQL Statements auf Liste mit Model-Objects ausführen
    public class MockDAL : IDataAccessLayer
    {
        public List<PictureModel> PictureList = new List<PictureModel>();
        public List<IPTCModel> IPTCList = new List<IPTCModel>();
        public List<EXIFModel> EXIFList = new List<EXIFModel>();
        public List<PhotographerModel> PhotographerList = new List<PhotographerModel>();
        public Dictionary<int, string> Tags = new Dictionary<int, string>();
        int i = 0;

        public MockDAL()
        {
            i = 1;
            PhotographerList.Clear();
            PictureList.Clear();
            IPTCList.Clear();
            EXIFList.Clear();
        }

        public PhotographerModel AddAndReturnPhotographer(string Vorname, string Nachname, string Geburtsdatum, string Notizen)
        {
            string geb = Geburtsdatum.ToString();
            PhotographerModel photographer = new PhotographerModel(i, Vorname, Nachname, geb, Notizen);
            PhotographerList.Add(photographer);
            i++;
            return photographer;
        }

        public void AddTagToPicture(int PicID, string Tag)
        {
            if (!Tags.ContainsValue(Tag))
            {
                Tags.Add(i, Tag);
            }

            int index = PictureList.FindIndex(i => i.ID == PicID);
            PictureList[index].Tags.Add(Tag);
        }

        public Dictionary<int, List<string>> AllEXIFInfoFromOnePicture(string title)
        {
            Dictionary<int, List<string>> exifofPic = new Dictionary<int, List<string>>();
            int i = 0;
            List<string> helper = new List<string>();

            foreach(PictureModel pic in PictureList)
            {
                if(pic.Title == title)
                {
                    foreach(EXIFModel exif in EXIFList)
                    {
                        if(exif.ID == pic.EXIF_ID)
                        {
                            i = exif.ID;
                            helper.Add(exif.Camera);
                            helper.Add(exif.Resolution);
                            helper.Add(exif.Date);
                            helper.Add(exif.Place);
                            helper.Add(exif.Country);
                        }
                    }
                }
            }
            exifofPic.Add(i, helper);
            helper.Clear();

            return exifofPic;
        }

        public void AssignPhotographertoPicture(int PictureID, int PhotographerID)
        {
            PhotographerModel model = new PhotographerModel(0, "", "", "", "");
            foreach(PhotographerModel photographer in PhotographerList)
            {
                if(photographer.ID == PhotographerID)
                {
                    model = photographer;
                }
            }

            foreach(PictureModel pic in PictureList)
            {
                if(pic.ID == PictureID)
                {
                    pic.Photographer = model;
                }
            }
        }

        public void DeleteAllData()
        {

            if (PictureList.Count > 0)
            {
                PictureList.Clear();
            }

            if (IPTCList.Count > 0)
            {
                IPTCList.Clear();
            }

            if (EXIFList.Count > 0)
            {
                EXIFList.Clear();
            }
        }

        public void DeletePhotographer(string Vorname, string Nachname)
        {
            string full = Vorname + " " + Nachname;
            int index = PhotographerList.FindIndex(i => i.FullName == full);

            PhotographerList.RemoveAt(index);
        }

        public void DeletePicture(string titel)
        {
            int exifId = 0;
            int iptcId = 0;

            if(PictureList.Count == 0)
            {
                this.InsertAllPictures();
            }

            int index = PictureList.FindIndex(i => i.Title == titel);
            exifId = PictureList[index].EXIF_ID;
            iptcId = PictureList[index].IPTC_ID;

            PictureList.RemoveAt(index);
            EXIFList.RemoveAt(exifId);
            IPTCList.RemoveAt(iptcId);
        }

        public void DeleteTagofPicture(string PicTitle, string TagTitle)
        {
            throw new NotImplementedException();
        }

        public void EditEXIF(int ID, List<string> Data)
        {
            if(EXIFList.Count == 0)
            {
                this.InsertAllEXIFData();
            }

            EXIFList[ID-1].Camera = Data[0];
            EXIFList[ID-1].Resolution = Data[1];
            EXIFList[ID-1].Date = Data[2];
            EXIFList[ID-1].Place = Data[3];
            EXIFList[ID-1].Country = Data[4];
        }

        public void EditIPTC(int ID, List<string> Data)
        {
            if (IPTCList.Count == 0)
            {
                this.InsertAllIPTCData();
            }

            IPTCList[ID - 1].Title = Data[0];
            IPTCList[ID - 1].Creator = Data[1];
            IPTCList[ID - 1].Description = Data[2];
        }

        public void EditPhotographer(int ID, List<string> Data)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, List<string>> GetAllPhotographers()
        {
            Dictionary<int, List<string>> results = new Dictionary<int, List<string>>();
            List<string> helper = new List<string>();
            int id = -1;

            foreach(PhotographerModel photographer in PhotographerList)
            {
                id = photographer.ID;
                helper.Add(photographer.FirstName);
                helper.Add(photographer.LastName);
                helper.Add(photographer.Birthday);
                helper.Add(photographer.Notes);

                results.Add(id, helper);
                helper.Clear();
            }

            return results;
        }

        public Dictionary<string, int> getAllTagsWithPicCount()
        {
            throw new NotImplementedException();
        }

        public EXIFModel GetEXIFInfoByID(int id)
        {
            int index = EXIFList.FindIndex(i => i.ID == id);
            return EXIFList[index];
        }

        public IPTCModel GetIPTCInfoByID(int id)
        {
            int index = IPTCList.FindIndex(i => i.ID == id);
            return IPTCList[index];
        }

        public void InsertAllEXIFData()
        {
            EXIFModel exif;
            EXIFList.Clear();

            if(PictureList.Count == 0)
            {
                this.InsertAllPictures();
            }

            exif = new EXIFModel(1, "Nikon", "240x240", "27.09.1997 04:46:35", "Wien", "Österreich");
            EXIFList.Add(exif);
            PictureList[0].EXIF_ID = 1;

            exif = new EXIFModel(2, "Sony", "240x240", "91.05.2003 04:46:35", "Berlin", "Deutschland");
            EXIFList.Add(exif);
            PictureList[1].EXIF_ID = 2;

            exif = new EXIFModel(3, "Canon", "240x240", "15.03.2010 04:46:35", "Wien", "Österreich");
            EXIFList.Add(exif);
            PictureList[2].EXIF_ID = 3;

            exif = new EXIFModel(4, "Huawei", "240x240", "01.01.2000 04:46:35", "Washington", "Amerika");
            EXIFList.Add(exif);
            PictureList[3].EXIF_ID = 4;

            exif = new EXIFModel(5, "Samsung", "240x240", "18.04.1999 04:46:35", "Wien", "Österreich");
            EXIFList.Add(exif);
            PictureList[4].EXIF_ID = 5;

            exif = new EXIFModel(6, "Nikon", "240x240", "02.12.2019 04:46:35", "Wien", "Österreich");
            EXIFList.Add(exif);
            PictureList[5].EXIF_ID = 6;
        }

        public void InsertAllIPTCData()
        {
            
            IPTCModel iptc;
            IPTCList.Clear();

            if (PictureList.Count == 0)
            {
                this.InsertAllPictures();
            }

            iptc = new IPTCModel(1, "amazingTitle", "Shrek", "");
            IPTCList.Add(iptc);
            PictureList[0].IPTC_ID = 1;

            iptc = new IPTCModel(2, "anothertitle", "Flo", "");
            IPTCList.Add(iptc);
            PictureList[1].IPTC_ID = 2;

            iptc = new IPTCModel(3, "CoolPicture", "Matschi", "");
            IPTCList.Add(iptc);
            PictureList[2].IPTC_ID = 3;

            iptc = new IPTCModel(4, "subtleTitle", "Chrissy", "");
            IPTCList.Add(iptc);
            PictureList[3].IPTC_ID = 4;

            iptc = new IPTCModel(5, "IAmBatman", "Batman", "I am Batman.");
            IPTCList.Add(iptc);
            PictureList[4].IPTC_ID = 5;

            iptc = new IPTCModel(6, "AwYissATitle", "Bob", "");
            IPTCList.Add(iptc);
            PictureList[5].IPTC_ID = 6;
        }

        public void InsertAllPictures()
        {
            PictureModel pic;
            PictureList.Clear();
            ObservableCollection<string> col = new ObservableCollection<string>();

            pic = new PictureModel(1, "amazingTitle", 0, 0, 0, col);
            PictureList.Add(pic);

            pic = new PictureModel(2, "anothertitle", 0, 0, 0, col);
            PictureList.Add(pic);

            pic = new PictureModel(3, "CoolPicture", 0, 0, 0, col);
            PictureList.Add(pic);

            pic = new PictureModel(4, "subtleTitle", 0, 0, 0, col);
            PictureList.Add(pic);

            pic = new PictureModel(5, "IAmBatman", 0, 0, 0, col);
            PictureList.Add(pic);

            pic = new PictureModel(6, "AwYissATitle", 0, 0, 0, col);
            PictureList.Add(pic);
        }

        public List<string> ListPicturesOfPhotographer(string Vorname, string Nachname)
        {
            throw new NotImplementedException();
        }

        public List<IPTCModel> ReturnAllIPTCModels()
        {
            return IPTCList;
        }

        public List<PictureModel> ReturnAllPictureModels()
        {
            return PictureList;
        }

        public List<PictureModel> SearchForPictures(string value)
        {
            List<PictureModel> results = new List<PictureModel>();

            for(int i = 0; i < PictureList.Count; i++)
            {
                if (PictureList[i].Title.Contains(value)){
                    results.Add(PictureList[i]);
                }
            }

            for (int i = 0; i < IPTCList.Count; i++)
            {
                if (IPTCList[i].Title.Contains(value))
                {
                    if (!results.Contains(PictureList[i]))
                    {
                        results.Add(PictureList[i]);
                    }
                } 
                else if (IPTCList[i].Creator.Contains(value))
                {
                    if (!results.Contains(PictureList[i]))
                    {
                        results.Add(PictureList[i]);
                    }
                } 
                else if (IPTCList[i].Creator.Contains(value))
                {
                    if (!results.Contains(PictureList[i]))
                    {
                        results.Add(PictureList[i]);
                    }
                } 
                else if (IPTCList[i].Description.Contains(value))
                {
                    if (!results.Contains(PictureList[i]))
                    {
                        results.Add(PictureList[i]);
                    }
                }
            }

            for (int i = 0; i < EXIFList.Count; i++)
            {
                if (EXIFList[i].Camera.Contains(value))
                {
                    if (!results.Contains(PictureList[i]))
                    {
                        results.Add(PictureList[i]);
                    }
                }
                else if (EXIFList[i].Resolution.Contains(value))
                {
                    if (!results.Contains(PictureList[i]))
                    {
                        results.Add(PictureList[i]);
                    }
                }
                else if (EXIFList[i].Date.Contains(value))
                {
                    if (!results.Contains(PictureList[i]))
                    {
                        results.Add(PictureList[i]);
                    }
                }
                else if (EXIFList[i].Place.Contains(value))
                {
                    if (!results.Contains(PictureList[i]))
                    {
                        results.Add(PictureList[i]);
                    }
                }
                else if (EXIFList[i].Country.Contains(value))
                {
                    if (!results.Contains(PictureList[i]))
                    {
                        results.Add(PictureList[i]);
                    }
                }
            }
            return results;
        }

        public List<string> SearchForPicturesWithTag(string Tag)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class AbstractTestFixture<T> where T : class, new()
    {

        #region Setup
        [SetUp]
        public void Setup()
        {

        }

        [OneTimeSetUp]
        public void TestSetup()
        {

        }
        #endregion

        public string WorkingDirectory
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        #region Support
        protected T CreateInstance(params object[] parameter)
        {
            return new T();
        }
        #endregion
    }
}