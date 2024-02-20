using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.MemberDtos;

namespace Synergy.TeamService.Application.Queries.GetMemberDetails;

public class GetMemberDetailsQueryHandler : IRequestHandler<GetMemberDetailsQuery, IResult<MemberDetailsDto>>
{
    private readonly IMemberRepo _memberRepo;
    private readonly ISkillRepo _skillRepo;
    private readonly ITechnologyRepo _technologyRepo;

    public GetMemberDetailsQueryHandler(IMemberRepo memberRepo, ISkillRepo skillRepo, ITechnologyRepo technologyRepo)
    {
        _memberRepo = memberRepo;
        _skillRepo = skillRepo;
        _technologyRepo = technologyRepo;
    }

    public async Task<IResult<MemberDetailsDto>> Handle(GetMemberDetailsQuery request, CancellationToken cancellationToken)
    {
        var memberQuery = await _memberRepo.GetAsync(x => x.Id == Guid.Parse(request.DeveloperId), x => x.Contact);

        if (!memberQuery.Any())
        {
            return Result<MemberDetailsDto>.Failure(404);
        }

        var memberSkillQuery = await _skillRepo.GetAsync(x => x.MemberId == Guid.Parse(request.DeveloperId));


        var member = await memberQuery.SingleOrDefaultAsync();
        var skills = await memberSkillQuery.ToListAsync();

        var memberSkills = new List<MemberSkill>();

        foreach (var item in skills)
        {
            var technology = await _technologyRepo.GetTechnology(item.TechnologyId);
            if (technology is not null)
            {
                memberSkills.Add(new MemberSkill(technology.Name, item.Experience));
            }
        }


        var memberDto = new MemberDto(member!.Id.ToString(), member.GivenName, member.LastName, member.Photo, member.Title, member.TeamId.ToString()!);
        var memberContact = new MemberContact(member.Contact.PhoneNumber, member.Contact.Address);

        var result = new MemberDetailsDto(memberDto, memberContact, memberSkills);

        return Result<MemberDetailsDto>.Success(value: result);
    }
}
