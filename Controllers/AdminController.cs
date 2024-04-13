using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Exceptions;
using PayRoll.Interfaces;
using PayRoll.Models;
using PayRoll.Models.DTOs;
using PayRoll.Services;


namespace PayRoll.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("ReactPolicy")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminLoginService _adminLoginService;
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;

        public AdminController(IAdminLoginService adminLoginService, ILogger<AdminController> logger, IAdminService adminService)
        {
            _adminLoginService = adminLoginService;
            _logger = logger;
            _adminService = adminService;
        }
   
        [HttpPost("Register")]
        public async Task<ActionResult<LoginUserDTO>> Register(RegisterAdminDTO user)
        {
            try
            {
                var result = await _adminLoginService.Register(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
       
        [HttpPost("Login")]
        public async Task<ActionResult<LoginUserDTO>> Login(LoginUserDTO user)
        {
            try
            {
                var result = await _adminLoginService.Login(user);
                return Ok(result);
            }
            catch (UserException iuse)
            {
                _logger.LogError(iuse.Message);
                return Unauthorized("Invalid username or password");
            }
            catch (DeactivatedUserException)
            {
                return Unauthorized("User deactivated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles ="Admin")]
       
        [Route("ChangePhonenumber")]
        [HttpPut]

        public async Task<IActionResult> ChangeAdminPhoneAsync(int id, string phone)
        {
            try
            {
                var updatedAdmin = await _adminService.ChangeAdminPhoneAsync(id, phone);
                return Ok(updatedAdmin);
            }
            catch (AdminException ex)
            {
                _logger.LogError(ex, $"Error changing customer phone: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing customer phone.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/change-name")]
        public async Task<IActionResult> ChangeAdminName(int id, string name)
        {
            try
            {
                var updatedAdmin = await _adminService.ChangeAdminName(id, name);
                return Ok(updatedAdmin);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, $"Error changing customer name: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing customer name.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize(Roles = "Admin")]
        
        [HttpGet("GetAllAdmin")]
        public async Task<ActionResult<List<Admin>>> GetAllAdmin()
        {
            try
            {
                return await _adminService.GetAllAdmin();
            }
            catch (AdminException e)
            {
                _logger.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAdminByEmail/{email}")]
        public async Task<IActionResult> GetAdminByEmail(string email)
        {
            try
            {
                var admin = await _adminService.GetAdminByEmail(email);
                if (admin != null)
                {
                    return Ok(admin);
                }
                else
                {
                    return NotFound("Admin not found for the provided email.");
                }
            }
            catch (AdminException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



    }
}
