﻿using SWE2_Projekt.Models;
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
        private PhotographerModel _selectedPhotographer;
        private ObservableCollection<PhotographerModel> _photographerModelList;
        private BusinessLayer _businessLayer = new BusinessLayer();

        public PhotographerListViewModel()
        {
            PhotographerModelList = _businessLayer.PhotographerModelList;
            SelectedPhotographer = _businessLayer.SelectedPhotographer;
        }

        public ObservableCollection<PhotographerModel> PhotographerModelList
        {
            get { return _photographerModelList; }
            set { _photographerModelList = value;
                OnPropertyChanged(nameof(PhotographerModelList));
            }
        }


        public PhotographerModel SelectedPhotographer
        {
            get
            {
                return _selectedPhotographer;
            }
            set
            {
                if (_selectedPhotographer != value)
                {
                    _selectedPhotographer = value;
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
                SelectedPhotographer.FullName = data[0] + " " + data[1];

                _businessLayer.EditPhotographer(id, data);
                
                MessageBox.Show("Die Daten wurden bearbeitet.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ein Nachname wird benötigt und der Geburtstag muss vor dem heutigen Datum liegen!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool AddPhotographer(List<string> data)
        {
            bool WasAdded = false; 
            if (data[1] != "" &&
                DateTime.Compare(DateTime.Today, Convert.ToDateTime(data[2])) > 0)
            {
                PhotographerModel newPhotographerModel = _businessLayer.AddAndReturnPhotographer(data);
                _photographerModelList.Add(newPhotographerModel);
                
                WasAdded = true;
            }
            else
            {
                MessageBox.Show("Ein Nachname wird benötigt und der Geburtstag muss vor dem heutigen Datum liegen!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return WasAdded;
        }
    }
}
