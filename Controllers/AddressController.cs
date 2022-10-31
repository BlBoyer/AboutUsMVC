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
        private readonly ILogger _logger;
        public AddressController(AddressContext context, ILogger<AddressController> logger)
        {
            _context = context;
            _logger = logger;
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
                var entry = new AddressModel(){Address=address};
                //make sure address doesn't exist in the dbcontext
                AddressModel cross;
                try {
                    cross = _context.Addresses.Single(a => a.Address == entry.Address);
                    return Forbid();
                } catch (InvalidOperationException)
                {
                    //execute, should be conditional for no elements exception
                    _context.Add(entry);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("New user credentials created successfully.");
                    return Redirect("/Home/Login");
                 }
            } else 
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            _context.Addresses.RemoveRange(_context.Addresses);
            await _context.SaveChangesAsync();
            //make sure addresses were deleted
            return new JsonResult(_context.Addresses);
        }
    } 