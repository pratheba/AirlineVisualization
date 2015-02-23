using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineVisualization.Airport
{
    interface AirportInterface
    {
        string Name { get; set; }
        string Location { get; set; }
        string Code { get; set; }
        float Latitude { get; set; }
        float Longitude { get; set; }


    }
}
