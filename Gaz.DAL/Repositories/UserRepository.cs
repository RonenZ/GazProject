using Gaz.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gaz.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context) { }

        /// <summary>
        /// return user details using sp
        /// </summary>
        public int GetUserIdByUsername(string username)
        {
            //if (include != null)
            //{
            //    return this.DbSet.Include(include).FirstOrDefault(f => f.Username.Equals(username));
            //}
            var user = this.DbSet.FirstOrDefault(f => f.Username.Equals(username));
            return user == null ? 0 : user.ID;
        }

        /// <summary>
        /// return user details using sp
        /// </summary>
        public int GetUserRoleByUsername(string username)
        {
            var user = this.DbSet.FirstOrDefault(f => f.Username.Equals(username));
            return user == null ? 0 : user.RoleID;
        }

        public IEnumerable<Counter> GetCountersByUserName(string userName)
        {
            var user = DbSet.SingleOrDefault(s => s.Username == userName);

            return user == null ? new List<Counter>() : user.User_Counters;
        }

        /// <summary>
        /// return user details using sp
        /// </summary>
        public User GetUserDetails(int userID)
        {
            return this.usp_GetUserDetails(userID);
        }

        /// <summary>
        /// returns user details after validating login using sp
        /// </summary>
        public User LoginUser(string userName, string passWord)
        {
            return this.usp_LoginUser(userName, passWord);
        }


        #region Stored Procedures

        protected virtual User usp_GetUserDetails(int? userId)
        {
            var userIdp = new SqlParameter("@userID", userId);

            return DbContext.Database
                            .SqlQuery<User>("usp_GetUserDetails @userID", userIdp)
                            .SingleOrDefault();
        }

        protected virtual User usp_LoginUser(string userName, string passWord)
        {
            var userNameP = new SqlParameter("@userName", userName);
            var passWordP = new SqlParameter("@passWord", passWord);

            return DbContext.Database
                            .SqlQuery<User>("usp_LoginUser @userName, @passWord", userNameP, passWordP)
                            .SingleOrDefault();
        }

        #endregion
        
    }
}
