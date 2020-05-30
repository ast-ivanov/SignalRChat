namespace ChatApi.Application.Users.Dtos
{
    public class RegistrationCommandResult
    {
        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }
    }
}
