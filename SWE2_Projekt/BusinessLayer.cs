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
        private ObservableCollection<PictureModel> _pictureModelList;
        private ObservableCollection<PhotographerModel> _photographerModelList;
        private PictureModel _selectedPicture;
        private PhotographerModel _selectedPhotographer;

        List<PictureModel> PictureList;
        List<IPTCModel> IPTCList;
        Dictionary<int, List<string>> EXIF;
        Dictionary<int, List<string>> AllPhotograpersInfo;

        public BusinessLayer()
        {
            _DataAccessLayer = new DataAccessLayer();
            PictureModelList = CreatePictureModelList();
            _selectedPicture = PictureModelList[0];
            PhotographerModelList = CreatePhotographerModelList();
            SelectedPicture = PictureModelList[0];
            SelectedPhotographer = PhotographerModelList[0];
        }

        public PictureModel SelectedPicture
        {
            get { return _selectedPicture; }
            set { _selectedPicture = value; }
        }

        public PhotographerModel SelectedPhotographer
        {
            get { return _selectedPhotographer; }
            set { _selectedPhotographer = value; }
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

        public ObservableCollection<PhotographerModel> PhotographerModelList
        {
            get { return _photographerModelList; }
            set { _photographerModelList = value; }
        }

        public ObservableCollection<PictureModel> CreatePictureModelList()
        {
            ObservableCollection<PictureModel> auxPictureModelList = new ObservableCollection<PictureModel>();
            List<PictureModel> PictureList = new List<PictureModel>();
            PictureList = _DataAccessLayer.ReturnAllPictureModels();

            foreach (PictureModel pictureModel in PictureList)
            {
                IPTCModel auxIPTCModel = _DataAccessLayer.GetIPTCInfoByID(pictureModel.IPTC_ID);
                pictureModel.IPTC = auxIPTCModel;

                EXIFModel auxEXIFModel = _DataAccessLayer.GetEXIFInfoByID(pictureModel.EXIF_ID);
                pictureModel.EXIF = auxEXIFModel;

                auxPictureModelList.Add(pictureModel);
            }
            Console.Write("\n\n There are " + auxPictureModelList.Count + " pictures in the list");
            return auxPictureModelList;
        }

        public ObservableCollection<PhotographerModel> CreatePhotographerModelList()
        {
            ObservableCollection<PhotographerModel> auxPhotographerModelList = new ObservableCollection<PhotographerModel>();
            List<PhotographerModel> PhotographerList = _DataAccessLayer.ReturnAllPhotographerModels();
            foreach (var photographer in PhotographerList)
            {
                auxPhotographerModelList.Add(photographer);
            }
            return auxPhotographerModelList;
        }

        public void EditIPTC(int id, List<string> data)
        {
            _DataAccessLayer.EditIPTC(id, data);
        }

        public void EditPhotographer(int id, List<string> data)
        {
            _DataAccessLayer.EditPhotographer(id, data);
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

        public ObservableCollection<PictureModel> SearchAllPictures(string search)
        {
            var results = new ObservableCollection<PictureModel>();

            var resultList = _DataAccessLayer.SearchForPictures(search);

            foreach (PictureModel pictureModel in resultList)
            {
                IPTCModel auxIPTCModel = _DataAccessLayer.GetIPTCInfoByID(pictureModel.IPTC_ID);
                pictureModel.IPTC = auxIPTCModel;

                EXIFModel auxEXIFModel = _DataAccessLayer.GetEXIFInfoByID(pictureModel.EXIF_ID);
                pictureModel.EXIF = auxEXIFModel;

                results.Add(pictureModel);
            }

            return results;
        }

        public void AddTagToPicture(string title, string tag)
        {
            _DataAccessLayer.AddTagToPicture(title, tag);
        }

        public Dictionary<string, int> returnAllTagsWithCount()
        {
            return _DataAccessLayer.getAllTagsWithPicCount();
        }
    }
}
