using PIB.Lab.OrderControl.Common.BaseClasses;
using ShowRoom.Modules.EmployeeManagment.DataLayer.Dtos;
using ShowRoom.Services.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace ShowRoom.Modules.EmployeeManagment.Commands
{
    public class FillAllEmployeesCommand : SyncCommandBase
    {
        private readonly ObservableCollection<EmployeeDto> _Employees;
        private readonly IEmployeeService _EmployeeService;

        public FillAllEmployeesCommand(ObservableCollection<EmployeeDto> employees, IEmployeeService employeeService)
        {
            _Employees = employees ?? throw new ArgumentNullException(nameof(employees));
            _EmployeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        public override void ExecuteSync(object parameter)
        {
            _Employees.Clear();
            var allEmployees = _EmployeeService.GetAll();
            _Employees.AddRange(allEmployees);
        }
    }
}
