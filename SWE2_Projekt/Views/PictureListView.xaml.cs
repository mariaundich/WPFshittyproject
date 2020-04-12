using SWE2_Projekt.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWE2_Projekt.Views
{
    /// <summary>
    /// Interaktionslogik für PictureListView.xaml
    /// </summary>
    public partial class PictureListView : UserControl
    {
        public PictureListView()
        {
            InitializeComponent();
            DataContext = new PictureListViewModel();
        }
    }
}
