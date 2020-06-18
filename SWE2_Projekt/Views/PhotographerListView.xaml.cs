using SWE2_Projekt.Models;
using SWE2_Projekt.ViewModels;
using SWE2_Projekt.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SWE2_Projekt.Views
{
    /// <summary>
    /// Interaction logic for PhotographerListView.xaml
    /// </summary>
    public partial class PhotographerListView : Window
    {
        BusinessLayer _businessLayer = new BusinessLayer();
        public PhotographerListView(MainWindowViewModel mainWindow)
        {
            InitializeComponent();
            this.DataContext = mainWindow;
        }

        private void Savebutton_Click(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            string NewFirstName = FirstNameField.Text;
            string NewLastName = LastNameField.Text;
            string NewBirthday = BirthdayField.Text;
            string NewNotes = NotesField.Text;

            data.Add(NewFirstName);
            data.Add(NewLastName);
            data.Add(NewBirthday);
            data.Add(NewNotes);

            int id = ((MainWindowViewModel)DataContext).photographerListViewModel.SelectedPhotographer.ID;

            ((MainWindowViewModel)DataContext).photographerListViewModel.EditPhotographer(id, data);
            ((MainWindowViewModel)DataContext).pictureListViewModel.RefreshImageList();
            MessageBox.Show("Die Daten wurden bearbeitet.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            string AddFirstName = AddFirstNameField.Text;
            string AddLastName = AddLastNameField.Text;
            string AddBirthday = AddBirthdayField.Text;
            string AddNotes = AddNotesField.Text;

            data.Add(AddFirstName);
            data.Add(AddLastName);
            data.Add(AddBirthday);
            data.Add(AddNotes);

            bool SuccessfullyAdded = ((MainWindowViewModel)DataContext).photographerListViewModel.AddPhotographer(data);
            if (SuccessfullyAdded)
            {
                MessageBox.Show("Die FotografIn wurde hinzugefügt.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
