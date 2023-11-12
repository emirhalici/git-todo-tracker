using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Models
{
    public class GitRepository
    {
        public required string Id { get; set; }
        public required string BaseUrl { get; set; } = "https://github.com";
        public required string UserName { get; set; }
        public required string ProjectName { get; set; }

        public string FullGitLink
        {
            get => $"{BaseUrl}/{UserName}/{ProjectName}";
        }
    }
}