using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;

namespace SWE2_Projekt.Models
{
    public class PhotographerModel:INotifyPropertyChanged
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _fullName;
        private string _birthday;
        private string _notes;

        public event PropertyChangedEventHandler PropertyChanged;

        public PhotographerModel(int id, string first, string last, string birthday, string notes)
        {
            ID = id;
            FirstName = first;
            LastName = last;
            FullName = first +" "+ last;
            Birthday = birthday;
            Notes = notes;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value;
                NotifyPropertyChanged(nameof(ID));
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value;
                NotifyPropertyChanged(nameof(LastName));
            }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value;
                NotifyPropertyChanged(nameof(FullName));
            }
        }

        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value;
                Console.WriteLine("Geburtstag im Model: " + _birthday);
                NotifyPropertyChanged(nameof(Birthday));
            }
        }

        public string Notes
        {
            get { return _notes; }
            set { _notes = value;
                NotifyPropertyChanged(nameof(Notes));
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
