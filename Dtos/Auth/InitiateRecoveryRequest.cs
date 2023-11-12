using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Dtos.Auth
{
    public class InitiateRecoveryRequest
    {
        public required string EmailAddress { get; set; }
    }
}