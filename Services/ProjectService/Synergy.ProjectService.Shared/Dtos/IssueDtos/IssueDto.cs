using Synergy.ProjectService.Shared.Dtos.CommentDtos;

namespace Synergy.ProjectService.Shared.Dtos.IssueDtos;

public record IssueDto(string Id, string Summary, string Description, List<CommentDto>Comments);

