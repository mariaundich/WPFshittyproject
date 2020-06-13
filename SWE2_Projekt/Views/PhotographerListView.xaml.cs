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
        public PhotographerListView(MainWindowViewModel mainWindow)
        {
            InitializeComponent();
            this.DataContext = mainWindow;
        }
    }
}
