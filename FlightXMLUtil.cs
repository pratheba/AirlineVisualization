using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web.Services;

namespace AirlineVisualization
{
     public class FlightXMLUtil 
    {
        private static FlightXML2 FlightXMLInstance ;
        
        public static FlightXML2 getXMLInstance()
        {
            if (FlightXMLInstance == null)
            {
                FlightXMLInstance = new FlightXML2();
                FlightXMLInstance.Credentials = new NetworkCredential("dedual", "7a4f1d723b7ce020ee6ba1bbf1517b20342cdce4");
                FlightXMLInstance.PreAuthenticate = true;
            }
            return FlightXMLInstance;
        }
     }
}
