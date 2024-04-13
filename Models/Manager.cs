using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayRoll.Models
{
    public class Manager:IEquatable<Manager>
    {
        [Key]
        public int ManagerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        [ForeignKey("Email")]
        public Validation? Validation { get; set; }

        public ICollection<Payroll> Payroll { get; set; }
        public ICollection<LeaveRequest> LeaveRequest { get; set; }

        public Manager()
        {

        }

        public Manager(int managerID, string name, string email, string position, string phone)
        {
            ManagerID = managerID;
            Name = name;
            Email = email;

            Phone = phone;

        }
        public bool Equals(Manager? other)
        {
            return ManagerID == other.ManagerID;
        }
    }
}
    

