using NotesFinal.DTOs.NoteDTOs;

namespace NotesFinal.Services.Interfaces
{
    public interface INoteService
    {
        Task<NoteDto> GetByIdAsync(int id, int userId);
        Task CreateNote(int userId, NoteCreateDto dto);
        Task <List<NoteListDto>> GetAllByUserId(int userId);
        Task DeleteNoteById(int id, int userId);
        Task UpdateNote(int id, NoteCreateDto dto, int userId);
    }
}
