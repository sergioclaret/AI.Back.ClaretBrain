namespace ClaretBrain.Api.Filters;

public class TokenAuthFilter
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _cfg;

    public TokenAuthFilter(RequestDelegate next, IConfiguration cfg)
    {
        _next = next; _cfg = cfg;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger") ||
            context.Request.Path.StartsWithSegments("/health") ||
            context.Request.Path.StartsWithSegments("/auth/login"))
        {
            await _next(context); return;
        }

        var token = context.Request.Headers["x-panel-token"].FirstOrDefault()
                    ?? context.Request.Query["token"].FirstOrDefault();
        var expected = _cfg["Auth:Token"];
        if (!string.IsNullOrWhiteSpace(expected) && token != expected)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "unauthorized" });
            return;
        }

        await _next(context);
    }
}
