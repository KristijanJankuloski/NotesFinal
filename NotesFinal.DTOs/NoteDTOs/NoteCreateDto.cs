using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesFinal.DTOs.NoteDTOs
{
    public class NoteCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string Text { get; set; }
        [Required]
        [Range(1,3, ErrorMessage = "Priority must be 1 - 3, low=1, medium=2, hig=3")]
        public int Priority { get; set; }
        [Required]
        [Range(1,3, ErrorMessage = "Tag must be 1 - 3, Work=1, Health=2, SocialLife=3")]
        public int Tag { get; set; }
    }
}
