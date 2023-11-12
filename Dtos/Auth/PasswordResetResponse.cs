using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Dtos.Auth
{
    public class PasswordResetResponse
    {
        public required string Message { get; set; }
        public string? RefreshToken { get; set; }
    }
}