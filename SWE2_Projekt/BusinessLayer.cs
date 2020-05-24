using System;
using System.Collections.Generic;
using System.Text;

namespace SWE2_Projekt
{
    class BusinessLayer
    {
        public DataAccessLayer _DataAccessLayer;
        public BusinessLayer()
        {
            _DataAccessLayer = new DataAccessLayer();
            _DataAccessLayer.DeleteAllData();
            _DataAccessLayer.InsertAllPictures();
            _DataAccessLayer.InsertAllEXIFData();
        }
    }
}
