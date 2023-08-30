using Microsoft.Data.SqlClient;
using NotesFinal.Domain.Enums;
using NotesFinal.Domain.Models;

namespace NotesFinal.DataAccess.Repositories.Implementations
{
    public class AdonetRepository : IRepository<Note>
    {
        private string _connectionString;
        public AdonetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task DeleteByIdAsync(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "DELETE FROM dbo.Notes WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task<List<Note>> GetAllAsync()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "SELECT * FROM dbo.Notes";
            List<Note> notesDb = new List<Note>();
            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                notesDb.Add(new Note
                {
                    Id = (int)dataReader["Id"],
                    Text = (string)dataReader["Text"],
                    Priority = (Priority)dataReader["Priority"],
                    Tag = (Tag)dataReader["Tag"],
                    UserId = (int)dataReader["UserId"],
                });
            }
            await sqlConnection.CloseAsync();
            return notesDb;
        }

        public Task<Note> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" + "VALUES (@text, @priority, @tag, @userId)";
            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@priority", entity.Priority);
            command.Parameters.AddWithValue("@tag", entity.Tag);
            command.Parameters.AddWithValue("@userId", entity.UserId);
            await command.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public Task UpdateAsync(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
