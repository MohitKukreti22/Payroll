using PayRoll.Models;
using PayRoll.Models.DTOs;

namespace PayRoll.Mappers
{
    public class RegisterToManager
    {

        Manager manager;
        public RegisterToManager(RegisterManagerDTO register)
        {
            manager = new Manager();
            manager.Name = register.Name;
            manager.Email = register.Email;
            manager.Phone = register.Phone;
        }
        public Manager GetManager()
        {
            return manager;
        }
    }
}