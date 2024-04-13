using PayRoll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Interfaces
{
    public interface IEmployeeAuditTrailService
    {
        Task<List<AuditTrail>> GetAuditTrailsByEmployee(int employeeId);
        Task<AuditTrail> AddAuditTrail(AuditTrail auditTrail);
        Task<AuditTrail?> DeleteAuditTrail(int auditTrailId);
        Task<AuditTrail?> GetAuditTrailById(int auditTrailId);
        Task<List<AuditTrail>> GetAllAuditTrails();
        Task<AuditTrail> UpdateAuditTrail(AuditTrail auditTrail);
    }
}
