using Castle.Core.Resource;
using PayRoll.Models;
using PayRoll.Services;
namespace PayRoll.Interfaces
    
{
    public interface IEmployeeService
    {

        
        public Task<Employee> ChangeEmployeePhoneAsync(int id, string phone);
        public Task<Employee> ChangeEmployeeName(int id, string name);
        public Task<Employee> ChangeEmployeeAddress(int id, string address);
        public Task<Employee> DeleteEmployee(int id);
        Task<bool> UpdateEmployeePassword(string email, string newPassword);
        public  Task<Employee> GetEmployeeByEmail(string email);


    }
}