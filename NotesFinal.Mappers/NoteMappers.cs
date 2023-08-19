using NotesFinal.Domain.Enums;
using NotesFinal.Domain.Models;
using NotesFinal.DTOs.NoteDTOs;

namespace NotesFinal.Mappers
{
    public static class NoteMappers
    {
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Text = note.Text,
                Priority = note.Priority,
                Tag = note.Tag,
                User = note.User.ToUserShortDto()
            };
        }

        public static NoteListDto ToNoteListDto(this Note note)
        {
            return new NoteListDto
            {
                Id = note.Id,
                Text = note.Text,
                Priority = note.Priority,
                Tag = note.Tag,
            };
        }

        public static Note ToNote(this NoteCreateDto note)
        {
            return new Note
            {
                Text = note.Text,
                Priority = (Priority)note.Priority,
                Tag = (Tag)note.Tag,
            };
        }
    }
}
