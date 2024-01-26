namespace Synergy.IdentityService.Application.TokenService.Options;

internal class TokenOption : ITokenOption
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Key { get; set; }
}
