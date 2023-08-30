using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesFinal.DataAccess.Repositories;
using NotesFinal.Domain.Enums;
using NotesFinal.Domain.Models;
using NotesFinal.DTOs.NoteDTOs;
using NotesFinal.Mappers;

namespace NotesFinal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoController : ControllerBase
    {
        private readonly IRepository<Note> _adoRepository;
        public AdoController(IRepository<Note> repository)
        {
            _adoRepository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<NoteListDto>>> Get()
        {
            try
            {
                List<Note> notes = await _adoRepository.GetAllAsync();
                return Ok(notes.Select(x => x.ToNoteListDto()).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNoteAdo(int id, NoteCreateDto dto)
        {
            try
            {
                Note note = new Note
                {
                    Id = 0,
                    Text = dto.Text,
                    Tag = (Tag)dto.Tag,
                    Priority = (Priority)dto.Priority,
                    UserId = id
                };
                await _adoRepository.InsertAsync(note);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
