using PayRoll.DTOs;
using PayRoll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Interfaces
{
    public interface IAdminEmployeeService
    {
        //Task<Employee> AddEmployee(Employee employee);
        //Task<Employee> UpdateEmployee(Employee employee);
        //Task<Employee?> DeleteEmployee(int employeeId);
        //Task<Employee?> GetEmployeeById(int employeeId);
        //Task<List<Employee>?> GetAllEmployees();
        Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO);
        Task<EmployeeDTO?> DeleteEmployee(int employeeId);
        Task<List<EmployeeDTO>?> GetAllEmployees();
        Task<EmployeeDTO?> GetEmployeeById(int employeeId);
        Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO);
    }
}