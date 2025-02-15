namespace EmployeeManagementSystem.DTOs
{
  
        public class PendingVacationRequestDTO
        {
            public int RequestId { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public string Description { get; set; }
            public DateTime SubmissionDate { get; set; }
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }
            public string VacationDuration { get; set; }
            public decimal Salary { get; set; }


    }
}
