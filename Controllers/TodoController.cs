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
    public ActionResult<IEnumerable<string>> List(string repoId)
    {
        return Ok(new string[] {
            "do something",
            "do something else"
        });
    }

    [HttpPost("convert-to-issue")]
    public ActionResult<bool> ConvertToIssue(ConvertToIssueRequest convertToIssueRequest)
    {
        return Ok(false);
    }
}
