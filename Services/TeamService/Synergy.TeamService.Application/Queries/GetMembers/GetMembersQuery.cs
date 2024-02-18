using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.MemberDtos;

namespace Synergy.TeamService.Application.Queries.GetMembers;

public class GetMembersQuery : IRequest<Result<MemberDto>>
{
}
