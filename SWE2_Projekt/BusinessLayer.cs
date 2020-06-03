using System;
using System.Collections.Generic;
using System.Text;

namespace SWE2_Projekt
{
    class BusinessLayer
    {
        public DataAccessLayer _DataAccessLayer;
        List<string> PictureList;
        Dictionary<int, List<string>> EXIF;
        Dictionary<int, List<string>> IPTC;
        Dictionary<int, List<string>> AllPhotograpersInfo;

        public BusinessLayer()
        {
            _DataAccessLayer = new DataAccessLayer();
        }

        public void RefreshPictureData()
        {
            _DataAccessLayer.DeleteAllData();
            _DataAccessLayer.InsertAllPictures();
            _DataAccessLayer.InsertAllEXIFData();
            _DataAccessLayer.InsertAllIPTCData();
        }

        public string[] AllPictureNames()
        {
            PictureList = new List<string>();
            PictureList = _DataAccessLayer.returnAllPictureNames();
            return PictureList.ToArray();
        }

        public Dictionary<int, List<string>> AllEXIFInfoForOnePic(string name)
        {
            EXIF = new Dictionary<int, List<string>>();
            EXIF = _DataAccessLayer.AllEXIFInfoFromOnePicture(name);
            return EXIF;
        }

        public Dictionary<int, List<string>> AllIPTCInfoForOnePic(string name)
        {
            IPTC = new Dictionary<int, List<string>>();
            IPTC = _DataAccessLayer.AllIPTCInfoFromOnePicture(name);
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
