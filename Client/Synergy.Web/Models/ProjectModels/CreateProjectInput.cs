namespace Synergy.Web.Models.ProjectModels;

public record CreateProjectInput(string Title, string Description, StatusDto ProjectStatus, DateTime StartDate, DateTime EndDate, string TeamId);

public enum StatusDto
{
    Start,
    Continue,
    Done,
    Canceled
}