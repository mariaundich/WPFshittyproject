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
        private ObservableCollection<PictureViewModel> _pictureViewModelList;
        private List<IPTCModel> _iptcModelList;
        private BusinessLayer _businessLayer = new BusinessLayer();

        public PictureListViewModel()
        {
            IPTCModelList = new List<IPTCModel>();
            PictureViewModelList = new ObservableCollection<PictureViewModel>();
        }

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

            List<string> searchResults = _businessLayer.SearchAllPictures(search);

            Console.Write(searchResults.ToString());
            _pictureViewModelList.Clear();
        }

        public List<IPTCModel> IPTCModelList
        {
            set
            {
                _iptcModelList = _businessLayer.AllIPTCModels();
            }
        }

        public ObservableCollection<PictureViewModel> PictureViewModelList
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
                            }
                        }
                        _pictureViewModelList.Add(new PictureViewModel(pictureModel, auxIPTCModel));
                    }

                }
            }
            get { return _pictureViewModelList; }
        }

    }
}