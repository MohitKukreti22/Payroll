namespace PayRoll.Models.DTOs
{
    public class RegisterPayrollProcessorDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserType { get; set; } = "PayrollProcessor";

        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
