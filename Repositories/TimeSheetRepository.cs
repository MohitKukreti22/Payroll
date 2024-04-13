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
    public class TimeSheetRepository : IRepository<int, TimeSheet>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<TimeSheetRepository> _logger;

        public TimeSheetRepository(RequestTarkerContext context, ILogger<TimeSheetRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TimeSheet> Add(TimeSheet item)
        {
            _context.TimeSheets.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added new TimeSheet: ID {item.TimeSheetID}");
            return item;
        }

        public async Task<TimeSheet?> Delete(int key)
        {
            var foundTimeSheet = await Get(key);
            if (foundTimeSheet == null)
            {
                return null;
            }
            else
            {
                _context.TimeSheets.Remove(foundTimeSheet);
                await _context.SaveChangesAsync();
                return foundTimeSheet;
            }
        }

        public async Task<TimeSheet?> Get(int key)
        {
            var foundTimeSheet = await _context.TimeSheets.FirstOrDefaultAsync(timesheet => timesheet.TimeSheetID == key);
            return foundTimeSheet;
        }

        public async Task<List<TimeSheet>?> GetAll()
        {
            var allTimeSheets = await _context.TimeSheets.ToListAsync();
            return allTimeSheets.Count == 0 ? null : allTimeSheets;
        }

        public async Task<TimeSheet> Update(TimeSheet item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
