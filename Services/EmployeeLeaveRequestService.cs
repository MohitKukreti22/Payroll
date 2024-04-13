using Microsoft.Extensions.Logging;
using PayRoll.Exceptions;
using PayRoll.Interfaces;
using PayRoll.Models;
using PayRoll.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayRoll.Services
{
    public class EmployeeLeaveRequestService : IEmployeeLeaveRequestService
    {
        private readonly ILogger<EmployeeLeaveRequestService> _logger;
        private readonly IRepository<int, LeaveRequest> _leaveRequestRepository;
        private readonly IRepository<int, Employee> _employeeRepository;

        public EmployeeLeaveRequestService(ILogger<EmployeeLeaveRequestService> logger, IRepository<int, LeaveRequest> leaveRequestRepository, IRepository<int, Employee> employeeRepository)
        {
            _logger = logger;
            _leaveRequestRepository = leaveRequestRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<LeaveRequest> RequestLeave(int employeeId, string leaveType, DateTime startDate, DateTime endDate)
        {
            if (startDate.Date == endDate.Date)
            {
                throw new ArgumentException("Invalid Date.");
            }
            if (endDate <= startDate)
            {
                throw new DateException();
            }

            var employee = await _employeeRepository.Get(employeeId);
            if (employee == null)
            {
                throw new EmployeeException();
            }
            var leaveRequest = new LeaveRequest
            {
                EmployeeID = employeeId,
                LeaveType = leaveType,
                StartDate = startDate,
                EndDate = endDate,
                Status = "Pending",
                ApprovedAt = DateTime.MinValue
            };

            return await _leaveRequestRepository.Add(leaveRequest);
        }

        public async Task<List<LeaveRequest>> GetEmployeeLeaveRequests(int employeeId)
        {
            var allLeaveRequests = await _leaveRequestRepository.GetAll();
            if (allLeaveRequests != null)
            {
                return allLeaveRequests.Where(lr => lr.EmployeeID == employeeId).ToList();
            }
            return new List<LeaveRequest>();
        }
    }
}
