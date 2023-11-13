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

        private readonly ILogger<AuthService> logger;

        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, ILogger<AuthService> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.refreshTokenRepository = refreshTokenRepository;
            this.logger = logger;
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

        public async Task<AuthResponse> RefreshToken(string refreshToken)
        {
            var invalidAuthResponse = new AuthResponse()
            {
                AccessToken = null,
                RefreshToken = null,
                Message = "Invalid token",
                StatusCode = 403,
            };
            var storedToken = await refreshTokenRepository.GetByToken(refreshToken);
            if (storedToken is null)
            {
                return invalidAuthResponse;
            }

            var user = await userManager.FindByIdAsync(storedToken.UserId);
            if (user is null)
            {
                return invalidAuthResponse;
            }

            var accessToken = GenerateAccessToken(user);
            return new AuthResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Message = "Successful.",
                StatusCode = 200,
            };
        }

        public async Task<User?> GetUserFromAccessToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                if (!tokenHandler.CanReadToken(accessToken))
                {
                    return null;
                }
                var token = tokenHandler.ReadJwtToken(accessToken);
                var email = token.Claims.SingleOrDefault(c => c?.Type == JwtRegisteredClaimNames.Email, null)?.Value;
                if (string.IsNullOrEmpty(email))
                {
                    return null;
                }
                return await userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                logger.LogError(ex.StackTrace);
            }

            return null;
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]!);

            var notBefore = DateTime.UtcNow.ToUniversalTime();
            var issuedAt = DateTime.UtcNow.AddSeconds(1).ToUniversalTime();
            var expires = DateTime.UtcNow.AddMinutes(5).ToUniversalTime();

            var claims = new List<Claim> {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Email ?? "empty-email"),
                new(JwtRegisteredClaimNames.Email, user.Email ?? "empty-email"),
                new(JwtRegisteredClaimNames.Iss, configuration["Jwt:Issuer"] ?? "empty-issuer"),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                IssuedAt = issuedAt,
                NotBefore = notBefore,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }


}