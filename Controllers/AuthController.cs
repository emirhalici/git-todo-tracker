using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using git_todo_tracker.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace git_todo_tracker.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet("me")]
    public ActionResult<string> GetMe()
    {
        return Ok("user-id");
    }

    [HttpPost("register")]
    public ActionResult<string> Register()
    {
        return Ok("access-token");
    }

    [HttpPost("login")]
    public ActionResult<string> Login(LoginRequest loginRequest)
    {
        return Ok("access-token");
    }

    [HttpPost("refresh-token")]
    public ActionResult<string> RefreshToken()
    {
        return Ok("access-token");
    }

    [HttpPost("recovery/initiate")]
    public void InitiateRecovery(InitiateRecoveryRequest initiateRecoveryRequest)
    {
    }

    [HttpPost("recovery/reset-password")]
    public ActionResult<PasswordResetResponse> InitiateRecovery(PasswordResetRequest passwordResetRequest)
    {
        return Ok(new PasswordResetResponse() { Message = "Internal failure" });
    }
}
