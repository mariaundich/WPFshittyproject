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
        private int _exif;
        private int _iptc;
        private string _picturePath; 
        //ImagePath is not part of the data of a picture but is constructed out of the name and the file structure to load the picture

        public PictureModel(int id, string title, int photographer, int exif, int iptc)
        {
            ID = id;
            Title = title;
            Photographer = photographer;
            EXIF = exif;
            IPTC = iptc;

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

        public int EXIF
        {
            get
            {
                return _exif;
            }
            set
            {
                _exif = value;
            }
        }

        public int IPTC
        {
            get
            {
                return _iptc;
            }
            set
            {    
                _iptc = value;
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

        /*public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }*/
    }
}

