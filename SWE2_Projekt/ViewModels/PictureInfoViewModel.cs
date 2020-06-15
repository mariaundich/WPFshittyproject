using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SWE2_Projekt.Models;
using System.Security.Permissions;

namespace SWE2_Projekt.ViewModels
{
    public class PictureInfoViewModel:ViewModel
    {
        private IPTCModel _iptcModel;
        private EXIFModel _exifModel;
        private BusinessLayer _businessLayer = new BusinessLayer();
        private ObservableCollection<PhotographerModel> _photographerModelList;
        private PhotographerModel _selectedPhotographerInInfo;

        public PictureInfoViewModel()
        {
            IPTCModel = _businessLayer.SelectedPicture.IPTC;
            EXIFModel = _businessLayer.SelectedPicture.EXIF;
            PhotographerModelList = _businessLayer.PhotographerModelList;
            SelectedPhotographerInInfo = _businessLayer.SelectedPicture.Photographer;
            //Console.WriteLine("SelectedPhotographerInInfo: " + SelectedPhotographerInInfo);
        }

        public EXIFModel EXIFModel
        {
            get { return _exifModel; }
            set { _exifModel = value; }
        }

        public IPTCModel IPTCModel
        {
            get { return _iptcModel; }
            set { _iptcModel = value;  }
        }

        public ObservableCollection<PhotographerModel> PhotographerModelList
        {
            get { return _photographerModelList; }
            set { _photographerModelList = value; }
        }

       public PhotographerModel SelectedPhotographerInInfo
        {
            get { return _selectedPhotographerInInfo; }
            set { _selectedPhotographerInInfo = value; }
        }

        public string Title
        {
            get { return _iptcModel.Title; }
        }

        public string Creator
        {
            get { return _iptcModel.Creator; }
        }

        public string Description
        {
            get { return _iptcModel.Description; } 
        }

        public string Camera
        {
            get { return _exifModel.Camera; }
        }

        public string Resolution
        {
            get { return _exifModel.Resolution; }
        }

        public string Date
        {
            get { return _exifModel.Date; }
        }

        public string Place
        {
            get { return _exifModel.Place; }
        }

        public string Country
        {
            get { return _exifModel.Country; }
        }


    }
}
