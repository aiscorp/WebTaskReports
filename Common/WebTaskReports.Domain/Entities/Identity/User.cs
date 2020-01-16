using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTaskReports.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        //public const string Administrator = "Administrator";
        //public const string AdminPasswordDefault = "AdminPassword";

        // возможно это можно убрать
        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public string Description { get; set; }

        [PersonalData]
        public string Name { get; set; }

        [PersonalData]
        public string Surname { get; set; }

        [PersonalData]
        public DateTime DOB { get; set; }
       
        public DateTime LastAuthorized { get; set; }

        // https://docs.microsoft.com/ru-ru/aspnet/core/security/authentication/add-user-data?view=aspnetcore-3.0&tabs=visual-studio#test-create-view-download-delete-custom-user-data
        // Следующие поля наследуются
        // string UserName, PasswordHash, PhoneNumber, PhoneNumber

    }
}
