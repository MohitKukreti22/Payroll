using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Models;
using PayRoll.Models.DTOs;
using PayRoll.Interfaces;
using PayRoll.Exceptions;
using PayRoll.Contexts;
using PayRoll.Mappers;
using PayRoll.Services;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace PayRoll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    [EnableCors("ReactPolicy")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeLoginService _employeeLoginService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeLoginService employeeLoginService, ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _employeeLoginService = employeeLoginService;
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<LoginUserDTO>> Register(RegisterEmployeeDTO user)
        {
            try
            {
                var result = await _employeeLoginService.Register(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Login")]

        public async Task<ActionResult<LoginUserDTO>> Login(LoginUserDTO user)
        {
            try
            {
                var result = await _employeeLoginService.Login(user);
                return Ok(result);
            }
            catch (UserException iuse)
            {
                _logger.LogError(iuse.Message);
                return Unauthorized("Invalid username or password");
            }
            catch (DeactivatedUserException)
            {
                return Unauthorized("User deactivated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [Authorize(Roles = "Employee")]
        [HttpPut("{id}/change-phone")] 
      
        public async Task<IActionResult> ChangeEmployeePhoneAsync(int id, string phone)
        {
            try
            {
                var updatedEmployee = await _employeeService.ChangeEmployeePhoneAsync(id, phone);
                return Ok(updatedEmployee);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, $"Error changing customer phone: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing customer phone.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [Authorize(Roles = "Employee")]
        [HttpPut("{id}/change-name")]
        public async Task<IActionResult> ChangeEmployeeName(int id, string name)
        {
            try
            {
                var updatedEmployee = await _employeeService.ChangeEmployeeName(id, name);
                return Ok(updatedEmployee);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, $"Error changing customer name: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing customer name.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [Authorize(Roles = "Employee")]
        [HttpPut("{id}/change-address")]
        public async Task<IActionResult> ChangeEmployeeAddress(int id, string address)
        {
            try
            {
                var updatedEmployee = await _employeeService.ChangeEmployeeAddress(id, address);
                return Ok(updatedEmployee);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, $"Error changing customer address: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing customer address.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("delete-customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var deletedEmployee = await _employeeService.DeleteEmployee(id);
                return Ok(deletedEmployee);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, $"Error deleting customer: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost("update-password")]
        public async Task<IActionResult> UpdateEmployeerPassword(string email, string newPassword)
        {
            try
            {
                var updated = await _employeeService.UpdateEmployeePassword(email, newPassword);

                return Ok("Password updated successfully.");

            }
            catch (ValidationNotFoundException ex)
            {
                _logger.LogError(ex, $"Error updating password: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating password.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> GetEmployeeByEmail(string email)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByEmail(email);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound($"Employee with email {email} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}


   