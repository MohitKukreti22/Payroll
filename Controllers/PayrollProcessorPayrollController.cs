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
    public class PayrollProcessorPayrollController : ControllerBase
    {
        private readonly IPayrollProcessorPayrollService _payrollService;
        private readonly ILogger<PayrollProcessorPayrollController> _logger;

        public PayrollProcessorPayrollController(IPayrollProcessorPayrollService payrollService, ILogger<PayrollProcessorPayrollController> logger)
        {
            _payrollService = payrollService;
            _logger = logger;
        }

        [Authorize(Roles = "PayrollProcessor")]
        [HttpPost("add")]
        public async Task<IActionResult> AddPayroll(int employeeId, int payrollMonth, string status, int payrollYear, double totalEarnings, double totalDeductions, int payrollProcessorId)
        {
            try
            {
                var payroll = await _payrollService.AddPayroll(employeeId, payrollMonth, status, payrollYear, totalEarnings, totalDeductions, payrollProcessorId);
                return Ok(payroll);
            }
            catch(ArgumentException ex)
            {
                return StatusCode(500, $"Total earnings and total deductions must be greater than zero.: {ex.Message}");
            }

            catch (DeductionException ex)
            {
                return StatusCode(500, $"Total earnings and total deductions must be greater than zero.: {ex.Message}");
            }


            catch (Exception ex)
            {
                _logger.LogError($"Error adding payroll: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "PayrollProcessor")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployeePayrolls()
        {
            try
            {
                var payrolls = await _payrollService.GetAllEmployeePayrolls();
                return Ok(payrolls);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving employee payrolls: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
