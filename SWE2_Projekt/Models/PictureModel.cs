using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SWE2_Projekt.Models
{
    public class PictureModel //: INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private int _photographer;
        private int _exifID;
        private int _iptcID;
        private string _picturePath;
        // ImagePath is not part of the data of a picture but is constructed out of the name and the file structure to load the picture
        private IPTCModel _iptc;
        private EXIFModel _exif;
        // The IPTCModel and EXIFModel are set in the Business Layer where the model with the correct ID is added to the PictureModel

        public PictureModel(int id, string title, int photographer, int exif, int iptc)
        {
            ID = id;
            Title = title;
            Photographer = photographer;
            EXIF_ID = exif;
            IPTC_ID = iptc;

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
            }
        }

        public int Photographer
        {
            get
            {
                return _photographer;
            }
            set
            {
                _photographer = value;
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

        /*public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }*/
    }
}

