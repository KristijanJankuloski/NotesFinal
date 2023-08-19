using NotesFinal.Domain.Models;
using NotesFinal.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesFinal.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(UserRegisterDto dto);
        Task<UserTokenDto> LoginUser(UserLoginDto dto);
        Task SaveToken(int userId, string token);
        Task<bool> CheckLastToken(int userId, string token);
        Task DeleteUserById(int userId);
    }
}
