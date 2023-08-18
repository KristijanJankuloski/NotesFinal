using NotesFinal.DTOs.NoteDTOs;

namespace NotesFinal.Services.Interfaces
{
    public interface INoteService
    {
        Task<NoteDto> GetByIdAsync(int id);
    }
}
