using Gaz.DAL;
using Gaz.DAL.DbContexts;
using Gaz.DAL.Repositories;
using Gaz.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace GazProjec.Models
{
    public class ExtendedRoleProvider : SimpleRoleProvider
    {
        private static Dictionary<int, string> rolemap = new Dictionary<int, string>()
        {
            { 0 , "None" }, { 1 , "EndUser" }, { 2 , "Administrator" }
        };

        public override string[] GetRolesForUser(string username)
        {
            int roleId = 0;

            using (var dbcxt = new GazSimpleUsersDbContext())
            {
                var urepo = new UserRepository(dbcxt);
                roleId = urepo.GetUserRoleByUsername(username);
            }

            if (roleId == 0 || !rolemap.ContainsKey(roleId)) return new string[] { };

            return new string[] { rolemap[roleId] };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            int id = int.Parse(roleName);
            var dbcxt = new GazSimpleUsersDbContext();
            var urrepo = new UserRolesRepository(dbcxt);
            var role = urrepo.GetByID(id, r => r.Users);

            return role.Users.Select(s => s.Username).ToArray();
        }
        
        

        public override bool IsUserInRole(string username,string roleName)
        {
            if (HttpContext.Current.Session["UserRole"] == null)
                return false;

            if(true)
            {
                string userrolename = string.Empty;
                int UserRoleId = (int)HttpContext.Current.Session["UserRole"];
                switch (UserRoleId)
                {
                    case 1: {userrolename = "EndUser"; break;}
                    case 2: {userrolename = "Administrator"; break;}
                    default:
                        break;
                }

                if (userrolename.Equals(roleName))
                    return true;
            }

            return false;
        }
    }
}