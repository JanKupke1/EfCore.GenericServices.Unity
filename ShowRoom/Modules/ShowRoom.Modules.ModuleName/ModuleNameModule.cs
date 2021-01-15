using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ShowRoom.Core;
using ShowRoom.Modules.EmployeeManagment.Views;

namespace ShowRoom.Modules.EmployeeManagment
{
    public class ModuleNameModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleNameModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
        }
    }
}