using SWE2_Projekt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        Dictionary<int, List<string>> AllPhotographers = new Dictionary<int, List<string>>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            BusinessLayer BusinessLayer = new BusinessLayer();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Searchbutton_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchField.Text;

            ((MainWindowViewModel)DataContext).pictureListViewModel.UpdateImageList(search);
        }

        private void PictureInfoView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
