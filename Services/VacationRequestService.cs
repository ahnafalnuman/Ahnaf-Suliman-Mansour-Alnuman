using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class VacationRequestService : IVacationRequestService
{
    private readonly AppDbContext _dbContext;
    private readonly IRequestStateService _requestStateService;

    public VacationRequestService(AppDbContext dbContext, IRequestStateService requestStateService)
    {
        _dbContext = dbContext;
        _requestStateService = requestStateService;
    }

    private void UpdateVacationBalance(Employee employee, int daysRequested)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee), "Employee data is invalid.");

        if (daysRequested <= 0)
            throw new ArgumentOutOfRangeException(nameof(daysRequested), "The number of vacation days must be a positive number.");

        if (employee.VacationDaysLeft < daysRequested)
            throw new InvalidOperationException($"Remaining vacation balance ({employee.VacationDaysLeft} days) is not enough for the request ({daysRequested} days).");

        employee.VacationDaysLeft -= daysRequested;
        _dbContext.SaveChanges();
    }

    private Employee GetEmployee(string employeeNumber)
    {
        return _dbContext.Employees.SingleOrDefault(e => e.EmployeeNumber == employeeNumber)
               ?? throw new Exception($"Employee {employeeNumber} not found.");
    }

    private VacationRequest GetVacationRequest(int requestId)
    {
        return _dbContext.VacationRequests.Find(requestId)
               ?? throw new Exception("Vacation request not found.");
    }

    private void ValidateNoOverlap(string employeeNumber, DateOnly startDate, DateOnly endDate)
    {
        var pendingStateId = _requestStateService.GetRequestStateIdByName("Pending");

        bool isOverlapping = _dbContext.VacationRequests.Any(vr =>
            vr.EmployeeNumber == employeeNumber &&
            vr.RequestStateId == pendingStateId &&
            ((startDate >= vr.StartDate && startDate <= vr.EndDate) ||
             (endDate >= vr.StartDate && endDate <= vr.EndDate) ||
             (startDate <= vr.StartDate && endDate >= vr.EndDate)));

        if (isOverlapping)
            throw new Exception("Vacation request overlaps with an existing request.");
    }

    public void SubmitVacationRequest(VacationRequestDTO requestDto)
    {
        try
        {
            ValidateEmployeeNumber(requestDto.EmployeeNumber);
            ValidateDates(requestDto.StartDate, requestDto.EndDate);
            ValidateNoOverlap(requestDto.EmployeeNumber, requestDto.StartDate, requestDto.EndDate);

            var totalVacationDays = (requestDto.EndDate.DayNumber - requestDto.StartDate.DayNumber) + 1;
            var pendingStateId = _requestStateService.GetRequestStateIdByName("Pending");
            ValidateRequestStateId(pendingStateId);

            var newRequest = new VacationRequest
            {
                SubmissionDate = DateTime.Now,
                Description = requestDto.Description,
                EmployeeNumber = requestDto.EmployeeNumber,
                VacationTypeCode = requestDto.VacationTypeCode,
                StartDate = requestDto.StartDate,
                EndDate = requestDto.EndDate,
                TotalVacationDays = totalVacationDays,
                RequestStateId = pendingStateId
            };

            _dbContext.VacationRequests.Add(newRequest);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("An error occurred while saving the vacation request.", ex);
        }
    }

    public IEnumerable<PendingVacationRequestDTO> GetPendingRequests()
    {
        var pendingStateId = _requestStateService.GetRequestStateIdByName("Pending");

        var query = from vr in _dbContext.VacationRequests.AsNoTracking()
                    join e in _dbContext.Employees.AsNoTracking() on vr.EmployeeNumber equals e.EmployeeNumber
                    join rs in _dbContext.RequestStates.AsNoTracking() on vr.RequestStateId equals rs.StateId
                    where vr.RequestStateId == pendingStateId
                    select new PendingVacationRequestDTO
                    {
                        RequestId = vr.RequestId,
                        EmployeeNumber = e.EmployeeNumber,
                        EmployeeName = e.EmployeeName,
                        Description = vr.Description,
                        SubmissionDate = vr.SubmissionDate,
                        StartDate = vr.StartDate,
                        EndDate = vr.EndDate,
                        VacationDuration = $"{vr.TotalVacationDays} Days",
                        Salary = e.Salary
                    };

        return query.ToList();
    }

    public void ApproveVacationRequest(int requestId, string approvedByEmployeeNumber)
    {
        var request = GetVacationRequest(requestId);
        var employee = GetEmployee(request.EmployeeNumber);

        UpdateVacationBalance(employee, request.TotalVacationDays);

        var approvedStateId = _requestStateService.GetRequestStateIdByName("Approved");
        ValidateRequestStateId(approvedStateId);

        UpdateRequestState(request, approvedStateId, approvedByEmployeeNumber);
    }

    public void DeclineVacationRequest(int requestId, string declinedByEmployeeNumber)
    {
        var request = GetVacationRequest(requestId);

        var declinedStateId = _requestStateService.GetRequestStateIdByName("Declined");
        ValidateRequestStateId(declinedStateId);

        UpdateRequestState(request, declinedStateId, declinedByEmployeeNumber);
    }



    

    private void UpdateRequestState(VacationRequest request, int stateId, string employeeNumber)
    {
        request.RequestStateId = stateId;
        var stateName = _requestStateService.GetRequestStateNameById(stateId);

        if (stateName == "Approved")
            request.ApprovedByEmployeeNumber = employeeNumber;
        else if (stateName == "Declined")
            request.DeclinedByEmployeeNumber = employeeNumber;

        _dbContext.SaveChanges();
    }

    private void ValidateRequestStateId(int stateId)
    {
        var stateExists = _dbContext.RequestStates.Any(rs => rs.StateId == stateId);
        if (!stateExists)
            throw new Exception($"RequestStateId {stateId} does not exist in the RequestStates table.");
    }

    private void ValidateDates(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
            throw new Exception("Start date cannot be after end date.");
    }

    private void ValidateEmployeeNumber(string employeeNumber)
    {
        var employeeExists = _dbContext.Employees.Any(e => e.EmployeeNumber == employeeNumber);
        if (!employeeExists)
            throw new Exception($"Employee with number {employeeNumber} does not exist.");
    }

    public void UpdateVacationRequest(int requestId, VacationRequestDTO updatedRequestDto)
    {
        var request = GetVacationRequest(requestId);
        if (request.RequestStateId != _requestStateService.GetRequestStateIdByName("Pending"))
        {
            throw new InvalidOperationException("Cannot update a request that is already processed.");
        }

        ValidateDates(updatedRequestDto.StartDate, updatedRequestDto.EndDate);
        ValidateNoOverlap(updatedRequestDto.EmployeeNumber, updatedRequestDto.StartDate, updatedRequestDto.EndDate);

        request.Description = updatedRequestDto.Description;
        request.StartDate = updatedRequestDto.StartDate;
        request.EndDate = updatedRequestDto.EndDate;
        request.VacationTypeCode = updatedRequestDto.VacationTypeCode;
        request.TotalVacationDays = (updatedRequestDto.EndDate.DayNumber - updatedRequestDto.StartDate.DayNumber) + 1;

        _dbContext.SaveChanges();
    }


}