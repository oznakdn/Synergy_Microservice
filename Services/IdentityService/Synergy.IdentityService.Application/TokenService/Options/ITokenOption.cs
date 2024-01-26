namespace Synergy.IdentityService.Application.TokenService.Options;

public interface ITokenOption
{
    string Audience { get; set; }
    string Issuer { get; set; }
    string Key { get; set; }
}
