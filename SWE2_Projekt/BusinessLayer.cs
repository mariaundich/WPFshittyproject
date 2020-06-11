using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SWE2_Projekt
{
    class BusinessLayer
    {
        public DataAccessLayer _DataAccessLayer;
        //private ObservableCollection<IPTCModel> _iptcModelList;
        private ObservableCollection<PictureModel> _pictureModelList;

        List<PictureModel> PictureList;
        List<IPTCModel> IPTCList;
        Dictionary<int, List<string>> EXIF;
        Dictionary<int, List<string>> AllPhotograpersInfo;

        public BusinessLayer()
        {
            _DataAccessLayer = new DataAccessLayer();
            PictureModelList = CreatePictureModelList();
            
        }

        public void RefreshPictureData()
        {
            _DataAccessLayer.DeleteAllData();
            _DataAccessLayer.InsertAllPictures();
            _DataAccessLayer.InsertAllEXIFData();
            _DataAccessLayer.InsertAllIPTCData();
        }

        public ObservableCollection<PictureModel> PictureModelList
        {
            get { return _pictureModelList; }
            set { _pictureModelList = value; }
        }

        public ObservableCollection<PictureModel> CreatePictureModelList()
        {
            ObservableCollection<PictureModel> auxPictureModelList = new ObservableCollection<PictureModel>();
            List<PictureModel> PictureList = new List<PictureModel>();
            PictureList = _DataAccessLayer.ReturnAllPictureModels();

            foreach (PictureModel pictureModel in PictureList)
            {
                IPTCModel auxIPTCModel =_DataAccessLayer.GetIPTCInfoByID(pictureModel.IPTC_ID);
                pictureModel.IPTC = auxIPTCModel;

                if (auxIPTCModel == null) { Console.WriteLine("The IPTCmodel of Pic " + pictureModel.Title + " is null :(("); }

                EXIFModel auxEXIFModel = _DataAccessLayer.GetEXIFInfoByID(pictureModel.EXIF_ID);
                pictureModel.EXIF = auxEXIFModel;

                auxPictureModelList.Add(pictureModel);
            }
            Console.Write("\n\n There are "+auxPictureModelList.Count+" pictures in the list" );
            return auxPictureModelList;
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
