using PayRoll.Models;
using PayRoll.Interfaces;
using Castle.Core.Resource;
using PayRoll.Models.DTOs;
using PayRoll.Mappers;
using PayRoll.Exceptions;
using System.Security.Cryptography;
using System.Text;
using PayRoll.Repositories;



namespace PayRoll.Services
{
    public class ManagerService : IManagerLoginService, IManagerService
    {
        private readonly IRepository<int, Manager> _managerRepository;
        private readonly ILogger<ManagerService> _logger;
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly ITokenService _tokenService;

        public ManagerService(IRepository<int, Manager> managerRepository,
             ILogger<ManagerService> logger,
            IRepository<string, Validation> validationRepository,
            ITokenService tokenService)
        {
            _managerRepository = managerRepository;
            _logger = logger;
            _validationRepository = validationRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginUserDTO> Login(LoginUserDTO user)
        {
            try
            {
                _logger.LogInformation("Attempting to log in user with email: {0}", user.Email);

                var myUser = await _validationRepository.Get(user.Email);
                if (myUser == null || myUser.Status != "Active")
                {
                    _logger.LogWarning("Invalid user login attempt for email: {0}", user.Email);
                    throw new UserException();
                }

                var userPassword = GetPasswordEncrypted(user.Password, myUser.Key);
                var checkPasswordMatch = ComparePasswords(myUser.Password, userPassword);
                if (checkPasswordMatch)
                {
                    _logger.LogInformation("User logged in successfully: {0}", user.Email);
                    user.Password = "";
                    user.UserType = myUser.UserType;
                    user.Token = await _tokenService.GenerateToken(user);
                    return user;
                }

                _logger.LogWarning("Invalid password for user: {0}", user.Email);
                throw new UserException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user: {0}", user.Email);
                throw;
            }
        }

        private bool ComparePasswords(byte[] password, byte[] userPassword)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] != userPassword[i])
                    return false;
            }
            return true;
        }

        private byte[] GetPasswordEncrypted(string password, byte[] key)
        {
            HMACSHA512 hmac = new HMACSHA512(key);
            var userpassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return userpassword;
        }

        public async Task<LoginUserDTO> Register(RegisterManagerDTO user)
        {
            try
            {
                _logger.LogInformation("Registering new Manager with email: {0}", user.Email);

                Validation myuser = new RegisterToManagerUser(user).GetValidation();
                myuser.Status = "Active"; // Set status to "Active" by default
                myuser = await _validationRepository.Add(myuser);
                Manager managers = new RegisterToManager(user).GetManager();
                managers = await _managerRepository.Add(managers);
                LoginUserDTO result = new LoginUserDTO
                {
                    Email = myuser.Email,
                    UserType = myuser.UserType,
                };

                _logger.LogInformation("Manager registered successfully: {0}", user.Email);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering manager: {0}", user.Email);
                throw;
            }
        }

        public async Task<List<Manager>> GetAllManager()
        {
            var allManager = await _managerRepository.GetAll();
            if (allManager == null)
            {
                throw new AdminException();
            }
            return allManager;
        }


        public async Task<Manager> DeleteManager(int key)
        {
            var deletedManager = await _managerRepository.Delete(key);
            if (deletedManager == null)
            {
                throw new AdminException();
            }
            return deletedManager;
        }


        public async Task<Manager> ChangeManagerPhoneAsync(int id, string phone)
        {
            var manager = await _managerRepository.Get(id);
            if (manager != null)
            {
                manager.Phone = phone;
                manager = await _managerRepository.Update(manager);
                return manager;
            }
            return manager;
        }

        public async Task<Manager> ChangeManagerName(int id, string name)
        {
            var manager = await _managerRepository.Get(id);
            if (manager != null)
            {
                manager.Name = name;
                manager = await _managerRepository.Update(manager);
                return manager;
            }
            return manager;
        }
        public async Task<Manager>GetManagerByEmail(string email)
        {
            var validation = await _validationRepository.Get(email);

            if(validation!=null)
            {
                var allManager=await _managerRepository.GetAll();
                if(allManager!=null)
                {
                    var managerinfo=allManager.FirstOrDefault(manager=>manager.Email==email);
                    return managerinfo;
                }
                else
                {
                    throw new EmployeeException();
                }
            }
            else
            {
                throw new EmployeeException();
            }
        }









    }
}