using AboutUs.Models;
namespace AboutUs.Services;
public class SessionService
{
    //check if the user is in instance
    private bool _isAdmin;
    public SessionService()
    {
        _isAdmin = false;
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
        //return Session.People.Single(i => i == user);
/*         foreach (var i in Session.People)
        //return (identities.Contains(identity)); //doesn't work
*/
    }
    public bool adminStatus()
    {
        return _isAdmin;
    }
    public void activateUser(string ip, string username, string uCode)
    {
        var user = new Identity {UserName = username, ip = ip, code = uCode};
        Session.People.Add(user);
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
    public void activateAdmin()
    {
        //change code to admin instead
        _isAdmin = true;
    }
    public void Logout(string ip)
    {
        try
        {
            //this could be remove all with ip, but we only want one user logged in so NO
            var user = Session.People.Single(u => u.ip == ip);
            Session.People.Remove(user);
        } catch 
        {
            return;
        }
    }

}