using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace SWE2_Projekt
{
    public interface IDataAccessLayer
    {
        void DeleteAllData() {  }

        void InsertAllPictures() { }

        List<PictureModel> ReturnAllPictureModels() { }

        EXIFModel GetEXIFInfoByID(int id) { }

        EXIFModel GetEXIFInfoByID(int id) { }

        IPTCModel GetIPTCInfoByID(int id) { }

        Dictionary<int, List<string>> AllEXIFInfoFromOnePicture(string title) { }



    }
}
