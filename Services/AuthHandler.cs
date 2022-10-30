using System.Text.Json;
using AboutUs.Models;
namespace AboutUs.Services;
public class AuthHandler
{
    private readonly RequestDelegate _next;
    private readonly SessionService _sessionService;
    public AuthHandler(RequestDelegate next, SessionService sessionService)
    {
        _next = next;
        _sessionService = sessionService;
    }
    public async Task Invoke(HttpContext context)
    {
        //request handler
        //get ip @request
        var userIp = context.Connection.RemoteIpAddress!.ToString();
        //authorize ip against static people, can we map this to a principal and use authorize???
        Identity? user = _sessionService.getUser(userIp);
        //at this point the user may be null
        context.Items["userIdentity"] = JsonSerializer.Serialize<Identity>(user);
        //now we can authorize or pass identity into the request and auth or no auth in controller
        await _next(context);
    }
}
public static class AuthHandlerExtensions
{
    public static IApplicationBuilder UseAuthHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthHandler>();
    }
}