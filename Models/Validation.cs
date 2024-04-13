using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Castle.Core.Resource;
namespace PayRoll.Models
{
    public class Validation : IEquatable<Validation>
    {


        [Key]
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string UserType { get; set; }
        public byte[] Key { get; set; }
        public string Status { get; set; }
        public PayrollProcessor? PayrollProcessor { get; set; }
        public Employee? Employee { get; set; }
        public Admin ? Admin { get; set; }

        public Manager? Manager { get; set; }

        public bool Equals(Validation? other)
        {
            if (Email == other.Email && Password == other.Password)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}