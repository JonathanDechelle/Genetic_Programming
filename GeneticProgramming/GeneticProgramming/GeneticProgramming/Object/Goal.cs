using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    /// <summary>
    /// This object is te goal of the player. He must reached it
    /// </summary>
    class Goal:BaseObject
    {
        public Goal()
        {
            m_BaseOpacity = 0.75f;
            m_Opacity = m_BaseOpacity;
            m_Color = Microsoft.Xna.Framework.Color.Blue;
        }
    }
}
