namespace PayRoll.Models.DTOs
{
    public class RegisterManagerDTO
    {

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserType { get; set; } = "Manager";

        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
