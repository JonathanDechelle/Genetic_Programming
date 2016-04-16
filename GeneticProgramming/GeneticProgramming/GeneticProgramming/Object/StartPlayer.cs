using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GeneticProgramming
{
    /// <summary>
    /// This object is the starter. The player starting point
    /// </summary>
    class StartPlayer:BaseObject
    {
        public StartPlayer()
        {
            m_BaseOpacity = 0.35f;
            m_Opacity = m_BaseOpacity;
            m_Color = Microsoft.Xna.Framework.Color.Green;
        }
    }
}
