using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web.Services;
 
namespace AirlineVisualization.Airport
{
    public class Airport : AirportInterface
    {
        private  string Name_;
        private  string Location_;
        private  string Code_;
        private float Latitude_;
        private float Longitude_;

        List<Airline.AirlineClass> OperatingAirlines_;
        private FlightXML2 flightAPI;

        public Airport()
        {
            Initialize();
        }

        private void Initialize()
        {
            Name_ = "";
            Location_ = "";
            Code_ = "";
            Latitude_ = 0.0f;
            Longitude_ = 0.0f;
        }

        public string Name
        {
            get { return Name_; }
            set { Name_ = value; }
        }
        public string Location
        {
            get { return Location_; }
            set { Location_ = value; }
        }
        public string Code
        {
            get { return Code_; }
            set { Code_ = value; }
        }

        public float Latitude
        {
            get { return Latitude_; }
            set { Latitude_ = value; }
        }

        public float Longitude
        {
            get { return Longitude_; }
            set { Longitude_ = value; }
        }

        public List<Airline.AirlineClass> OperatingAirlines
        {
            get { return OperatingAirlines_; }
            set { OperatingAirlines_ = value; }
        }

    }
}
