using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayRoll.Models
{
    public class AuditTrail
    {
            [Key]
            public int AuditTrailID { get; set; }
            public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
            public string Action { get; set; }
            public string Details { get; set; }
            public DateTime Timestamp { get; set; }

        public Employee? Employee {  get; set; }

        public AuditTrail()
        {

        }

        public AuditTrail(int EmployeeId, string action, string details, DateTime timestamp)
        {
            EmployeeID = EmployeeId;
            Action = action;
            Details = details;
            Timestamp = timestamp;
        }

        public bool Equals(AuditTrail? other)
        {
            var audittrail = other ?? new AuditTrail();
            return this.AuditTrailID.Equals(audittrail.AuditTrailID);
        }





    }
}
