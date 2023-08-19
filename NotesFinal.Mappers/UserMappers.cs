using NotesFinal.Domain.Models;
using NotesFinal.DTOs.UserDTOs;

namespace NotesFinal.Mappers
{
    public static class UserMappers
    {
        public static UserShortDto ToUserShortDto(this User user)
        {
            return new UserShortDto
            {
                Username = user.Username,
                FirstName = user.FirstName != null ? user.FirstName : "/",
                LastName = user.LastName != null ? user.LastName : "/",
            };
        }

        public static UserTokenDto ToUserTokenDto(this User user)
        {
            return new UserTokenDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName != null ? user.FirstName : "/",
                LastName = user.LastName != null ? user.LastName : "/",
            };
        }
    }
}
