using ShowRoom.Modules.EmployeeManagment.DataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowRoom.Services.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAll();
    }
}
