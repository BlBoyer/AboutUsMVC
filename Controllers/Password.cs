using Microsoft.AspNetCore.Mvc;
using AboutUs.Services;

namespace AboutUs.Controllers;

public class PasswordController : Controller
{
    private readonly IConfiguration _config;
    public PasswordController(IConfiguration config)
    {
        _config = config;
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
        SessionService.activateAdmin();
        return RedirectToAction("Index", "Profile");
    }
    return Unauthorized();
}

}