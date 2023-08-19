using Microsoft.EntityFrameworkCore;
using NotesFinal.Domain.Models;

namespace NotesFinal.DataAccess.Repositories.Implementations
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesDbContext _context;
        public NoteRepository(NotesDbContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(int id)
        {
            Note note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if (note == null)
                return;
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAllAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<List<Note>> GetAllByUserId(int userId)
        {
            return await _context.Notes.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            return await _context.Notes
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Note entity)
        {
            await _context.Notes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Note entity)
        {
            _context.Notes.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
