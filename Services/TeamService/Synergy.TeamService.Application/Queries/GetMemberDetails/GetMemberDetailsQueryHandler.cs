using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.MemberDtos;

namespace Synergy.TeamService.Application.Queries.GetMemberDetails;

public class GetMemberDetailsQueryHandler : IRequestHandler<GetMemberDetailsQuery, IResult<MemberDetailsDto>>
{
    private readonly IMemberRepo _developerRepo;
    private readonly ISkillRepo _developerSkillRepo;

    public GetMemberDetailsQueryHandler(IMemberRepo developerRepo, ISkillRepo developerSkillRepo)
    {
        _developerRepo = developerRepo;
        _developerSkillRepo = developerSkillRepo;
    }

    public async Task<IResult<MemberDetailsDto>> Handle(GetMemberDetailsQuery request, CancellationToken cancellationToken)
    {
        var developerQuery = await _developerRepo.GetAsync(x => x.Id == Guid.Parse(request.DeveloperId), x => x.Contact, y => y.Team!);
        var developerSkillQuery = await _developerSkillRepo.GetAsync(x => x.MemberId == Guid.Parse(request.DeveloperId), x => x.Technology!);

        var developer = await developerQuery.SingleOrDefaultAsync();
        var skills = await developerSkillQuery.ToListAsync();

        if (developer is null)
        {
            return Result<MemberDetailsDto>.Failure(404);
        }

        var developerContact = new MemberContact(developer.Contact.PhoneNumber, developer.Contact.Address);
        var developerSkill = skills.Select(x => new MemberSkill(x.Technology!.Name, x.Experience)).ToList();
        var result = new MemberDetailsDto(
            new MemberDto(
             developer.Id.ToString()
            , developer.GivenName,
             developer.LastName,
             developer.Photo,
             developer.Title,
             developer.Team!.TeamName)
            , developerContact,
            developerSkill);
        return Result<MemberDetailsDto>.Success(value: result);
    }
}
