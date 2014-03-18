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
        private Gaz.Models.Models.User currUser;
        public override bool ValidateUser(string login, string password)
        {
            var dbcxt = new GazSimpleUsersDbContext();
            var urepo = new UserRepository(dbcxt);
            currUser = urepo.LoginUser(login, password);
            if (currUser != null)
            {
                return true;
            }
            else
            {
                return false;
            } 

        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if(currUser == null){
                var dbcxt = new GazSimpleUsersDbContext();
                var urepo = new UserRepository(dbcxt);
                currUser = urepo.GetAll().FirstOrDefault(u => u.Username == username);
            
                if(currUser != null) return null;

                MembershipUser user = new MembershipUser(currUser.FirstName, currUser.FirstName, null, currUser.Email, string.Empty,
                    string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);

                return user;
            }

            return null;
        }
    }
}