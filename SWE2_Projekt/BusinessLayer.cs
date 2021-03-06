﻿using MetadataExtractor;
using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SWE2_Projekt
{
    public class BusinessLayer
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
            //_DataAccessLayer.InsertPhotographerToPicture();
            PictureModelList = CreatePictureModelList();
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
            _DataAccessLayer.RefreshPictures();
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

                PhotographerModel auxPhotographerModel = _DataAccessLayer.GetPhotographerByID(pictureModel.Photographer_ID);
                pictureModel.Photographer = auxPhotographerModel;

                ObservableCollection<string> auxTags = _DataAccessLayer.GetTagsByPictureID(pictureModel.ID);
                pictureModel.Tags = auxTags;

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

        public void EditEXIF(int id, List<string> data)
        {
            _DataAccessLayer.EditEXIF(id, data);
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

                PhotographerModel auxPhotographerModel = _DataAccessLayer.GetPhotographerByID(pictureModel.Photographer_ID);
                pictureModel.Photographer = auxPhotographerModel;

                ObservableCollection<string> auxTags = _DataAccessLayer.GetTagsByPictureID(pictureModel.ID);
                pictureModel.Tags = auxTags;

                results.Add(pictureModel);
            }        

            return results;
        }

        public void AddTagToPicture(int PicID, string tag)
        {
            _DataAccessLayer.AddTagToPicture(PicID, tag);
        }

        public Dictionary<string, int> returnAllTagsWithCount()
        {
            return _DataAccessLayer.getAllTagsWithPicCount();
        }

        public PhotographerModel AssignPhotographerToPictureAndReturnPhotographerModel(int PictureID, int PhotographerID)
        {
            _DataAccessLayer.AssignPhotographertoPicture(PictureID, PhotographerID);

            PhotographerModel auxPhotographerModel = _DataAccessLayer.GetPhotographerByID(PhotographerID);

            return auxPhotographerModel;
        }

        public ObservableCollection<string> GetTagsByPictureID(int PictureID)
        {
            ObservableCollection<string> Tags = _DataAccessLayer.GetTagsByPictureID(PictureID);
            return Tags;
        }

        public void EditTags(int PictureID, string[] Tags)
        {
            _DataAccessLayer.RemoveTagsByPictureID(PictureID);
            foreach(var Tag in Tags)
            {
                _DataAccessLayer.AddTagToPicture(PictureID, Tag);
            }
        }

        public PhotographerModel AddAndReturnPhotographer(List<string> data)
        {
            PhotographerModel newPhotographer = _DataAccessLayer.AddAndReturnPhotographer(data[0], data[1], data[2], data[3]);
            //Console.WriteLine("newPhotographer im BL, ID: " + newPhotographer.ID);
            return newPhotographer;
        }
    }
}
