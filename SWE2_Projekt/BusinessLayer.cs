using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWE2_Projekt
{
    class BusinessLayer
    {
        public DataAccessLayer _DataAccessLayer;
        List<PictureModel> PictureList;
        List<IPTCModel> IPTCList;
        Dictionary<int, List<string>> EXIF;
        Dictionary<int, List<string>> AllPhotograpersInfo;

        public BusinessLayer()
        {
            _DataAccessLayer = new DataAccessLayer();
            //RefreshPictureData();
        }

        public void RefreshPictureData()
        {
            _DataAccessLayer.DeleteAllData();
            _DataAccessLayer.InsertAllPictures();
            _DataAccessLayer.InsertAllEXIFData();
            _DataAccessLayer.InsertAllIPTCData();
        }

        public List<PictureModel> AllPictureModels()
        {
            PictureList = new List<PictureModel>();
            PictureList = _DataAccessLayer.ReturnAllPictureModels();
            return PictureList;
        }

        public Dictionary<int, List<string>> AllEXIFInfoForOnePicture(string name)
        {
            EXIF = new Dictionary<int, List<string>>();
            EXIF = _DataAccessLayer.AllEXIFInfoFromOnePicture(name);
            return EXIF;
        }

        public List<IPTCModel> AllIPTCModels()
        {
            IPTCList = new List<IPTCModel>();
            IPTCList = _DataAccessLayer.ReturnAllIPTCModels();
            return IPTCList;
        }

        public Dictionary<int, List<string>> GetAllPhotographersInfo()
        {
            AllPhotograpersInfo = new Dictionary<int, List<string>>();
            AllPhotograpersInfo = _DataAccessLayer.GetAllPhotographers();
            return AllPhotograpersInfo;
        }

        public List<string> SearchAllPictures(string search)
        {
            return _DataAccessLayer.SearchForPictures(search);
        }
    }
}
