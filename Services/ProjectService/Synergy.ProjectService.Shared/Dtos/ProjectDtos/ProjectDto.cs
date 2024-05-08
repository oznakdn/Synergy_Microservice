namespace Synergy.ProjectService.Shared.Dtos.ProjectDtos;

public record ProjectDto(string Id, string Title, string Description, string? Github, string StartDate, string EndDate, string Team);
