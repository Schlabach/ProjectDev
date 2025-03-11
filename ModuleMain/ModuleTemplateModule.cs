using ModuleMain.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleMain
{
    public class ModuleTemplateModule : IModule
    {
        IRegionManager regionManager;
        public ModuleTemplateModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ProjectView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}