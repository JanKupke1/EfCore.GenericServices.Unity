using Prism.Ioc;
using Prism.Modularity;
using ShowRoom.Modules.EmployeeManagment;
using ShowRoom.Services;
using ShowRoom.Services.Interfaces;
using ShowRoom.Views;
using System.Windows;

namespace ShowRoom
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<EmployeeManagmentModule>();
        }
    }
}
