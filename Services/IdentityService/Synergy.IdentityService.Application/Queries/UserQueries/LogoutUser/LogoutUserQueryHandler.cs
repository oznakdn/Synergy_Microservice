using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.LogoutUser;

public class LogoutUserQueryHandler : IRequestHandler<LogoutUserQuery, Result>
{
    private readonly IUserRepository _userRepo;

    public LogoutUserQueryHandler(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<Result> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetAsync(_ => _.Token == request.RefreshToken);

        if (user is null)
            return Result.Failure(404);

        user.Token = string.Empty;
        user.TokenExpire = null;

        await _userRepo.Update(user);
        return Result.Success(200, "User was be log out.");
    }
}
