using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Automation.Peers;

namespace SWE2_Projekt.ViewModels

{
    public class PictureInfoViewModel: INotifyPropertyChanged
    {
        private string _title;
        private string _creator;
        private string _description;
        private Dictionary <int, List<string>> _iptc = new Dictionary<int, List<string>>();
        private BusinessLayer _businessLayer = new BusinessLayer();
        
       public void LoadData(string title)
        {
            _iptc = _businessLayer.AllIPTCInfoForOnePic(title);

            List<string> _info = _iptc[_iptc.Keys.ElementAt(0)];

            Title = _info[0];
            Creator = _info[1];
            Description = _info[2];
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
        public string Creator
        {
            get
            {
                return _creator;
            }
            set
            {
                _creator = value;
                NotifyPropertyChanged(nameof(Creator));

            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
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
