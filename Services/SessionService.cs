using AboutUs.Models;
using System.Text.Json;
namespace AboutUs.Services;
 public static class SessionService
{
    //create admin code
    //private static string adminCode="admin";
    //create user code
    //private static string userCode="ord";
    private static bool isAdmin = false;
    private static string? username{get; set;}
    public static string? remoteIp{get; set;}
    private static string? uCode{get; set;}
   
    public static List<Identity> People = new List<Identity>();
    //change the username and code to verify, verify takes in ip and returns verification value
    //this allows the user instance to change, activate can be called externally
    //selectUser, then activateUser()
    public static void selectUser(string _userName, string _code)
    {
        username = _userName;
        uCode = _code;
    }
    //check if the user is in instance
    public static bool verifyUser()
    {
        foreach (var i in People)
        {
            if (i.UserName == username && i.ip == remoteIp && i.code == uCode){
                return true;
            }

        }
        return false;
        //var identity = new Identity {UserName = username, ip = remoteIp, code = uCode};
        //return (identities.Contains(identity)); //doesn't work
    }
    public static bool adminStatus()
    {
        return isAdmin;
    }
    public static void activateUser()
    {
        var user = new Identity {UserName = username, ip = remoteIp, code = uCode};
        People.Add(user);
    }
    public static string getMyUserName()
    {
        return username;
    }
    public static void activateAdmin()
    {
        isAdmin = true;
    }
    public static void Logout()
    {
        Identity? user = null;
         foreach (var i in People)
        {
            if (i.UserName == username && i.ip == remoteIp && i.code == uCode)
            {
                user = i;
            }
        }
        if (user != null)
        {
            People.Remove(user);
        } else
        {
            return;
        }
    }

}