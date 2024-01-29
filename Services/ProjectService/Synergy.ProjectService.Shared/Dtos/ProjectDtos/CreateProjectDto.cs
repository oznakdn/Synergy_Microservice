namespace Synergy.ProjectService.Shared.Dtos.ProjectDtos;

public record CreateProjectDto(string Title, string Description, StatusDto ProjectStatus, DateTime StartDate, DateTime EndDate, string TeamId);

public enum StatusDto
{
    Start,
    Continue,
    Done,
    Canceled
}