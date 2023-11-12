using git_todo_tracker.Dtos.Todo;
using Microsoft.AspNetCore.Mvc;

namespace git_todo_tracker.Controllers;

[ApiController]
[Route("api/repository")]
public class RepositoryController : ControllerBase
{
    [HttpGet("list")]
    public ActionResult<IEnumerable<string>> ListRepositories()
    {
        return Ok(new string[] {
            "repo-id-1",
            "repo-id-2",
        });
    }

    [HttpPost("register")]
    public ActionResult<string> RegisterGitRepository(RegisterGitRepositoryRequest registerGitRepositoryRequest)
    {
        return Ok("repo-id");
    }

    [HttpDelete("remove")]
    public ActionResult RemoveGitRepository(string repoId)
    {
        return Ok();
    }
}

