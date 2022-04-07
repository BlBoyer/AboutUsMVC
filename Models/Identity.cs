//our real service will be middleware to be used with app.Use

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AboutUs.Models;
public class Identity
{
    public int Id {get; set;}
    public string? UserName {get; set;}
    //gets determined by controller login type, or more basically the login place, we can hardcode the manager username
    //and put it in the service so that when we login as a manager it adds that username to the admin list
    //gets returned in the req body
    public string? ip {get; set;}
    public string? code {get; set;}
}