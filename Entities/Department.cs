

namespace EmployeeManagementSystem.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public ICollection<Employee> Employees { get; set; } =null!;

        public Department() { } 
        public Department( string departmentName)
        {
            DepartmentName = departmentName;
        }   
    }

}