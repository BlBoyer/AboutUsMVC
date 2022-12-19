using Microsoft.AspNetCore.Mvc;
using AboutUs.Services;

namespace AboutUs.Controllers;

public class PasswordController : Controller
{
    private readonly IConfiguration _config;
    private readonly SessionService _sessionService;
    private IdentityProvider _identityProvider;
    public PasswordController(IConfiguration config, SessionService sessionService, IdentityProvider identityProvider)
    {
        _config = config;
        _sessionService = sessionService;
        _identityProvider = identityProvider;
    }

public IActionResult Index()
{
    return View();
}
//set auth for this user/me
[HttpPost]
public IActionResult Index(string _password)
{
    if(_password == _config["myAdminPassword"])
    {
        _sessionService.activateAdmin(_identityProvider.GetUser());
        return RedirectToAction("Index", "Profile");
    }
    return Unauthorized();
}

}