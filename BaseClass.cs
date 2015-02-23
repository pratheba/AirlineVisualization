using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GoblinXNA;
using GoblinXNA.Graphics;
using GoblinXNA.SceneGraph;
using Model = GoblinXNA.Graphics.Model;
using GoblinXNA.Graphics.Geometry;
using GoblinXNA.Device.Generic;
using GoblinXNA.UI.UI2D;
using GoblinXNA.Shaders;
using GoblinXNA.Physics.Newton1;
using GoblinXNA.Device.Capture;
using GoblinXNA.Device.Vision.Marker;
using GoblinXNA.Physics;

namespace AirlineVisualization
{
    public class BaseClass
    {
        public static readonly BaseClass baseInstance = new BaseClass();
        public static Scene baseScene;

        private Airport.AirportServiceClass AirportServieClass_;
        private Flight.FlightServiceClass FlightServiceClass_;
        private List<Flight.Flight> flightsList;
        private Map.VELatLong LatLongClass_;
        
        // A GoblinXNA scene graph
        private static Scene scene;
        private static GraphicsDeviceManager graphics;
      
        // A sprite font to draw a 2D text string
        SpriteFont textFont;
        SpriteBatch spriteBatch;

        // Object Transformation Node
        // CameraNode cameraNode;
        TransformNode sphereTransNode;
     
        // Camera For 2 viewports
        CameraNode staticCameraNode;
        public Camera staticCamera;

        // Viewport Setting
        RenderTarget2D staticRenderTarget;
        Rectangle staticRectangle;

        Matrix projectionMatrix;
        Matrix viewMatrix;
        Matrix worldMatrix;
        Matrix mat;

        float x, y, width, height;
        float angle = 0.0f;
        FlightXML2 flightXML;
      
        public BaseClass()
        {
                AirportSerivceClass_ = new Airport.AirportServiceClass();
                FlightServiceClass_ = new Flight.FlightServiceClass();
                LatLongClass_ = new Map.VELatLong();
                flightsList = new List<Flight.Flight>();
                flightXML = FlightXMLUtil.getXMLInstance();
        }

        public void Initialize(ref Scene refscene, ref GraphicsDeviceManager refGraphics)
        {
            if (scene == null)
                scene = refscene;
            if (graphics == null)
                graphics = refGraphics;

            CreateLights();
            CreateCamera();
            CreateObject();
        }
       

        private static void CreateLights()
        {
            // Create a directional light source
            LightSource lightSource = new LightSource();
            lightSource.Direction = new Vector3(-1, -1, -1);
            lightSource.Diffuse = Color.White.ToVector4();
            lightSource.Specular = new Vector4(0.6f, 0.6f, 0.6f, 1);

            // Create a light node to hold the light source
            LightNode lightNode = new LightNode();
            lightNode.LightSource = lightSource;

            // Add this light node to the root node
            scene.RootNode.AddChild(lightNode);
        }

        private void CreateCamera()
        {
            // Create a camera for 2D visualization 
            staticCamera = new Camera();
            staticCamera.Translation = new Vector3(0, 0, 0);
            staticCamera.FieldOfViewY = MathHelper.ToRadians(45);
            staticCamera.ZNearPlane = 1.0f;
            staticCamera.ZFarPlane = 1000;
            staticCamera.AspectRatio = (float)(graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(staticCamera.FieldOfViewY, staticCamera.AspectRatio,
                                                        staticCamera.ZNearPlane, staticCamera.ZFarPlane);
            viewMatrix = Matrix.CreateLookAt(new Vector3(0,0,-10),Vector3.Zero, Vector3.Up);
            staticCamera.View = viewMatrix;
            staticCamera.Projection = projectionMatrix;
          
            // Now assign camera to a camera node, and add this camera node to our scene graph
            staticCameraNode = new CameraNode(staticCamera);
            scene.RootNode.AddChild(staticCameraNode);

            scene.CameraNode = staticCameraNode;

        }

        private void CreateObject()
        {
            GeometryNode sphereNode = new GeometryNode("Sphere");
            sphereNode.Model = new Box(60);
            sphereTransNode = new TransformNode();
            sphereTransNode.Translation = new Vector3(0, 0, 0);

            Material sphereMaterial = new Material();
            sphereMaterial.Diffuse = new Vector4(0.5f, 0, 0, 1);
            sphereMaterial.Specular = Color.White.ToVector4();
            sphereMaterial.SpecularPower = 10;

            // Apply this material to the sphere model
            sphereNode.Material = sphereMaterial;
            sphereTransNode.AddChild(sphereNode);

            scene.RootNode.AddChild(sphereTransNode);
        }

        public Airport.Airport getAirportInformation(string Ident)
        {
            return (AirportServieClass_.getAirport(Ident));
        }
        public List<Flight.Flight> getInformationForFlightVisualization()
        {
            //flightsList = FlightServiceClass_.getFlightFromAirport("JFK", 10, null, 0);
            return flightsList;
        }
        // Key Board Event to reset and Exit
       

        public List<Vector3> getListofVectorsForRoute(List<Flight.Flight> flights)
        {
            List<Vector3> routeLines = new List<Vector3>();
            Vector3 axis;
            int i=0;
            Random r = new Random(10);
               
            foreach (Flight.Flight f in flights)
            {
                r = new Random(10+i);
                axis = new Vector3();
                axis.X = r.Next(-10, 10);
                axis.Y = r.Next(-10, 10);
                axis.Z = r.Next(0,10);            
                routeLines.Add(axis);
                i = i + 10;
            }

            return routeLines;
      
        }

     }
}
