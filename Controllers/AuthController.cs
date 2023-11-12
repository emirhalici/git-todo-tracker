using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using git_todo_tracker.Dtos.Auth;
using git_todo_tracker.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace git_todo_tracker.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> logger;
    private readonly IAuthService authService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        this.logger = logger;
        this.authService = authService;
    }

    [HttpGet("me")]
    public ActionResult<string> GetMe()
    {
        return Ok("user-id");
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest registerRequest)
    {
        var response = await authService.Register(registerRequest);
        bool success = response.RefreshToken is null || response.AccessToken is null;
        return success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest loginRequest)
    {
        var response = await authService.Login(loginRequest);
        bool success = response.RefreshToken is null || response.AccessToken is null;
        return success ? Ok(response) : BadRequest(response);
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
