using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayRoll.Interfaces;
using PayRoll.Services;
using System;
using System.Threading.Tasks;

namespace PayRoll.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ManagerLeaveRequestController : ControllerBase
    {
        private readonly IManagerLeaveRequestService _leaveRequestService;
        private readonly ILogger<ManagerLeaveRequestController> _logger;

        public ManagerLeaveRequestController(IManagerLeaveRequestService leaveRequestService, ILogger<ManagerLeaveRequestController> logger)
        {
            _leaveRequestService = leaveRequestService;
            _logger = logger;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("/leave-requests")]
        public async Task<IActionResult> GetAllLeaveRequestsByEmployeeId()
        {
            try
            {
                var leaveRequests = await _leaveRequestService.GetAllLeaveRequestsByEmployeeId();
                return Ok(leaveRequests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting leave requests ");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles ="Manager")]
        [HttpPost("employees/{employeeId}/leave-requests/approve")]
        public async Task<IActionResult> ApproveLeaveRequestByEmployeeId(int employeeId)
        {
            try
            {
                var approvedLeaveRequest = await _leaveRequestService.ApproveLeaveRequestByEmployeeId(employeeId);
                return Ok(approvedLeaveRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while approving leave request by employee ID {EmployeeId}", employeeId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("employees/{employeeId}/leave-requests/reject")]
        public async Task<IActionResult> RejectLeaveRequestByEmployeeId(int employeeId)
        {
            try
            {
                var rejectedLeaveRequest = await _leaveRequestService.RejectLeaveRequestByEmployeeId(employeeId);
                return Ok(rejectedLeaveRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while rejecting leave request by employee ID {EmployeeId}", employeeId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
