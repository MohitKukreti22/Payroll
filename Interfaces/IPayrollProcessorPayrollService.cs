using PayRoll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Interfaces
{
    public interface IPayrollProcessorPayrollService
    {
        Task<Payroll> AddPayroll(int employeeId, int payrollMonth, string status, int payrollYear, double totalEarnings, double totalDeductions, int payrollProcessorId);
        Task<List<Payroll>> GetAllEmployeePayrolls();
    }
}
