﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace SWE2_Projekt.Tests
{
    public class AbstractTestTixture<T> where T: class, new()
    {

        #region Setup
        [SetUp]
        public void Setup()
        {

        }

        [OneTimeSetUp]
        public void TestSetup()
        {

        }
        #endregion

        public string WorkingDirectory
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        #region Support
        protected T CreateInstance(params object[] parameter)
        {
            return new T();
        }
        #endregion
    }
}