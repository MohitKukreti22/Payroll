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
    public class PayrollProcessorService:IPayrollProcessorLoginService ,IPayrollProcessorService
    {
        private readonly IRepository<int, PayrollProcessor> _payrollProcessorRepository;
        private readonly ILogger<PayrollProcessorService> _logger;
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly ITokenService _tokenService;

        public PayrollProcessorService(IRepository<int, PayrollProcessor> payrollProcessorRepository,
            ILogger<PayrollProcessorService> logger,
            IRepository<string, Validation> validationRepository,
            ITokenService tokenService)
        {
            _payrollProcessorRepository = payrollProcessorRepository;
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

        public async Task<LoginUserDTO> Register(RegisterPayrollProcessorDTO user)
        {
            try
            {
                _logger.LogInformation("Registering new admin with email: {0}", user.Email);

                Validation myuser = new RegisterToPayrollProcessorUser(user).GetValidation();
                myuser.Status = "Active"; // Set status to "Active" by default
                myuser = await _validationRepository.Add(myuser);
                PayrollProcessor payrollProcessors = new RegisterToPayrollProcessor(user).GetPayrollProcessor();
                payrollProcessors = await _payrollProcessorRepository.Add(payrollProcessors);
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

        public async Task<List<PayrollProcessor>> GetAllPayrollProcessor()
        {
            var allPayrollProcessor = await _payrollProcessorRepository.GetAll();
            if (allPayrollProcessor == null)
            {
                throw new AdminException();
            }
            return allPayrollProcessor;
        }


        public async Task<PayrollProcessor> DeletePayrollProcessor(int key)
        {
            var deletedPayrollProcessor = await _payrollProcessorRepository.Delete(key);
            if (deletedPayrollProcessor == null)
            {
                throw new AdminException();
            }
            return deletedPayrollProcessor;
        }


        public async Task<PayrollProcessor> ChangePayrollProcessorPhoneAsync(int id, string phone)
        {
            var payrollProcessor = await _payrollProcessorRepository.Get(id);
            if (payrollProcessor != null)
            {
                payrollProcessor.Phone = phone;
                payrollProcessor = await _payrollProcessorRepository.Update(payrollProcessor);
                return payrollProcessor;
            }
            return payrollProcessor;
        }

        public async Task<PayrollProcessor> ChangePayrollProcessorName(int id, string name)
        {
            var payrollProcessor = await _payrollProcessorRepository.Get(id);
            if (payrollProcessor  != null)
            {
                payrollProcessor.Name = name;
                payrollProcessor = await _payrollProcessorRepository.Update(payrollProcessor);
                return payrollProcessor;
            }
            return payrollProcessor;
        }

        public async Task<PayrollProcessor>GetPayrollProcessorByEmail(string email)
        {
            var validation=await _validationRepository.Get(email);
            if(validation != null)
            {
                var allPayrollProcessor=await _payrollProcessorRepository.GetAll();
                if(allPayrollProcessor != null)
                {
                    var payrollprocessorinfo=allPayrollProcessor.FirstOrDefault(payrollprocessor=> payrollprocessor.Email==email);
                    return payrollprocessorinfo;
                }
                else
                {
                    throw new AdminException();
                }
            }
            else { 
                throw new AdminException();
            }
        }

    }
}
