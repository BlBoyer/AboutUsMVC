using Microsoft.AspNetCore.Mvc;
using AboutUs.Data;
using AboutUs.Models;
using Newtonsoft.Json;

namespace AboutUs.Controllers;
    [ApiController]
    [Route("UserApi")]
    public class UserBase : ControllerBase{
        private readonly ProfileContext _context;
        public UserBase(ProfileContext context)
        {
            _context = context;
        }
        [HttpGet("Profiles")]
        public List<Profile> GetAllProfiles()
        {
            return _context.Profile.Select(p=>p).ToList();
        }
        [HttpGet("Content")]
        public List<Content> GetAllContent()
        {
            return _context.Content.Select(c=>c).ToList();
        }
        [HttpDelete("Profiles/{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _context.Profile.FindAsync(id);
            if (profile == null)
            {
                return new JsonResult(new {Profile="null"});
            }
            _context.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profiles");
        }
        [HttpDelete("Content/{id}")]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var content = await _context.Content.FindAsync(id);
            if (content == null)
            {
                return new JsonResult(new {Profile="null"});
            }
            _context.Remove(content);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profiles");
        }
    }