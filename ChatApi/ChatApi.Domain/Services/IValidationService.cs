using ChatApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Domain.Services
{
    /// <summary>
    /// Validation service
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Validate message
        /// </summary>
        (bool Success, string? Error) ValidateMessage(Message message);
    }
}
