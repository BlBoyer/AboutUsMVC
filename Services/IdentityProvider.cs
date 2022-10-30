using System.Text.Json;
using AboutUs.Models;
public class IdentityProvider
{
    private readonly string _ip;
    //map json content to identity
    //user may be null, erro-check in controllers
    //private readonly Identity? _user;
    IHttpContextAccessor _accessor; 
    public IdentityProvider(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        _ip = accessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
        //_user = JsonSerializer.Deserialize<Identity>(accessor.HttpContext.Items["userIdentity"].ToString());
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