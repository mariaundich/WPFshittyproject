using System;
using System.Collections.Generic;
using System.Text;

namespace SWE2_Projekt
{
    class BusinessLayer
    {
        public DataAccessLayer _DataAccessLayer;
        List<string> PictureList;
        List<string> EXIF;
        List<string> IPTC;
        Dictionary<int, List<string>> AllPhotograpersInfo;

        public BusinessLayer()
        {
            _DataAccessLayer = new DataAccessLayer();
            //_DataAccessLayer.DeleteAllData();
            //_DataAccessLayer.InsertAllPictures();
            //_DataAccessLayer.InsertAllEXIFData();
            //_DataAccessLayer.InsertAllIPTCData();
        }

        public void RefreshPictureData()
        {
            _DataAccessLayer.DeleteAllData();
            _DataAccessLayer.InsertAllPictures();
            _DataAccessLayer.InsertAllEXIFData();
            _DataAccessLayer.InsertAllIPTCData();
        }

        public List<string> AllPictureNames()
        {
            PictureList = new List<string>();
            PictureList = _DataAccessLayer.returnAllPicNames();
            return PictureList;
        }

        public List<string> AllEXIFInfoForOnePic(string name)
        {
            EXIF = new List<string>();
            EXIF = _DataAccessLayer.AllEXIFInfoFromOnePic(name);
            return EXIF;
        }

        public List<string> AllIPTCInfoForOnePic(string name)
        {
            IPTC = new List<string>();
            IPTC = _DataAccessLayer.AllIPTCInfoFromOnePic(name);
            return IPTC;
        }

        public Dictionary<int, List<string>> GetAllPhotographersInfo()
        {
            AllPhotograpersInfo = new Dictionary<int, List<string>>();
            AllPhotograpersInfo = _DataAccessLayer.GetAllPhotographers();
            return AllPhotograpersInfo;
        }
    }
}
