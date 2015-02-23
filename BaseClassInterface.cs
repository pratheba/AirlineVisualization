using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoblinXNA;
using GoblinXNA.Graphics;
using GoblinXNA.SceneGraph;
using GoblinXNA.Shaders;
using GoblinXNA.UI;
using GoblinXNA.Device;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.MapPoint.GraphicsAPI;

namespace AirlineVisualization
{
    interface BaseClassInterface
    {
        public Airport.Airport getAirportInformation(string Ident);
        public List<Flight.Flight> getInformationForFlightVisualization();
        public List<Vector3> getListofVectorsForRoute(List<Flight.Flight> flights);
    }
}
