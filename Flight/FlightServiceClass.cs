using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineVisualization.Flight
{
    public sealed class FlightServiceClass : FlightServiceInterface
    {
        public static readonly FlightServiceClass instance = new FlightServiceClass();
        private FlightXML2 flightXML_;
        private Airport.AirportServiceClass AirportService_;
        
       
        public FlightServiceClass()
        {
            flightXML_ = FlightXMLUtil.getXMLInstance();
            AirportService_ = new Airport.AirportServiceClass();
        }

        public List<Flight> getFlightFromAirport(string Airportname,  int noOfFlights, string filter,  int offset)
        {
           
            EnrouteStruct enrouteStruct = flightXML_.Enroute(Airportname, noOfFlights, filter, offset);
            List<Flight> FlightObjects = new List<Flight>();

            if (enrouteStruct.enroute != null)
            {
                int num = enrouteStruct.enroute.Length;
                
              
                for (int i = 0; i < num; i++)
                {
                    EnrouteFlightStruct enrouteflight = enrouteStruct.enroute[i];
                    List<Flight> flightList = getFlightInformation(enrouteflight.ident);
                    foreach (Flight f in flightList)
                        FlightObjects.Add(f);
                }

                return FlightObjects;
            }
            return FlightObjects;

        }

        public List<Flight> getFlightInformation(string Identity)
        {
            FlightInfoStruct flightInfoStruct = flightXML_.FlightInfo(Identity, 1);
            int num = flightInfoStruct.flights.Length;

            System.Console.WriteLine("Identity=" + Identity);
           // FlightStruct flight;
            List<Flight> flightList = new List<Flight>();

            for (int i = 0; i < num; i++)
            {
                FlightStruct flight = flightInfoStruct.flights[i];
                Flight flightObj_ = new Flight(Identity);
          
                flightObj_.Identity = Identity;
              //  System.Console.WriteLine("Actual Arrival time =" + fromUnixTimetoDate(flight.actualarrivaltime).Date);
               // System.Console.WriteLine("Actual Departure time = " + fromUnixTimetoDate(flight.actualdeparturetime).Date);
                System.Console.WriteLine("Origin= " + flight.destination + " " + flight.destinationName);
                flightObj_.Source = AirportService_.getAirport(flight.destination);
                System.Console.WriteLine("Destination= " + flight.origin + " " + flight.originName);
                flightObj_.Destination = AirportService_.getAirport(flight.origin);
                flightList.Add(flightObj_);
            }

            return flightList;

        }

        private DateTime fromUnixTimetoDate(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
    }
}
