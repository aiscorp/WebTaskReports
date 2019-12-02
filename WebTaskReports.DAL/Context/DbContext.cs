using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WebTaskReports.Domain.Entities;
using WebTaskReports.Domain.Entities.Identity;

namespace WebTaskReports.DAL.Context
{
    public class DbContext : IdentityDbContext<User, Role, string>
    {
        //public DbSet<Brand> Brands { get; set; }

        //public DbSet<Section> Sections { get; set; }

        //public DbSet<Product> Products { get; set; }


        public DbContext(DbContextOptions options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder model)
        //{
        //    base.OnModelCreating(model);
        //}

    }
}
