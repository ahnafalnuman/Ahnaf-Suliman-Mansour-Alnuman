

namespace EmployeeManagementSystem.Entities
{
    public class Employee
    {
        public string EmployeeNumber { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string ?GenderCode { get; set; }
        public int PositionId { get; set; }
        public string ? ReportedToEmployeeNumber { get; set; }
        public int VacationDaysLeft { get; set; }
        public decimal Salary { get; set; }

        public Department Department { get; set; } = null!; 
        public Position Position { get; set; } = null!;
        public Employee ? ReportedToEmployee { get; set; }
        public ICollection<Employee> ? Subordinates { get; set; }
        public ICollection<VacationRequest> VacationRequests { get; set; } = null!;
        public ICollection<VacationRequest> ? ApprovedVacationRequests { get; set; }


        public Employee() { }

        public Employee(string employeeNumber, string employeeName, int departmentId, string? genderCode, int positionId, string? reportedToEmployeeNumber, decimal salary)
        {
            EmployeeNumber = employeeNumber;
            EmployeeName = employeeName;
            DepartmentId = departmentId;
            GenderCode = genderCode;
            PositionId = positionId;
            ReportedToEmployeeNumber = reportedToEmployeeNumber;
            VacationDaysLeft = 24; // defult value 
            Salary = salary;

        }   
    }
}
