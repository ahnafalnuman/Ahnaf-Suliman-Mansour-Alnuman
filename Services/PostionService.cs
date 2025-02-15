using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;
using System.Collections.Generic;

public class PositionService : IPositionService
{
    private readonly AppDbContext _dbContext;

    public PositionService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddPositions()
    {
        var positionNames = new List<string>
        {
            "Software Engineer", "Data Scientist", "Project Manager", "Marketing Manager"
            , "Financial Analyst", "HR Specialist", "Operations Manager",
            "Research Scientist", "Customer Service"
            , "Product Manager", "Business Development Manager",
            "Quality Assurance", "Training Specialist", "Network Engineer",
            "Purchasing Agent", "Compliance Officer", "Security Analyst",
            "Junior Software",  
            "Senior Software", 
            "Team Leader"            
        };

        foreach (var name in positionNames)
        {
            var position = new Position(name);
            _dbContext.Add<Position>(position);
        }

        _dbContext.SaveChanges();
    }
}