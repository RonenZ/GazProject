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

                var references = db.Users.Single(o => o.ID == userID).User_Counter_Reference;
                
                foreach (var reference in references)
                {
                    Counters.Add(new CounterModel(reference.CounterID));    
                }

            }
        }
    }
}