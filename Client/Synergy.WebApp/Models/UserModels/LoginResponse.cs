﻿namespace Synergy.WebApp.Models.UserModels;

public record LoginResponse(string Token, string TokenExpire, UserModel User);
