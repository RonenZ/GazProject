using System.Collections.Generic;
using System.Linq;
using Gaz.DAL;

namespace GazProjec.Areas.Admin.Models
{
    public class UserCounterModel
    {
        public UserModel UserDetails { get; set; }
        public List<CounterModel> Counters { get; set; }


        public UserCounterModel() { }

        public UserCounterModel(int userID)
        {
            using (var db = new GazDBContext())
            {
                UserDetails = new UserModel(userID, db);

                Counters = new List<CounterModel>();

                var counters = db.Users.Find(userID).User_Counters;

                foreach (var count in counters)
                {
                    Counters.Add(new CounterModel(count));    
                }

            }
        }
    }
}