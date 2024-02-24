using MediatR;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.UserCommands.RegisterUser;

/// Handles registering a new user. Validates username is unique, creates a new User entity, and persists it via the user repository. 
/// Returns a Result indicating success or failure.
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
            return Result.Failure(400, error: "You cannot use this username!");

        var user = new User
        {
            Username = request.Register.Username,
            Email = request.Register.Email,
            Password = request.Register.Password
        };

        await userRepo.CreateAsync(user);
        return Result.Success(204);
    }
}

/*
This is a class that handles registering a new user.

It takes in a RegisterUserCommand object which contains the registration details, and a CancellationToken for async operation.

It returns a Result object indicating success or failure of the registration.

The main logic is:

1. Check if the username already exists by calling the userRepo's GetAsync method. If it does, return failure.

2. Create a new User object with the registration details.

3. Call the userRepo's CreateAsync method to persist the new user.

4. Return a success result.

So in summary, it validates that the username is unique, creates a User entity with the provided details, saves it to storage via the repository, and returns the result of the operation. The main purpose is to encapsulate the user registration logic in a reusable way that other application code can call. It focuses on the specific task of registering a user while handling all the necessary validations, conversions, and persistence operations under the hood.

*/