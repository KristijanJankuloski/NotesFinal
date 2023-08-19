using NotesFinal.DataAccess.Repositories;
using NotesFinal.Domain.Enums;
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

        public async Task CreateNote(int userId, NoteCreateDto dto)
        {
            Note note = dto.ToNote();
            note.UserId = userId;
            await _noteRepository.InsertAsync(note);
        }

        public async Task DeleteNoteById(int id, int userId)
        {
            Note note = await _noteRepository.GetByIdAsync(id);
            if(note == null || note.UserId != userId)
            {
                return;
            }
            await _noteRepository.DeleteByIdAsync(id);
        }

        public async Task<List<NoteListDto>> GetAllByUserId(int userId)
        {
            List<Note> notes = await _noteRepository.GetAllByUserId(userId);
            return notes.Select(x => x.ToNoteListDto()).ToList();
        }

        public async Task<NoteDto> GetByIdAsync(int id, int userId)
        {
            Note note = await _noteRepository.GetByIdAsync(id);
            if (note.UserId != userId)
            {
                return null;
            }
            return note.ToNoteDto();
        }

        public async Task UpdateNote(int id, NoteCreateDto dto, int userId)
        {
            Note note = await _noteRepository.GetByIdAsync(id);
            if (note == null || note.UserId != userId)
            {
                return;
            }
            note.Text = dto.Text;
            note.Priority = (Priority)dto.Priority;
            note.Tag = (Tag)dto.Tag;
        }
    }
}
