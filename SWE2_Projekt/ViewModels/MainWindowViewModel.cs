using SWE2_Projekt.Models;
using SWE2_Projekt.ViewModels;
using SWE2_Projekt.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SWE2_Projekt
{
    public class MainWindowViewModel : ViewModel
    {
        private BusinessLayer businessLayer = new BusinessLayer();
        ICommand _editIPTC;

        public MainWindowViewModel()
        {
            pictureListViewModel.PropertyChanged += (s, e) => {
                switch (e.PropertyName) {
                    case nameof(PictureListViewModel.SelectedImage):
                        OnPropertyChanged(nameof(pictureViewModel));
                        OnPropertyChanged(nameof(pictureInfoViewModel));
                        break;
                    }
           
            };

            //_editIPTC = new EditCommand
        }

        public ICommand EditIPTC 
        { 
            get { return _editIPTC; }
            set { _editIPTC = value; }
        }
        
        public PictureModel pictureViewModel
        {
            get
            {
                return pictureListViewModel.SelectedImage;
            }
        }

        public PictureListViewModel pictureListViewModel { get; } = new PictureListViewModel();

        public PictureInfoViewModel pictureInfoViewModel { get; } = new PictureInfoViewModel();
  


    }
}
