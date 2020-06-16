using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Text;

namespace SWE2_Projekt.Models
{
    public class IPTCModel : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private string _creator;
        private string _description;

        public IPTCModel(int id, string title, string creator, string description)
        {
            ID = id;
            Title = title;
            Creator = creator;
            Description = description;
        }
        public int ID
        {
            get { return _id; }
            set { _id = value;  }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged(nameof(Title));
                }
            }
        }

        public string Creator
        {
            get { return _creator; }
            set { _creator = value;
                NotifyPropertyChanged(nameof(Creator));
            }
        }

        public string Description
        {
            get { return _description; }
            set 
            { 
                _description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
