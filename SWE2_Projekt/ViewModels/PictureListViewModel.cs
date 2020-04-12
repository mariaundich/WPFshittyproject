using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SWE2_Projekt.ViewModels
{
    class PictureListViewModel : ViewModel
    {
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
