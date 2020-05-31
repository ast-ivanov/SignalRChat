using System;
using ChatApi.Application.Users.Dtos;

namespace ChatApi.Application.Messages.Dtos
{
    /// <summary> Message DTO for sending </summary>
    public class MessageDto
    {
        /// <summary> Identifier </summary>
        public Guid Id { get; set; }

        /// <summary> Message text </summary>
        public string Text { get; set; }

        /// <summary> Send time </summary>
        public DateTime Time { get; set; }

        /// <summary> User </summary>
        public UserChatDto User { get; set; }
    }
}