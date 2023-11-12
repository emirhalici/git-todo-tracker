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
    public string GetMe()
    {
        return "user-id";
    }

    [HttpPost("register")]
    public string Register()
    {
        return "access-token";
    }

    [HttpPost("login")]
    public string Login(LoginRequest loginRequest)
    {
        return "access-token";
    }

    [HttpPost("refresh-token")]
    public string RefreshToken()
    {
        return "access-token";
    }

    [HttpPost("recovery/initiate")]
    public void InitiateRecovery(InitiateRecoveryRequest initiateRecoveryRequest)
    {
    }

    [HttpPost("recovery/reset-password")]
    public PasswordResetResponse InitiateRecovery(PasswordResetRequest passwordResetRequest)
    {
        return new PasswordResetResponse() { Message = "Internal failure" };
    }
}
