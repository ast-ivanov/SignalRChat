namespace ChatApi.Application.Users.Dtos
{
    public class RegisterCommandResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public int? CreatedId { get; set; }
    }
}