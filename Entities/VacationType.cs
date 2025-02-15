

namespace EmployeeManagementSystem.Entities
{
    public class VacationType
    {
        public char VacationTypeCode { get; set; }
        public string VacationTypeName { get; set; } = null!;
        public ICollection<VacationRequest> VacationRequests { get; set; } =null!;

        public VacationType() { }   
        public VacationType(char vacationTypeCode, string vacationTypeName)
        {
            VacationTypeCode = vacationTypeCode;
            VacationTypeName = vacationTypeName;
        }
    }
}
