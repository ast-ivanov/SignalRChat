using System;
using ChatApi.Domain.Entities;

namespace ChatApi.Domain.Services.Impl
{
    /// <inheritdoc/>
    public class ValidationService : IValidationService
    {
        public (bool Success, string? Error) ValidateMessage(Message message)
        {
            throw new NotImplementedException();
            /*
            if (message.User is null)
            {
                return (false, "User is empty");
            }

            if (message.User.Id == 0)
            {
                return (false, $"User '{message.User.Name}' doesn't exist");
            }

            if (string.IsNullOrEmpty(message.Text))
            {
                return (false, "Message is empty");
            }

            return (true, null);
            */
        }
    }
}
