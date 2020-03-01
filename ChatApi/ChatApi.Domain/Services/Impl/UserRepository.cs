using ChatApi.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Domain.Services.Impl
{
    public class UserRepository : IAsyncRepository<User>
    {
        private IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteAsync(User entity)
        {
            using var dbConnection = GetConnection();

            await dbConnection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { entity.Id });
        }

        public async Task<User[]> GetAsync()
        {
            using var dbConnection = GetConnection();

            var users = await dbConnection.QueryAsync<User>("SELECT * FROM Users");

            return users.ToArray();
        }

        public async Task<User> GetAsync(object id)
        {
            using var dbConnection = GetConnection();

            var users = await dbConnection.QueryAsync<User>("SELECT * FROM Users WHERE Id = @id", new { id });

            return users.Single();
        }

        public async Task<User> InsertAsync(User entity)
        {
            using var dbConnection = GetConnection();

            var sql = "INSERT INTO Users (Name) VALUES(@Name); SELECT CAST(SCOPE_IDENTITY() as int)";

            var ids = await dbConnection.QueryAsync<int>(sql, entity);

            entity.Id = ids.Single();

            return entity;
        }

        public async Task UpdateAsync(User entity)
        {
            using var dbConnection = GetConnection();

            await dbConnection.ExecuteAsync("UPDATE Users SET Name = @Name WHERE Id = @Id", entity);
        }

        private SqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("ChatConnection");
            return new SqlConnection(connectionString);
        }
    }
}
