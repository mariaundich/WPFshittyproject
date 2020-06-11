using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Text;

namespace SWE2_Projekt.Models
{
    public class EXIFModel : INotifyPropertyChanged
    {
        private int _id;
        private string _camera;
        private string _resolution;
        private string _date;
        private string _place;
        private string _country;

        public EXIFModel(int id, string camera, string resolution, string date, string place, string country)
        {
            ID = id;
            Camera = camera;
            Resolution = resolution;
            Date = date;
            Place = place;
            Country = country;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }
        public string Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Place
        {
            get { return _place; }
            set { _place = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    
}
