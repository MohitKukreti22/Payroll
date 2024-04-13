using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Exceptions;
using PayRoll.Interfaces;
using PayRoll.Services;
using System;
using System.Threading.Tasks;

namespace PayRoll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeLeaveRequestController : ControllerBase
    {
        private readonly IEmployeeLeaveRequestService _leaveRequestService;
        private readonly ILogger<EmployeeLeaveRequestController> _logger;

        public EmployeeLeaveRequestController(IEmployeeLeaveRequestService leaveRequestService, ILogger<EmployeeLeaveRequestController> logger)
        {
            _leaveRequestService = leaveRequestService;
            _logger = logger;
        }
        [Authorize(Roles = "Employee")]
        [HttpPost("request")]
        public async Task<IActionResult> RequestLeave(int employeeId, string leaveType, DateTime startDate, DateTime endDate)
        {
            try
            {
                var leaveRequest = await _leaveRequestService.RequestLeave(employeeId, leaveType, startDate, endDate);
                return Ok(leaveRequest);
            }
            catch(DateException ex)
            {
                _logger.LogError($"Error adding timesheet: {ex.Message}");
                return StatusCode(500, $"Invalid Date: {ex.Message}");
            }
        catch(ArgumentException ex)
            {
                _logger.LogError($"Error adding timesheet: {ex.Message}");
                return StatusCode(500, $"StartDate And EndDate Cannot be Same: {ex.Message}");
            }
           
            catch (Exception ex)
            {
                _logger.LogError($"Error adding timesheet: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Employee")]
        [HttpGet("{employeeId}/leave-requests")]
        public async Task<IActionResult> GetEmployeeLeaveRequests(int employeeId)
        {
            try
            {
                var leaveRequests = await _leaveRequestService.GetEmployeeLeaveRequests(employeeId);
                return Ok(leaveRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
