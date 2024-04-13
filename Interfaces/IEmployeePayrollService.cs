using PayRoll.Models;
using System.Threading.Tasks;

namespace PayRoll.Interfaces
{
    public interface IEmployeePayrollService
    {
        Task<Payroll?> GetEmployeePayroll(int employeeId);
    }
}