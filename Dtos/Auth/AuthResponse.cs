using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Dtos.Auth
{
    public class AuthResponse
    {
        public required string? AccessToken { get; set; }

        public required string? RefreshToken { get; set; }

        public required string Message { get; set; }
        public required int StatusCode { get; set; }
    }
}