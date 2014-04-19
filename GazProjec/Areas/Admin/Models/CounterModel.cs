using System.Linq;
using Gaz.DAL;
using Gaz.Models.Models;

namespace GazProjec.Areas.Admin.Models
{
    public class CounterModel
    {
        public int CounterID { get; set; }
        public int AddressID { get; set; }
        public AddressModel AddressData { get; set; }

        public CounterModel()
        {
            AddressData = new AddressModel();
        }

        public CounterModel(Counter counter)
        {
            CounterID = counter.ID;
            AddressID = counter.AddressID;
            AddressData = new AddressModel(counter.Address);
        }
    }
}
