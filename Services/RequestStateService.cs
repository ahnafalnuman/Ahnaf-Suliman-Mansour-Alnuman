using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;
using System.Linq;

namespace EmployeeManagementSystem.Services
{
    public class RequestStateService : IRequestStateService
    {
        private readonly AppDbContext _dbContext;

        public RequestStateService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetRequestStateIdByName(string stateName)
        {
            return _dbContext.RequestStates
                   .Where(rs => rs.StateName == stateName)
                   .Select(rs => rs.StateId)
                   .FirstOrDefault();
        }

        public string GetRequestStateNameById(int stateId)
        {
            return _dbContext.RequestStates
                   .Where(rs => rs.StateId == stateId)
                   .Select(rs => rs.StateName)
                   .FirstOrDefault() ?? "Unknown";
        }
    }
}
