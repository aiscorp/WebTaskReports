using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTaskReports.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public const string Administrator = "Administrator";
        public const string User = "User";

        public string Description { get; set; }
    }
}
