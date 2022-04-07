#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AboutUs.Models;

namespace AboutUs.Data;
    public class AddressContext : DbContext
    {
        public AddressContext (DbContextOptions<AddressContext> options)
            : base(options)
        {
        }

        public DbSet<AboutUs.Models.AddressModel> Addresses { get; set; }
    }
