using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Dtos.Todo
{
    public class ConvertToIssueRequest
    {
        public required string UserName { get; set; }
        public required string RepositoryName { get; set; }
        public required string GitRepositoryUrl { get; set; }
        public required string TodoLine { get; set; }
        public string? IssueMessage { get; set; }
    }
}