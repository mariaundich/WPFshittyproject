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

       /* public void UpdateImageList(string search)
        {
            _pictureViewModelList.Clear();
            List<string> searchResults = _businessLayer.SearchAllPictures(search);
;

            if (searchResults.Count > 0)
            {
                foreach (string item in searchResults)
                {
                    var trueimage = Path.GetFullPath("../../../images/" + item);
                    _results.Add(new PictureViewModel(new PictureModel() { ImagePath = trueimage }));
                }
            }
        }*/

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