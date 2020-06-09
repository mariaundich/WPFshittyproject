using NUnit.Framework;
using SWE2_Projekt;
using System;
using System.Collections.Generic;

namespace SWE2_Projekt
{
    [TestFixture]
    public class Tests : AbstractTestFixture<UnitTestAufrufe>   
    {
        [Test]
        public void HelloWorldTest()
        {
            Assert.Pass();
        }

        [Test]
        public void StringTest()
        {
            string teststring = "testing";
            Assert.That(teststring, Is.EqualTo("testing"));
        } 

        [Test]
        public void TestPictureData()
        {
            var obj = CreateInstance().GetString("hello there");
            Assert.That(obj.getConnectionstring(), Is.EqualTo("hello there"));
        }
    }
}