using PayRoll.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Interfaces
{
    public interface IEmployeeTimeSheetService
    {
        Task<TimeSheet> AddTimeSheet(int employeeId, DateTime weekStartDate, DateTime weekEndDate, double totalHoursWorked, string status, DateTime approvedAt);
        Task<List<TimeSheet>> GetTimesheetsByEmployeeId(int employeeId);
    }
}
