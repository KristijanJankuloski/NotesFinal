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
        Task<UserShortDto> LoginUser(UserLoginDto dto);
    }
}
