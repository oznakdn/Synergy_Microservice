namespace Synergy.ProjectService.Shared.Dtos.ProjectDtos;

public record CreateProjectDto(string Title, string Description,string Github, DateTime StartDate, DateTime EndDate, string TeamId);
