using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTaskReports.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        //public const string Admin = "Admin";
        //public const string User = "User";
        //public string Description { get; set; }

        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
