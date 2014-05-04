using Gaz.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Gaz.DAL.Repositories
{
    public class UserNotificationRepository : GenericRepository<UserNotification>
    {
        public UserNotificationRepository(DbContext context) : base(context) { }


        public void AddNotificationForMultipleUsers(int[] userIds, string message)
        {
            var notifs = userIds.Distinct().Select(s => new UserNotification()
            {
                    UserID = s,
                    CreateTime = DateTime.Now,
                    Disabled = false,
                    NotificationDescription = message
            });

            DbSet.AddRange(notifs);
        }
    }
}
