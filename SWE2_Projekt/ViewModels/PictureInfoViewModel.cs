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

        public PictureInfoViewModel()
        {
            IPTCModel = _businessLayer.SelectedPicture.IPTC;
            EXIFModel = _businessLayer.SelectedPicture.EXIF;
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

        public string Title
        {
            get { return _iptcModel.Title; }
        }

        public string Description
        {
            get { return _iptcModel.Description; } 
        }

        public void EditIPTC()
        {

        }
    }
}
