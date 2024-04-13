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
    public class PayrollPolicyRepository : IRepository<int, PayrollPolicy>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<PayrollPolicyRepository> _logger;

        public PayrollPolicyRepository(RequestTarkerContext context, ILogger<PayrollPolicyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PayrollPolicy> Add(PayrollPolicy item)
        {
            _context.PayrollPolicies.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added new PayrollPolicy: {item.PolicyID}");
            return item;
        }

        public async Task<PayrollPolicy?> Delete(int key)
        {
            var foundPayrollPolicy = await Get(key);
            if (foundPayrollPolicy == null)
            {
                return null;
            }
            else
            {
                _context.PayrollPolicies.Remove(foundPayrollPolicy);
                await _context.SaveChangesAsync();
                return foundPayrollPolicy;
            }
        }

        public async Task<PayrollPolicy?> Get(int key)
        {
            var foundPayrollPolicy = await _context.PayrollPolicies.FirstOrDefaultAsync(policy => policy.PolicyID == key);
            return foundPayrollPolicy;
        }

        public async Task<List<PayrollPolicy>?> GetAll()
        {
            var allPayrollPolicies = await _context.PayrollPolicies.ToListAsync();
            return allPayrollPolicies.Count == 0 ? null : allPayrollPolicies;
        }

        public async Task<PayrollPolicy> Update(PayrollPolicy item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
