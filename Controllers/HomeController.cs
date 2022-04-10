#nullable enable
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AboutUs.Services;
using AboutUs.Data;
using AboutUs.Models;

namespace AboutUs.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
     private readonly AddressContext _contextAddress;
    private readonly ProfileContext _contextProfile;

    public HomeController(ILogger<HomeController> logger, ProfileContext contextProfile, AddressContext contextAddress)
    {
        _logger = logger;
        _contextProfile = contextProfile;
        _contextAddress = contextAddress;
        
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    //put _username and _password in view like the link, bind to the form action
    public IActionResult Login(string _username, string _password)
    {
        //get username and password form form data
        //if we want to make this async, we get the full list, which we don't really want to do
        //we need to be 100% sure that no usernames are duplicate when saving profile, and no address matches in the db
       Profile profile;
        try {
            profile = _contextProfile.Profile
            .Single(p => p.UserName == _username);
        } catch (InvalidOperationException)
        {
            //this returns regular status code pages **
            //return StatusCode(404,"User not found.");
            return new ContentResult() { Content = "", StatusCode = 700 };
        }
        string _address = new SecureAddress().mesh(_username, _password);
        if (_address == null)
        {
            return StatusCode(500);
        }
        AddressModel passKey;
        try {
            passKey = _contextAddress.Addresses
            .Single(a => a.Address == _address);
        } catch (InvalidOperationException)
        {
            //return Json(new {InternalError="Can't Log In"})
            return new ContentResult() { Content = "", StatusCode = 701 };
        }
        //return RedirectToAction("Details", "Profile", _username); //used the same naming convention...created errors
        if (passKey.Address == _address)
        {    
            SessionService.selectUser(profile.UserName, "ord");
            SessionService.activateUser();
            return Redirect($"/User/Index/{profile.UserName}");
        }
        return Json(new {InternalError="Can't Log In"}); //Tell us if there's a problem logging in
    }
    [ActionName("Delete")]
    [AcceptVerbs("Get", "Post")]
    //we have password verification here, so tech doesn't matter to verify, but could still lead to attacks
    //also we want to be able to delete accounts as manager, so it would be admin verification anyway
    public async Task<IActionResult> Delete(int id, string _password)
    {
        if (_password == null)
        {
            return Unauthorized();
        }
       Profile? profile;
       Content content;
        try {
            profile = await _contextProfile.Profile.FindAsync(id);
            if (profile == null){
                return new ContentResult() { Content = "", StatusCode = 700 };
            } else {
                content = _contextProfile.Content.Single(u => u.UserName == profile.UserName);
            }
        } catch (Exception)
        {
            return new ContentResult() { Content = "", StatusCode = 500 };
        }
        string _address = new SecureAddress().mesh(profile.UserName, _password);
        if (_address == null)
        {
            return StatusCode(500);
        }
        AddressModel passKey;
        try {
            passKey = _contextAddress.Addresses
            .Single(a => a.Address == _address);
        } catch (InvalidOperationException)
        {
            return new ContentResult() { Content = "", StatusCode = 701 };
        }
        if (passKey.Address == _address)
        {
            _contextAddress.Remove(passKey);
            await _contextAddress.SaveChangesAsync();
            _contextProfile.Remove(profile);
            await _contextProfile.SaveChangesAsync();
            _contextProfile.Remove(content);
            await _contextProfile.SaveChangesAsync();
        return RedirectToAction("Index");
        }
        return Json(new {InternalError="Couldn't Perform Delete"}); //Tell us if there's a problem deleting info
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult ErrorPage(int code, string msg)
    {
        msg = new SecureAddress().decodeUrl(msg);
        return View(new {code, msg});
    }
    public IActionResult Logout()
    {
        SessionService.Logout();
        return RedirectToAction("Index");
    }
    public IActionResult Lookup()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Lookup(string _username)
    {
        return RedirectToAction("Search", "User", new{username=_username});
    }
}
