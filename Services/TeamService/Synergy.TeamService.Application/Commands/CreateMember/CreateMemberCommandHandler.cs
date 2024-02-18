using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Application.Commands.CreateMember;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result>
{
    private readonly IRepositoryManager _manager;

    public CreateMemberCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var developer = new Member
        {
            CreatedDate = DateTime.Now,
            CreatedBy = request.CreatedBy,
            GivenName = request.CreateMember.GivenName,
            LastName = request.CreateMember.LastName,
            Photo = request.CreateMember.Photo,
            Title = request.CreateMember.Title,
            TeamId = Guid.Parse(request.CreateMember.TeamId)
        };
        _manager.Member.Insert(developer);

        var contact = new Contact
        {
            MemberId = developer.Id,
            Email = request.CreateMember.ContractDto.Email,
            PhoneNumber = request.CreateMember.ContractDto.PhoneNumber,
            Address = request.CreateMember.ContractDto.Address
        };

        _manager.Contact.Insert(contact);

        await _manager.SaveAsync(cancellationToken);

        return Result.Success(204);
    }
}
