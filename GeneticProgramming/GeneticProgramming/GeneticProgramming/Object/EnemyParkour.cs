using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    /// <summary>
    /// with an Opacity > 0 this object mark the enemy parkour
    /// </summary>
    class EnemyParkour:BaseObject
    {
        public EnemyParkour()
        {
            m_BaseOpacity = 0.00f;
            m_Opacity = m_BaseOpacity;
            m_Color = Microsoft.Xna.Framework.Color.Purple;
        }
    }
}
