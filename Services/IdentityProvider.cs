using System.Text.Json;
using AboutUs.Models;
public class IdentityProvider
{
    private readonly string _ip;
    private readonly IHttpContextAccessor _accessor; 
    public IdentityProvider(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        _ip = accessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
    }
    public string GetIp()
    {
        return this._ip;
    }
    public Identity GetUser()
    {
        return JsonSerializer.Deserialize<Identity>(_accessor.HttpContext.Items["userIdentity"].ToString());
    }
}