using SWE2_Projekt.Models;
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
using static System.Net.Mime.MediaTypeNames;

namespace SWE2_Projekt.ViewModels
{
    public class PictureListViewModel : ViewModel, INotifyPropertyChanged
    {
        private PictureModel _pictureModel;
        private ObservableCollection<PictureModel> _pictureModelList;
        private BusinessLayer _businessLayer = new BusinessLayer();

        public PictureListViewModel()
        {
            PictureModelList = _businessLayer.PictureModelList;
            SelectedImage = _businessLayer.SelectedPicture;
        }

        public ObservableCollection<PictureModel> PictureModelList
        {
            get { return _pictureModelList; }
            set { _pictureModelList = value; }
        }

        public PictureModel SelectedImage
        {
            get
            {
                return _pictureModel;
            }
            set
            {
                if (_pictureModel != value)
                {
                    _pictureModel = value;
                    OnPropertyChanged(nameof(SelectedImage));
                }
            }
        }

       public void UpdateImageList(string search)
        {

            /*
             If the search button was pressed while the search field is empty, show all images.
             The selected image remains the same and is placed to the front of the list.
             */

            if (search.Length == 0)
            {
                foreach (var entry in _pictureModelList.ToList())
                {
                    if (entry != SelectedImage)
                    {
                        _pictureModelList.Remove(entry);
                    }
                }

                foreach (var entry in _businessLayer.CreatePictureModelList())
                {                    
                    if (entry.Title != SelectedImage.Title)
                    {
                        _pictureModelList.Add(entry);
                    }
                }
            }
                        
            else
            {
                var searchResults = _businessLayer.SearchAllPictures(search);

                if (searchResults.Count > 0)
                {
                    _pictureModelList.Add(searchResults.First());

                    searchResults.Remove(searchResults.First());

                    SelectedImage = _pictureModelList.Last();

                    for (var i = _pictureModelList.Count - 2; i >= 0; i--)
                    {
                        _pictureModelList.RemoveAt(i);
                    }

                    if (searchResults.Count > 1)
                    {
                        for (var i = 1; i < searchResults.Count; i++)
                        {

                            _pictureModelList.Add(searchResults[i]);
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Keine Ergebnisse zu diesem Suchbegriff!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /*public List<IPTCModel> IPTCModelList
        {
            set
            {
                _iptcModelList = _businessLayer.AllIPTCModels();
            }
        }*/

        /*public ObservableCollection<PictureViewModel> PictureViewModelList
        {
            set
            {
                if (_pictureViewModelList == null)
                {
                    _pictureViewModelList = new ObservableCollection<PictureViewModel>();
                    foreach (var pictureModel in _businessLayer.AllPictureModels())
                    {
                        IPTCModel auxIPTCModel = null;
                        foreach (var iptcModel in _iptcModelList)
                        {
                            if(iptcModel.ID == pictureModel.IPTC)
                            {
                                auxIPTCModel = iptcModel;
                                break;
                            }
                        }
                        _pictureViewModelList.Add(new PictureViewModel(pictureModel, auxIPTCModel));
                    }

                }
            }
            get { return _pictureViewModelList; }
        }*/

    }
}