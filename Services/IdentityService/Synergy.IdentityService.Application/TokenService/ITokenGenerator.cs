using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Shared.Dtos.UserDtos;

namespace Synergy.IdentityService.Application.TokenService;

public interface ITokenGenerator
{
    TokenDto GenerateToken(User user);
}
