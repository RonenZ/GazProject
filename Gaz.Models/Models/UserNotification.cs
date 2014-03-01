using System;
using System.Collections.Generic;

namespace Gaz.Models.Models
{
    public partial class UserNotification : IBaseModel
    {
        public int ID { get; set; }
        public string NotificationDescription { get; set; }
        public int UserID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public bool Disabled { get; set; }
        public virtual User User { get; set; }
    }
}
