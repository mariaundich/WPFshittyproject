using System;
using System.Collections.Generic;
using System.Text;

namespace SWE2_Projekt.Models
{
    public class PhotographerModel
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _birthday;
        private string _notes;

        public PhotographerModel(int id, string first, string last, string birthday, string notes)
        {
            ID = id;
            FirstName = first;
            LastName = last;
            Birthday = birthday;
            Notes = notes;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
    }
}
