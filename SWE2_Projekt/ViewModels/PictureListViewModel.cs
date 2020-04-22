using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace SWE2_Projekt.ViewModels
{
    public class PictureListViewModel : ViewModel, INotifyPropertyChanged
    {

        private PictureModel _picture;

        public PictureListViewModel(PictureModel picture)
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

        public ObservableCollection<string> ImageList
        {
            get
            {                
                var results = new ObservableCollection<string>();
                foreach (var image in Directory.GetFiles("../../../images"))
                {
                    var trueimage = Path.GetFullPath(image);
                    results.Add(trueimage);
                }
                return results;
            }
        }

        
      
    }
}
