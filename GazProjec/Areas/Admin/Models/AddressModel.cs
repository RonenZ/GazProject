using System.Linq;
using Gaz.DAL;
using Gaz.Models.Models;

namespace GazProjec.Areas.Admin.Models
{
    public class AddressModel
    {
        public int AddressID { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }


        public AddressModel()
        {
        }

        public AddressModel(Address address)
        {
            AddressID = address.ID;
            CityName = address.CityName;
            StreetName = address.StreetName;
            HouseNumber = address.HouseNumber;
            ApartmentNumber = address.ApartmentNumber;
            Latitude = address.latitude;
            Longitude = address.longitude;
        }

        public void ToDbAddress(Address result)
        {
            result.CityName = this.CityName;
            result.StreetName = this.StreetName;
            result.HouseNumber = this.HouseNumber;
            result.ApartmentNumber = this.ApartmentNumber;
            result.latitude = this.Latitude;
            result.longitude = this.Longitude;
        }
    }
}