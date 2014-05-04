using System;
using Gaz.Models.Models;
namespace GazProjec.Areas.Admin.Models
{
    public class NotificationModel
    {
        public int NotificationID { get; set; }
        public string NotificationDescription { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Disabled { get; set; }

        public NotificationModel()
        {
        }

        public NotificationModel(UserNotification note)
        {
            NotificationID = note.ID;
            NotificationDescription = note.NotificationDescription;
            UserID = note.UserID;
            CreateTime = note.CreateTime;
            Disabled = note.Disabled;
        }


    }
}