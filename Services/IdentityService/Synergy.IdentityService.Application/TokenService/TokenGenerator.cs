using Microsoft.IdentityModel.Tokens;
using Synergy.IdentityService.Application.TokenService.Options;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Synergy.IdentityService.Application.TokenService;

public class TokenGenerator : ITokenGenerator
{
    private readonly ITokenOption _option;
    public TokenGenerator(ITokenOption option)
    {
        _option = option;
    }



    public TokenDto GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.Username),
            new Claim(ClaimTypes.Email,user.Email)
        };

        if (user.Role is not null)
        {
            claims.Add(new Claim(ClaimTypes.Role, user.Role.RoleName));
        }


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials,
            Audience = _option.Audience,
            Issuer = _option.Issuer,
            Expires = DateTime.Now.AddDays(5),
            Subject = new ClaimsIdentity(claims)
        };

        var securityToken = new JwtSecurityTokenHandler();
        SecurityToken createdToken = securityToken.CreateToken(tokenDescriptor);
        var token = securityToken.WriteToken(createdToken);
        var refreshToken = GenerateRefreshToken();


        return new TokenDto(
            Token: token,
            TokenExpire: DateTime.Now.AddDays(5),
            RefreshToken: refreshToken,
            RefreshExpire: DateTime.Now.AddDays(6),
            User: new UserDto(user.Id, user.Username, user.Email, user.Role != null ? user.Role.RoleName : default));
    }

    private string GenerateRefreshToken() => Guid.NewGuid().ToString();


}
