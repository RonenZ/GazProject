using System;
using System.Collections.Generic;

namespace Gaz.Models.Models
{
    public partial class UserRole : IBaseModel
    {
        public UserRole()
        {
            this.Users = new List<User>();
        }

        public int ID { get; set; }
        public string RoleDescription { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
