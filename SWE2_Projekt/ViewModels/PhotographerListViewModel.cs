using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SWE2_Projekt.ViewModels
{
    public class PhotographerListViewModel: ViewModel, INotifyPropertyChanged
    {
        private PhotographerModel _photographer;
        private ObservableCollection<PhotographerModel> _photographerModelList;
        private BusinessLayer _businessLayer = new BusinessLayer();

        public PhotographerListViewModel()
        {
            PhotographerModelList = _businessLayer.PhotographerModelList;
            Photographer = _businessLayer.SelectedPhotographer;
            //Console.WriteLine(SelectedPhotographer.FirstName);
        }

        public ObservableCollection<PhotographerModel> PhotographerModelList
        {
            get { return _photographerModelList; }
            set { _photographerModelList = value; }
        }

        public PhotographerModel Photographer
        {
            get { return _photographer; }
            set { _photographer = value; }
        }

        public PhotographerModel SelectedPhotographer
        {
            get
            {
                return _photographer;
            }
            set
            {
                if (_photographer != value)
                {
                    _photographer = value;
                    //Console.WriteLine(SelectedPhotographer.FirstName);
                    OnPropertyChanged(nameof(SelectedPhotographer));
                }
            }
        }
    }
}
