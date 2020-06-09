using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SWE2_Projekt;

namespace SWE2_Projekt
{

    // To Do: interface für DAL und BL schreiben, für den Mock ableiten und alle
    // abgeleiteten Funktionen statt SQL Statements auf Liste mit Model-Objects ausführen
    public class MockDAL
    {
        private string _connectionstring;

        public MockDAL(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public string getConnectionstring()
        {
            return _connectionstring;
        }
    }

    public abstract class AbstractTestFixture<T> where T : class, new()
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