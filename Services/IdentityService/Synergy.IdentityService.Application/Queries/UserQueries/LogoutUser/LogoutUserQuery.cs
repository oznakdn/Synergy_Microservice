using MediatR;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.LogoutUser;

public class LogoutUserQuery : IRequest<Result>
{
    public LogoutUserQuery(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public string RefreshToken { get;set; }
}
