using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SWE2_Projekt.Models
{
    public class PictureModel : INotifyPropertyChanged
    {
        private string _imagePath;
        private string _title;
        private string _creator;
        private string _description;

        public string ImagePath {
            get
            {
                return _imagePath;
            }
            set
            {
                if(_imagePath != value)
                {        
                    _imagePath = value;
                    _title = _imagePath.Split("\\").Last();
                }
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

