using git_todo_tracker.Dtos.Todo;
using Microsoft.AspNetCore.Mvc;

namespace git_todo_tracker.Controllers;

[ApiController]
[Route("api/repository")]
public class RepositoryController : ControllerBase
{
    [HttpGet("list")]
    public IEnumerable<string> ListRepositories()
    {
        return new string[] {
            "repo-id-1",
            "repo-id-2",
        };
    }

    [HttpPost("register")]
    public string RegisterGitRepository(RegisterGitRepositoryRequest registerGitRepositoryRequest)
    {
        return "repo-id";
    }

    [HttpDelete("remove")]
    public void RemoveGitRepository(string repoId)
    {
    }
}

