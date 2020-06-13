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
        private BusinessLayer _businessLayer = new BusinessLayer();
        public PictureInfoView()
        {
            InitializeComponent();
            //this.DataContext = new PictureInfoViewModel();     
        }

        private void Savebutton_Click(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            string NewCreator = CreatorField.Text;
            string NewTitle = TitleField.Text;
            string NewDescription = DescriptionField.Text;
            int id = ((MainWindowViewModel)DataContext).pictureInfoViewModel.IPTCModel.ID;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.IPTCModel.Title = NewTitle;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.IPTCModel.Creator = NewCreator;
            ((MainWindowViewModel)DataContext).pictureInfoViewModel.IPTCModel.Description = NewDescription;

            data.Add(NewTitle);
            data.Add(NewCreator);
            data.Add(NewDescription);
            _businessLayer.EditIPTC(id, data);

        }
    }
}
