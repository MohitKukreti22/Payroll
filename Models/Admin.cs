using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PayRoll.Models
{
    public class Admin : IEquatable<Admin>
    {
        [Key]
        public int AdminID { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [ForeignKey("Email")]
        public Validation? Validation { get; set; }

        public ICollection<Employee> Employee { get; set; }
        public ICollection<PayrollPolicy> PayrollPolicy { get; set; }
       

        public Admin(int adminID, string name, string email, string phone)
        {
            AdminID = adminID;
            Name = name;
            Email = email;
            Phone = phone;
        }
        public Admin()
        {

        }
        public bool Equals(Admin? other)
        {
            return AdminID == this.AdminID;
        }
    }
}