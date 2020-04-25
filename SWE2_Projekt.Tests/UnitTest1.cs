using NUnit.Framework;
using SWE2_Projekt;
using System;

namespace SWE2_Projekt.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void HelloWorldTest()
        {
            Assert.Pass();
        }

        [Test]
        public void StringTest()
        {
            string teststring = "henlo";
            Assert.That(teststring, Is.EqualTo("henlo"));
        } 

        [Test]
        public void ViewModelTest()
        {
            MainWindowViewModel testModel = new MainWindowViewModel();
        }
    }
}