using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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


        public AppDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted(); // удалять при запуске
            Database.EnsureCreated(); // создавать при запуске
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Category>().HasData(
                new Category[]
                {
                    new Category { Id=1, Name="Category 1", Description="Описание подробнее", Color="00ffbbff", UserId=1},
                    new Category { Id=2, Name="Category 2", Description="Описание подробнее", Color="00ffbbff", UserId=1},
                    new Category { Id=3, Name="Category 3", Description="Описание подробнее", Color="00ffbbff", UserId=1}
                });

            model.Entity<Project>().HasData(
                new Project[]
                {
                    new Project { Id=1, Name="Project 1", Description="Описание подробнее", Color="00ffbbff", UserId=1},
                    new Project { Id=2, Name="Project 2", Description="Описание подробнее", Color="00ffbbff", UserId=1},
                    new Project { Id=3, Name="Project 3", Description="Описание подробнее", Color="00ffbbff", UserId=1}
                });

        }



    }

}



