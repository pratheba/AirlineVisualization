using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineVisualization.Airport
{
    interface AirportServiceInterface
    {
        Airport getAirport(string Name);
        List<Airport> getAllAirport();
       // List<Flight.Flight> getFlightFromAirport(ref string Airportname,ref int noOfFlights,ref string filter,ref int offset);
    }
}
