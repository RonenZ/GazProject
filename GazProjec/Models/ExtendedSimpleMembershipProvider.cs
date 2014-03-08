using Gaz.DAL;
using Gaz.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace GazProjec.Models
{
    public class ExtendedSimpleMembershipProvider : SimpleMembershipProvider
    {
        public override bool ValidateUser(string login, string password)
        {
            var dbcxt = new GazSimpleUsersDbContext();
            var urepo = new UserRepository(dbcxt);
            var user = urepo.LoginUser(login, password);
            if (user != null)
            {
                //if (user.RoleID == 1)
                //{
                //    if (!Roles.RoleExists("EndUser"))
                //        Roles.CreateRole("EndUser");

                //    Roles.AddUsersToRole(new[] { login }, "EndUser");
                //}
                //else if (user.RoleID == 2)
                //{
                //    if (!Roles.RoleExists("Administrator"))
                //        Roles.CreateRole("Administrator");

                //    Roles.AddUsersToRole(new[] { login }, "Administrator");
                //}
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}