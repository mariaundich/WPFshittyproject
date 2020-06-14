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

        private BusinessLayer _businessLayer;

        public PictureModel SelectedPicture;

        ICommand _editIPTC;

        public MainWindowViewModel()
        {
            _businessLayer = new BusinessLayer();

            SelectedPicture = _businessLayer.SelectedPicture;

            pictureInfoViewModel = new PictureInfoViewModel();

            pictureViewModel = new PictureViewModel();

            pictureListViewModel = new PictureListViewModel();

            photographerListViewModel = new PhotographerListViewModel();


            pictureListViewModel.PropertyChanged += (s, e) => {
                switch (e.PropertyName)
                {
                    case nameof(PictureListViewModel.SelectedImage):
                        pictureViewModel.Picture = pictureListViewModel.SelectedImage;
                        OnPropertyChanged(nameof(pictureViewModel));
                        pictureInfoViewModel.IPTCModel = pictureListViewModel.SelectedImage.IPTC;
                        pictureInfoViewModel.EXIFModel = pictureListViewModel.SelectedImage.EXIF;
                        OnPropertyChanged(nameof(pictureInfoViewModel));
                        break;
                }
            };

            /*photographerListViewModel.PropertyChanged += (s, e) => {
                switch (e.PropertyName)
                {
                    case nameof(PhotographerListViewModel.SelectedPhotographer):
                        photographerListViewModel.SelectedPhotographer = 
                        break;
                }
            };*/

            //_editIPTC = new EditCommand
        }

        public ICommand EditIPTC
        {
            get { return _editIPTC; }
            set { _editIPTC = value; }
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

        /*public PictureModel pictureViewModel
        {
            get
            {
                return pictureListViewModel.SelectedImage;
            }
        }*/
    }
}
