using NotesFinal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesFinal.DTOs.NoteDTOs
{
    public class NoteListDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
    }
}
