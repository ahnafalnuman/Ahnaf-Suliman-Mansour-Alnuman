using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    internal class EmployeeService
    {

        public class EmployeeAddService : IEmployeeService
        {
            private readonly AppDbContext _dbContext;

            public EmployeeAddService(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public void AddEmployees()
            {
                var employees = new List<Employee>
{
                new Employee(
                    employeeNumber: "E001",
                    employeeName: "Ahnaf Al-numan",
                    departmentId: 1, // IT Department
                    genderCode: "M",
                    positionId: 38, // Team Leader
                    reportedToEmployeeNumber: null, // Ahnaf is the team leader, no one reports to him
                    salary: 5000
                ),
                new Employee(
                    employeeNumber: "E002",
                    employeeName: "Aisha Omar",
                    departmentId: 2, // HR Department
                    genderCode: "F",
                    positionId: 24, // HR Specialist
                    reportedToEmployeeNumber: "E001", // Aisha reports to Ahnaf 
                    salary: 5500
                ),
                new Employee(
                    employeeNumber: "E003",
                    employeeName: "Khaled Mohammed",
                    departmentId: 3, // Marketing Department
                    genderCode: "M",
                    positionId: 21, // Project Manager
                    reportedToEmployeeNumber: "E001", // Khaled reports to Ahnaf 
                    salary: 6000
                ),
                new Employee(
                    employeeNumber: "E004",
                    employeeName: "Sara Hani",
                    departmentId: 4, // Sales Department
                    genderCode: "F",
                    positionId: 20, // Data Scientist
                    reportedToEmployeeNumber: "E003", // Sara reports to Khaled 
                    salary: 4500
                ),
                new Employee(
                    employeeNumber: "E005",
                    employeeName: "Ayman Ali",
                    departmentId: 5, // Finance Department
                    genderCode: "M",
                    positionId: 22, // Marketing Manager
                    reportedToEmployeeNumber: "E003", // Ayman reports to Khaled 
                    salary: 4800
                ),
                new Employee(
                    employeeNumber: "E006",
                    employeeName: "Leila Ahmed",
                    departmentId: 6, // Operations Department
                    genderCode: "F",
                    positionId: 23, // Financial Analyst
                    reportedToEmployeeNumber: "E004", // Leila reports to Sara 
                    salary: 5000
                ),
                new Employee(
                    employeeNumber: "E007",
                    employeeName: "Khalid Ahmad",
                    departmentId: 7, // Legal Department
                    genderCode: "M",
                    positionId: 25, // Operations Manager
                    reportedToEmployeeNumber: "E004", // Khalid reports to Sara 
                    salary: 5200
                ),
                new Employee(
                    employeeNumber: "E008",
                    employeeName: "Noor Abdullah",
                    departmentId: 8, // R&D Department
                    genderCode: "F",
                    positionId: 27, // Customer Service
                    reportedToEmployeeNumber: "E005", // Noor reports to Ayman 
                    salary: 5300
                ),
                new Employee(
                    employeeNumber: "E009",
                    employeeName: "Hana Ghazi",
                    departmentId: 9, // Customer Service Department
                    genderCode: "F",
                    positionId: 28, // Business Development Manager
                    reportedToEmployeeNumber: "E005", // Hana reports to Ayman (Finance)
                    salary: 5600
                ),
                new Employee(
                    employeeNumber: "E010",
                    employeeName: "Bana Malek",
                    departmentId: 10, // Logistics Department
                    genderCode: "F",
                    positionId: 29, // Quality Assurance
                    reportedToEmployeeNumber: "E006", // Bana reports to Leila 
            salary: 5800
                )
            };


                foreach (var employee in employees)
                {
                    _dbContext.Add(employee);
                }

                _dbContext.SaveChanges();
            }


            public void UpdateEmployee(string employeeNumber, string newName, int newDepartmentId, int newPositionId, decimal newSalary)
            {
                var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeNumber.ToUpper() == employeeNumber.ToUpper());

                if (employee == null)
                {
                    throw new Exception($"Employee with number {employeeNumber} does not exist.");
                }
                employee.EmployeeName = newName;
                employee.DepartmentId = newDepartmentId;
                employee.PositionId = newPositionId;
                employee.Salary = newSalary;

                _dbContext.SaveChanges();

                Console.WriteLine($"Employee {employeeNumber} information updated successfully.");
            }
        }
    }
}
