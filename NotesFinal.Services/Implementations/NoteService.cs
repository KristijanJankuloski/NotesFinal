using NotesFinal.DataAccess.Repositories;
using NotesFinal.Domain.Models;
using NotesFinal.DTOs.NoteDTOs;
using NotesFinal.Mappers;
using NotesFinal.Services.Interfaces;

namespace NotesFinal.Services.Implementations
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
    
        public async Task<NoteDto> GetByIdAsync(int id)
        {
            Note note = await _noteRepository.GetByIdAsync(id);
            return note.ToNoteDto();
        }
    }
}
