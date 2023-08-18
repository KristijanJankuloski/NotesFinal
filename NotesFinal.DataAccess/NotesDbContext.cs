using Microsoft.EntityFrameworkCore;
using NotesFinal.Domain.Models;

namespace NotesFinal.DataAccess
{
    public class NotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public NotesDbContext(DbContextOptions options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();
        }
    }
}
