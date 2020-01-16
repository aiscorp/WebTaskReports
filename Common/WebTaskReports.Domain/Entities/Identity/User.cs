using Microsoft.AspNetCore.Identity;
using System;

namespace WebTaskReports.Domain.Entities.Identity
{
    public class User : IdentityUser
    {

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
