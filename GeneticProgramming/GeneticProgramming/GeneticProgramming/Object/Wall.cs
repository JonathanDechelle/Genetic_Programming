using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GeneticProgramming
{
    /// <summary>
    /// This object is an obstacles for the player.
    /// </summary>
    class Wall:BaseObject
    {
        public Wall(Vector2 aPosition)
            : base(aPosition)
        {
            m_Color = Microsoft.Xna.Framework.Color.Sienna;
            m_InitialColor = m_Color;
            m_Texture = Ressources.Rock_Texture;
            m_OutLine = Ressources.Outline_Texture;
        }
    }
}
