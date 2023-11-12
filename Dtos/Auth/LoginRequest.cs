using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Dtos.Auth
{
    public class LoginRequest
    {
        public required string MailAddress { get; set; }
        public required string Password { get; set; }
    }
}