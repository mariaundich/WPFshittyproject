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

namespace SWE2_Projekt
{
    /// <summary>
    /// Interaktionslogik für HowTo.xaml
    /// </summary>
    public partial class HowTo : Window
    {
        private string _helpText = "Du kannst die Informationen, die dir rechts neben den Bildern angesezigt werden, bearbeiten. Du kannst zum Beispiel den zugeordneten Fotografen ändern oder die Tags eines Bildes bearbeiten.";

        public HowTo(string text)
        {
            InitializeComponent();
            //this._helpText = text;
        }

        public string HelpText
        {
            get { return string.Format(_helpText); }
            set { HelpText = value;  }
        }

    }
}
