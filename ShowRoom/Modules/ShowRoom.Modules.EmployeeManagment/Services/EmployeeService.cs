using GenericServices.Unity;
using ShowRoom.Modules.EmployeeManagment.DataLayer.Dtos;
using ShowRoom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowRoom.Modules.EmployeeManagment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ICrudServices _CrudServices;

        public EmployeeService(ICrudServices crudServices)
        {
            _CrudServices = crudServices ?? throw new ArgumentNullException(nameof(crudServices));
        }

        public List<EmployeeDto> GetAll()
        {
           return _CrudServices.ReadManyNoTracked<EmployeeDto>().ToList();
        }
    }
}
