using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AboutUs.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [StringLength(30, MinimumLength=5, ErrorMessage = "Username must be between 5 and 30 characters long.")]
        [Required]
        public string? UserName {get; set;}
        [StringLength(30, MinimumLength=3, ErrorMessage = "Name must be at least 3 characters long.")]
        [Required]
        public string? Name { get; set; }
        [EmailAddress]
        [Required]
        //not sure how we want to val yet
        public string? Email { get; set; }    //verification in app, this way we can make string items list and add as many fields as we desire(primary email secondary, etc)
        [Phone]
        [Required]
        public string? Phone { get; set; } 
        [StringLength(40)]
        public string? Business { get; set; }
    }
}