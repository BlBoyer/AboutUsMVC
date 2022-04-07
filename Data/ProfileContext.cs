#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AboutUs.Models;

namespace AboutUs.Data;
    public class ProfileContext : DbContext
    {
        public ProfileContext (DbContextOptions<ProfileContext> options)
            : base(options)
        {
        }

        public DbSet<AboutUs.Models.Profile> Profile { get; set; }
        public DbSet<AboutUs.Models.Content> Content {get; set;}
    }
