using git_todo_tracker.Dtos.Todo;
using Microsoft.AspNetCore.Mvc;
namespace git_todo_tracker.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;

    public TodoController(ILogger<TodoController> logger)
    {
        _logger = logger;
    }

    [HttpGet("list")]
    public IEnumerable<string> List(string repoId)
    {
        return new string[] {
            "do something",
            "do something else"
        };
    }

    [HttpPost("convert-to-issue")]
    public bool ConvertToIssue(ConvertToIssueRequest convertToIssueRequest)
    {
        return false;
    }
}
