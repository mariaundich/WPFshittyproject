using MetadataExtractor;
using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string _tagString;

        public PictureViewModel()
        {
            Picture = _businessLayer.SelectedPicture;
            IPTC = Picture.IPTC;
            EXIF = Picture.EXIF;
            TagString = MakeTagString();
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
            set
            {
                _picture.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public PhotographerModel Photographer
        {
            get { return _picture.Photographer; }
            set
            {
                _picture.Photographer = value;
                OnPropertyChanged(nameof(Photographer));
            }
        }

        public int Photographer_ID
        {
            get { return _picture.Photographer_ID; }
        }

        public string PhotographerFullName
        {
            get { return _picture.Photographer.FullName; }
            set
            {
                _picture.Photographer.FullName = value;
                OnPropertyChanged(nameof(PhotographerFullName));
            }
        }

        public string PicturePath
        {
            get
            {
                return _picture.PicturePath;
            }

        }

        public ObservableCollection<string> Tags
        {
            get { return _picture.Tags; }
            set
            {
                _picture.Tags = value;
                TagString = MakeTagString();
                OnPropertyChanged(nameof(Tags));
            }
        }

        public string TagString
        {
            get { return _tagString; }
            set {
                _tagString = value;
                OnPropertyChanged(nameof(TagString));
            }
        }

        public string MakeTagString()
        {
            string auxTags = "";
            foreach (string Tag in Tags)
            {
                auxTags += (Tag + ", ");
            }
            auxTags = auxTags.Remove(auxTags.Length - 2);
            return auxTags;
        }
    }
}