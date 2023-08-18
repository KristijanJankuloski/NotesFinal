using NotesFinal.Domain.Enums;
using NotesFinal.DTOs.UserDTOs;

namespace NotesFinal.DTOs.NoteDTOs
{
    public class NoteDto
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
        public UserShortDto User { get; set; }
    }
}
