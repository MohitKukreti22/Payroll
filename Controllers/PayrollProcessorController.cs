using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Exceptions;
using PayRoll.Interfaces;
using PayRoll.Models;
using PayRoll.Models.DTOs;
using PayRoll.Services;

namespace PayRoll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollProcessorController : ControllerBase
    {
        private readonly IPayrollProcessorLoginService _payrollProcessorLoginService;
        private readonly ILogger<PayrollProcessorController> _logger;
        private readonly IPayrollProcessorService _payrollProcessorService;
        public PayrollProcessorController(IPayrollProcessorLoginService payrollProcessorLoginService, ILogger<PayrollProcessorController> logger, IPayrollProcessorService payrollProcessorService)
        {
            _payrollProcessorService = payrollProcessorService;
            _logger = logger;
            _payrollProcessorLoginService = payrollProcessorLoginService;

        }
        
        [HttpPost("Register")]
        public async Task<ActionResult<LoginUserDTO>> Register(RegisterPayrollProcessorDTO user)
        {
            try
            {
                var result = await _payrollProcessorLoginService.Register(user);
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
                var result = await _payrollProcessorLoginService.Login(user);
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

        [Authorize(Roles = "PayrollProcessor")]
        [HttpPut("{id}/change-phone")]
        public async Task<IActionResult> ChangePayrollProcessorPhoneAsync(int id, string phone)
        {
            try
            {
                var updatedPayrollProcessor = await _payrollProcessorService.ChangePayrollProcessorPhoneAsync(id, phone);
                return Ok(updatedPayrollProcessor);
            }
            catch (AdminException ex)
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


        [Authorize(Roles = "PayrollProcessor")]
        [HttpPut("{id}/change-name")]
        public async Task<IActionResult> ChangePayrollProcessorName(int id, string name)
        {
            try
            {
                var updatedPayrollProcessor = await _payrollProcessorService.ChangePayrollProcessorName(id, name);
                return Ok(updatedPayrollProcessor);
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

        [Authorize(Roles = "PayrollProcessor")]
      
        [HttpGet("GetAllPayrollProcessor")]
        public async Task<ActionResult<List<PayrollProcessor>>> GetAllPayrollProcessor()
        {
            try
            {
                return await _payrollProcessorService.GetAllPayrollProcessor();
            }
            catch (AdminException e)
            {
                _logger.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
        [Authorize(Roles = "PayrollProcessor")]
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetPayrollProcessorByEmail(string email)
        {
            try
            {
                var payrollProcessor = await _payrollProcessorService.GetPayrollProcessorByEmail(email);
                if (payrollProcessor != null)
                {
                    return Ok(payrollProcessor);
                }
                else
                {
                    return NotFound($"Payroll processor with email {email} not found.");
                }
            }
            catch (AdminException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






    }
}
