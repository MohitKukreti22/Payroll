
using PayRoll.Models.DTOs;

namespace PayRoll.Interfaces
{
    public interface IPayrollProcessorLoginService
    {
        public Task<LoginUserDTO> Login(LoginUserDTO user);
        public Task<LoginUserDTO> Register(RegisterPayrollProcessorDTO user);
    }
}
