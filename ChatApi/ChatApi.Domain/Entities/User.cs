namespace ChatApi.Domain.Entities
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
