using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryAPI.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username)
        {
            var secretKey = _configuration.GetValue<string>("JwtSettings:SecretKey") ?? throw new InvalidOperationException("SecretKey is not configured.");
            var key = Encoding.UTF8.GetBytes(secretKey);
            var issuer = _configuration.GetValue<string>("JwtSettings:Issuer");
            var audience = _configuration.GetValue<string>("JwtSettings:Audience");

            // Get expiration time from configuration (default to 60 minutes if not set)
            var expirationMinutes = _configuration.GetValue<int>("JwtSettings:TokenExpirationMinutes", 60);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),  // Use the dynamic expiration time here
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
