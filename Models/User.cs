
namespace git_todo_tracker.Models
{
    public class User
    {
        public required string MailAddress { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime RefreshTokenExpiration { get; set; }

        public AuthResponse ToAuthResponse(string AccessToken)
        {
            return new AuthResponse()
            {
                Message = "Successful.",
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
            };
        }
    }
}