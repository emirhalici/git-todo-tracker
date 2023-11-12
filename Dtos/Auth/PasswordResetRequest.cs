using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Dtos.Auth
{
    public class PasswordResetRequest
    {
        public required string EmailAddress { get; set; }
        public required string RecoveryKey { get; set; }
        public required string NewPassword { get; set; }
    }
}