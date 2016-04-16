using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    /// <summary>
    /// with an Opacity > 0 this object mark the enemy starting point
    /// </summary>
    class EnemyIndicator:BaseObject
    {
        public EnemyIndicator()
        {
            m_BaseOpacity = 0.00f; 
            m_Opacity = m_BaseOpacity;
            m_Color = Microsoft.Xna.Framework.Color.Red;
        }
    }
}
