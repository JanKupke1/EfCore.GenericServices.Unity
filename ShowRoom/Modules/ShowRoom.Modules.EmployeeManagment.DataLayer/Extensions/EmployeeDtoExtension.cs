﻿using ShowRoom.Modules.EmployeeManagment.DataLayer.Dtos;

namespace ShowRoom.Modules.EmployeeManagment.DataLayer.Extensions
{
    public static class EmployeeDtoExtension
    {
        public static string FullName(this EmployeeDto employeeDto)
        {
            return $"{employeeDto.FirstName} {employeeDto.LastName}";
        }
    }
}
