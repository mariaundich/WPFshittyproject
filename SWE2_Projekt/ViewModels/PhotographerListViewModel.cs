using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
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

        public void EditPhotographer(int id, List<string> data)
        {
            if (data[1] != "" &&
                DateTime.Compare(DateTime.Today, Convert.ToDateTime(data[2])) > 0)
            {
                SelectedPhotographer.FirstName = data[0];
                SelectedPhotographer.LastName = data[1];
                SelectedPhotographer.Birthday = data[2];
                SelectedPhotographer.Notes = data[3];

                _businessLayer.EditPhotographer(id, data);
            }
            else
            {
                MessageBox.Show("Ein Nachname wird benötigt und der Geburtstag muss vor dem heutigen Datum liegen!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            

        }
    }
}
