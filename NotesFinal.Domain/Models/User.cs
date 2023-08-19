using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesFinal.Domain.Models
{
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? LastUsedToken { get; set; }
        [InverseProperty("User")]
        public List<Note> Notes { get; set; } = new();
    }
}
