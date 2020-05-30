using System;
using System.Data;
using ChatApi.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.Domain.Constants;

namespace ChatApi.Domain.Services.Impl
{
    public class UserRepository : IAsyncRepository<User>
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
            using var dbConnection = GetConnection();

            await dbConnection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { entity.Id });
        }

        public async Task<User[]> GetAsync()
        {
            throw new NotImplementedException();
            using var dbConnection = GetConnection();

            var users = await dbConnection.QueryAsync<User>("SELECT * FROM Users");

            return users.ToArray();
        }

        public async Task<User> GetAsync(object id)
        {
            throw new NotImplementedException();
            using var dbConnection = GetConnection();

            var users = await dbConnection.QueryAsync<User>("SELECT * FROM Users WHERE Id = @id", new { id });

            return users.Single();
        }

        public async Task<User> InsertAsync(User entity)
        {
            throw new NotImplementedException();
            /*
            using var dbConnection = GetConnection();

            var sql = @$"
DECLARE @CreatedId int
DECLARE @ErrorNumber int

EXECUTE [dbo].[InsertUserSP]
   {entity.Name}
  ,{entity.Password}
  ,@CreatedId OUTPUT
  ,@ErrorNumber OUTPUT

SELECT @CreatedId AS CreatedId, @ErrorNumber AS ErrorNumber";

            var result = await dbConnection.QuerySingleAsync(sql);

            if (result.ErrorNumber == null)
            {
                entity.Id = result.CreatedId;
                return entity;
            }

            if (result.ErrorNumber == DbErrorNumbers.DuplicateKey)
            {
                throw new DuplicateNameException();
            }

            throw new Exception();
        */
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
