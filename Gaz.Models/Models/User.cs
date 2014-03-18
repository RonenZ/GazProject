using System;
using System.Collections.Generic;

namespace Gaz.Models.Models
{
    public partial class User : IBaseModel
    {
        public User()
        {
            this.User_Counters = new List<Counter>();
            this.UserComplaints = new List<UserComplaint>();
            this.UserNotifications = new List<UserNotification>();
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int RoleID { get; set; }
        public virtual ICollection<Counter> User_Counters { get; set; }
        public virtual ICollection<UserComplaint> UserComplaints { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
