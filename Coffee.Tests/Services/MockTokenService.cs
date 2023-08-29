using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Coffee.Domain.Services;

namespace Coffee.Tests.Services;

public class MockTokenService : ITokenService
{
    private ClaimsPrincipal _claimsPrincipal = new();

    public MockTokenService()
    {
        _claimsPrincipal = GetClaimsFromToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InJvYmluLXdheW5lLWNvbSIsInJvbGUiOiJDdXN0b21lciIsIm5iZiI6MTY5MTE3MjE4MCwiZXhwIjoxNjkxMjAwOTgwLCJpYXQiOjE2OTExNzIxODB9.FZS3Qy7tWSteHNIEV9UR2OVF-aeIRdrWyvOv3C7l8-c");
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Configuration.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new (ClaimTypes.Name, user.Link),
                new (ClaimTypes.Role, user.Type.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Configuration.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[50];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetClaimsFromToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.JwtKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public async Task SaveRefreshTokenAsync(string username, string refreshToken)
    {
        await Task.CompletedTask;
        ListToken._refreshTokens.Add(new(username, refreshToken));
    }
    public async Task<string?> GetRefreshTokenAsync(string username)
    {
        await Task.CompletedTask;
        return ListToken._refreshTokens.FirstOrDefault(x => x.Item1 == username).Item2;
    }

    public void DeleteRefreshToken(string username)
    {
        var item = ListToken._refreshTokens.FirstOrDefault(x => x.Item1 == username);
        ListToken._refreshTokens.Remove(item);
    }

    public void LoadClaimsPrincipal(IEnumerable<Claim> claims)
    {
        _claimsPrincipal = GetClaimsFromToken(GenerateToken(claims));
    }

    public ClaimsPrincipal GetUserClaims()
    {
        return _claimsPrincipal;
    }

}

