
using System;
using PayRoll.Models.DTOs;

namespace PayRoll.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(LoginUserDTO user);
    }
}