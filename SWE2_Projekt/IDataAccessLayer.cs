using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace SWE2_Projekt
{
    public interface IDataAccessLayer
    {
        void DeleteAllData();

        void InsertAllPictures();

        void InsertAllEXIFData();

        void InsertAllIPTCData();

        List<PictureModel> ReturnAllPictureModels();

        EXIFModel GetEXIFInfoByID(int id);

        IPTCModel GetIPTCInfoByID(int id);

        Dictionary<int, List<string>> AllEXIFInfoFromOnePicture(string title);

        List<IPTCModel> ReturnAllIPTCModels();

        void DeletePicture(string titel);

        void AddPhotographer(string Vorname, string Nachname, DateTime Geburtsdatum, string Notizen);

        void EditPhotagrapher(int ID, List<string> Data);

        void DeletePhotographer(string Vorname, string Nachname);

        Dictionary<int, List<string>> GetAllPhotographers();

        void EditEXIF(int ID, List<string> Data);

        void EditIPTC(int ID, List<string> Data);

        void AddTagToPicture(string PicTitle, string Tag);

        void DeleteTagofPicture(string PicTitle, string TagTitle);

        void AssignPhotographertoPicture(int PhotographerID, string Title);

        List<string> ListPicturesOfPhotographer(string Vorname, string Nachname);

        List<string> SearchForPicturesWithTag(string Tag);

        List<string> SearchForPictures(string value);

    }
}
