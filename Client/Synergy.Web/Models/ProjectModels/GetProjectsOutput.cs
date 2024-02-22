namespace Synergy.Web.Models.ProjectModels;

public record GetProjectsOutput(string Id, string Title, string Description, string ProjectStatus, string StartDate, string EndDate, string Team);
