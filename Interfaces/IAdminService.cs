using Castle.Core.Resource;
using PayRoll.Models;
using PayRoll.Services;




namespace PayRoll.Interfaces
{
    public interface IAdminService
    {
        public Task<Admin> ChangeAdminPhoneAsync(int id, string phone);
        public Task<Admin> ChangeAdminName(int id, string name);
       
        public Task<Admin> DeleteAdmin(int id);

        public Task<List<Admin>> GetAllAdmin();

        public Task<Admin> GetAdminByEmail(string email);
        //Task<bool> UpdateEmployeePassword(string email, string newPassword);
    }
}
