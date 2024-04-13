using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayRoll.Models
{
    public class Employee: IEquatable<Employee>
    {
        [Key]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }= string.Empty;

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set;}

        public string Department {  get; set; }=string.Empty;
        public string Position { get; set; } = string.Empty;
        
        public string ContactNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string BankDetails { get; set; } = string.Empty;
        public string TaxInformation { get; set; } = string.Empty; 

        public string  Email { get; set; }
        [ForeignKey("Email")]

        public Validation? Validation { get; set; }

        //public ICollection<TimeSheet> TimeSheet { get; set; }

        //public ICollection<LeaveRequest> LeaveRequest { get; set; }






        public Employee()
        {
           
        }


        public Employee ( string  employeeName, string email, DateTime dateOfBirth, DateTime joiningDate, string department, string position, string contactNumber, string address, string bankDetails, string taxInformation)
        {
           
            Email = email;
            EmployeeName = employeeName;
            DateOfBirth = dateOfBirth;
            JoiningDate = joiningDate;
            Department = department;
            Position = position;
            ContactNumber = contactNumber;
            Address = address;
            BankDetails = bankDetails;
            TaxInformation = taxInformation;
           
        }

        public bool Equals(Employee? other)
        {
            var employee = other ?? new Employee();
            return this.EmployeeID.Equals(employee.EmployeeID);
        }

    }
}
