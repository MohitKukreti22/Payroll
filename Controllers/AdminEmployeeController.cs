
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayRoll.DTOs;
using PayRoll.Interfaces;
using PayRoll.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminEmployeeController : ControllerBase
    {
        private readonly ILogger<AdminEmployeeController> _logger;
        private readonly IAdminEmployeeService _adminEmployeeService;

        public AdminEmployeeController(ILogger<AdminEmployeeController> logger, IAdminEmployeeService adminEmployeeService)
        {
            _logger = logger;
            _adminEmployeeService = adminEmployeeService;
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int employeeId)
        {
            try
            {
                var employee = await _adminEmployeeService.GetEmployeeById(employeeId);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving employee with ID {employeeId}: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<EmployeeDTO>> AddEmployee(EmployeeDTO employeeDTO)
        //{
        //    try
        //    {
        //        var addedEmployee = await _adminEmployeeService.AddEmployee(employeeDTO);
        //        return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = addedEmployee.EmployeeID }, addedEmployee);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error adding employee: {ex.Message}");
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee(int employeeId, EmployeeDTO employeeDTO)
        {
            try
            {
                if (employeeId != employeeDTO.EmployeeID)
                {
                    return BadRequest("Employee ID mismatch");
                }

                var updatedEmployee = await _adminEmployeeService.UpdateEmployee(employeeDTO);
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating employee with ID {employeeId}: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{employeeId}")]
        public async Task<ActionResult<EmployeeDTO>> DeleteEmployee(int employeeId)
        {
            try
            {
                var deletedEmployee = await _adminEmployeeService.DeleteEmployee(employeeId);
                if (deletedEmployee == null)
                {
                    return NotFound();
                }
                return Ok(deletedEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting employee with ID {employeeId}: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
