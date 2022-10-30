#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AboutUs.Data;
using AboutUs.Models;
using AboutUs.Services;

namespace AboutUs.Controllers
{
    //we authorize this for only loggd-in users, then, make sure we have view result for finding profiles/ disable edits for other users
    //[Authorize]
public class UserController : Controller
{
    private readonly ProfileContext _context;
    private readonly IdentityProvider _idProvider;
    private readonly SessionService _sessionService;

    public UserController(ProfileContext context, IdentityProvider idProvider, SessionService sessionService)
    {
        _context = context;
        _idProvider = idProvider;
        _sessionService = sessionService;
     }
        //make a controller layout for this so we don't have to display index in route and the ird is called username
        public IActionResult Index(string id) //auth
        {
            if (id == null)
            {
                //if user can verify before selecting, then we are logged in, set id, skip select user
                if (_idProvider.GetUser() == null)
                {
                    return NotFound();
                }
                //reset id if logged in and id isn't provided(link clicked)
                id = _idProvider.GetUser().UserName!;
            }
            //id is provided, needs authorization
            if (_idProvider.GetUser() != null || _sessionService.adminStatus())
            {
                Profile profile;
                try{
                    profile = _context.Profile
                .Single(p => p.UserName == id);
                } catch (InvalidOperationException)
                {
                    return new ContentResult() { Content = "", StatusCode = 700 };
                }
                Content content;
                try{
                    content = _context.Content.Single(c => c.UserName == id);
                } catch (InvalidOperationException)
                {
                    return new ContentResult(){Content="", StatusCode=500};
                }
                List<string> likes  = content.Likes.Split(' ').ToList();
                List<string> qualifications  = content.Qualifications.Split(' ').ToList();
                return View(new{profile, content, likes, qualifications});
            } else 
            {
                return Unauthorized();
            }
        }
       
        public async Task<IActionResult> Edit(string id) //auth
        {
            if (id == null)
            {
                return NotFound();
            }
            //_sessionService.selectUser(id, "ord");
            if (_idProvider.GetUser() != null || _sessionService.adminStatus())
            {
                Content content;
                try
                {
                    content = await _context.Content.SingleAsync(c => c.UserName == id);
                } catch (InvalidOperationException)
                {
                    return new ContentResult(){Content="", StatusCode=500};
                }
                return View(content);
            } else 
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,ProfileImg,BgImg,Title,About,Likes,Qualifications,Place")] Content content)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(content);
                await _context.SaveChangesAsync();
            }
            //catching?
            catch (DbUpdateConcurrencyException)
            {
                if (content == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", new{id = content.UserName});
        }
        return BadRequest();
    }
    public IActionResult Search(string username)
    {
     /*if (!_sessionService.verifyUser())
        {
            return "Not logged in!";
        }*/
        //maybe we get profile listing profiles and only allowing selection
        Profile profile;
            try{
                profile = _context.Profile
            .Single(p => p.UserName == username);
            } catch (InvalidOperationException)
            {
                return new ContentResult() { Content = "", StatusCode = 700 };
            }
            Content content;
            try{
                content = _context.Content.Single(c => c.UserName == username);
            } catch (InvalidOperationException)
            {
                return new ContentResult(){Content="", StatusCode=500};
            }
            List<string> likes  = content.Likes.Split(' ').ToList();
            List<string> qualifications  = content.Qualifications.Split(' ').ToList();
            return View(new{profile, content, likes, qualifications});
    }
}
}
