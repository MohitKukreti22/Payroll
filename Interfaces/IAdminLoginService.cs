using PayRoll.Models.DTOs;

namespace PayRoll.Interfaces
{
    public interface IAdminLoginService
    {
        public Task<LoginUserDTO> Login(LoginUserDTO user);
        public Task<LoginUserDTO> Register(RegisterAdminDTO user);
    }
}
