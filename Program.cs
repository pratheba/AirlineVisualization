using System;
using System.Windows.Forms;


namespace AirlineVisualization
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1("dedual","7a4f1d723b7ce020ee6ba1bbf1517b20342cdce4"))
            {
                game.Run();
            }
        }
    }
}

