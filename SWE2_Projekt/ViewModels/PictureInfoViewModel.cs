using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SWE2_Projekt.ViewModels
{
    public class PictureInfoViewModel:ViewModel
    {
        private ObservableCollection<IPTCViewModel> _iptcViewModelList;
        private BusinessLayer _businessLayer = new BusinessLayer();

        public PictureInfoViewModel()
        {
            IPTCViewModelList = new ObservableCollection<IPTCViewModel>();
        }

        public ObservableCollection<IPTCViewModel> IPTCViewModelList
        {
            set
            {
                if (_iptcViewModelList == null)
                {
                    _iptcViewModelList = new ObservableCollection<IPTCViewModel>();
                    Console.WriteLine("Number of Models in the list from the businesslayer: " + _businessLayer.AllIPTCModels().Count);
                    foreach (var iptcModel in _businessLayer.AllIPTCModels())
                    {
                        Console.WriteLine("Adding the IPTCViewModel with Title " + new IPTCViewModel(iptcModel).Title);
                        _iptcViewModelList.Add(new IPTCViewModel(iptcModel));
                    }
                    Console.WriteLine("The list of IPTCViewModels has elements: " + _iptcViewModelList.Count);
                }
            }
            get { return _iptcViewModelList; }
        }
    }
}
