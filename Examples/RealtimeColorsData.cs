using GeometryDashAPI.Realtime;
using System;

namespace Examples
{
    class RealtimeColorsData
    {
        public static void Invoke()
        {
            // Open the game
            PlayingColors colors = new PlayingColors();
            // Open the level
            colors.Update();
            if (colors.ContainsID(1000))
            {
                // Output the background color to the console in hex value
                Console.WriteLine(colors[1000]);
            }
        }
    }
}
