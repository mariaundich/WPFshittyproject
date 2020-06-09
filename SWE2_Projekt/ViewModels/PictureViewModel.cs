using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace SWE2_Projekt.ViewModels
{
    public class PictureViewModel : ViewModel
    {
        private PictureInfoViewModel pictureInfoViewModel = new PictureInfoViewModel();
        private PictureModel _picture;
        private IPTCModel _iptc;

        public PictureViewModel(PictureModel picture, IPTCModel iptc)
        {
            Picture = picture;
            IPTC = iptc; 
        }

        public PictureModel Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        public IPTCModel IPTC
        {
            get { return _iptc; }
            set { _iptc = value; }
        }
        

        public string Title
        {
            get
            {
                return _picture.Title;
            }
        }

        public string PicturePath
        {
            get
            {
                return _picture.PicturePath;
            }

        }
    }
}