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
    }
}
