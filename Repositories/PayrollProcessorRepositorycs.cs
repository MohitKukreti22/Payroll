using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayRoll.Contexts;
using PayRoll.Exceptions;
using PayRoll.Interfaces;
using PayRoll.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayRoll.Repositories
{
    public class PayrollProcessorRepository : IRepository<int, PayrollProcessor>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<PayrollProcessorRepository> _logger;

        public PayrollProcessorRepository(RequestTarkerContext context, ILogger<PayrollProcessorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PayrollProcessor> Add(PayrollProcessor item)
        {
            _context.PayrollProcessors.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added new Payroll Processor: {item.PayrollProcessorID}");
            return item;
        }

        public async Task<PayrollProcessor?> Delete(int key)
        {
            var foundProcessor = await Get(key);
            if (foundProcessor == null)
            {
                return null;
            }
            else
            {
                _context.PayrollProcessors.Remove(foundProcessor);
                await _context.SaveChangesAsync();
                return foundProcessor;
            }
        }

        public async Task<PayrollProcessor?> Get(int key)
        {
            var foundProcessor = await _context.PayrollProcessors.FirstOrDefaultAsync(payprocessor => payprocessor.PayrollProcessorID == key);
            return foundProcessor;
        }

        public async Task<List<PayrollProcessor>?> GetAll()
        {
            var allProcessors = await _context.PayrollProcessors.ToListAsync();
            return allProcessors.Count == 0 ? null : allProcessors;
        }

        public async Task<PayrollProcessor> Update(PayrollProcessor item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}