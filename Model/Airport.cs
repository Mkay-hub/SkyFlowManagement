using System;
using System.Collections.Generic;
using System.Text;

namespace SkyFlowManagement.Model
{
    public class Airport
    {
        public int AirportId { get; private set; }
        public string AirportCode { get; private set; }
        public string AirportName { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public Airport(int airportId, string airportCode, string airportName, string city, string country)
        {
            AirportId = airportId;
            AirportCode = airportCode;
            AirportName = airportName;
            City = city;
            Country = country;
        }
    }
}