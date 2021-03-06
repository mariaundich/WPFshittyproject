using NUnit.Framework;
using SWE2_Projekt;
using SWE2_Projekt.Models;
using System;
using System.Collections.Generic;

namespace SWE2_Projekt
{
    [TestFixture]
    public class Tests : AbstractTestFixture<UnitTestAufrufe>   
    {
        [Test]
        public void DeleteAllPictures()
        {
            MockDAL mock = new MockDAL();

            mock.DeleteAllData();
            Assert.That(mock.PictureList.Count, Is.EqualTo(0));
        }

        [Test]
        public void DeleteAllIPTC()
        {
            MockDAL mock = new MockDAL();

            mock.DeleteAllData();
            Assert.That(mock.IPTCList.Count, Is.EqualTo(0));
        }

        [Test]
        public void DeleteAllEXIF()
        {
            MockDAL mock = new MockDAL();

            mock.DeleteAllData();
            Assert.That(mock.EXIFList.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddPictures()
        {
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();

            Assert.That(mock.PictureList.Count, Is.EqualTo(6));
        }

        [Test]
        public void AddIPTCDataToPictures()
        {
            MockDAL mock = new MockDAL();
            mock.InsertAllIPTCData();

            //Assert.That(mock.IPTCList.Count, Is.EqualTo(6));
            Assert.That(mock.IPTCList[4].Creator, Is.EqualTo("Batman"));
        }

        [Test]
        public void AddEXIFDataToPictures()
        {
            MockDAL mock = new MockDAL();
            mock.InsertAllEXIFData();

            Assert.That(mock.EXIFList.Count, Is.EqualTo(6));
            //Assert.That(mock.IPTCList[4].Creator, Is.EqualTo("Batman"));
        }

        [Test]
        public void EXIFInfoOfPicture()
        {
            Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();
            MockDAL mock = new MockDAL();
            mock.InsertAllEXIFData();
            dict = mock.AllEXIFInfoFromOnePicture("IAmBatman");

            Assert.That(dict.Count, Is.EqualTo(1));
        }

        [Test]
        public void DeletePicture()
        {
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();

            int count_before = mock.PictureList.Count;
            mock.DeletePicture("IAmBatman");
            
            Assert.That(mock.PictureList.Count, Is.LessThan(count_before));
        }

        [Test]
        public void EXIFInfoByID()
        {
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();

            EXIFModel model = mock.GetEXIFInfoByID(3);
            Assert.That(model.Camera, Is.EqualTo("Canon"));
        }

        [Test]
        public void IPTCInfoByID()
        {
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();

            IPTCModel model = mock.GetIPTCInfoByID(2);
            Assert.That(model.Title, Is.EqualTo("anothertitle"));
        }

        [Test]
        public void SearchForPictures()
        {
            List<PictureModel> results = new List<PictureModel>();
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();

            results = mock.SearchForPictures("Title");

            Assert.That(results.Count, Is.EqualTo(3));
        }

        [Test]
        public void AllPictureModels()
        {
            List<PictureModel> results = new List<PictureModel>();
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();

            results = mock.ReturnAllPictureModels();

            Assert.That(results.Count, Is.EqualTo(6));
        }

        [Test]
        public void AllIPTCModels()
        {
            List<IPTCModel> results = new List<IPTCModel>();
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();

            results = mock.ReturnAllIPTCModels();

            Assert.That(results.Count, Is.EqualTo(6));
        }

        [Test]
        public void EditEXIFData()
        {
            List<string> data = new List<string> { "Sony", "240x240", "01.01.2000 04:46:35", "Washington", "Amerika" };
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();

            mock.EditEXIF(4, data);

            Assert.That(mock.EXIFList[3].Camera, Is.EqualTo("Sony"));
        }

        [Test]
        public void EditIPTCData()
        {
            List<string> data = new List<string> { "AmazingPicture", "Matschi", "" };
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();

            mock.EditIPTC(3, data);

            Assert.That(mock.IPTCList[2].Title, Is.EqualTo("AmazingPicture"));
        }

        [Test]
        public void AddPhotographer()
        {
            MockDAL mock = new MockDAL();
            mock.AddAndReturnPhotographer("Bruce", "Wayne", "14.01.1993", "");

            Assert.That(mock.PhotographerList.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllPhotographersData()
        {
            MockDAL mock = new MockDAL();
            Dictionary<int, List<string>> allPhotographers = new Dictionary<int, List<string>>();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();
            mock.AddAndReturnPhotographer("Bruce", "Wayne", "01.19.2011", "");
            mock.AddAndReturnPhotographer("Homer", "Simpson", "01.19.2011", "");
            mock.AddAndReturnPhotographer("Peter", "Griffin", "01.19.2011", "");

            allPhotographers = mock.GetAllPhotographers();

            Assert.That(allPhotographers.Count, Is.EqualTo(3));
        }

        [Test]
        public void AssignPhotographerToPicture()
        {
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();
            mock.AddAndReturnPhotographer("Bruce", "Wayne", "01.01.1990", "");

            mock.AssignPhotographertoPicture(2, 1);

            Assert.That(mock.PictureList[1].Photographer.FirstName, Is.EqualTo("Bruce"));
        }

        [Test]
        public void DeletePhotographer()
        {
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();
            mock.AddAndReturnPhotographer("John", "Wayne", "03.04.1981", "");
            mock.AddAndReturnPhotographer("Bruce", "Wayne", "03.04.1981", "");
            mock.AddAndReturnPhotographer("Homer", "Simpson", "03.04.1981", "");
            mock.AddAndReturnPhotographer("Peter", "Griffin", "03.04.1981", "");

            mock.DeletePhotographer("Homer", "Simpson");

            Assert.That(mock.PhotographerList.Count, Is.EqualTo(3));
        }

        [Test]
        public void AddTagsToPicture()
        {
            MockDAL mock = new MockDAL();
            mock.InsertAllPictures();
            mock.InsertAllEXIFData();
            mock.InsertAllIPTCData();

            mock.AddTagToPicture(5, "Batsignal");

            Assert.That(mock.Tags.Count, Is.EqualTo(1));
            Assert.That(mock.PictureList[4].Tags[0], Is.EqualTo("Batsignal"));
        }
    }
}