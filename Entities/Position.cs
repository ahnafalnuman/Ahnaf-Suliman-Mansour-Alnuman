

namespace EmployeeManagementSystem.Entities
{
    public class Position
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; } = null!;
        public ICollection<Employee> Employees { get; set; } = null!;  
        
        public Position() { }
        public Position( string positionName)
        { 
            PositionName = positionName;
        }
    }
}
