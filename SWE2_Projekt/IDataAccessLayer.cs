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
        /// <summary>
        /// Deletes all data from all tables in the database.
        /// </summary>
        void DeleteAllData();

        /// <summary>
        /// Insert all Picture Data in the Picture table.
        /// </summary>
        void InsertAllPictures();

        /// <summary>
        /// Adds EXIF data of all pictures in the EXIF table. Adds foreign keys to picture table. 
        /// </summary>
        void InsertAllEXIFData();

        /// <summary>
        /// Adds IPTC data of all pictures in the IPTC table. Adds foreign keys to picture table. 
        /// </summary>
        void InsertAllIPTCData();

        /// <summary>
        /// Returns a list with PictureModel objects of all pictures in the picture table.
        /// </summary>
        List<PictureModel> ReturnAllPictureModels();

        /// <summary>
        /// Returns the EXIFModel of one picture, searched for with the ID of the picture.
        /// </summary>
        /// <param name="id"></param>
        EXIFModel GetEXIFInfoByID(int id);

        /// <summary>
        /// Returns the IPTCModel of one picture, searched for with the ID of the picture.
        /// </summary>
        /// <param name="id"></param>
        IPTCModel GetIPTCInfoByID(int id);

        /// <summary>
        /// Returns all EXIF information of one picture in a dictionary.
        /// </summary>
        /// <param name="title"></param>
        Dictionary<int, List<string>> AllEXIFInfoFromOnePicture(string title);

        /// <summary>
        /// Returns a list with IPTCModel objects of all IPTC data in the IPTC table.
        /// </summary>
        List<IPTCModel> ReturnAllIPTCModels();

        /// <summary>
        /// Deletes a picture from the picture table, according to the title.
        /// </summary>
        /// <param name="titel"></param>
        void DeletePicture(string titel);

        /// <summary>
        /// Adds a Photographer to the Photographer table.
        /// </summary>
        /// <param name="Vorname"></param>
        /// <param name="Nachname"></param>
        /// <param name="Geburtsdatum"></param>
        /// <param name="Notizen"></param>
        PhotographerModel AddAndReturnPhotographer(string Vorname, string Nachname, string Geburtsdatum, string Notizen);

        /// <summary>
        /// Edits a specific photographer.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        void EditPhotographer(int ID, List<string> Data);

        /// <summary>
        /// Deletes a specific photographer.
        /// </summary>
        /// <param name="Vorname"></param>
        /// <param name="Nachname"></param>
        void DeletePhotographer(string Vorname, string Nachname);

        /// <summary>
        /// Returns all data of the Photographer table in a dictionary.
        /// </summary>
        Dictionary<int, List<string>> GetAllPhotographers();

        /// <summary>
        /// Edits a specific EXIF dataset.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        void EditEXIF(int ID, List<string> Data);

        /// <summary>
        /// Edits a specific EXIF dataset.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        void EditIPTC(int ID, List<string> Data);

        /// <summary>
        /// Adds a tag to a picture, if it does not yet exist in the tag table, it is added.
        /// </summary>
        /// <param name="PicID"></param>
        /// <param name="Tag"></param>
        void AddTagToPicture(int PicID, string Tag);

        /// <summary>
        /// Deletes a tag from a picture.
        /// </summary>
        /// <param name="PicTitle"></param>
        /// <param name="TagTitle"></param>
        void DeleteTagofPicture(string PicTitle, string TagTitle);

        /// <summary>
        /// Adds a photographer to a picture.
        /// </summary>
        /// <param name="PictureID"></param>
        /// <param name="PhotographerID"></param>
        void AssignPhotographertoPicture(int PictureID, int PhotographerID);

        /// <summary>
        /// Lists all pictures of one photographer.
        /// </summary>
        /// <param name="Vorname"></param>
        /// <param name="Nachname"></param>
        List<string> ListPicturesOfPhotographer(string Vorname, string Nachname);

        /// <summary>
        /// Searches for all pictures, which contain the given tag.
        /// </summary>
        /// <param name="Tag"></param>
        List<string> SearchForPicturesWithTag(string Tag);

        /// <summary>
        /// Searches for all pictures with the given term, in all tables.
        /// </summary>
        /// <param name="Tag"></param>
        List<PictureModel> SearchForPictures(string value);

        /// <summary>
        /// Returns all tags with their count.
        /// </summary>
        Dictionary<string, int> getAllTagsWithPicCount();

    }
}
