
using PayRoll.Models.DTOs;

namespace PayRoll.Interfaces
{
    public interface IManagerLoginService
    {
        public Task<LoginUserDTO> Login(LoginUserDTO user);
        public Task<LoginUserDTO> Register(RegisterManagerDTO user);
    }
}
