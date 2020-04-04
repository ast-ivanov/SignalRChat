using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChatApi.Domain.Services.Impl
{
    public class UserManager : IUserManager
    {
        private readonly IConfiguration _configuration;

        public UserManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<(int? UserId, string Message)> LoginAsync(string name, string password)
        {
            using var dbConnection = GetConnection();

            var sql = $@"
DECLARE	@userID int,
		@responseMessage nvarchar(250)

EXEC	[dbo].[LoginUserSP]
		{name},
		{password},
		@userID OUTPUT,
		@responseMessage OUTPUT

SELECT	@userID as UserId, @responseMessage as Message";

            var result = await dbConnection.QuerySingleAsync(sql);

            return (result.UserId, result.Message);
        }

        private SqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("ChatConnection");
            return new SqlConnection(connectionString);
        }
    }
}