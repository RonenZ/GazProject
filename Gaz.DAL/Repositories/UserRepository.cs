using Gaz.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Gaz.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context) { }

        /// <summary>
        /// return user details using sp
        /// </summary>
        public User GetUserDetails(int userID)
        {
            return this.usp_GetUserDetails(userID).SingleOrDefault();
        }

        /// <summary>
        /// returns user details after validating login using sp
        /// </summary>
        public User LoginUser(string userName, string passWord)
        {
            return this.usp_LoginUser(userName, passWord).SingleOrDefault();
        }


        #region Stored Procedures

        protected virtual ObjectResult<User> usp_GetUserDetails(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));

            return ((IObjectContextAdapter)DbContext).ObjectContext.ExecuteFunction<User>("usp_GetUserDetails", userIDParameter);
        }

        protected virtual ObjectResult<User> usp_LoginUser(string userName, string passWord)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));

            var passWordParameter = passWord != null ?
                new ObjectParameter("passWord", passWord) :
                new ObjectParameter("passWord", typeof(string));

            return ((IObjectContextAdapter)DbContext).ObjectContext.ExecuteFunction<User>("usp_LoginUser", userNameParameter, passWordParameter);
        }

        #endregion
        
    }
}
