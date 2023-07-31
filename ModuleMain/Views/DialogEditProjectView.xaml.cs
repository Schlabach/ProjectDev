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
    /// Interaction logic for DialogEditProjectView.xaml
    /// </summary>
    public partial class DialogEditProjectView : UserControl
    {
        public DialogEditProjectView()
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
    }
}