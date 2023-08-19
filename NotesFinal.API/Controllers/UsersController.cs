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
                await _userService.SaveToken(user.Id, response.Token);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            try
            {
                await _userService.RegisterUser(dto);
                return Ok("User created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("refresh")]
        [Authorize]
        public async Task<ActionResult<UserLoginResponseDto>> RefreshToken(TokenRefresh old)
        {
            try
            {
                var user = JwtHelper.GetCurrentUser(this.HttpContext.User);
                UserLoginResponseDto dto = new UserLoginResponseDto
                {
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
                if(!await _userService.CheckLastToken(user.Id, old.Token))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Token invalid");
                }
                dto.Token = JwtHelper.GenerateToken(user, _configuration);
                dto.RefreshToken = JwtHelper.GenerateRefreshToken(user, _configuration);
                await _userService.SaveToken(user.Id, dto.Token);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                var currnetUser = JwtHelper.GetCurrentUser(this.HttpContext.User);
                await _userService.DeleteUserById(currnetUser.Id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
