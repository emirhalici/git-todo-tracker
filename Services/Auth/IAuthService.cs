namespace git_todo_tracker.Services.Auth
{
    public interface IAuthService
    {
        public Task<AuthResponse> Register(RegisterRequest registerRequest);
        public Task<AuthResponse> Login(LoginRequest loginRequest);
        public Task<AuthResponse> RefreshToken(string refreshToken);
    }
}