using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Coffee.Domain.Models.User;
using Microsoft.IdentityModel.Tokens;
using Coffee.Domain.Services;
using System.Security.Cryptography;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Api.Services;

public class TokenService : ITokenService
{
    private readonly IRefreshLoginUserRepository _refreshTokens;
    public static ClaimsPrincipal _claimsPrincipal = new();

    public TokenService(IRefreshLoginUserRepository refreshTokens)
    {
        _refreshTokens = refreshTokens;
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

    public async Task SaveRefreshTokenAsync(string userName, string refreshToken)
    {
        var refreshLoginUser = new RefreshLoginUser(userName, refreshToken);

        await _refreshTokens.CreateAsync(refreshLoginUser);
    }
    public async Task<string?> GetRefreshTokenAsync(string userName)
    {
        var refreshToken = await _refreshTokens.GetByUserNameAsync(userName);
        return refreshToken;
    }

    public async void DeleteRefreshToken(string userName)
    {
        var refreshToken = await GetRefreshTokenAsync(userName);
        if (refreshToken != null)
        {
            var item = await _refreshTokens.GetByUserNameAndRefreshTokenAsync(userName, refreshToken);
            if (item != null)
                _refreshTokens.Delete(item);
        }
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
