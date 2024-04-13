using Microsoft.Extensions.Logging;
using PayRoll.Exceptions;
using PayRoll.Interfaces;
using PayRoll.Models;
using PayRoll.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PayRoll.Services
{
    public class EmployeeTimeSheetService : IEmployeeTimeSheetService
    {
        private readonly IRepository<int, TimeSheet> _timeSheetRepository;
        private readonly ILogger<EmployeeTimeSheetService> _logger;

        public EmployeeTimeSheetService(IRepository<int, TimeSheet> timeSheetRepository, ILogger<EmployeeTimeSheetService> logger)
        {
            _timeSheetRepository = timeSheetRepository;
            _logger = logger;
        }

        public async Task<TimeSheet> AddTimeSheet(int employeeId, DateTime weekStartDate, DateTime weekEndDate, double totalHoursWorked, string status, DateTime approvedAt)
        {
            try
            {
               
                if ((weekEndDate - weekStartDate).Days > 7)
                {
                    throw new DateException();
                }

              
                if (weekEndDate <= weekStartDate)
                {
                    throw new DateException();
                }

             
                var existingTimeSheet = await _timeSheetRepository.GetAll();
                if (existingTimeSheet != null && existingTimeSheet.Exists(t => t.EmployeeID == employeeId && t.WeekStartDate == weekStartDate && t.WeekEndDate == weekEndDate))
                {
                    throw new TimeSheetException();
                }

                
                var timeSheet = new TimeSheet
                {
                    EmployeeID = employeeId,
                    WeekStartDate = weekStartDate,
                    WeekEndDate = weekEndDate,
                    TotalHoursWorked = totalHoursWorked,
                    Status = status,
                    ApprovedAt = DateTime.Now
                };

                return await _timeSheetRepository.Add(timeSheet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding timesheet: {ex.Message}");
                throw;
            }
        }
        public async Task<List<TimeSheet>> GetTimesheetsByEmployeeId(int employeeId)
        {
            try
            {
                var timeSheets = await _timeSheetRepository.GetAll();
                if (timeSheets != null)
                {
                    return timeSheets.FindAll(t => t.EmployeeID == employeeId);
                }
                return new List<TimeSheet>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving timesheets for employee ID {employeeId}: {ex.Message}");
                throw;
            }
        }
    }
}
