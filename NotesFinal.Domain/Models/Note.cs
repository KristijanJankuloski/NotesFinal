using NotesFinal.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesFinal.Domain.Models
{
    public class Note : BaseEntity
    {
        [MaxLength(255)]
        public string Text { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
