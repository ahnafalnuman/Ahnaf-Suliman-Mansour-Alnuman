

namespace EmployeeManagementSystem.Entities
{
    public class VacationRequest
    {
        public int RequestId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Description { get; set; } = null!;    
        public string EmployeeNumber { get; set; } = null!; 
        public char VacationTypeCode { get; set; } 
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int TotalVacationDays { get; set; }
        public int RequestStateId { get; set; }
        public string? ApprovedByEmployeeNumber { get; set; }
        public string? DeclinedByEmployeeNumber { get; set; }

        public Employee Employee { get; set; } = null!;
        public VacationType VacationType { get; set; } = null!;    
        public RequestState RequestState { get; set; } = null!;

        public Employee?ApprovedByEmployee { get; set; }
        public Employee? DeclinedByEmployee { get; set; }

        public VacationRequest() { }

        public VacationRequest( DateTime submissionDate, string description, string employeeNumber, char vacationTypeCode, DateOnly startDate, DateOnly endDate, int totalVacationDays, int requestStateId, string? approvedByEmployeeNumber, string? declinedByEmployeeNumber)
        {
            SubmissionDate = submissionDate;
            Description = description;
            EmployeeNumber = employeeNumber;
            VacationTypeCode = vacationTypeCode;
            StartDate = startDate;
            EndDate = endDate;
            TotalVacationDays = totalVacationDays;
            RequestStateId = requestStateId;
            ApprovedByEmployeeNumber = approvedByEmployeeNumber;
            DeclinedByEmployeeNumber = declinedByEmployeeNumber;
       
        }
    }

       
}
