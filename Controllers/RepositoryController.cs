using git_todo_tracker.Dtos.Todo;
using git_todo_tracker.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace git_todo_tracker.Controllers;

[ApiController]
[Route("api/repository")]
public class RepositoryController : ControllerBase
{
    private readonly IRepositoryService repositoryService;

    public RepositoryController(IRepositoryService repositoryService)
    {
        this.repositoryService = repositoryService;
    }

    [HttpGet("list")]
    public ActionResult<IEnumerable<string>> ListRepositories()
    {
        var repos = repositoryService.GetAllRepositories();
        return Ok(repos);
    }

    [HttpPost("register")]
    public ActionResult<string> RegisterGitRepository(RegisterGitRepositoryRequest registerGitRepositoryRequest)
    {
        // TODO: Convert to PUT request if id is generated on server
        var registeredRepository = repositoryService.RegisterGitRepository(registerGitRepositoryRequest);
        return Ok(registeredRepository.Id);
    }

    [HttpDelete("remove")]
    public ActionResult RemoveGitRepository(string repoId)
    {
        var removed = repositoryService.RemoveGitRepository(repoId);
        return removed ? Ok() : BadRequest();
    }
}

