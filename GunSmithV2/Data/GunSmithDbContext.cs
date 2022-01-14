using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GunSmithV2.Models;
namespace GunSmithV2.Data
{


    public class GunSmithDbContext : DbContext
    {
        public DbSet<GunItem> GunItems { get; set; }

        public GunSmithDbContext(DbContextOptions<GunSmithDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

 
    }
}
