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
using System.Collections.ObjectModel;

namespace SWE2_Projekt
{
    public class MainWindowViewModel : ViewModel
    {
        private PictureListViewModel _pictureListViewModel;
        private PictureInfoViewModel _pictureInfoViewModel;
        private PictureViewModel _pictureViewModel;
        private PhotographerListViewModel _photographerListViewModel;

        public BusinessLayer _businessLayer;

        public MainWindowViewModel()
        {
            _businessLayer = new BusinessLayer();

            pictureInfoViewModel = new PictureInfoViewModel();

            pictureViewModel = new PictureViewModel();

            pictureListViewModel = new PictureListViewModel();

            photographerListViewModel = new PhotographerListViewModel();


            pictureListViewModel.PropertyChanged += (s, e) => {
                switch (e.PropertyName)
                {
                    case nameof(PictureListViewModel.SelectedImage):
                        pictureViewModel.Picture = pictureListViewModel.SelectedImage;
                        pictureViewModel.TagString = pictureViewModel.MakeTagString();
                        pictureViewModel.SelectedPhotographerName = pictureViewModel.Picture.Photographer.FullName;
                        OnPropertyChanged(nameof(pictureViewModel));

                        pictureInfoViewModel.IPTCModel = pictureListViewModel.SelectedImage.IPTC;
                        pictureInfoViewModel.EXIFModel = pictureListViewModel.SelectedImage.EXIF;
                        OnPropertyChanged(nameof(pictureInfoViewModel));
                        break;
                }
            };
        }

        public PictureListViewModel pictureListViewModel 
        { 
            get { return _pictureListViewModel; } 
            set { _pictureListViewModel = value; }
        }

        public PhotographerListViewModel photographerListViewModel
        {
            get { return _photographerListViewModel; }
            set { _photographerListViewModel = value; }
        }

        public PictureInfoViewModel pictureInfoViewModel
        {
            get { return _pictureInfoViewModel; }
            set { _pictureInfoViewModel = value; }
        }

        public PictureViewModel pictureViewModel
        {
            get { return _pictureViewModel; }
            set { _pictureViewModel = value; }
        }
    }
}
