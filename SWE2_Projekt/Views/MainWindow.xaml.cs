﻿using System;
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
            BusinessLayer _BusinessLayer = new BusinessLayer();
            AllPhotographers = _BusinessLayer.GetAllPhotographersInfo();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
