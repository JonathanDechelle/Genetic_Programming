using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GeneticProgramming
{
    /// <summary>
    /// This object represent the surface where the player is allow to pass
    /// </summary>
    class Floor:BaseObject
    {
        public Floor()
        {
            m_BaseOpacity = 1.00f;
            m_Opacity = m_BaseOpacity;
            m_Color = BasicFloor.FloorColor;
        }
    }

    public static class BasicFloor
    {
        public static Color FloorColor = Microsoft.Xna.Framework.Color.CornflowerBlue; 
    }
}
