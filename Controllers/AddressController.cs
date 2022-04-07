using Microsoft.AspNetCore.Mvc;
using AboutUs.Services;
using AboutUs.Data;
using AboutUs.Models;
namespace AboutUs.Controllers;
//need to filter this controller to redirect only (shouldn't be accesed except programmatically)
    [ApiController]
    [Route("Address")]
    public class AddressController : ControllerBase
    {
        private readonly AddressContext _context;
        public AddressController(AddressContext context)
        {
            _context = context;
        }
        //auth
        [HttpGet("ViewKeys")]
        public List<AddressModel> ViewKeys()
        {
            var keys = from k in _context.Addresses
            select k;
            return keys.ToList();
        }
        [ActionName("Create")]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> Create(string _username, string _password)
        {
            if (_username!=null && _password!=null){
                string address = new SecureAddress().mesh(_username, _password);
               /* var entries = from a in _context.Addresses
                select a;*/
                //turn our address into db context, we need to get the right id or insert or something
                var entry = new AddressModel(){Address=address};
                //make sure address doesn't exist in the dbcontext
                AddressModel cross;
                try {
                    cross = _context.Addresses.Single(a => a.Address == entry.Address);
                    return RedirectToAction("Index", "Profile");
                } catch (InvalidOperationException)
                {
                    //execute, should be conditional for no elements exception
                    _context.Add(entry);
                    await _context.SaveChangesAsync();
                    //var result = new JsonResult(entry);
                    //return result;
                    return Redirect("Home/Login");
                 }
            } else 
            {
                return BadRequest();
            }
        }
        //auth with (SessionService.isAdmin)
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            _context.Addresses.RemoveRange(_context.Addresses);
            await _context.SaveChangesAsync();
            return new JsonResult(_context.Addresses);
        }
    } 