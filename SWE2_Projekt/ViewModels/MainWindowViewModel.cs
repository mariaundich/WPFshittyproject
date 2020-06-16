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


        ICommand _editIPTC;

        public MainWindowViewModel()
        {
            _businessLayer = new BusinessLayer();

            /*_businessLayer.AddTagToPicture("astro-cat.png", "Katze");
            _businessLayer.AddTagToPicture("astro-cat.png", "Emoji");
            _businessLayer.AddTagToPicture("astro-cat.png", "Astronaut");
            _businessLayer.AddTagToPicture("basilhat.jpg", "Katze");
            _businessLayer.AddTagToPicture("basilhat.jpg", "Hut");
            _businessLayer.AddTagToPicture("dino-cat.png", "Katze");
            _businessLayer.AddTagToPicture("dino-cat.png", "Emoji");
            _businessLayer.AddTagToPicture("dino-cat.png", "Dino");
            _businessLayer.AddTagToPicture("hacker-cat.png", "Katze");
            _businessLayer.AddTagToPicture("hacker-cat.png", "Emoji");
            _businessLayer.AddTagToPicture("hacker-cat.png", "Hacker");
            _businessLayer.AddTagToPicture("hats-for-cats.jpg", "Katze");
            _businessLayer.AddTagToPicture("hats-for-cats.jpg", "Hut");
            _businessLayer.AddTagToPicture("hipster-cat.png", "Katze");
            _businessLayer.AddTagToPicture("hipster-cat.png", "Emoji");
            _businessLayer.AddTagToPicture("hipster-cat.png", "Tee");
            _businessLayer.AddTagToPicture("hipster-cat.png", "Buch");
            _businessLayer.AddTagToPicture("ninja-cat.png", "Katze");
            _businessLayer.AddTagToPicture("ninja-cat.png", "Emoji");
            _businessLayer.AddTagToPicture("ninja-cat.png", "Hipster");
            _businessLayer.AddTagToPicture("stunt-cat.png", "Katze");
            _businessLayer.AddTagToPicture("stunt-cat.png", "Emoji");
            _businessLayer.AddTagToPicture("stunt-cat.png", "Stunt");
            _businessLayer.AddTagToPicture("stunt-cat.png", "Cape");*/

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
