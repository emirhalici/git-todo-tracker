using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using git_todo_tracker.Repositories.RefreshToken;


namespace git_todo_tracker.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponse> Login(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.MailAddress);
            if (user is null)
            {
                return new AuthResponse()
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "User doesn't exist.",
                    StatusCode = 404,
                };
            }

            var result = await signInManager.PasswordSignInAsync(user, loginRequest.Password, true, false);
            if (!result.Succeeded)
            {
                return new AuthResponse()
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "Invalid credentials.",
                    StatusCode = 401,
                };
            }

            var accessToken = GenerateAccessToken(user);
            var refreshToken = await refreshTokenRepository.Create(user);

            return new AuthResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                Message = "Login successful.",
                StatusCode = 200,
            };
        }

        public async Task<AuthResponse> Register(RegisterRequest registerRequest)
        {
            var existingUser = await userManager.FindByEmailAsync(registerRequest.MailAddress);

            if (existingUser is not null)
            {
                return new AuthResponse()
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "User already exists.",
                    StatusCode = 409,
                };
            }

            var newUser = new User()
            {
                UserName = registerRequest.MailAddress,
                Email = registerRequest.MailAddress,
            };
            var result = await userManager.CreateAsync(newUser, registerRequest.Password);
            if (!result.Succeeded)
            {
                var err = result.Errors.First().Description;
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthResponse()
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Message = $"Registration failed! {errors}",
                    StatusCode = 400,
                };
            }

            var accessToken = GenerateAccessToken(newUser);
            var refreshToken = await refreshTokenRepository.Create(newUser);

            return new AuthResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                Message = "Registration successful.",
                StatusCode = 200,
            };
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
            }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }


}