using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Net;
using System.Web.Services;

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
//using SimpleForm;

using Microsoft.MapPoint.Rendering3D;
using Microsoft.MapPoint.Rendering3D.UI;
using Microsoft.MapPoint.Rendering3D.GraphicsProxy;
using Microsoft.MapPoint.Binding;
//using InfoStrat.VE.Utilities;
//using InfoStrat.VE;
using InfoStrat;


namespace AirlineVisualization
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Flight.Flight> listflights;
        List<Vector3> routevectors;
        BaseClass baseClass;
        Scene scene;
        
      
        
        public Game1(string name, string APICode)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();
        }

      
        protected override void Initialize()
        {
            routevectors = new List<Vector3>();
            base.Initialize();
            KeyboardInput.Instance.KeyPressEvent += new HandleKeyPress(Instance_KeyPress);
     
        }

         
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            State.InitGoblin(graphics, Content, "");
            scene = new Scene();
            scene.BackgroundColor = Color.Black;
    
            baseClass = new BaseClass();
            baseClass.Initialize(ref scene, ref graphics);
            listflights = baseClass.getInformationForFlightVisualization();
            routevectors = baseClass.getListofVectorsForRoute(listflights);

            // TODO: use this.Content to load your game content here
           
            
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

      
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // TODO: Add your update logic here
            base.Update(gameTime);
            scene.Update(gameTime.ElapsedGameTime, gameTime.IsRunningSlowly, this.IsActive);
  
        }

        public void Instance_KeyPress(Keys key, KeyModifier target)
        {
            if (Keys.Z == key)
            {
                if(baseClass.staticCamera.FieldOfViewY < MathHelper.ToRadians(180))
                    baseClass.staticCamera.FieldOfViewY = baseClass.staticCamera.FieldOfViewY + (float)0.01;
                else
                    baseClass.staticCamera.FieldOfViewY = MathHelper.ToRadians(45);
            }
            if (Keys.X == key)
            {
                if (baseClass.staticCamera.FieldOfViewY > 0)
                    baseClass.staticCamera.FieldOfViewY = baseClass.staticCamera.FieldOfViewY - (float)0.01;
                else
                    baseClass.staticCamera.FieldOfViewY = MathHelper.ToRadians(45);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Vector3 source = new Vector3(0,0, 0);
            foreach(Vector3 v in routevectors)
            {        
                DebugShapeRenderer.AddLine(source, v, Color.Red);        
            }

          //  System.Console.WriteLine(latLong.Latitude.);

          //   latLong.GetCoordinate2D().ToString();
            // TODO: Add your drawing code here
          //  basicEffect.CurrentTechnique.Passes[0].Apply();
          //  var vertices = new[] { new VertexPositionColor(startPoint, Color.White), new VertexPositionColor(endPoint, Color.White) };
          //  GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
           // base.Draw(gameTime);
          //  scene.BackgroundColor = Color.Black;
            scene.Draw(gameTime.ElapsedGameTime, gameTime.IsRunningSlowly);
          
         
        }
    }
}
