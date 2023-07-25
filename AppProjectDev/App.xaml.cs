using AppProjectDev.core.DataAccess;
using AppProjectDev.Views;
using ModuleMain;
using ModuleMain.ViewModels;
using ModuleMain.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AppProjectDev
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        public App()
        {
            //Register Syncfusion license
            string key = "MTc3ODU5MUAzMjMxMmUzMTJlMzQzMVRXdngxbUdxRHJudmo5cGJackVqZSs0M1JuUTlTd21wYXZhMUNKTEZoalk9";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(key);
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Register Data Access
            containerRegistry.RegisterSingleton<IDataRepository, DataRepository>();
            containerRegistry.RegisterDialog<DialogAddProjectView, DialogAddProjectViewModel>();
            containerRegistry.RegisterDialog<DialogEndTimeView, DialogEndTimeViewModel>();
            containerRegistry.RegisterDialog<DialogEditProjectView, DialogEditProjectViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleTemplateModule>();
        }
    }
}
