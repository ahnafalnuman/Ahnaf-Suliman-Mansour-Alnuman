using EmployeeManagementSystem.DTOs;
using System.Linq;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IVacationRequestService
    {
        // Submit a new vacation request
        void SubmitVacationRequest(VacationRequestDTO requestDto);

        // Get all pending vacation requests
        IEnumerable<PendingVacationRequestDTO> GetPendingRequests();

        // Approve a vacation request
        void ApproveVacationRequest(int requestId, string approvedByEmployeeNumber);

        // Decline a vacation request
        void DeclineVacationRequest(int requestId, string declinedByEmployeeNumber);
    }
}
