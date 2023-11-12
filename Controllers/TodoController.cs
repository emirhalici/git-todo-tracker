using git_todo_tracker.Dtos.Todo;
using git_todo_tracker.Repositories.Repository;
using git_todo_tracker.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace git_todo_tracker.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> logger;
    private readonly GitRepositoryService gitRepositoryService;
    private readonly IRepositoryRepository repositoryRepository;

    public TodoController(ILogger<TodoController> logger, GitRepositoryService gitRepositoryService, IRepositoryRepository repositoryRepository)
    {
        this.logger = logger;
        this.gitRepositoryService = gitRepositoryService;
        this.repositoryRepository = repositoryRepository;
    }

    [Authorize]
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<string>>> List(string repoId)
    {
        var repository = await repositoryRepository.GetById(repoId);
        if (repository is null)
        {
            return NotFound($"Git repository of {repoId} not found. Please register the repository first.");
        }

        gitRepositoryService.CloneOrPullRepository(repository);
        return Ok(gitRepositoryService.ScanForTodos(repository));
    }

    [Authorize]
    [HttpPost("convert-to-issue")]
    public async Task<ActionResult<bool>> ConvertToIssue(ConvertToIssueRequest convertToIssueRequest)
    {
        return Ok(false);
    }
}
