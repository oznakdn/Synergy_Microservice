using MediatR;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.UserCommands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly IUserRepository userRepo;

    public RegisterUserCommandHandler(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existUserName = await userRepo.GetAsync(_ => _.Username.Equals(request.Register.Username),cancellationToken);
        if (existUserName is not null)
            return Result.Failure(400, new List<string> { "You cannot use this username!" });

        var user = new User
        {
            Username = request.Register.Username,
            Password = request.Register.Password
        };

        await userRepo.CreateAsync(user);
        return Result.Success(204);
    }
}
