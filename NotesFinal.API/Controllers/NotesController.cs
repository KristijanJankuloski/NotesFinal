using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesFinal.DataAccess.Repositories;
using NotesFinal.Domain.Enums;
using NotesFinal.Domain.Models;
using NotesFinal.DTOs.NoteDTOs;
using NotesFinal.Helpers;
using NotesFinal.Services.Interfaces;

namespace NotesFinal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IRepository<Note> _adoRepository;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<NoteListDto>>> GetAll()
        {
            try
            {
                var currentUser = JwtHelper.GetCurrentUser(this.HttpContext.User);
                List < NoteListDto > dto = await _noteService.GetAllByUserId(currentUser.Id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<NoteDto>> GetById(int id)
        {
            try
            {
                var currentUser = JwtHelper.GetCurrentUser(this.HttpContext.User);
                if (id <= 0)
                {
                    return BadRequest("Id cannot be negative value");
                }
                NoteDto dto = await _noteService.GetByIdAsync(id, currentUser.Id);
                if (dto == null)
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNote(NoteCreateDto dto)
        {
            try
            {
                var currentUser = JwtHelper.GetCurrentUser(this.HttpContext.User);
                await _noteService.CreateNote(currentUser.Id, dto);
                return StatusCode(StatusCodes.Status201Created, "Note created");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                var currentUser = JwtHelper.GetCurrentUser(this.HttpContext.User);
                await _noteService.DeleteNoteById(id, currentUser.Id);
                return Ok("Note deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] NoteCreateDto dto)
        {
            try
            {
                var currentUser = JwtHelper.GetCurrentUser(this.HttpContext.User);
                if(id <= 0)
                {
                    return BadRequest("Id cannot be negative value");
                }
                await _noteService.UpdateNote(id, dto, currentUser.Id);
                return Ok("Note updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
