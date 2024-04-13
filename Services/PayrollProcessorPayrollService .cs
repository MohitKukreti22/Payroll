using PayRoll.Exceptions;
using PayRoll.Interfaces;
using PayRoll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayRoll.Services
{
    public class PayrollProcessorPayrollService : IPayrollProcessorPayrollService
    {
        private readonly IRepository<int, Payroll> _payrollRepository;

        public PayrollProcessorPayrollService(IRepository<int, Payroll> payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }

        public async Task<Payroll> AddPayroll(int employeeId, int payrollMonth, string status, int payrollYear, double totalEarnings, double totalDeductions, int payrollProcessorId)
        {
            // Check if a payroll for the specified employee and month already exists
            var allPayrolls = await _payrollRepository.GetAll();
            if (allPayrolls != null && allPayrolls.Any(p => p.EmployeeID == employeeId && p.PayrollMonth == payrollMonth ))
            {
                throw new PayrollException();
            }
            if (totalEarnings <= 0 || totalDeductions <= 0)
            {
                throw new ArgumentException("Total earnings and total deductions must be greater than zero.");
            }

            // Check if totalEarnings is greater than totalDeductions
            if (totalEarnings <= totalDeductions)
            {
                throw new DeductionException();
            }

            // Calculate NetSalary
            double netSalary = totalEarnings - totalDeductions;

            // Create Payroll instance
            var payroll = new Payroll(employeeId, payrollMonth, status, payrollYear, totalEarnings, totalDeductions, netSalary, payrollProcessorId, null, null);

            // Add payroll to repository
            return await _payrollRepository.Add(payroll);
        }

        public async Task<List<Payroll>> GetAllEmployeePayrolls()
        {
            return await _payrollRepository.GetAll();
        }
    }
}
