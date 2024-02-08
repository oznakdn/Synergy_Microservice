namespace Synergy.WebApp.Models.UserModels;

public record LoginResponse(string Token, DateTimeOffset TokenExpire, string RefreshToken, DateTimeOffset RefreshExpire, UserModel User);
