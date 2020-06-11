using SWE2_Projekt.Models;
using SWE2_Projekt.ViewModels;using System;
using System.Collections.Generic;
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
        public PictureInfoView()
        {
            InitializeComponent();
            //this.DataContext = PictureViewModel(); // I couldn't manage to put the info models into the InfoViewModel so I had to keep using the PictureViewModel
            //this.DataContext = new PictureViewModel();
        }

        private void Savebutton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
