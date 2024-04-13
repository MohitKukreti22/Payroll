using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayRoll.Contexts;
using PayRoll.Interfaces;
using PayRoll.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayRoll.Repositories
{
    public class AuditTrailRepository : IRepository<int, AuditTrail>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<AuditTrailRepository> _logger;

        public AuditTrailRepository(RequestTarkerContext context, ILogger<AuditTrailRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AuditTrail> Add(AuditTrail item)
        {
            _context.AuditTrails.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added new AuditTrail: {item.Action}");
            return item;
        }

        public async Task<AuditTrail?> Delete(int key)
        {
            var foundAuditTrail = await Get(key);
            if (foundAuditTrail == null)
            {
                return null;
            }
            else
            {
                _context.AuditTrails.Remove(foundAuditTrail);
                await _context.SaveChangesAsync();
                return foundAuditTrail;
            }
        }

        public async Task<AuditTrail?> Get(int key)
        {
            var foundAuditTrail = await _context.AuditTrails.FirstOrDefaultAsync(audittrail => audittrail.AuditTrailID == key);
            return foundAuditTrail;
        }

        public async Task<List<AuditTrail>?> GetAll()
        {
            var allAuditTrails = await _context.AuditTrails.ToListAsync();
            return allAuditTrails.Count == 0 ? null : allAuditTrails;
        }

        public async Task<AuditTrail> Update(AuditTrail item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}