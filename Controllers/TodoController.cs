using git_todo_tracker.Dtos.Todo;
using git_todo_tracker.Repositories.Repository;
using git_todo_tracker.Services.Repository;
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

    [HttpGet("list")]
    public ActionResult<IEnumerable<string>> List(string repoId)
    {
        var repository = repositoryRepository.GetById(repoId);
        if (repository is null)
        {
            return NotFound($"Git repository of {repoId} not found. Please register the repository first.");
        }

        gitRepositoryService.CloneOrPullRepository(repository);
        return Ok(gitRepositoryService.ScanForTodos(repository));
    }

    [HttpPost("convert-to-issue")]
    public ActionResult<bool> ConvertToIssue(ConvertToIssueRequest convertToIssueRequest)
    {
        return Ok(false);
    }
}
