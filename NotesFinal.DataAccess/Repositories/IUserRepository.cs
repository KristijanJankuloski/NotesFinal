using NotesFinal.Domain.Models;

namespace NotesFinal.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
