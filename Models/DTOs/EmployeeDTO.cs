namespace PayRoll.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string BankDetails { get; set; }
        public string TaxInformation { get; set; }
        public string Email { get; set; }
    }
}
