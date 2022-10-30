#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AboutUs.Data;
using AboutUs.Models;
using AboutUs.Services;

namespace AboutUs.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileContext _context;
        private readonly IdentityProvider _idProvider;
        private readonly SessionService _sessionService;

        public ProfileController(ProfileContext context, IdentityProvider idProvider, SessionService sessionService)
        {
            _context = context;
            _idProvider = idProvider;
            _sessionService = sessionService;
        }
        //this will be an onlyOwner Index, so we can do anything to profiles...hahaha
        //needs authorization, which we will have to execute in the password page
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            if (_sessionService.adminStatus())
            {
            return View(await _context.Profile.ToListAsync());
            } else 
            {
                return Unauthorized();
            }
        }
        // GET: Profile/Create
        public IActionResult Create()
        {
            return View();
        }
        //don't create if username exists, pass error back to view as arg
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Name,Email,Phone,Business")] Profile profile, string password)
        {
            if (ModelState.IsValid && password != null)
            {
                Profile user;
                try {
                    user = _context.Profile.Single(p => p.UserName == profile.UserName);
                    } catch (InvalidOperationException) {
                    var content = new Content()
                {
                    UserName=profile.UserName,
                    ProfileImg="~/img/baseImg.png",
                    BgImg="~/img/baseImg.png",
                    Title="Custom Title",
                    About="I like pancakes!",
                    Likes="What do you like to do?",
                    Qualifications="Type in some skills/certificates.",
                    Place="Olympia, WA"
                };
                        _context.Add(profile);
                        _context.Add(content);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Create", "Address", new {_username = profile.UserName, _password = password});
                    }
                return new ContentResult() {Content="", StatusCode=702};
            }
            return BadRequest();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var profile = await _context.Profile.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            //should be if we have an identity in our response, then getUsername and check that they are the same
            if (_idProvider.GetUser().UserName == profile.UserName || _sessionService.adminStatus())
            {
            return View(profile);
            } else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Name,Email,Phone,Business")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (profile == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "User", new {id=profile.UserName});
            }
            return View(profile);
        }
        //only manager can delete by request
        public async Task<IActionResult> Delete(int? id)
        {
            if (!_sessionService.adminStatus())
            {
                return Unauthorized();
            }
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Profile profile, string password)
        {
            Console.WriteLine($"{id}");
            return RedirectToAction("Delete", "Home", new {id, _password = password});
        }
    }
}
