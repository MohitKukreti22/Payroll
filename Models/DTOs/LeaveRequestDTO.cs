namespace PayRoll.DTOs
{
    public class LeaveRequestDTO
    {
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
