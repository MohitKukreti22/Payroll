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
    public class PayrollRepository : IRepository<int, Payroll>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<PayrollRepository> _logger;

        public PayrollRepository(RequestTarkerContext context, ILogger<PayrollRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Payroll> Add(Payroll item)
        {
            _context.Payrolls.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added new Payroll: ID {item.PayrollID}");
            return item;
        }

        public async Task<Payroll?> Delete(int key)
        {
            var foundPayroll = await Get(key);
            if (foundPayroll == null)
            {
                return null;
            }
            else
            {
                _context.Payrolls.Remove(foundPayroll);
                await _context.SaveChangesAsync();
                return foundPayroll;
            }
        }

        public async Task<Payroll?> Get(int key)
        {
            var foundPayroll = await _context.Payrolls.FirstOrDefaultAsync(payroll => payroll.PayrollID == key);
            return foundPayroll;
        }

        public async Task<List<Payroll>?> GetAll()
        {
            var allPayrolls = await _context.Payrolls.ToListAsync();
            return allPayrolls.Count == 0 ? null : allPayrolls;
        }

        public async Task<Payroll> Update(Payroll item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
