using PayRoll.Models;
using PayRoll.Interfaces;
using Castle.Core.Resource;
using PayRoll.Models.DTOs;
using PayRoll.Mappers;
using PayRoll.Exceptions;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore; 
using PayRoll.Contexts;


namespace PayRoll.Services
{
    public class EmployeeService:IEmployeeLoginService,IEmployeeService
    {
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly ITokenService _tokenService;

        public EmployeeService(IRepository<int, Employee> employeeRepository,
            ILogger<EmployeeService> logger,
            IRepository<string, Validation> validationRepository,
            ITokenService tokenService)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
            _validationRepository = validationRepository;
            _tokenService = tokenService;
        }



        

        public async Task<LoginUserDTO> Login(LoginUserDTO user)
        {
            var myUser = await _validationRepository.Get(user.Email);
            if (myUser == null || myUser.Status != "Active")
            {
                throw new EmployeeException();
            }
            var userPassword = GetPasswordEncrypted(user.Password, myUser.Key);
            var checkPasswordMatch = ComparePasswords(myUser.Password, userPassword);
            if (checkPasswordMatch)
            {
                user.Password = "";
                user.UserType = myUser.UserType;
                user.Token = await _tokenService.GenerateToken(user);
                return user;
            }
            throw new EmployeeException();
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

        //public async Task<LoginUserDTO> Register(RegisterCustomerDTO user)
        //{
        //    Validation myuser = new RegisterToCustomerUser(user).GetValidation();
        //    myuser = await _validationRepository.Add(myuser);
        //    Customers customers = new RegisterToCustomer(user).GetCustomers();
        //    customers = await _customerRepository.Add(customers);
        //    LoginUserDTO result = new LoginUserDTO
        //    {
        //        Email = myuser.Email,
        //        UserType = myuser.UserType,
        //    };
        //    return result;
        //}


        public async Task<LoginUserDTO> Register(RegisterEmployeeDTO user)
        {
            Validation myuser = new RegisterToEmployeeUser(user).GetValidation();
            myuser.Status = "Active"; // Set status to "Active" by default
            myuser = await _validationRepository.Add(myuser);
            Employee employee = new RegisterToEmployee(user).GetEmployee();
            employee = await _employeeRepository.Add(employee);
            LoginUserDTO result = new LoginUserDTO
            {
                Email = myuser.Email,
                UserType = myuser.UserType,
            };
            return result;
        }


        public async Task<List<Employee>> GetEmployeeListasync()
        {
            var employee = await _employeeRepository.GetAll();
            return employee;
        }

        public async Task<Employee> ChangeEmployeePhoneAsync(int id, string phone)
        {
            var employee = await _employeeRepository.Get(id);
            if (employee != null)
            {
                employee.ContactNumber = phone;
                employee = await _employeeRepository.Update(employee);
                return employee;
            }
            return employee;
        }

        public async Task<Employee> ChangeEmployeeName(int id, string name)
        {
            var employee = await _employeeRepository.Get(id);
            if (employee != null)
            {
                employee.EmployeeName = name;
                employee = await _employeeRepository.Update(employee);
                return employee;
            }
            return employee;
        }

        public async Task<Employee> ChangeEmployeeAddress(int id, string address)
        {
            var employee = await _employeeRepository.Get(id);
            if (employee != null)
            {
                employee.Address = address;
                employee = await _employeeRepository.Update(employee);
                return employee;
            }
            return employee;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.Get(id);
            if (employee == null)
            {
                throw new EmployeeException();
            }
            var result = await _employeeRepository.Delete(id);
            return result;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var employee = await _employeeRepository.Get(id);
            return employee;
        }

        public async Task<bool> UpdateEmployeePassword(string email, string newPassword)
        {
            // Fetch the validation information for the customer by email
            var validation = await _validationRepository.Get(email);

            // Check if the validation information exists
            if (validation == null)
            {
                // Customer not found
                return false;
            }

            // Generate a new key for password encryption (assuming it's required)
            byte[] newKey = GenerateNewKey(); // Implement the method to generate a new key

            // Encrypt the new password using the new key
            byte[] encryptedPassword = GetPasswordEncrypted(newPassword, newKey);

            // Update the validation information with the new password and key
            validation.Password = encryptedPassword;
            validation.Key = newKey;

            // Save the updated validation information back to the repository
            await _validationRepository.Update(validation);

            return true; // Password updated successfully
        }

        // Method to generate a new key for password encryption
        private byte[] GenerateNewKey()
        {
            // Generate a new random key using cryptographic functions
            byte[] newKey = new byte[64]; // Assuming the key size is 64 bytes
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(newKey);
            }
            return newKey;
        }
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            var validation = await _validationRepository.Get(email);
            if(validation != null)
            {
                var allEmployee=await _employeeRepository.GetAll(); 

                if(allEmployee != null)
                {
                    var employeeinfo = allEmployee.FirstOrDefault(employee => employee.Email == email);
                    return employeeinfo;
                }
                else
                {
                    throw new  EmployeeException();
                }

            }
            else
            {
                throw new EmployeeException();
            }
        }

    }
}


