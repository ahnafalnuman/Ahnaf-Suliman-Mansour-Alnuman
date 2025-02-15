using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IEmployeeService
    {
        void AddEmployees();
        void UpdateEmployee(string employeeNumber, string newName, int newDepartmentId, int newPositionId, decimal newSalary);
    }
}
