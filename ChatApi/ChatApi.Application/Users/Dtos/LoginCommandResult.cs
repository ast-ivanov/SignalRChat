namespace ChatApi.Application.Users.Dtos
{
    public class LoginCommandResult
    {
        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }

        public string Token { get; set; }
    }
}