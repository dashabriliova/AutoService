using AutoService.DAL.Data;
using AutoService.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoService.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context,
                                        UserManager<ApplicationUser> userManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if(!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                var result = await roleManager.CreateAsync(roleAdmin);
            }

            if(!context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "123456");

                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "123456");

                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");
            }
            if(!context.Autos.Any())
            {
                context.Autos.AddRange(
                    new List<Auto>
                    {
                        new Auto
                        {
                            AutoName="Honda civic",
                            AutoNumber="9048-AA 6",
                            AutoYear=new DateTime(2020,01,01),
                            AutoVIN="asdfghjk",
                        },
                        new Auto
                        {
                            AutoName="Citroen Xsara",
                            AutoNumber="9084-AA 7",
                            AutoYear=new DateTime(2010,01,01),
                            AutoVIN="dfghjkl",
                        }
                    });
                await context.SaveChangesAsync();
            }
            await context.SaveChangesAsync();
        }
    }
}
