using Microsoft.Extensions.Configuration;
using NotesFinal.DataAccess.Repositories;
using NotesFinal.Domain.Models;
using NotesFinal.DTOs.UserDTOs;
using NotesFinal.Mappers;
using NotesFinal.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace NotesFinal.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
        }

        public async Task<UserShortDto> LoginUser(UserLoginDto dto)
        {
            User user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null)
                return null;
            if (!IsPasswordValid(dto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user.ToUserShortDto();
        }

        public async Task RegisterUser(UserRegisterDto dto)
        {
            User user = new User
            {
                Username = dto.Username,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
            CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            await _userRepository.InsertAsync(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool IsPasswordValid(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
