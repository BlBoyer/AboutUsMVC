namespace AboutUs.Services;
public class ErrorHandler
{
    private readonly RequestDelegate _next;
    public ErrorHandler(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        await _next(context);
		//Pass all successful http response codes:
        HttpResponse response = context.Response;
        if (response.StatusCode >= 400)
        {
            Dictionary<int, string> StatusCodes = new Dictionary<int, string>
            {
                {400, "Bad Request, Please go back and check your details."},
                {401, "Unauthorized Access. You don't have permissions for this content."},
                {403, "Forbidden by server."},
                {404, "Page Not Found. Try again."},
                {405, "Not Allowed Here."},
                {408, "Timed-Out!"},
                {410, "This resource is no longer available!"},
                {413, "Oversized Request"},
                {415, "Media Type Not Acceptable"},
                {426, "Outdated Protocol, Upgrade your browser."},
                {429, "Too Many Requests!"},
                {500, "Internal Error"},
                {501, "Not Supported"},
                {503, "Unavailable"},
                {504, "Gateway Timed-Out!"},
                {507, "Oops..No more room!"},
                {700, "Username Not Found. If you aren't a user, please create and account."},
                {701, "Oops...! Your credentials are invalid."},
                {702, "Oops...! This username already exists. Are you trying to sign-in?"},
                {703, "You're already logged in!"}
            };
            string msgStr = "Internal Access Error.";
            var value="";
            if(StatusCodes.TryGetValue(response.StatusCode, out value))
            {
                msgStr = value;
            }
            string msg = new SecureAddress().singleCode(msgStr);
            context.Response.Redirect($"/Home/ErrorPage?code={response.StatusCode}&msg={msg}", false);
            return;
        }
        //Console.WriteLine($"Using address: {context.Connection.RemoteIpAddress}");
    }
}
public static class ErrorHandlerExtensions
{
    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandler>();
    }
}