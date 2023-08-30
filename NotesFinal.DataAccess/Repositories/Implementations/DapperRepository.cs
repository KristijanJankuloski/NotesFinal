using Dapper;
using Microsoft.Data.SqlClient;
using NotesFinal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesFinal.DataAccess.Repositories.Implementations
{
    public class DapperRepository : IRepository<Note>
    {
        private string _connectionString;
        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task DeleteByIdAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string deleteQuery = "DELETE FROM dbo.Notes WHERE Id = @id";
                await sqlConnection.QueryAsync(deleteQuery, new { id = id });
            }
        }

        public async Task<List<Note>> GetAllAsync()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string getQuery = "SELECT * FROM dbo.Notes";
                IEnumerable<Note> notes = await sqlConnection.QueryAsync<Note>(getQuery);
                return notes.ToList();
            }
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string query = "SELECT * FROM dbo.Notes WHERE Id = @id";
                IEnumerable<Note> notes = await sqlConnection.QueryAsync<Note>(query, new { id = id });
                return notes.FirstOrDefault();
            }
        }

        public async Task InsertAsync(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string insertQuery = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId) VALUES (@text, @priority, @tag, @userId)";
                await sqlConnection.QueryAsync(insertQuery, new
                {
                    text = entity.Text,
                    priority = entity.Priority,
                    tag = entity.Tag,
                    userId = entity.UserId,
                });
            }
        }

        public Task UpdateAsync(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
