using Microsoft.AspNetCore.Mvc;
using AboutUs.Services;

namespace AboutUs.Controllers;

public class PasswordController : Controller
{
public IActionResult Index()
{
    return View();
}
//set auth for this user/me
[HttpPost]
public IActionResult Index(string _password)
{
    if(_password == "benjamin")
    {
        SessionService.activateAdmin();
        return RedirectToAction("Index", "Profile");
    }
    return Unauthorized();
}

}