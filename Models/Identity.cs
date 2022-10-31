//our real service will be middleware to be used with app.Use

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AboutUs.Models;
public class Identity
{
    public int Id {get; set;}
    public string? UserName {get; set;}
    public string? ip {get; set;}
    public string? code {get; set;}
}