﻿using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace SWE2_Projekt.ViewModels
{
    public class PictureViewModel : ViewModel
    {
        private BusinessLayer _businessLayer = new BusinessLayer();
        private PictureModel _picture;
        private IPTCModel _iptc;
        private EXIFModel _exif;

        public PictureViewModel()
        {
            Picture = _businessLayer.SelectedPicture;
            IPTC = Picture.IPTC;
            EXIF = Picture.EXIF;
        }

        public PictureModel Picture
        {
            get { return _picture; }
            set { 
                _picture = value;
                IPTC = _picture.IPTC;
                EXIF = _picture.EXIF;
            }
        }

        public IPTCModel IPTC
        {
            get { return _iptc; }
            set { _iptc = value; }
        }

        public EXIFModel EXIF
        {
            get { return _exif; }
            set { _exif = value; }
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