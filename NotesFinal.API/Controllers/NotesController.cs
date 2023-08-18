using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesFinal.Domain.Models;
using NotesFinal.DTOs.NoteDTOs;
using NotesFinal.Services.Interfaces;

namespace NotesFinal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<NoteDto>>> GetAll()
        //{
            
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Id cannot be negative value");
                }
                NoteDto dto = await _noteService.GetByIdAsync(id);
                if(dto == null)
                {
                    return NotFound($"Note with id {id} was not found");
                }
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
