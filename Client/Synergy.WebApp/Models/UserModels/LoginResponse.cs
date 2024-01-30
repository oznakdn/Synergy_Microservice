namespace Synergy.WebApp.Models.UserModels;

public record LoginResponse(string Token, string TokenExpire, string RefreshToken, string RefreshExpire, UserModel User);
