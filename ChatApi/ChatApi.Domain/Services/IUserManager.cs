using System.Threading.Tasks;
using ChatApi.Domain.Entities;

namespace ChatApi.Domain.Services
{
    public interface IUserManager
    {
        Task<(int? UserId, string Message)> LoginAsync(string name, string password);
    }
}