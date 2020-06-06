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
    public class IPTCViewModel: INotifyPropertyChanged
    {
        private IPTCModel _iptcModel;
        
        public IPTCViewModel(IPTCModel IptcModel)
        {
            IPTCModel = IptcModel;
        }

        public IPTCModel IPTCModel 
        { 
            get { return _iptcModel; }
            set { _iptcModel = value; }
        }

        public int ID
        {
            get { return _iptcModel.ID; }
        }

        public string Title
        {
            get { return _iptcModel.Title; }
        }

        public string Creator
        {
            get { return _iptcModel.Creator; }
        }

        public string Description
        {
            get { return _iptcModel.Description; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
