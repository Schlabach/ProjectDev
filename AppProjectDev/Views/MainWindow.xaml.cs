using AppProjectDev.core;
using AppProjectDev.core.DataAccess;
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.FluentDark.WPF;
using Syncfusion.Themes.FluentLight.WPF;
using Syncfusion.Themes.MaterialDark.WPF;
using Syncfusion.Themes.MaterialLight.WPF;
using Syncfusion.Windows.Shared;
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

namespace AppProjectDev.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ChromelessWindow
    {
        public MainWindow(IDataRepository dataRepository)
        {
            //Set Theme settings
            SfSkinManager.ApplyStylesOnApplication = true;
            MaterialLightThemeSettings fluentLightThemeSettings = new MaterialLightThemeSettings();
            fluentLightThemeSettings.PrimaryBackground = new SolidColorBrush(Color.FromRgb(154, 194, 201));
            fluentLightThemeSettings.PrimaryForeground = new SolidColorBrush(Colors.White);
            fluentLightThemeSettings.BodyFontSize = 15;
            fluentLightThemeSettings.HeaderFontSize = 18;
            fluentLightThemeSettings.SubHeaderFontSize = 17;
            fluentLightThemeSettings.TitleFontSize = 17;
            fluentLightThemeSettings.SubTitleFontSize = 16;
            fluentLightThemeSettings.FontFamily = new FontFamily("Callibri");
            SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
            MaterialDarkThemeSettings fluentDarkThemeSettings = new MaterialDarkThemeSettings();
            fluentDarkThemeSettings.PrimaryBackground = new SolidColorBrush(Color.FromRgb(74, 80, 67));
            fluentDarkThemeSettings.PrimaryForeground = new SolidColorBrush(Colors.Linen);
            fluentDarkThemeSettings.BodyFontSize = 15;
            fluentDarkThemeSettings.HeaderFontSize = 18;
            fluentDarkThemeSettings.SubHeaderFontSize = 17;
            fluentDarkThemeSettings.TitleFontSize = 17;
            fluentDarkThemeSettings.SubTitleFontSize = 16;
            fluentDarkThemeSettings.FontFamily = new FontFamily("Callibri");
            SfSkinManager.RegisterThemeSettings("FluentDark", fluentDarkThemeSettings);
            GlobalVars.Theme = dataRepository.GetTheme();
            SfSkinManager.SetTheme(this, new Theme(GlobalVars.Theme));
            InitializeComponent();
        }
        /// <summary>
        /// Set Light Theme
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.Theme = "FluentLight";
            SfSkinManager.SetTheme(this, new Theme(GlobalVars.Theme));

        }
        /// <summary>
        /// Set Dark Theme
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GlobalVars.Theme = "FluentDark";
            SfSkinManager.SetTheme(this, new Theme(GlobalVars.Theme));
        }
    }
}
