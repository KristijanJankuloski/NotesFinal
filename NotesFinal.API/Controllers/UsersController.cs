using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesFinal.DTOs.UserDTOs;
using NotesFinal.Helpers;
using NotesFinal.Services.Interfaces;

namespace NotesFinal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginDto dto)
        {
            try
            {
                UserTokenDto user = await _userService.LoginUser(dto);
                if (user == null)
                    return BadRequest("Bad credentials");
                UserLoginResponseDto response = new UserLoginResponseDto
                {
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
                response.Token = JwtHelper.GenerateToken(user, _configuration);
                response.RefreshToken = JwtHelper.GenerateRefreshToken(user, _configuration);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            try
            {
                await _userService.RegisterUser(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
