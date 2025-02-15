using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _dbContext;

    public DepartmentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddDepartments()
    {
        var departmentNames = new List<string>
        {
            "IT", "HR", "Marketing", "Sales", "Finance", "Operations", "Legal", "R&D",
            "Customer Service", "Logistics", "Product Management", "Business Development",
            "Quality Assurance", "Training", "Engineering", "Purchasing", "Compliance",
            "Security", "Administration", "Health & Safety"
        };

  
        foreach (var name in departmentNames)
        {
            var department = new Department(name);
            _dbContext.Departments.Add(department);
        }

        _dbContext.SaveChanges();
    }
}
