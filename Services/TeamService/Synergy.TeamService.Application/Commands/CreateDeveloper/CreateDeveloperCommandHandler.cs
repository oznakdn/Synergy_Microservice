using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Application.Commands.CreateDeveloper;

public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, Result>
{
    private readonly IRepositoryManager _manager;

    public CreateDeveloperCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
    {
        var developer = new Developer
        {
            CreatedDate = DateTime.Now,
            CreatedBy = request.CreatedBy,
            GivenName = request.CreateDeveloper.GivenName,
            LastName = request.CreateDeveloper.LastName,
            Photo = request.CreateDeveloper.Photo,
            Title = request.CreateDeveloper.Title,
            TeamId = Guid.Parse(request.CreateDeveloper.TeamId)
        };
        _manager.Developer.Insert(developer);

        var contact = new Contact
        {
            DeveloperId = developer.Id,
            Email = request.CreateDeveloper.ContractDto.Email,
            PhoneNumber = request.CreateDeveloper.ContractDto.PhoneNumber,
            Address = request.CreateDeveloper.ContractDto.Address
        };

        _manager.Contact.Insert(contact);

        await _manager.SaveAsync(cancellationToken);

        return Result.Success(204);
    }
}
