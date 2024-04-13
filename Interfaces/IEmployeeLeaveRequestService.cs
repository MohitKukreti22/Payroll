using PayRoll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Interfaces
{
    public interface IEmployeeLeaveRequestService
    {
        Task<LeaveRequest> RequestLeave(int employeeId, string leaveType, DateTime startDate, DateTime endDate);
        Task<List<LeaveRequest>> GetEmployeeLeaveRequests(int employeeId);
    }
}