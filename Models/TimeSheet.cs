using System.ComponentModel.DataAnnotations.Schema;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace PayRoll.Models
{
    public class TimeSheet:IEquatable<TimeSheet>
    {
        [Key]
        public int TimeSheetID { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public double TotalHoursWorked { get; set; }
        public string Status { get; set; }=string.Empty;
       
        public DateTime ApprovedAt { get; set; }

        [JsonIgnore]
        public Employee? Employee { get; set; }

        

        public TimeSheet()
        {
            
        }

        public TimeSheet(DateTime weekStartDate, DateTime weekEndDate, double totalHoursWorked,
                        string status,  DateTime approvedAt)
        {
            WeekStartDate = weekStartDate;
            WeekEndDate = weekEndDate;
            TotalHoursWorked = totalHoursWorked;
            Status = status;
            
            ApprovedAt = approvedAt;
        }

      
        public bool Equals(TimeSheet? other)
        {
            return TimeSheetID == other.TimeSheetID;
        }

    }
}
