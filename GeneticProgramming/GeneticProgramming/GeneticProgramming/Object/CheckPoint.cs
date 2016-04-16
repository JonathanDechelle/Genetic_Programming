using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    /// <summary>
    /// Use like a Save spot for the player
    /// </summary>
    class CheckPoint:BaseObject
    {
        public CheckPoint()
        {
            m_BaseOpacity = 0.75f;
            m_Opacity = m_BaseOpacity;
            m_Color = Microsoft.Xna.Framework.Color.Orange;
        }
    }
}
