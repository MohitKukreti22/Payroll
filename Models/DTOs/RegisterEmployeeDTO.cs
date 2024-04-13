namespace PayRoll.Models.DTOs
{
    public class RegisterEmployeeDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserType { get; set; } = "Employee";

        public string EmployeeName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }

        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string BankDetails { get; set; } = string.Empty;
        public string TaxInformation { get; set; } = string.Empty;





    }
}
