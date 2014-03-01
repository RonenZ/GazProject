using System;
using System.Collections.Generic;

namespace Gaz.Models.Models
{
    public partial class Address : IBaseModel
    {
        public Address()
        {
            this.Counters = new List<Counter>();
        }

        public int ID { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public virtual ICollection<Counter> Counters { get; set; }
    }
}
