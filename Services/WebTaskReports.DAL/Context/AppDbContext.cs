using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WebTaskReports.Domain.Entities;
using WebTaskReports.Domain.Entities.Identity;

namespace WebTaskReports.DAL.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        //public  DbSet<User> Users { get; set; } - из-за наследования IdentityDbContext уже не нужно свойство Users?

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Project> Projects { get; set; }


        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            //Генерация паролей для пустой базы даных
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            // Пользователи для тестирования
            var adminUser = new User { Id = "1", UserName = "admin", RoleId = 2, PasswordHash = passwordHasher.HashPassword(new User(), "AdminPassword"), Description = "Nothing", Name = "Petr", Surname = "Petrov", DOB = DateTime.Parse("1994-10-31T20:59Z").ToUniversalTime(), LastAuthorized = DateTime.Parse("2020-01-01T00:00Z").ToUniversalTime(), Email = "email@ya.ru" };

            var noAdminUser = new User { Id = "2", UserName = "ivanivanov", RoleId = 2, PasswordHash = passwordHasher.HashPassword(new User(), "Qwerty12345"), Description = "Nothing", Name = "Ivan", Surname = "Ivanov", DOB = DateTime.Parse("1990-12-31T20:59Z").ToUniversalTime(), LastAuthorized = DateTime.Parse("1900-01-01T00:00Z").ToUniversalTime(), Email = "email@ya.ru" };

            var adminRole = new Role { Name = "Admin", NormalizedName = "ADMIN" };
            var userRole = new Role { Name = "User", NormalizedName = "USER" };

            model.Entity<User>()
                .HasData(new User[] { adminUser, noAdminUser });

            model.Entity<Role>().HasData(
                new Role[]
                {
                    new Role{ Id="1", Name = "Admin", NormalizedName = "ADMIN" },
                    new Role{ Id="2", Name = "User", NormalizedName = "USER" }
                });

            //доп инфа если не заработает
            // https://entityframeworkcore.com/knowledge-base/50742754/is-it-possible-advisable-to-seed-users-roles-using-the-efcore-2-1-data-seeding-system-
            // https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
            // https://metanit.com/sharp/entityframeworkcore/2.14.php

            // роли
            // https://metanit.com/sharp/aspnet5/15.5.php




        }

    }
}
