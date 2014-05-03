using System.Linq;
using Gaz.DAL.DbContexts;
using Gaz.Models.Models;

namespace GazProjec.Areas.Customers.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int RoleID { get; set; }

        public UserModel()
        {
        }

        public UserModel(User user)
        {
            UserID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            Password = user.Password;
        }
    }
}