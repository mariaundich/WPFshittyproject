using MetadataExtractor;
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

        public PhotographerModel Photographer
        {
            get { return _picture.Photographer; }
        }

        public int Photographer_ID
        {
            get { return _picture.Photographer_ID; }
        }

        public string PhotographerFullName
        {
            get { return _picture.Photographer.FullName; }
        }

        public string PicturePath
        {
            get
            {
                return _picture.PicturePath;
            }

        }

        public string Tags
        {
            get
            {
                string auxTags = "";
                foreach(string Tag in _picture.Tags)
                {
                    auxTags += (Tag + ", ");
                }
                auxTags = auxTags.Remove(auxTags.Length - 2);
                return auxTags;
            }
        }
    }
}