using System;

namespace ChatApi.Domain.Entities
{
    /// <summary>
    /// Message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Message text
        /// </summary>
        public string Text { get; set; } = default!;

        /// <summary>
        /// Message sending time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        public User User { get; set; } = default!;
    }
}
