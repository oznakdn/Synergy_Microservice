namespace Synergy.ProjectService.Shared.Dtos.IssueDtos;

public enum StatusDto
{
    Start,
    Continue,
    Done,
    Canceled
}
public record CreateIssueDto(string StatusId, string Summary, string? MemberId,int PriorityType,int IssueType, DateTime StartDate, DateTime? EndDate);

