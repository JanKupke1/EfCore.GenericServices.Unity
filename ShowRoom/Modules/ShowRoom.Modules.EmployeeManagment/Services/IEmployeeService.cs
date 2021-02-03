using ShowRoom.Modules.EmployeeManagment.DataLayer.Dtos;
using System.Collections.Generic;

namespace ShowRoom.Services.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAll();
    }
}
