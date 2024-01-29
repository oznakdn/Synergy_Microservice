namespace Synergy.WebApp.Helpers;

public static class CookieHelper
{
    private static IHttpContextAccessor _contextAccessor = new HttpContextAccessor();

    public static void SetCookie(string key, string value, DateTime expires) => _contextAccessor.HttpContext.Response.Cookies.Append(key, value, new CookieOptions
    {
        Expires = expires,
    });
    public static string GetCookie(string key) => _contextAccessor.HttpContext.Request.Cookies[key];
    public static void RemoveCookie(string key) => _contextAccessor.HttpContext.Response.Cookies.Delete(key);

}
