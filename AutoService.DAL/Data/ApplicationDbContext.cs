using AutoService.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoService.DAL.Data
{
   public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base (options)
        {
            
        }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Serv> Servs { get; set; }
    }
}
