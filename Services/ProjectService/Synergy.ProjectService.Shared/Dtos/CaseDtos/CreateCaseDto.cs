namespace Synergy.ProjectService.Shared.Dtos.CaseDtos;

public enum StatusDto
{
    Start,
    Continue,
    Done,
    Canceled
}
public record CreateCaseDto(string ProjectId, string Title, string? MemberId, StatusDto CaseStatus, DateTime StartDate, DateTime? EndDate);

