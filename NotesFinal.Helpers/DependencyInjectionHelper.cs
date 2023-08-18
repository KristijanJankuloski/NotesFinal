using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesFinal.DataAccess;
using NotesFinal.DataAccess.Repositories;
using NotesFinal.DataAccess.Repositories.Implementations;
using NotesFinal.Services.Implementations;
using NotesFinal.Services.Interfaces;

namespace NotesFinal.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
