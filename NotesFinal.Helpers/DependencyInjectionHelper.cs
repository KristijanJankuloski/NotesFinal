using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NotesFinal.DataAccess;
using NotesFinal.DataAccess.Repositories;
using NotesFinal.DataAccess.Repositories.Implementations;
using NotesFinal.Domain.Models;
using NotesFinal.Services.Implementations;
using NotesFinal.Services.Interfaces;
using System.Text;

namespace NotesFinal.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IRepository<Note>>(x => new AdonetRepository(connectionString));
            services.AddTransient<DapperRepository>(x => new DapperRepository(connectionString));
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

        public static void ConfigureJwt(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
        }
    }
}
