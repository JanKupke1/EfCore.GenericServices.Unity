using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ShowRoom.Core;
using ShowRoom.Modules.EmployeeManagment.Views;

namespace ShowRoom.Modules.EmployeeManagment
{
    public class EmployeeManagmentModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public EmployeeManagmentModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "EmployeeSelectionView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<EmployeeSelectionView>();
        }
    }
}