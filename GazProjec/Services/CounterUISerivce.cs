using Gaz.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GazProjec.Services
{
    public static class CounterUiSerivce
    {
        public static string GetDetails(Counter counter)
        {
            return string.Format("{0} {1} {2}, {3}", counter.Address.CityName, counter.Address.StreetName, counter.Address.HouseNumber,
                counter.Address.ApartmentNumber);
        }
    }
}