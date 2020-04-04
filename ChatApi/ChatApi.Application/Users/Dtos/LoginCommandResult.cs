namespace ChatApi.Application.Users.Dtos
{
    public class LoginCommandResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public UserChatDto User { get; set; }
    }
}