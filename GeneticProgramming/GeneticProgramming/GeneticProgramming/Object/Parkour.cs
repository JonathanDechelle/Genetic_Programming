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
    public class Floor:BaseObject
    {
        public Floor(Vector2 aPosition)
            : base(aPosition)
        {
            m_Color = Microsoft.Xna.Framework.Color.CornflowerBlue;

            m_OutLine = Ressources.Outline_Texture;
            //m_Texture = Ressources.Gazon_Texture;
        }
    }
}
