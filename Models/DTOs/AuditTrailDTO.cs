using System;

namespace PayRoll.DTOs
{
    public class AuditTrailDTO
    {
        public int AuditTrailID { get; set; }
        public int EmployeeID { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
