using System.ComponentModel.DataAnnotations.Schema;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayRoll.Models
{
    public class LeaveRequest:IEquatable<LeaveRequest>
    {
        [Key]
        public int LeaveRequestID { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public string LeaveType { get; set; }=string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? ManagerID { get; set; }
        [ForeignKey("ManagerID")]
        public DateTime ApprovedAt { get; set; }

        [JsonIgnore]
        public Employee? Employee { get; set; }

        public Manager? Manager { get; set; }
        

        public LeaveRequest()
        {
           
        }

        public LeaveRequest(string leaveType, DateTime startDate, DateTime endDate,
                            string status,  DateTime approvedAt,int ManagerId)
        {
            LeaveType = leaveType;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            ApprovedAt = approvedAt;
            ManagerID = ManagerId;


        }
        public bool Equals(LeaveRequest? other)
        {
            var leaverequest = other ?? new LeaveRequest();
            return this.LeaveRequestID.Equals(leaverequest.LeaveRequestID);
        }


    }
}
