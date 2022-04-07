#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AboutUs.Data;
using AboutUs.Models;
using AboutUs.Services;

namespace AboutUs.Controllers;
    //we authorize this for only loggd-in users, then, make sure we have view result for finding profiles/ disable edits for other users
    //[Authorize]
public class UserController : Controller
{
        private readonly ProfileContext _context;

        public UserController(ProfileContext context)
        {
            _context = context;
        }
        //make a controller layout for this so we don't have to display index in route and the ird is called username
        public IActionResult Index(string id) //auth
        {
            if (id == null)
            {
                //if user can verify before selecting, then we are logged in, set id, skip select user
                if (!SessionService.verifyUser())
                {
                    return NotFound();
                }
                id = SessionService.getMyUserName();
            } else 
            {
                SessionService.selectUser(id, "ord");
            }
            if (SessionService.verifyUser() || SessionService.adminStatus())
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
            SessionService.selectUser(id, "ord");
            if (SessionService.verifyUser() || SessionService.adminStatus())
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,ProfileImg,BgImg,Title,About,Likes,Qualifications,Place")] Content content)
        {
            SessionService.selectUser(content.UserName, "ord");
            if (SessionService.verifyUser() || SessionService.adminStatus())
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
            return View(content);
        }
        else 
        {
            return Unauthorized();
        }
    }
    public IActionResult Search(string username)
    {
     /*if (!SessionService.verifyUser())
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
