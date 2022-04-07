using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AboutUs.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string? UserName {get; set;}
        [Url]
        public string? ProfileImg {get; set;}
        [Url]
        public string? BgImg {get; set;}
        [StringLength(100, ErrorMessage = "40 character limit.")]
        public string? Title {get; set;}
        [StringLength(200, ErrorMessage = "Limited to 200 characters. Choose wisely.")]
        public string? About {get; set;}
        [StringLength(50, ErrorMessage = "50 character limit.")]  
        public string? Likes {get; set;}
        [StringLength(100, ErrorMessage = "100 character limit.")]
        public string? Qualifications {get; set;}
        [StringLength(100, ErrorMessage = "40 character limit.")]
        public string? Place {get; set;}
    }
}