using PayRoll.Models;
using System.Threading.Tasks;

namespace PayRoll.Interfaces
{
    public interface IEmployeePayrollService
    {
        Task<List<Payroll>?> GetEmployeePayroll(int employeeId);
    }
}