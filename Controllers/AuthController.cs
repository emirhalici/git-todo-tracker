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
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest loginRequest)
    {
        var response = await authService.Login(loginRequest);
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthResponse>> RefreshToken(string refreshToken)
    {
        var response = await authService.RefreshToken(refreshToken);
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
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
