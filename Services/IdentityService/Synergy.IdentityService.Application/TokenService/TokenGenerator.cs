using Microsoft.IdentityModel.Tokens;
using Synergy.IdentityService.Application.TokenService.Options;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Synergy.IdentityService.Application.TokenService;


/**
 * Generates a JWT access token, refresh token, and associated user DTO
 * for the provided user.
 * 
 * Signs the JWT with HMAC SHA256 using the configured private key.
 * Sets claims for standard fields like ID, name, email, and role.
 * Expires access tokens after 5 days, refresh after 6.
 *  
 * Returns a TokenDto containing the generated tokens and user info.
 */
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


        var userDto = new UserDto(user.Id, user.Username, user.Email, user.MemberId, user.Role!.RoleName ?? string.Empty);

        return new TokenDto(
            Token: token,
            TokenExpire: DateTime.Now.AddDays(5),
            RefreshToken: refreshToken,
            RefreshExpire: DateTime.Now.AddDays(6),
            User: userDto);
    }

    private string GenerateRefreshToken() => Guid.NewGuid().ToString();

}


/*
The TokenGenerator class is responsible for generating JWT authentication tokens and refresh tokens for a user. It takes a User object as input and returns a TokenDto object containing the generated tokens and user information.

The class has a dependency on an ITokenOption interface which provides the JWT signing configuration like the secret key, issuer, audience etc. This is injected via the constructor.

The main logic happens in the GenerateToken method. It first creates the cryptographic components needed to sign the JWT - the secret key and the signing credentials object.

It then builds the JWT claims - things like user ID, name, email etc. It adds the user's role as a claim if it exists.

Next it creates the JWT descriptor object with all the signing details and claims. This is used by the JWT handler to create the actual JWT token string.

It also generates a new random refresh token string.

Finally it creates a UserDto object from the User object to return relevant user details.

The TokenDto returned contains the generated JWT token, refresh token, their expiration times, and the UserDto.

So in summary, this class takes in a User, leverages JWT libraries to create signed JWT and refresh tokens for that user, and returns these tokens along with user details in a structured TokenDto object. The purpose is to encapsulate the JWT generation logic and provide easy access to properly formed auth tokens for a user.
*/
