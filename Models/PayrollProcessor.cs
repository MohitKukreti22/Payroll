using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayRoll.Models
{
    public class PayrollProcessor : IEquatable<PayrollProcessor>
    {

        [Key]
        public int PayrollProcessorID { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
       
        public string Phone { get; set; }
        [ForeignKey("Email")]
        public Validation? Validation { get; set; }
        public ICollection<Payroll> Payroll {  get; set; }

        public PayrollProcessor() 
        {
            
        }

        public PayrollProcessor(int payrollProcessorID, string name, string email, string position, string phone)
        {
            PayrollProcessorID = payrollProcessorID;
            Name = name;
            Email = email;
           
            Phone = phone;
           
        }
        public bool Equals(PayrollProcessor? other)
        {
            return PayrollProcessorID == other.PayrollProcessorID;
        }
    }
}
