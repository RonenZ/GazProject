using System.Linq;
using Gaz.DAL;

namespace GazProjec.Areas.Admin.Models
{
    public class CounterModel
    {
        public int CounterID { get; set; }
        public int AddressID { get; set; }
        public AddressModel AddressData { get; set; }

        public CounterModel(int counterID)
        {
            using (var db = new GazDBContext())
            {
                var result = db.Counters.Single(o => o.ID == counterID);

                CounterID = result.ID;
                AddressID = result.AddressID;
                AddressData = new AddressModel(AddressID);
            }
        }
    }
}
