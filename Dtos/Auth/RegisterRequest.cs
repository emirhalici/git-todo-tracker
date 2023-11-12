namespace git_todo_tracker.Dtos.Auth
{
    public class RegisterRequest
    {
        public required string MailAddress { get; set; }
        public required string Password { get; set; }
    }
}