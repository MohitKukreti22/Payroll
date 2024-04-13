using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PayRoll.Models;
using PayRoll.Models.DTOs;

namespace PayRoll.Mappers
{
    public class RegisterToEmployee
    {
        Employee employee;

        public RegisterToEmployee(RegisterEmployeeDTO register)
        {
            employee=new Employee();
            employee.EmployeeName=register.EmployeeName;
            employee.DateOfBirth=register.DateOfBirth;
            employee.JoiningDate=register.JoiningDate;
            employee.Department=register.Department;
            employee.Position=register.Position;
            employee.ContactNumber=register.ContactNumber;
            employee.Address=register.Address;
            employee.BankDetails= register.BankDetails;
            employee.TaxInformation=register.TaxInformation;
            employee.Email=register.Email;
        }
        public Employee GetEmployee()
        {
            return employee;
        }
    }
}
