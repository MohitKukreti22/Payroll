
using Castle.Core.Resource;
using PayRoll.Models;
using PayRoll.Services;




namespace PayRoll.Interfaces
{
    public interface IManagerService
    {
        public Task<Manager> ChangeManagerPhoneAsync(int id, string phone);
        public Task<Manager> ChangeManagerName(int id, string name);

        public Task<Manager> DeleteManager(int id);

        public Task<List<Manager>> GetAllManager();

        public Task<Manager> GetManagerByEmail(string email);
        //Task<bool> UpdateEmployeePassword(string email, string newPassword);
    }
}
