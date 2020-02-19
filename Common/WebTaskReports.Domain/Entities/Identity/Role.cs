using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebTaskReports.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public Role()
        {
        }

        public Role(string roleName)
        {
            Name = roleName;
        }

        //public const string Admin = "Admin";
        //public const string User = "User";
        //public string Description { get; set; }

        //public List<User> Users { get; set; }
        //public Role()
        //{
        //    Users = new List<User>();
        //}

        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            string adminEmail = "admin@ya.ru";
            string userEmail = "user@ya.ru";
            string password = "Password";


            if (await roleManager.FindByNameAsync(adminEmail) == null)
                await roleManager.CreateAsync(new Role("admin"));

            if (await roleManager.FindByNameAsync(userEmail) == null)
                await roleManager.CreateAsync(new Role("user"));

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { UserName = adminEmail, Name = "Petr", Surname = "Petrov", DOB = DateTime.Parse("1994-10-31T20:59Z").ToUniversalTime(), LastAuthorized = DateTime.Parse("2020-01-01T00:00Z").ToUniversalTime(), Email = adminEmail };
                
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                User user = new User { UserName = userEmail, Name = "Ivan", Surname = "Ivanov", DOB = DateTime.Parse("1990-10-31T20:59Z").ToUniversalTime(), LastAuthorized = DateTime.Parse("2019-10-01T00:00Z").ToUniversalTime(), Email = userEmail };


                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }

        }
    }
}
