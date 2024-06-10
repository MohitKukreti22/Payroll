using PayRoll.Interfaces;
using PayRoll.Models;
using PayRoll.Repositories;
using System.Threading.Tasks;
using PayRoll.Exceptions;

namespace PayRoll.Services
{
    public class EmployeePayrollService : IEmployeePayrollService
    {
        private readonly ILogger<EmployeePayrollService> _logger;
        private readonly IRepository<int, Payroll> _payrollRepository;
        private readonly IRepository<int, Employee> _employeeRepository;

        public EmployeePayrollService(
            ILogger<EmployeePayrollService> logger,
            IRepository<int, Payroll> payrollRepository,
            IRepository<int, Employee> employeeRepository)
        {
            _logger = logger;
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Payroll>?> GetEmployeePayroll(int employeeId)
        {
            try
            {
                // Check if the employee exists
                var employee = await _employeeRepository.Get(employeeId);
                if (employee == null)
                {
                    _logger.LogError($"Employee with ID {employeeId} not found.");
                    return null;
                }

                // Retrieve all payroll records from the repository
                var allPayrolls = await _payrollRepository.GetAll();

                // Filter the payroll records for the specific employee
                var employeePayrolls = allPayrolls?.Where(p => p.EmployeeID == employeeId).ToList();

                return employeePayrolls;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving payroll for employee ID {employeeId}: {ex.Message}");

                throw new EmployeeException();
            }
        }


    }
}