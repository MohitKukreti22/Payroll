using System;
using PayRoll.Models.DTOs;

namespace PayRoll.Interfaces
{
    public interface IEmployeeLoginService
    {
        public Task<LoginUserDTO> Login(LoginUserDTO user);
        public Task<LoginUserDTO> Register(RegisterEmployeeDTO user);
    }
}
