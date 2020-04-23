using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SWE2_Projekt.ViewModels
{
    public class PictureViewModel : ViewModel
    {
        private PictureModel _picture;


        public PictureViewModel(PictureModel picture)
        {
            this._picture = picture;
        }



        public PictureModel Picture
        {
            get
            {
                return _picture;
            }
        }
        public string TestPath
        {
            get
            {
                return _picture.ImagePath;
            }

        }

    }
}