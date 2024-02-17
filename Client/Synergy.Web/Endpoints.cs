namespace Synergy.Web;

public class Endpoints
{
    public Identity Identity { get; set; }
    public Team Team { get; set; }
    public Project Project { get; set; }

}

public class Identity
{
    public string Login { get; set; }
    public string Register { get; set; }
    public string Logout { get; set; }
    public string GetProfile { get; set; }
    public string Relogin { get; set; }
    public string GetUsers { get; set; }
    public string GetRoles { get; set; }
    public string CreateRole { get; set; }
    public string UpdateRole { get; set; }
    public string AssignRole { get; set; }
}

public class Team
{
    public string GetTeams { get; set; }
    public string GetTeamById { get; set; }
    public string CreateTeam { get; set; }
    public string GetDevelopers { get; set; }
    public string GetDevelopersByTeamId { get; set; }
    public string GetDeveloperDetails { get; set; }
    public string CreateDeveloper { get; set; }
    public string AddDeveloperSkill { get; set; }
    public string GetTechnologies { get; set; }
    public string CreateTechnology { get; set; }

}

public class Project
{
    public string GetProjects { get; set; }
    public string CreateProject { get; set; }

}
