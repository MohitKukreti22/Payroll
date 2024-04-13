using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayRoll.Interfaces;
using PayRoll.Models;
using System.Threading.Tasks;
using PayRoll.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace PayRoll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeePayrollController : ControllerBase
    {
        private readonly ILogger<EmployeePayrollController> _logger;
        private readonly IEmployeePayrollService _employeePayrollService;

        public EmployeePayrollController(
            ILogger<EmployeePayrollController> logger,
            IEmployeePayrollService employeePayrollService)
        {
            _logger = logger;
            _employeePayrollService = employeePayrollService;
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeePayroll(int employeeId)
        {
            try
            {
                var payroll = await _employeePayrollService.GetEmployeePayroll(employeeId);
                if (payroll == null)
                {
                    return NotFound();
                }

                return Ok(payroll);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError($"Error retrieving payroll for employee ID {employeeId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}