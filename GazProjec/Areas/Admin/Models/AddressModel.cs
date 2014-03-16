using System.Linq;
using Gaz.DAL;

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


        public AddressModel(int addressID)
        {
            using (var db = new GazDBContext())
            {
                var result = db.Addresses.Single(o => o.ID == addressID);

                AddressID = result.ID;
                CityName = result.CityName;
                StreetName = result.StreetName;
                HouseNumber = result.HouseNumber;
                ApartmentNumber = result.ApartmentNumber;
                Latitude = result.latitude;
                Longitude = result.longitude;
            }
        }

    }
}