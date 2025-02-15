using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DTOs
{
    public class EmployeeDetailsDTO
    {
        public string EmployeeNumber { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string PositionName { get; set; } = null!;
        public decimal Salary { get; set; }
        public string Position { get; set; } = null!;
        public string? ReportsTo { get; set; }
        public int VacationDaysLeft { get; set; } 
        public string VacationType { get; set; } = null!;   
        public string ReportedToEmployeeNumber { get; set; } =null!;
        public string? ApprovedBy { get; set; }
        public int TotalDays { get; set; }

        public DateOnly SubmissionDate { get; set; }    

        public string Description { get; set; } = null!;   

        public string SubmittedOn { get; set; } = null!;
        public string Duration { get; set; } = null!; 
        public DateOnly StartDate { get; set; } 
        public DateOnly EndDate { get; set; } 


    }
}
