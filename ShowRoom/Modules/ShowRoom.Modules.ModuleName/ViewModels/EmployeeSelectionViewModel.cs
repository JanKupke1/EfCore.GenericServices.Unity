using Prism.Regions;
using ShowRoom.Core.Mvvm;
using ShowRoom.Modules.EmployeeManagment.DataLayer.Dtos;
using ShowRoom.Services.Interfaces;
using System.Collections.ObjectModel;

namespace ShowRoom.Modules.EmployeeManagment.ViewModels
{
    public class EmployeeSelectionViewModel : RegionViewModelBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private ObservableCollection<EmployeeDto> Employees=new ObservableCollection<EmployeeDto>();
        public ObservableCollection<EmployeeDto> PropertyName
        {
            get { return Employees; }
            set { SetProperty(ref Employees, value); }
        }


        public EmployeeSelectionViewModel(IRegionManager regionManager, IMessageService messageService) :
            base(regionManager)
        {
            Message = messageService.GetMessage();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
