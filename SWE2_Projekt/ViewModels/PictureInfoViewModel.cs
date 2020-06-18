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
            set { _photographerModelList = value;
                OnPropertyChanged(nameof(PhotographerModelList));
                }
        }

       public PhotographerModel SelectedPhotographerInInfo
        {
            get { return _selectedPhotographerInInfo; }
            set { _selectedPhotographerInInfo = value; }
        }

        public string Title
        {
            get { return _iptcModel.Title; }
            set 
            { 
                _iptcModel.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Creator
        {
            get { return _iptcModel.Creator; }
            set
            {
                _iptcModel.Creator = value;
                OnPropertyChanged(nameof(Creator));
            }
        }

        public string Description
        {
            get { return _iptcModel.Description; }
            set
            {
                _iptcModel.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Camera
        {
            get { return _exifModel.Camera; }
            set
            {
                _exifModel.Camera = value;
                OnPropertyChanged(nameof(Camera));
            }
        }

        public string Resolution
        {
            get { return _exifModel.Resolution; }
            set
            {
                _exifModel.Resolution = value;
                OnPropertyChanged(nameof(Resolution));
            }
        }

        public string Date
        {
            get { return _exifModel.Date; }
            set
            {
                _exifModel.Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public string Place
        {
            get { return _exifModel.Place; }
            set
            {
                _exifModel.Place = value;
                OnPropertyChanged(nameof(Place));
            }
        }

        public string Country
        {
            get { return _exifModel.Country; }
            set
            {
                _exifModel.Camera = value;
                OnPropertyChanged(nameof(Camera));
            }
        }


    }
}
