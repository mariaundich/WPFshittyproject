using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SWE2_Projekt.Models
{
    public class PictureModel : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private int _photographer_ID;
        private int _exifID;
        private int _iptcID;
        private string _picturePath;
        // ImagePath is not part of the data of a picture but is constructed out of the name and the file structure to load the picture
        private IPTCModel _iptc = null;
        private EXIFModel _exif = null;
        private PhotographerModel _photographer = null;
        // The Models are set in the Business Layer where the model with the correct ID is added to the PictureModel
        private ObservableCollection<string> _tags;

        public PictureModel(int id, string title, int photographer, int exif, int iptc, ObservableCollection<string> tags)
        {
            ID = id;
            Title = title;
            Photographer_ID = photographer;
            EXIF_ID = exif;
            IPTC_ID = iptc;
            Tags = tags;

            string auxPath = "../../../images/" + Title;
            PicturePath = Path.GetFullPath(auxPath); 

        }

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        public int Photographer_ID
        {
            get
            {
                return _photographer_ID;
            }
            set
            {
                _photographer_ID = value;
            }
        }

        public int EXIF_ID
        {
            get
            {
                return _exifID;
            }
            set
            {
                _exifID = value;
            }
        }

        public int IPTC_ID
        {
            get
            {
                return _iptcID;
            }
            set
            {    
                _iptcID = value;
            }
        }

        public string PicturePath
        {
            get
            {
                return _picturePath;
            }
            set
            {   
                if(_title != null)
                {
                    _picturePath = value;
                }
            }
        }

        public EXIFModel EXIF
        {
            get { return _exif; }
            set { _exif = value; }
        }

        public IPTCModel IPTC
        {
            get { return _iptc; }
            set { _iptc = value; }
        }

        public PhotographerModel Photographer
        {
            get { return _photographer; }
            set { _photographer = value;
                NotifyPropertyChanged(nameof(Photographer));
            }
        }

        public ObservableCollection<string> Tags
        {
            get { return _tags; }
            set { _tags = value;
                NotifyPropertyChanged(nameof(Tags));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

