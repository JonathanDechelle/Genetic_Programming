using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GeneticProgramming
{
    public class AIOutline : AI
    {

        public AIOutline(Vector2 aPosition)
            : base(aPosition)
        {
        }

        protected override void DoNextMove(Map aMap)
        {
            base.DoNextMove(aMap);
            if (aMap.HasElementAtIndex(m_NextMoveIndexed, typeof(Parkour)))
            {
                if (!m_KnowPositions.Contains(m_NextMoveIndexed))
                {
                    m_KnowPositions.Add(m_NextMoveIndexed);
                    aMap.PassOnElement(m_NextMoveIndexed);
                }
            }
        }
    }
}
