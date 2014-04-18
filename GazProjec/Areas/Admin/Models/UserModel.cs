using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gaz.DAL;
using Gaz.DAL.DbContexts;

namespace GazProjec.Areas.Admin.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public string Username { get; set; }

        public UserModel() {}

        public UserModel(int userID, GazDbContext db)
        {
            var result = db.Users.Single(o => o.ID == userID);

            ID = result.ID;
            FirstName = result.FirstName;
            LastName = result.LastName;
            PhoneNumber = result.PhoneNumber;
            Email = result.Email;
            Password = result.Password;
            RoleID = result.RoleID;
            Username = result.Username;
        }

    }
}