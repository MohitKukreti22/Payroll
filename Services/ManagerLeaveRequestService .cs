using Microsoft.Extensions.Logging;
using PayRoll.Interfaces;
using PayRoll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayRoll.Services
{
    public class ManagerLeaveRequestService : IManagerLeaveRequestService
    {
        private readonly IRepository<int, LeaveRequest> _leaveRequestRepository;
        private readonly ILogger<ManagerLeaveRequestService> _logger;

        public ManagerLeaveRequestService(IRepository<int, LeaveRequest> leaveRequestRepository, ILogger<ManagerLeaveRequestService> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _logger = logger;
        }

        public async Task<List<LeaveRequest>> GetAllLeaveRequestsByEmployeeId()
        {
            try
            {
                var allLeaveRequests = await _leaveRequestRepository.GetAll();
                if (allLeaveRequests != null)
                {
                    return allLeaveRequests.Where(lr => lr.Status=="Pending").ToList();
                }
                return new List<LeaveRequest>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting leave requests ");
                throw;
            }
        }

        public async Task<LeaveRequest> ApproveLeaveRequestByEmployeeId(int employeeId)
        {
            try
            {
                var leaveRequests = await _leaveRequestRepository.GetAll();
                if (leaveRequests != null && leaveRequests.Any())
                {
                    var leaveRequestToApprove = leaveRequests.FirstOrDefault(lr => lr.EmployeeID == employeeId);
                    if (leaveRequestToApprove != null)
                    {
                        leaveRequestToApprove.Status = "Approved";
                        leaveRequestToApprove.ApprovedAt = DateTime.Now;
                        return await _leaveRequestRepository.Update(leaveRequestToApprove);
                    }
                    throw new Exception("No leave requests found for the employee.");
                }
                throw new Exception("No leave requests found for the employee.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while approving leave request by employee ID {EmployeeId}", employeeId);
                throw;
            }
        }

        public async Task<LeaveRequest> RejectLeaveRequestByEmployeeId(int employeeId)
        {
            try
            {
                var leaveRequests = await _leaveRequestRepository.GetAll();
                if (leaveRequests != null && leaveRequests.Any())
                {
                    var leaveRequestToReject = leaveRequests.FirstOrDefault(lr => lr.EmployeeID == employeeId);
                    if (leaveRequestToReject != null)
                    {
                        leaveRequestToReject.Status = "Rejected";
                        leaveRequestToReject.ApprovedAt = DateTime.Now;
                        return await _leaveRequestRepository.Update(leaveRequestToReject);
                    }
                    throw new Exception("No leave requests found for the employee.");
                }
                throw new Exception("No leave requests found for the employee.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while rejecting leave request by employee ID {EmployeeId}", employeeId);
                throw;
            }
        }
    }
}
