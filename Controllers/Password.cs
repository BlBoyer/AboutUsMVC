using Microsoft.AspNetCore.Mvc;
using AboutUs.Services;

namespace AboutUs.Controllers;

public class PasswordController : Controller
{
    private readonly IConfiguration _config;
    private readonly SessionService _sessionService;
    public PasswordController(IConfiguration config, SessionService sessionService)
    {
        _config = config;
        _sessionService = sessionService;
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
        _sessionService.activateAdmin();
        return RedirectToAction("Index", "Profile");
    }
    return Unauthorized();
}

}