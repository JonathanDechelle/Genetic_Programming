using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GeneticProgramming
{
    /// <summary>
    /// Manage all the light
    /// </summary>
    static public class Light_Management
    {
        public static List<Light> List_Light = new List<Light>();
        public static void Update(Map Map, Enemy[,] Enemy)
        {
            //Reset the opacity in the dark(Opacity == 0)
            if (Map.FlashLight_Mode != FlashLightMode.Any)
            {
                for (int y2 = 0; y2 < Map.Height; y2++)
                {
                    for (int x2 = 0; x2 < Map.Width; x2++)
                    {
                        Map.Grid[y2, x2].Opacity = 0.00f;
                        if (Enemy[y2, x2] != null) Enemy[y2, x2].Opacity = 0.00f;
                    }
                }
                foreach (Light light in List_Light) light.Update(Map, Enemy);
            }
            
            //Reset Opacity with basic opacity
            else Map.Reset_Opacity(Enemy);
        }
    }
}
