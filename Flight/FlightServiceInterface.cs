using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineVisualization.Flight
{
    interface FlightServiceInterface
    {
       List<Flight> getFlightFromAirport(string Airportname, int noOfFlights, string filter, int offset);
       List<Flight> getFlightInformation(string Identity);
    }
}
