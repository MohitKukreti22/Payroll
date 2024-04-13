using PayRoll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Interfaces
{
    public interface IManagerLeaveRequestService
    {
        Task<List<LeaveRequest>> GetAllLeaveRequestsByEmployeeId();
        Task<LeaveRequest> ApproveLeaveRequestByEmployeeId(int employeeId);
        Task<LeaveRequest> RejectLeaveRequestByEmployeeId(int employeeId);
    }
}
