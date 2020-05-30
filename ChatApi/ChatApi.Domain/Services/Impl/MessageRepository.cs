using ChatApi.Domain.Entities;
using ChatApi.Domain.Models.Exceptions;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Domain.Services.Impl
{
    public class MessageRepository : IAsyncRepository<Message>
    {
        private readonly IConfiguration _configuration;
        private readonly IValidationService _validationService;

        public MessageRepository(IConfiguration configuration, IValidationService validationService)
        {
            _configuration = configuration;
            _validationService = validationService;
        }

        public async Task DeleteAsync(Message entity)
        {
            using var dbConnection = GetConnection();

            await dbConnection.ExecuteAsync("DELETE FROM Messages WHERE Id = @Id", new { entity.Id });
        }

        public async Task<Message[]> GetAsync()
        {
            using var dbConnection = GetConnection();

            string Sql = @"
SELECT m.Id, m.Text, m.Time, u.Id, u.UserName FROM Messages m
JOIN AspNetUsers u ON u.Id = m.UserId";

            var messages = await dbConnection.QueryAsync<Message, User, Message>(Sql, (m, u) =>
            {
                m.User = u;

                return m;
            });

            return messages.ToArray();
        }

        public async Task<Message> GetAsync(object id)
        {
            using var dbConnection = GetConnection();

            string Sql = @"
SELECT m.Id, m.Text, m.Time, u.Id, u.UserName FROM Messages m
JOIN AspNetUsers u ON u.Id = m.UserId
WHERE m.Id = @id";

            var messages = await dbConnection.QueryAsync<Message, User, Message>(Sql, (m, u) =>
            {
                m.User = u;

                return m;
            },
            new { id });

            return messages.Single();
        }

        public async Task<Message> InsertAsync(Message entity)
        {
            var (success, error) = _validationService.ValidateMessage(entity);

            if (!success)
            {
                throw new ValidationException(error!);
            }

            using var dbConnection = GetConnection();

            var sql = @"
INSERT INTO Messages (Text, Time, UserID)
OUTPUT inserted.Id
VALUES(@Text, @Time, @User)";

            var message = new
            {
                entity.Text,
                entity.Time,
                User = entity.User.Id
            };

            var ids = await dbConnection.QueryAsync<Guid>(sql, message);

            entity.Id = ids.Single();

            return entity;
        }

        public async Task UpdateAsync(Message entity)
        {
            var (success, error) = _validationService.ValidateMessage(entity);

            if (!success)
            {
                throw new ValidationException(error!);
            }

            using var dbConnection = GetConnection();
            
            var message = new
            {
                entity.Text,
                entity.Time,
                User = entity.User.Id
            };

            await dbConnection.ExecuteAsync("UPDATE Messages SET Text = @Text, Time = @Time, UserID = @User WHERE Id = @Id", message);
        }

        private SqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("ChatConnection");
            return new SqlConnection(connectionString);
        }
    }
}
