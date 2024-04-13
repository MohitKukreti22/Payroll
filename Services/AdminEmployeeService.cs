
using Microsoft.Extensions.Logging;
using PayRoll.Exceptions;
using PayRoll.Interfaces;
using PayRoll.Models;
using PayRoll.Repositories;
using PayRoll.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Services
{
    public class AdminEmployeeService : IAdminEmployeeService
    {
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly ILogger<AdminEmployeeService> _logger;

        public AdminEmployeeService(IRepository<int, Employee> employeeRepository, ILogger<AdminEmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO)
        {
            var employee = MapToEmployeeModel(employeeDTO);
            var addedEmployee = await _employeeRepository.Add(employee);
            _logger.LogInformation($"Employee added: {addedEmployee.EmployeeID}");
            return MapToEmployeeDTO(addedEmployee);
        }

        public async Task<EmployeeDTO?> DeleteEmployee(int employeeId)
        {
            var deletedEmployee = await _employeeRepository.Delete(employeeId);
            if (deletedEmployee != null)
            {
                _logger.LogInformation($"Employee deleted: {deletedEmployee.EmployeeID}");
                return MapToEmployeeDTO(deletedEmployee);
            }
            else
            {
                _logger.LogWarning($"Employee with ID {employeeId} not found.");
                return null;
            }
        }

        public async Task<List<EmployeeDTO>?> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAll();
            if (employees != null)
            {
                var employeeDTOs = new List<EmployeeDTO>();
                foreach (var employee in employees)
                {
                    employeeDTOs.Add(MapToEmployeeDTO(employee));
                }
                return employeeDTOs;
            }
            return null;
        }

        public async Task<EmployeeDTO?> GetEmployeeById(int employeeId)
        {
            var employee = await _employeeRepository.Get(employeeId);
            if (employee != null)
            {
                return MapToEmployeeDTO(employee);
            }
            return null;
        }

        public async Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO)
        {
            var employee = MapToEmployeeModel(employeeDTO);
            if (await _employeeRepository.Get(employee.EmployeeID) == null)
            {
                throw new EmployeeException();
            }
            var updatedEmployee = await _employeeRepository.Update(employee);
            _logger.LogInformation($"Employee updated: {updatedEmployee.EmployeeID}");
            return MapToEmployeeDTO(updatedEmployee);
        }

        private EmployeeDTO MapToEmployeeDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                EmployeeID = employee.EmployeeID,
                EmployeeName = employee.EmployeeName,
                DateOfBirth = employee.DateOfBirth,
                JoiningDate = employee.JoiningDate,
                Department = employee.Department,
                Position = employee.Position,
                ContactNumber = employee.ContactNumber,
                Address = employee.Address,
                BankDetails = employee.BankDetails,
                TaxInformation = employee.TaxInformation,
                Email = employee.Email
            };
        }

        private Employee MapToEmployeeModel(EmployeeDTO employeeDTO)
        {
            return new Employee
            {
                EmployeeID = employeeDTO.EmployeeID,
                EmployeeName = employeeDTO.EmployeeName,
                DateOfBirth = employeeDTO.DateOfBirth,
                JoiningDate = employeeDTO.JoiningDate,
                Department = employeeDTO.Department,
                Position = employeeDTO.Position,
                ContactNumber = employeeDTO.ContactNumber,
                Address = employeeDTO.Address,
                BankDetails = employeeDTO.BankDetails,
                TaxInformation = employeeDTO.TaxInformation,
                Email = employeeDTO.Email
            };
        }
    }
}
