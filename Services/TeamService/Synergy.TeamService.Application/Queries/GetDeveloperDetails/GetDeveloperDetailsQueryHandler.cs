using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.DeveloperDtos;

namespace Synergy.TeamService.Application.Queries.GetDeveloperDetails;

public class GetDeveloperDetailsQueryHandler : IRequestHandler<GetDeveloperDetailsQuery, IResult<DeveloperDetailsDto>>
{
    private readonly IDeveloperRepo _developerRepo;
    private readonly IDeveloperSkillRepo _developerSkillRepo;

    public GetDeveloperDetailsQueryHandler(IDeveloperRepo developerRepo, IDeveloperSkillRepo developerSkillRepo)
    {
        _developerRepo = developerRepo;
        _developerSkillRepo = developerSkillRepo;
    }

    public async Task<IResult<DeveloperDetailsDto>> Handle(GetDeveloperDetailsQuery request, CancellationToken cancellationToken)
    {
        var developerQuery = await _developerRepo.GetAsync(x => x.Id == Guid.Parse(request.DeveloperId), x => x.Contact, y => y.Team!);
        var developerSkillQuery = await _developerSkillRepo.GetAsync(x => x.DeveloperId == Guid.Parse(request.DeveloperId), x => x.Technology!);

        var developer = await developerQuery.SingleOrDefaultAsync();
        var skills = await developerSkillQuery.ToListAsync();

        if (developer is null)
        {
            return Result<DeveloperDetailsDto>.Failure(404);
        }

        var developerContact = new DeveloperContact(developer.Contact.Email, developer.Contact.PhoneNumber, developer.Contact.Address);
        var developerSkill = skills.Select(x => new DeveloperSkill(x.Technology!.Name, x.Experience)).ToList();
        var result = new DeveloperDetailsDto(
            new DeveloperDto(
             developer.Id.ToString()
            , developer.GivenName,
             developer.LastName,
             developer.Photo,
             developer.Title,
             developer.Team!.TeamName)
            , developerContact,
            developerSkill);
        return Result<DeveloperDetailsDto>.Success(value: result);
    }
}
