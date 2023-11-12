using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Dtos.Todo
{
    public class RegisterGitRepositoryRequest
    {
        public required string UserName { get; set; }
        public required string ProjectName { get; set; }
        public required string GitRepositoryUrl { get; set; }
    }
}