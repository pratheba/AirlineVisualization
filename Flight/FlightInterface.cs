using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineVisualization.Flight
{
    interface FlightInterface
    {
        string Identity { get; set; }
        Airport.Airport Source { get; set; }
        Airport.Airport Destination { get; set; }
        Airline.AirlineClass airline { get; set; }
       
    }
}
