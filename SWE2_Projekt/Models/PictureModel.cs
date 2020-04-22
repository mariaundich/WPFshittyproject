using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace SWE2_Projekt.Models
{
    public class PictureModel : INotifyPropertyChanged
    {

        private string _imagePath = Path.GetFullPath("../../../images/hats-for-cats1-300x250.jpg");

        public string ImagePath {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
                NotifyPropertyChanged("SelectedImagePath");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }


}

