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
    public class ManagerRepository : IRepository<int, Manager>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<ManagerRepository> _logger;

        public ManagerRepository(RequestTarkerContext context, ILogger<ManagerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Manager> Add(Manager item)
        {
            _context.Managers.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added new Manager: ID {item.ManagerID}");
            return item;
        }

        public async Task<Manager?> Delete(int key)
        {
            var foundManager = await Get(key);
            if (foundManager == null)
            {
                return null;
            }
            else
            {
                _context.Managers.Remove(foundManager);
                await _context.SaveChangesAsync();
                return foundManager;
            }
        }

        public async Task<Manager?> Get(int key)
        {
            var foundManager = await _context.Managers.FirstOrDefaultAsync(manager => manager.ManagerID == key);
            return foundManager;
        }

        public async Task<List<Manager>?> GetAll()
        {
            var allManagers = await _context.Managers.ToListAsync();
            return allManagers.Count == 0 ? null : allManagers;
        }

        public async Task<Manager> Update(Manager item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
