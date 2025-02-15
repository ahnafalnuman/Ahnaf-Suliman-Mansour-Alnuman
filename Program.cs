using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new AppDbContext();

        
            var departmentService = new DepartmentService(dbContext);
            departmentService.AddDepartments();

            var positionService = new PositionService(dbContext);
            positionService.AddPositions();

            var employeeService = new EmployeeService.EmployeeAddService(dbContext);
            employeeService.AddEmployees();

            // Update Employee Data (Example)
            employeeService.UpdateEmployee("E001", "Ahnaf Alnuman", 1, 38, 7000.00m);

            // Vacation Request Services
            var requestStateService = new RequestStateService(dbContext);
            var vacationRequestService = new VacationRequestService(dbContext, requestStateService);

            var newRequest = new VacationRequestDTO
            {
                EmployeeNumber = "E009",
                StartDate = new DateOnly(2025, 3, 11),
                EndDate = new DateOnly(2025, 3, 15),
                Description = "Test",
                VacationTypeCode = 'A'
            };

            try
            {
                vacationRequestService.SubmitVacationRequest(newRequest);
                Console.WriteLine(" Leave request submitted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error: {ex.Message}");
            }

            // Get all pending requests
            Console.WriteLine("\n List of Pending Vacation Requests:");
            var pendingRequests = vacationRequestService.GetPendingRequests();

            if (!pendingRequests.Any())
            {
                Console.WriteLine(" No pending vacation requests.");
            }
            else
            {
                foreach (var request in pendingRequests)
                {
                    Console.WriteLine($"Request ID: {request.RequestId}, Employee: {request.EmployeeName}, From {request.StartDate} To {request.EndDate}, Salary: {request.Salary}");
                }
            }

            // Approve or Decline Requests
            Console.Write("\n Enter request ID to take action: ");
            if (int.TryParse(Console.ReadLine(), out int requestId))
            {
                Console.Write("Choose an action: (1) Approve - (2) Decline: ");
                string choice = Console.ReadLine();

                Console.Write("Enter the employee number performing the action: ");
                string employeeNumber = Console.ReadLine();

                try
                {
                    if (choice == "1")
                    {
                        vacationRequestService.ApproveVacationRequest(requestId, employeeNumber);
                        Console.WriteLine(" Vacation request approved successfully!");
                    }
                    else if (choice == "2")
                    {
                        vacationRequestService.DeclineVacationRequest(requestId, employeeNumber);
                        Console.WriteLine(" Vacation request declined successfully!");
                    }
                    else
                    {
                        Console.WriteLine(" Invalid option!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine(" Invalid request ID.");
            }

            // 1. Get all employees
            Console.WriteLine("\n Employee List:");
            var employees = (from e in dbContext.Employees
                             join d in dbContext.Departments on e.DepartmentId equals d.DepartmentId
                             select new EmployeeDetailsDTO
                             {
                                 EmployeeNumber = e.EmployeeNumber,
                                 EmployeeName = e.EmployeeName,
                                 Department = d.DepartmentName,
                                 Salary = e.Salary
                             }).ToList();

            employees.ForEach(e => Console.WriteLine($"{e.EmployeeNumber} - {e.EmployeeName} - {e.Department} - {e.Salary}"));

            // 2. Get employee by unique number
            string empNumber = "E002";
            var employeeDetails = GetEmployeeByNumber(dbContext, empNumber);
            if (employeeDetails != null)
            {
                Console.WriteLine($"\n Employee Details: " +
                  $"- Employee Number: {employeeDetails.EmployeeNumber} " +
                  $"- Employee Name: {employeeDetails.EmployeeName} " +
                  $"- Department: {employeeDetails.Department} " +
                  $"- Position: {employeeDetails.Position} " +
                  $"- Reports To: {employeeDetails.ReportsTo} " +
                  $"- Vacation Days Left: {employeeDetails.VacationDaysLeft}");
                    }




            // 3. Get employees with pending vacation requests
            Console.WriteLine("\n📌 Employees with Pending Vacation Requests:");
            var employeesWithPendingRequests = (from v in dbContext.VacationRequests
                                                join e in dbContext.Employees on v.EmployeeNumber equals e.EmployeeNumber
                                                where v.RequestStateId == 1 // Pending state
                                                select e).Distinct().ToList();

            employeesWithPendingRequests.ForEach(e => Console.WriteLine($"{e.EmployeeNumber} - {e.EmployeeName}"));








            // 4. Get all approved vacation requests
            Console.WriteLine("\n Approved Vacation Requests:");
            var approvedVacations = (from v in dbContext.VacationRequests
                                     join e in dbContext.Employees on v.EmployeeNumber equals e.EmployeeNumber
                                     join a in dbContext.Employees on v.ApprovedByEmployeeNumber equals a.EmployeeNumber
                                     join vt in dbContext.VacationTypes on v.VacationTypeCode equals vt.VacationTypeCode
                                     where v.RequestStateId == 2 // Approved state
                                     select new EmployeeDetailsDTO
                                     {
                                         VacationType = vt.VacationTypeName,
                                         Description = v.Description,
                                         TotalDays = v.TotalVacationDays,
                                         ApprovedBy = a.EmployeeName
                                     }).ToList();

            approvedVacations.ForEach(v => Console.WriteLine($"{v.VacationType} - {v.Description} - {v.TotalDays} days - Approved by: {v.ApprovedBy}"));

            // 5. Get all pending vacation requests employees need to take action on
            Console.WriteLine("\n Pending Vacation Requests to Take Action On:");
            var pendingActions = (from v in dbContext.VacationRequests
                                  join e in dbContext.Employees on v.EmployeeNumber equals e.EmployeeNumber
                                  where v.RequestStateId == 1 // Pending state
                                  select new EmployeeDetailsDTO
                                  {
                                      Description = v.Description,
                                      EmployeeNumber = e.EmployeeNumber,
                                      EmployeeName = e.EmployeeName,
                                      SubmittedOn = v.SubmissionDate.ToString("yyyy-MM-dd HH:mm:ss"), 
                                      Duration = v.TotalVacationDays + " days", 
                                      StartDate = v.StartDate,
                                      EndDate = v.EndDate,
                                      Salary = e.Salary
                                  }).ToList();

            pendingActions.ForEach(v => Console.WriteLine($"{v.Description} - {v.EmployeeNumber} - {v.EmployeeName} - {v.SubmittedOn} - {v.Duration} - {v.StartDate} to {v.EndDate} - Salary: {v.Salary}"));

            static EmployeeDetailsDTO? GetEmployeeByNumber(AppDbContext dbContext, string employeeNumber)
            {
                return (from e in dbContext.Employees
                        join d in dbContext.Departments on e.DepartmentId equals d.DepartmentId
                        join p in dbContext.Positions on e.PositionId equals p.PositionId
                        join r in dbContext.Employees on e.ReportedToEmployeeNumber equals r.EmployeeNumber into reportsTo
                        from r in reportsTo.DefaultIfEmpty()
                        where e.EmployeeNumber == employeeNumber
                        select new EmployeeDetailsDTO
                        {
                            EmployeeNumber = e.EmployeeNumber,
                            EmployeeName = e.EmployeeName,
                            Department = d.DepartmentName,
                            Position = p.PositionName,
                            ReportsTo = r != null ? r.EmployeeName : "N/A",
                            VacationDaysLeft = e.VacationDaysLeft
                        }).FirstOrDefault();
            }









        }
    }
    }
