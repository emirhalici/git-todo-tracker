namespace git_todo_tracker.Services.Auth
{
    public class AuthService : IAuthService
    {
        public Task<AuthResponse> Login(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> Register(RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }
    }
}