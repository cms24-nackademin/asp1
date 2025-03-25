namespace WebApp.Middlewares;

public static class RootRedirectMiddlewareExtensions
{
    public static IApplicationBuilder UseRootRedirect(this IApplicationBuilder app, string redirectUrl)
    {
        return app.UseMiddleware<RootRedirectMiddleware>(redirectUrl);
    }
}

public class RootRedirectMiddleware(RequestDelegate next, string redirectUrl)
{
    private readonly RequestDelegate _next = next;
    private readonly string _redirectUrl = redirectUrl;

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;
        if (path == "/" || path == "~/")
        {
            context.Response.Redirect(_redirectUrl);
            return;
        }

        await _next(context);
    }
}
