using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.MapPoint.Rendering3D;
using Microsoft.MapPoint.Rendering3D.Utility;
using Microsoft.MapPoint.Rendering3D.Steps.Actors;
using Microsoft.MapPoint.Binding;
using ActorDataSource;
using Microsoft.MapPoint.Rendering3D.GraphicsProxy;
using Microsoft.MapPoint.Data;
using Microsoft.MapPoint.Geometry.Geometry2;
using Microsoft.MapPoint.CoordinateSystems;
using AirlineInfo.Flight;
using AirlineInfo;
using Microsoft.Xna.Framework;

namespace SimpleForm
{
    class ButtonClickClass
    {
        private bool labels = true;
        private bool flightlabels = true;
        private bool doneLoading = false;
        private bool modeChange = true;
        public System.Windows.Forms.Button Labels;


        private BunnyBuilder b_builder;
        private ActorDataSource.ActorDataSource a_dataSource;
        private ActorDataSourcePlugIn a_dataSourcePlugin;
        private SimpleObject.SimpleObjectPlugIn s_objectPlugin;
        private SimpleObject.SimpleObjectActor s_objectActor;

        private BindingsSource bindingsSource;
        private BunnyActor b_actor;
        private IEnumerable<BunnyActor> b_actors;
        private Host globleHost;

        private AirlineInfo.BaseClass a_baseClass;
        private AirlineInfo.Airport.AirportServiceClass a_airportClass;
        private AirlineInfo.Airport.Airport airport;
        private List<AirlineInfo.Flight.Flight> a_flightlist;

        //private TransformNode a_transNode;

        public static readonly ButtonClickClass buttonClass = new ButtonClickClass();
        private Texture texture;

        public ButtonClickClass()
        {
        }

        public ButtonClickClass(Host host)
        {
            globleHost = host;
            a_baseClass = new AirlineInfo.BaseClass();
            a_airportClass = new AirlineInfo.Airport.AirportServiceClass();
        }

        private void Initialize()
        {

        }

        public void buttonLabels_Click()
        {
           
            // swap the terrain texture source
            globleHost.DataSources.Remove("Texture", "Texture");
            labels = !labels;

            if (globleHost.Navigation.CameraPosition.Altitude < 500000)
            {
                ActivateFlight();
            }

            if (labels)
            {
                globleHost.DataSources.Add(new DataSourceLayerData("Texture", "Texture", @"http://go.microsoft.com/fwlink/?LinkID=98772", DataSourceUsage.TextureMap));
                this.Labels.Text = "Labels On";
            }
            else
            {
                globleHost.DataSources.Add(new DataSourceLayerData("Texture", "Texture", @"http://go.microsoft.com/fwlink/?LinkID=98771", DataSourceUsage.TextureMap));
                this.Labels.Text = "Labels Off";
            }
        }

        public void flightbuttonLabels_Click()
        {
          
            if (flightlabels)
            {
                this.ActivateFlight();
                this.Labels.Text = "Flight Status On";
            }
            else
            {
                this.DeactivateFlight();
                this.Labels.Text = "Flight Status Off";
            }
            flightlabels = !flightlabels;

        }

        public void changeDisplayMode_Click()
        {
            modeChange = !modeChange;
            if (modeChange)
            {
                //DeactivateFlight();
                //ActivateFlight();

                int index = 0;
                LatLonAlt position = LatLonAlt.CreateUsingDegrees(airport.Latitude, airport.Longitude, 0);

                foreach (BunnyActor b in b_actors)
                {
                    b.changeMode = !b.changeMode;
                    b.isStaticState = !b.isStaticState;
                    b.setLocation = position;
                    //bactor.isStaticState = false;
                    b.setDestination = LatLonAlt.CreateUsingDegrees(
                        ((double)(a_flightlist[index].Destination.Latitude)),
                        ((double)(a_flightlist[index].Destination.Longitude)),
                        a_flightlist[index].Destination.Altitude);
                    b.Flight = a_flightlist[index].Identity;
                    index++;
                }
            }
            else
            {
                foreach (BunnyActor b in b_actors)
                {

                    InFlightAircraftStruct f = a_baseClass.getFlightStatus(b.Flight);
                    LatLonAlt loc = LatLonAlt.CreateUsingDegrees(f.latitude, f.longitude, 0);
                    b.setLocation = loc;
                }
                foreach (BunnyActor b in b_actors)
                    b.changeMode = !b.changeMode;
            }
        }

        private void ActivateFlight()
        {
            airport = a_airportClass.getAirport("JFK");
            a_flightlist = a_baseClass.getInformationForFlightVisualization("JFK",3);
            texture = Texture.FromResource(typeof(BunnyActor).Assembly, "ActorDataSource.bunny.png");
            a_dataSourcePlugin = new ActorDataSourcePlugIn(globleHost);
            b_builder = new BunnyBuilder(globleHost, texture, a_dataSourcePlugin.BindingsSource);
            LatLonAlt position = LatLonAlt.CreateUsingDegrees(airport.Latitude,airport.Longitude,0);
            b_actors = b_builder.CreateBunnyActors(a_flightlist.Count, position);

            int index = 0;
            foreach (BunnyActor bactor in b_actors)
            {
                bactor.setDestination = LatLonAlt.CreateUsingDegrees(
                    ((double)(a_flightlist[index].Destination.Latitude)),
                    ((double)(a_flightlist[index].Destination.Longitude)),
                    a_flightlist[index].Destination.Altitude);
                bactor.Flight = a_flightlist[index].Identity;
                index++;

                globleHost.Actors.Add(bactor);
            }
            //globleHost.Actors.Add(b_actors.GetEnumerator().Current);
        }

        private void DeactivateFlight()
        {
            
            if (texture != null)
            {              
                texture.Dispose();
                texture = null;
            }

            this.globleHost.RenderEngine.ManuallyUninitializeRender();
            this.globleHost.RenderEngine.ManuallyRenderNextFrame();
            this.globleHost.RenderEngine.ManuallyInitializeRender();
             // globleHost.RenderEngine.Size = new System.Drawing.Size(
           // this.globeControl.Host.RenderEngine.Graphics
           // = new System.Drawing.Size(this.globleHost.Control.Size.Width,
             //   this.globleHost.Control.Size.Height);
            //a_dataSourcePlugin.Deactivate();
            foreach (BunnyActor b in b_actors)
            {
                b.Dispose();
                b.OnRemove();
            }

        }

    }
}
