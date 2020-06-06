using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace SWE2_Projekt.ViewModels
{
    public class PictureListViewModel : ViewModel, INotifyPropertyChanged
    {

        private PictureViewModel _picture;
        //private PictureInfoViewModel _info;
        private ObservableCollection<PictureViewModel> _results;
        private BusinessLayer _businessLayer = new BusinessLayer();

        public PictureViewModel SelectedImage
        {
            get
            {
                return _picture;
            }
            set
            {
                if (_picture != value)
                {
                    _picture = value;
                    OnPropertyChanged(nameof(SelectedImage));
                }
            }
        }

        public void UpdateImageList(string search)
        {
            _results.Clear();
            List<string> searchResults = _businessLayer.SearchAllPictures(search);

            if (searchResults.Count > 0)
            {
                foreach (string item in searchResults)
                {
                    var trueimage = Path.GetFullPath("../../../images/" + item);
                    _results.Add(new PictureViewModel(new PictureModel() { ImagePath = trueimage }));
                }
            }
        }

        public ObservableCollection<PictureViewModel> ImageList
        {
            get
            {
                if (_results == null)
                {
                    _results = new ObservableCollection<PictureViewModel>();
                    foreach (var image in _businessLayer.AllPictureNames())
                    {
                        var trueimage = Path.GetFullPath(image);
                        _results.Add(new PictureViewModel(new PictureModel() { ImagePath = trueimage  }));
                    }

                    /*_results = new ObservableCollection<PictureViewModel>(_businessLayer.AllPictureNames())
                        .Select(i => new PictureViewModel(new PictureModel() { ImagePath = Path.GetFullPath(i) })));*/
                }
                return _results;
            }
        }

    }
}