using Synergy.TeamService.Domain.Models.Abstracts;

namespace Synergy.TeamService.Domain.Models;

public class Contact : Entity
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public Guid DeveloperId { get; set; }
    public Developer? Developer { get; set; }

}
