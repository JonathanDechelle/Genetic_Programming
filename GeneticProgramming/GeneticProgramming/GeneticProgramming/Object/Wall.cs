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
        public Wall()
        {
            m_BaseOpacity = 1.00f;
            m_Opacity = m_BaseOpacity;
            m_Color = Microsoft.Xna.Framework.Color.Sienna;
        }
    }
}
