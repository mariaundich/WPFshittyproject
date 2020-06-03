using SWE2_Projekt.Models;
using SWE2_Projekt.ViewModels;
using SWE2_Projekt.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SWE2_Projekt
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            pictureListViewModel.PropertyChanged += (s, e) => {
                switch (e.PropertyName) {
                    case nameof(PictureListViewModel.SelectedImage):

                        OnPropertyChanged(nameof(pictureViewModel));
                        pictureInfoViewModel.LoadData(pictureViewModel.Title);
                        break;
                    }
            };
        }

        public PictureViewModel pictureViewModel
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
