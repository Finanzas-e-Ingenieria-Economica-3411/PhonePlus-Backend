using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;

namespace PhonePlus.Common.Auth;

public sealed class TokenService(IRoleRepository roleRepository, IConfiguration configuration) : ITokenService
{
    public async Task<(string, string)> GenerateCredentials(User user)
    {
        var claims = new List<Claim>();
        var role = await roleRepository.GetRoleName(user.RoleId);
        var secret = configuration["Secret"];
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.Username));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        claims.Add(new Claim(ClaimTypes.Role, role));

        if (secret != null)
        {
            var token = new JwtSecurityToken(
                issuer: "PhonePlus",
                audience: "PhonePlus",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), 
                    SecurityAlgorithms.HmacSha256
                )
            );
        
            var tokenGenerated = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenGenerated, role);
        }
        throw new BadHttpRequestException("Secret key not found");
    }

    public async Task<int?> ValidateToken(string token)
    {
        // If token is null or empty
        if (string.IsNullOrEmpty(token))
            return null;
        // Otherwise, perform validation
        var secret = configuration["Secret"];
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);
        try
        {
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            });
            Console.WriteLine("Hello");
            var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
            var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            Console.WriteLine($"UserId: {userId}");
            return userId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}