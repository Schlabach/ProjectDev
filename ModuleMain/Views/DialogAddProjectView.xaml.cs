using AppProjectDev.core;
using Syncfusion.SfSkinManager;
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

namespace ModuleMain.Views
{
    /// <summary>
    /// Interaction logic for DialogAddProjectView
    /// </summary>
    public partial class DialogAddProjectView : UserControl
    {
        public DialogAddProjectView()
        {
            InitializeComponent();
            if (GlobalVars.Theme == "FluentDark")
            {
                UC.Background = Brushes.Black;

                SfSkinManager.SetTheme(UC, new Theme("FluentDark"));
            }
            else
            {
                SfSkinManager.SetTheme(UC, new Theme("FluentLight"));
                UC.Background = Brushes.White;
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
