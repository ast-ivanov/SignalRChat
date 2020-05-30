namespace ChatApi.Application.Models
{
    public class ApplicationSettings
    {
        public string Client_URL { get; set; }

        public string JWT_Secret { get; set; }

        public int TokenLifeSpanInDays { get; set; }
    }
}
