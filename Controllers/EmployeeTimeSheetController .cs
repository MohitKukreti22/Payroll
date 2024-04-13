using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayRoll.Exceptions;
using PayRoll.Interfaces;
using PayRoll.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeTimeSheetController : ControllerBase
    {
        private readonly IEmployeeTimeSheetService _timeSheetService;
        private readonly ILogger<EmployeeTimeSheetController> _logger;

        public EmployeeTimeSheetController(IEmployeeTimeSheetService timeSheetService, ILogger<EmployeeTimeSheetController> logger)
        {
            _timeSheetService = timeSheetService;
            _logger = logger;
        }
        [Authorize(Roles ="Employee")]
        [HttpPost("add")]
        public async Task<IActionResult> AddTimeSheet(int employeeId, DateTime weekStartDate, DateTime weekEndDate, double totalHoursWorked, string status)
        {
            try
            {
               
                DateTime approvedAt = DateTime.Now;

                var timeSheet = await _timeSheetService.AddTimeSheet(employeeId, weekStartDate, weekEndDate, totalHoursWorked, status, approvedAt);
                return Ok(timeSheet);
            }
        
            catch (TimeSheetException ex)
            {
                _logger.LogError($"Error adding timesheet: {ex.Message}");
                return StatusCode(500, $" {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding timesheet: {ex.Message}");
                return StatusCode(500, $" {ex.Message}");
            }
        }
        [Authorize(Roles = "Employee")]
        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetTimeSheetsByEmployeeId(int employeeId)
        {
            try
            {
                var timeSheets = await _timeSheetService.GetTimesheetsByEmployeeId(employeeId);
                return Ok(timeSheets);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving timesheets for employee ID {employeeId}: {ex.Message}");
                return StatusCode(500, $" {ex.Message}");
            }
        }
    }
}
