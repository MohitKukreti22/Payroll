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
    public class AdminService:IAdminLoginService,IAdminService
    {
        private readonly IRepository<int, Admin> _adminRepository;
        private readonly ILogger<AdminService> _logger;
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly ITokenService _tokenService;

        public AdminService(IRepository<int, Admin> adminRepository,
             ILogger<AdminService> logger,
            IRepository<string, Validation> validationRepository,
            ITokenService tokenService)
        {
            _adminRepository = adminRepository;
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

        public async Task<LoginUserDTO> Register(RegisterAdminDTO user)
        {
            try
            {
                _logger.LogInformation("Registering new admin with email: {0}", user.Email);

                Validation myuser = new RegisterToAdminUser(user).GetValidation();
                myuser.Status = "Active"; // Set status to "Active" by default
                myuser = await _validationRepository.Add(myuser);
                Admin admins = new RegisterToAdmin(user).GetAdmin();
                admins = await _adminRepository.Add(admins);
                LoginUserDTO result = new LoginUserDTO
                {
                    Email = myuser.Email,
                    UserType = myuser.UserType,
                };

                _logger.LogInformation("Admin registered successfully: {0}", user.Email);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering admin: {0}", user.Email);
                throw;
            }
        }

        public async Task<List<Admin>> GetAllAdmin()
        {
            var allAdmin = await _adminRepository.GetAll();
            if (allAdmin == null)
            {
                throw new AdminException();
            }
            return allAdmin;
        }


        public async Task<Admin> DeleteAdmin(int key)
        {
            var deletedAdmin = await _adminRepository.Delete(key);
            if (deletedAdmin == null)
            {
                throw new AdminException();
            }
            return deletedAdmin;
        }


        public async Task<Admin> ChangeAdminPhoneAsync(int id, string phone)
        {
            var admin = await _adminRepository.Get(id);
            if (admin != null)
            {
                admin.Phone = phone;
                admin = await _adminRepository.Update(admin);
                return admin;
            }
            else
            {
                throw new AdminException();
            }
        }

        public async Task<Admin> ChangeAdminName(int id, string name)
        {
            var admin = await _adminRepository.Get(id);
            if (admin != null)
            {
                admin.Name = name;
                admin = await _adminRepository.Update(admin);
                return admin;
            }
            else
            {
                throw new AdminException();
            }
        }

        public async Task<Admin>GetAdminByEmail(string email)
        {
            var validation = await _validationRepository.Get(email);

            if(validation != null)
            {
                var allAdmin=await _adminRepository.GetAll();

                if(allAdmin != null)
                {
                    var admininfo = allAdmin.FirstOrDefault(admin=> admin.Email==email);

                    return admininfo;
                }
                else
                {
                    throw new AdminException();
                }
            }
            else
            {
                throw new AdminException();
            }
        }








    }
}