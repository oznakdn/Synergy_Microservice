using Synergy.TeamService.Domain.Models.Abstracts;

namespace Synergy.TeamService.Domain.Models;

public class Contact : Entity
{
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public Guid MemberId { get; set; }
    public Member? Member { get; set; }

}
