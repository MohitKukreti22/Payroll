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
    public class AdminRepository : IRepository<int, Admin>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<AdminRepository> _loggerAdminRepository;

        public AdminRepository(RequestTarkerContext context, ILogger<AdminRepository> loggerAdminRepository)
        {
            _context = context;
            _loggerAdminRepository = loggerAdminRepository;
        }

        public async Task<Admin> Add(Admin item)
        {
            _context.Admins.Add(item);
            await _context.SaveChangesAsync();
            _loggerAdminRepository.LogInformation($"Added New Admin: {item.Name}");
            return item;
        }

        public async Task<Admin?> Delete(int key)
        {
            var foundAdmin = await Get(key);
            if (foundAdmin == null)
            {
                return null;
            }
            else
            {
                _context.Admins.Remove(foundAdmin);
                await _context.SaveChangesAsync();
                return foundAdmin;
            }
        }

        public async Task<Admin?> Get(int key)
        {
            var foundAdmin = await _context.Admins.FirstOrDefaultAsync(admin => admin.AdminID == key);
            return foundAdmin;
        }

        public async Task<List<Admin>?> GetAll()
        {
            var allAdmins = await _context.Admins.ToListAsync();
            return allAdmins.Count == 0 ? null : allAdmins;
        }

        public async Task<Admin> Update(Admin item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
