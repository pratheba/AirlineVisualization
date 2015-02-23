using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineVisualization.Flight
{
    public class Flight
    {
        private string Identity_;
        private Airport.Airport Source_;
        private Airport.Airport Destination_;
        private Airline.AirlineClass Airline_;
        //List<Ai
        public Flight(string Identity)
        {
        }
        public string Identity
        {
            get { return Identity_; }
            set { Identity_ = value; }
        }

        public Airport.Airport Source 
        {
            get { return Source_; }
            set { Source_ = value; }
        }

        public Airport.Airport Destination
        {
            get { return Destination_; }
            set { Destination_ = value; }
        }

        public Airline.AirlineClass Airline
        {
            get { return Airline_; }
            set { Airline_ = value; }
        }



    }
}
