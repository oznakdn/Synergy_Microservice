using Synergy.ProjectService.Shared.Dtos.CommentDtos;

namespace Synergy.ProjectService.Shared.Dtos.CaseDtos;

public record CaseDto(string Id, string Title, string Description,string Status, List<CommentDto>Comments);

