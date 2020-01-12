using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTaskReports.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "Administrator";
        public const string AdminPasswordDefault = "AdminPassword";

        public string Description { get; set; }

        [PersonalData]
        public string Name { get; set; }

        [PersonalData]
        public string Surname { get; set; }

        [PersonalData]
        public DateTime DOB { get; set; }
       
        public DateTime LastAuthorized { get; set; }

        // Следующие поля наследуются
        // string UserName, PasswordHash, PhoneNumber, PhoneNumber

    }
}
