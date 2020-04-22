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
        private static PictureModel picture = new PictureModel();
        private PictureViewModel _pictureViewModel = new PictureViewModel(picture);
        private PictureListViewModel _pictureListViewModel = new PictureListViewModel(picture);

        public PictureViewModel pictureViewModel
        {
            get
            {
                return _pictureViewModel;
            }
        }

        public PictureListViewModel pictureListViewModel
        {
            get
            {
                return _pictureListViewModel;
            }
        }

    }
}
