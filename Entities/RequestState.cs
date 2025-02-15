

namespace EmployeeManagementSystem.Entities
{
    public class RequestState
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public ICollection<VacationRequest> VacationRequests { get; set; } = null!;

        public RequestState() { }
        public RequestState(int stateId, string stateName)
        {
            StateId = stateId;
            StateName = stateName;
        }
    }
}
