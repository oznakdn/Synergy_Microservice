
namespace Synergy.TeamService.Domain.Models.Abstracts;

public abstract class AuditableEntity : IEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set;}

    public DateTime? DeleteDate { get; set; }
    public string? DeleteBy { get; set; }

}
