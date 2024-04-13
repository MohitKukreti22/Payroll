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
    public class EmployeeRepository : IRepository<int, Employee>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(RequestTarkerContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Employee> Add(Employee item)
        {
            _context.Employees.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added new Employee: {item.EmployeeID}");
            return item;
        }

        public async Task<Employee?> Delete(int key)
        {
            var foundEmployee = await Get(key);
            if (foundEmployee == null)
            {
                return null;
            }
            else
            {
                _context.Employees.Remove(foundEmployee);
                await _context.SaveChangesAsync();
                return foundEmployee;
            }
        }

        public async Task<Employee?> Get(int key)
        {
            var foundEmployee = await _context.Employees.FirstOrDefaultAsync(employee => employee.EmployeeID == key);
            return foundEmployee;
        }

        public async Task<List<Employee>?> GetAll()
        {
            var allEmployees = await _context.Employees.ToListAsync();
            return allEmployees.Count == 0 ? null : allEmployees;
        }

        public async Task<Employee> Update(Employee item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

       
    }
}