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
    public class LeaveRequestRepository : IRepository<int, LeaveRequest>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<LeaveRequestRepository> _logger;

        public LeaveRequestRepository(RequestTarkerContext context, ILogger<LeaveRequestRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LeaveRequest> Add(LeaveRequest item)
        {
            _context.LeaveRequests.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added new LeaveRequest: ID {item.LeaveRequestID}");
            return item;
        }

        public async Task<LeaveRequest?> Delete(int key)
        {
            var foundLeaveRequest = await Get(key);
            if (foundLeaveRequest == null)
            {
                return null;
            }
            else
            {
                _context.LeaveRequests.Remove(foundLeaveRequest);
                await _context.SaveChangesAsync();
                return foundLeaveRequest;
            }
        }

        public async Task<LeaveRequest?> Get(int key)
        {
            var foundLeaveRequest = await _context.LeaveRequests.FirstOrDefaultAsync(request => request.LeaveRequestID == key);
            return foundLeaveRequest;
        }

        public async Task<List<LeaveRequest>?> GetAll()
        {
            var allLeaveRequests = await _context.LeaveRequests.ToListAsync();
            return allLeaveRequests.Count == 0 ? null : allLeaveRequests;
        }

        public async Task<LeaveRequest> Update(LeaveRequest item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
