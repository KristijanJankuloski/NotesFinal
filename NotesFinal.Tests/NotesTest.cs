using Microsoft.EntityFrameworkCore;
using NotesFinal.DataAccess;
using NotesFinal.DataAccess.Repositories;
using NotesFinal.DataAccess.Repositories.Implementations;
using NotesFinal.Domain.Models;
using NotesFinal.DTOs.NoteDTOs;
using NotesFinal.Services.Implementations;
using NotesFinal.Services.Interfaces;
using System.Text;

namespace NotesFinal.Tests
{
    [TestClass]
    public class NotesTest
    {
        private INoteRepository _repository;
        private INoteService _service;
        private NotesDbContext _context;

        [TestInitialize]
        public void NotesTestInitialize()
        {
            var builder = new DbContextOptionsBuilder<NotesDbContext>().UseSqlServer("Data Source=localhost\\SQLEXPRESS;Database=SedcNotesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            _context = new NotesDbContext(builder.Options);
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();

            _repository = new NoteRepository(_context);
            _service = new NoteService(_repository);
        }

        [TestMethod, TestCategory("Notes")]
        public async Task GetByIdAsync_GetNoteById_True()
        {
            var note = new Note
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "Hehhehe",
                User = new User
                {
                    Username = "John",
                    FirstName = "John",
                    LastName = "Doe",
                    PasswordHash = Encoding.UTF8.GetBytes("TestPasswordHash"),
                    PasswordSalt = Encoding.UTF8.GetBytes("TestPasswordSalt"),
                    LastUsedToken = "1234",
                },
            };

            await _context.AddAsync(note);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(note.Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Note));
        }

        [TestMethod, TestCategory("Notes")]
        public async Task GetAllAsync_True()
        {
            var note1 = new Note
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "Hehhehwq",
                User = new User
                {
                    Username = "Johnnny",
                    FirstName = "John",
                    LastName = "Doe",
                    PasswordHash = Encoding.UTF8.GetBytes("TestPasswordHash"),
                    PasswordSalt = Encoding.UTF8.GetBytes("TestPasswordSalt"),
                    LastUsedToken = "1234",
                },
            };
            var note2 = new Note
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "Huhuuhu",
                User = new User
                {
                    Username = "Bobski",
                    FirstName = "Bob",
                    LastName = "Doe",
                    PasswordHash = Encoding.UTF8.GetBytes("TestPasswordHash"),
                    PasswordSalt = Encoding.UTF8.GetBytes("TestPasswordSalt"),
                    LastUsedToken = "1234",
                },
            };

            await _context.Notes.AddAsync(note1);
            await _context.Notes.AddAsync(note2);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Note>));
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod, TestCategory("Notes")]
        public async Task GetAllByUserId_True()
        {
            var note = new Note
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "Hehhehe",
                User = new User
                {
                    Username = "John",
                    FirstName = "John",
                    LastName = "Doe",
                    PasswordHash = Encoding.UTF8.GetBytes("TestPasswordHash"),
                    PasswordSalt = Encoding.UTF8.GetBytes("TestPasswordSalt"),
                    LastUsedToken = "1234",
                },
            };

            await _context.Notes.AddAsync(note); 
            await _context.SaveChangesAsync();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Username == "John");

            var result = await _service.GetAllByUserId(user.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 1);
            Assert.IsInstanceOfType(result, typeof(List<NoteListDto>));
        }

        [TestMethod]
        public async Task GetNoteById_LargeInt_Null()
        {
            var result = await _service.GetByIdAsync(100, 1);

            Assert.IsNull(result);
        }
    }
}
