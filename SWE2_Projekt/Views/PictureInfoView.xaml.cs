using SWE2_Projekt.Models;
using SWE2_Projekt.ViewModels;using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWE2_Projekt.Views
{
    /// <summary>
    /// Interaction logic for PictureInfoView.xaml
    /// </summary>
    public partial class PictureInfoView : UserControl
    {
        private BusinessLayer _businessLayer = new BusinessLayer();
        public PictureInfoView()
        {
            InitializeComponent();
            //this.DataContext = new PictureInfoViewModel();     
        }

        private void IPTC_Savebutton_Click(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            string NewCreator = CreatorField.Text;
            string NewTitle = TitleField.Text;
            string NewDescription = DescriptionField.Text;
            int id = ((MainWindowViewModel)DataContext).pictureInfoViewModel.IPTCModel.ID;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.Title = NewTitle;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.Creator = NewCreator;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.Description = NewDescription;

            data.Add(NewTitle);
            data.Add(NewCreator);
            data.Add(NewDescription);
            _businessLayer.EditIPTC(id, data);

            MessageBox.Show("Die Daten wurden bearbeitet.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void General_Savebutton_Click(object sender, RoutedEventArgs e)
        {
            int PictureID = ((MainWindowViewModel)DataContext).pictureViewModel.Picture.ID;
            int PhotographerID = -1;
            PhotographerModel newPhotographer;
            string PhotographerInput = PhotographerField.Text;
            string PhotographerFullName = ((MainWindowViewModel)DataContext).pictureViewModel.PhotographerFullName;
            bool PhotographerEdited = false;
            bool TagsEdited = false;

            if (PhotographerInput != PhotographerFullName)
            {
                foreach (var Photographer in ((MainWindowViewModel)DataContext).pictureInfoViewModel.PhotographerModelList)
                {
                    if (Photographer.FullName == PhotographerInput)
                    {
                        PhotographerID = Photographer.ID;
                    }
                }

                if (PhotographerID != -1)
                {
                     newPhotographer = _businessLayer.AssignPhotographerToPictureAndReturnPhotographerModel(PictureID, PhotographerID);
                    
                    ((MainWindowViewModel)DataContext).pictureViewModel.Photographer = newPhotographer;
                    ((MainWindowViewModel)DataContext).pictureViewModel.PhotographerFullName = newPhotographer.FirstName + " " + newPhotographer.LastName;

                    PhotographerEdited = true;
                }
                else
                {
                    MessageBox.Show("Das ist kein gültiger Name einer FotografIn", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            

            string[] newTags;
            string TagsInput = TagsField.Text;
            string TagString = ((MainWindowViewModel)DataContext).pictureViewModel.TagString;
            Console.WriteLine("Input:"+ TagsInput);
            Console.WriteLine(" TagString: " + TagString);
            Console.WriteLine(TagsInput == TagString);


            if (TagsInput != TagString)
            {
                newTags = TagsInput.Split(", ");

                ObservableCollection<string> auxTags = new ObservableCollection<string>();
                foreach (var Tag in newTags)
                {
                    auxTags.Add(Tag);
                }

                ((MainWindowViewModel)DataContext).pictureViewModel.Tags = auxTags;
                _businessLayer.EditTags(PictureID, newTags);

                TagsEdited = true;
            }
            
            if(PhotographerEdited==true || TagsEdited == true)
            {
                Console.WriteLine("Edited photographer:" + PhotographerEdited + " or Tags: " + TagsEdited);
                MessageBox.Show("Die Daten wurden bearbeitet.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Schreibe deine Änderungen in eines der Textfelder um sie zu speichern.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void EXIF_Savebutton_Click(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            string NewCamera = CameraField.Text;
            string NewResolution = ResolutionField.Text;
            string NewDate = DateField.Text;
            string NewPlace = PlaceField.Text;
            string NewCountry = CountryField.Text;
            int id = ((MainWindowViewModel)DataContext).pictureInfoViewModel.EXIFModel.ID;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.Camera = NewCamera;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.Resolution = NewResolution;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.Date = NewDate;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.Place = NewPlace;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.Country = NewCountry;

            data.Add(NewCamera);
            data.Add(NewResolution);
            data.Add(NewDate);
            data.Add(NewPlace);
            data.Add(NewCountry);
            _businessLayer.EditEXIF(id, data);

            MessageBox.Show("Die Daten wurden bearbeitet.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
