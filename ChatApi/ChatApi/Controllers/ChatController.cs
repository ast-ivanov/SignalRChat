using ChatApi.Domain.Entities;
using ChatApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IAsyncRepository<Message> messageRepository;
        private readonly IAsyncRepository<User> userRepository;

        public ChatController(IAsyncRepository<Message> messageRepository,
                              IAsyncRepository<User> userRepository)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
        }

        [HttpGet("[action]")]
        public async Task<Message[]> GetMessages()
        {
            var messages = await messageRepository.GetAsync().ConfigureAwait(false);

            return messages;
        }

        [HttpGet("[action]")]
        public async Task<User[]> GetUsers()
        {
            var users = await userRepository.GetAsync().ConfigureAwait(false);

            return users;
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> CreateUser(User user)
        {
            var newUser = await userRepository.InsertAsync(user).ConfigureAwait(false);

            return Ok(newUser);
        }

        //todo Удалить
        [HttpPut("[action]")]
        public async Task<ActionResult> CreateMessage(Message message)
        {
            var newMessage = await messageRepository.InsertAsync(message).ConfigureAwait(false);

            return Ok(newMessage);
        }
    }
}
