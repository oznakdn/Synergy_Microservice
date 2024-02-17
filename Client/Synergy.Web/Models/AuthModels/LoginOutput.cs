namespace Synergy.Web.Models.AuthModels;

public record LoginOutput(string Token, DateTimeOffset TokenExpire, string RefreshToken, DateTimeOffset RefreshExpire, UserModel User);

