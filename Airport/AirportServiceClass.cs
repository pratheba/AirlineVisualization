using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web.Services;

namespace AirlineVisualization.Airport
{
    public sealed class AirportServiceClass : AirportServiceInterface
    {
        private static readonly AirportServiceClass instance = new AirportServiceClass();
        private FlightXML2 flightXML_;
        private List<Airport> AirportObjects;
        private List<Flight.Flight> FlightObjects; 
        private Flight.Flight FlightObj_;

  
        public static AirportServiceClass Instance
        {
            get { return instance; }
        }

        public AirportServiceClass()
        {
            flightXML_ = FlightXMLUtil.getXMLInstance();
            AirportObjects = new List<Airport>();
            FlightObjects = new List<Flight.Flight>();
        }

        public List<Airport> getAllAirport()
        {
            string[] AllAirports = flightXML_.AllAirports();
            for (int i = 0; i < AllAirports.Length; i++)
                AirportObjects.Add(getAirport(AllAirports[i]));
           
            return AirportObjects;
        }

        public Airport getAirport(string Code)
        {
             return (getAirportObject(Code));
           
        }

        private Airport getAirportObject(string Code)
        {
            Airport AirportObj_ = new Airport();
          
            System.Console.WriteLine("Airport= " + Code);
            if (!(flightXML_.AirportInfo(Code).GetType() == null))
            {
                AirportInfoStruct airport = flightXML_.AirportInfo(Code);
                AirportObj_.Code = Code;
                AirportObj_.Location = airport.location;
                AirportObj_.Name = airport.name;
            }
            return AirportObj_;
        }

       
   
    }
}
