using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Models
{
    public class GitRepository
    {
        public required string Id { get; set; }
        public string BaseUrl { get; set; } = "https://github.com";
        public required string UserName { get; set; }
        public required string ProjectName { get; set; }

        public string FullGitLink
        {
            get => $"{BaseUrl}/{UserName}/{ProjectName}";
        }

        public static GitRepository FromRegisterRequest(String id, RegisterGitRepositoryRequest registerRequest)
        {
            return new GitRepository()
            {
                Id = id,
                UserName = registerRequest.UserName,
                ProjectName = registerRequest.ProjectName,
            };
        }
    }
}