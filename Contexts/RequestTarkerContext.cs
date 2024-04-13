using Microsoft.EntityFrameworkCore;
using PayRoll.Models;
namespace PayRoll.Contexts
{
    public class RequestTarkerContext:DbContext
    {
        public RequestTarkerContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Admin>Admins { get; set; }
        public DbSet<PayrollProcessor> PayrollProcessors { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Validation>Validations { get; set; }
      
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PayrollPolicy> PayrollPolicies { get; set; }

        public DbSet<LeaveRequest>LeaveRequests { get; set; }

        public DbSet<AuditTrail> AuditTrails { get; set; }

        public DbSet<TimeSheet> TimeSheets { get; set; }


 
    }
}
