using NotesFinal.Domain.Models;

namespace NotesFinal.DataAccess.Repositories
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<List<Note>> GetAllByUserId(int userId);
    }
}
