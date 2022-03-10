using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EEMG_Login.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Events> Events { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated(); //make sure the db is created.
        }
    }
}
