
using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    public class ManagerController : ControllerBase
    {

        private readonly IManagerLoginService _managerLoginService;
        private readonly ILogger<ManagerController> _logger;
        private readonly IManagerService _managerService;

        public ManagerController(IManagerLoginService managerLoginService, ILogger<ManagerController> logger, IManagerService managerService)
        {
            _managerLoginService = managerLoginService;
            _logger = logger;
            _managerService = managerService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<LoginUserDTO>> Register(RegisterManagerDTO user)
        {
            try
            {
                var result = await _managerLoginService.Register(user);
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
                var result = await _managerLoginService.Login(user);
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
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}/change-phone")]
        public async Task<IActionResult> ChangeManagerPhoneAsync(int id, string phone)
        {
            try
            {
                var updatedManager = await _managerService.ChangeManagerPhoneAsync(id, phone);
                return Ok(updatedManager);
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
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}/change-name")]
        public async Task<IActionResult> ChangeManagerName(int id, string name)
        {
            try
            {
                var updatedAdmin = await _managerService.ChangeManagerName(id, name);
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
        [Authorize(Roles = "Manager")]
        
        [HttpGet("GetAllManager")]
        public async Task<ActionResult<List<Manager>>> GetAllManager()
        {
            try
            {
                return await _managerService.GetAllManager();
            }
            catch (AdminException e)
            {
                _logger.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetManagerByEmail(string email)
        {
            try
            {
                var manager = await _managerService.GetManagerByEmail(email);
                if (manager != null)
                {
                    return Ok(manager);
                }
                else
                {
                    return NotFound($"Manager with email {email} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
