using AboutUs.Models;
namespace AboutUs.Services;
public class SessionService
{
    private readonly ILogger _logger;
    public SessionService(ILogger<SessionService> logger)
    {
        _logger = logger;
    }
    public Identity? verifyUser(Identity user)
    {
        //we have to force a logout to login on same device
        try 
        {
            return Session.People.Single(i => i == user);
        } 
        catch
        {
            return null;
        }
    }
    public bool adminStatus(Identity user)
    {
        return user.code == "admin";
    }
    public void activateUser(string ip, string username, string uCode)
    {
        //if ip exists, then don't log in. the controller blocks the page if we're logged in, but, just in-case
        if (Session.People.Any(p => p.ip == ip))
        {
            return;
        } 
        else 
        {
            var user = new Identity {UserName = username, ip = ip, code = uCode};
            Session.People.Add(user);
        }
    }
    public Identity? getUser(string ip)
    {
        //test userName should not be null
        try 
        {
            return Session.People.Single(p => p.ip == ip);
        } catch
        {
        return null;
        }
    }
    public void activateAdmin(Identity user)
    {
        //get current user and add the admin code
        Session.People.Single(u=>u.UserName == user.UserName).code = "admin";
        _logger.LogInformation($"User {user.UserName} activated as admin!");
    }
    public void Logout(string ip)
    {
        try
        {
            var user = Session.People.Single(u => u.ip == ip);
            Session.People.Remove(user);
        } catch (Exception e)
        {
            _logger.LogInformation($"Logout: Our exception should be null: {e.Message}");
            return;
        }
    }

}