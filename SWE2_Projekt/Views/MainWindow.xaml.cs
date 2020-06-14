using SWE2_Projekt.Models;
using SWE2_Projekt.ViewModels;
using SWE2_Projekt.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWE2_Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<int, List<string>> Test = new Dictionary<int, List<string>>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            //BusinessLayer BusinessLayer = new BusinessLayer();
            //Test = BusinessLayer.AllEXIFInfoForOnePicture("hacker-cat.png");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Searchbutton_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchField.Text.ToLower();

            ((MainWindowViewModel)DataContext).pictureListViewModel.UpdateImageList(search);
        }

        private void PictureInfoView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void FotografInnenPopup(object sender, RoutedEventArgs e)
        {
            PhotographerListView photographerList = new PhotographerListView(((MainWindowViewModel)DataContext));
            photographerList.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BerichtErstellen(object sender, RoutedEventArgs e)
        {
            bool created = false;
            PictureModel Picture = ((MainWindowViewModel)DataContext).pictureListViewModel.SelectedImage;
            PDFCreator creator = new PDFCreator();
            created = creator.CreateReport(Picture);

            if (created)
            {
                MessageBox.Show("Bericht wurde erstellt und gespeichert.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }   
        }

        private void TagBerichtErstellen(object sender, RoutedEventArgs e)
        {
            bool created = false;
            PDFCreator creator = new PDFCreator();
            created = creator.TagReport();

            if (created)
            {
                MessageBox.Show("Bericht wurde erstellt und gespeichert.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
