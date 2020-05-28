using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class UserManagerHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

        public static void CreateRole(string roleName)
        {
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole
                {
                    Name = roleName
                });

                db.SaveChanges();
            }
        }

        public static void DeleteRole(string roleName)
        {
            if (roleManager.RoleExists(roleName))
            {
                roleManager.Delete(new IdentityRole { Name = roleName });
            }
        }

        public static bool UserIsInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        public static bool RemoveUserFromRole(string userId, string roleName)
        {
            if (UserIsInRole(userId, roleName))
            {
                userManager.RemoveFromRole(userId, roleName);
                return true;
            }

            return false;
        }

        public static void AddUserToRole(string roleName, string userName)
        {
            IdentityRole role = roleManager.FindByName(roleName);
            ApplicationUser user = userManager.FindByName(userName);
            if (role != null && user != null && !userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, roleName);
            }
            db.SaveChanges();
        }

    }

}