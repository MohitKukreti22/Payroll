using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PayRoll.Models
{
    public class Payroll: IEquatable<Payroll>
    {
        [Key]
        public int PayrollID { get; set; }
        public int ? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        
        public int PayrollMonth { get; set; }

        public string Status { get; set; } = string.Empty;
        public int PayrollYear { get; set; }
        public double TotalEarnings { get; set; }
        public double TotalDeductions { get; set; }
        public double NetSalary { get; set; }
      
        public int? PayrollProcessorID { get; set; }
        [ForeignKey("PayrollProcessorID")]
        public DateTime? VerifiedAt { get; set; }

        public PayrollProcessor?PayrollProcessor { get; set; }    

        public Employee? Employee { get; set; }

        
   
        


        public Payroll()
        {
            
        }


        public Payroll( int? employeeId,  int payrollMonth, string status , int payrollYear, double totalEarnings, double totalDeductions, double netSalary,  int? payrollProcessorId, DateTime? verifiedAt, Employee? employee)
        {
          
            Status = status;
            EmployeeID = employeeId;
          
            PayrollMonth = payrollMonth;
            PayrollYear = payrollYear;
            TotalEarnings = totalEarnings;
            TotalDeductions = totalDeductions;
            NetSalary = netSalary;
            PayrollProcessorID = payrollProcessorId;
            VerifiedAt = verifiedAt;
            Employee = employee;
           
            
        }
        public bool Equals(Payroll? other)
        {
            var payroll = other ?? new Payroll();
            return this.PayrollID.Equals(payroll.PayrollID);
        }


    }
}
