using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GeneticProgramming
{
    public enum EMovement
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
        NONE = 4
    }
        
    public class IA : BaseObject
    {
        public float m_MovementSpeed;
        public bool m_HasFinish;

        private int m_MovementIndex = 0;
        public int[] m_Movements; 

        private List<Vector2> m_KnowPositions = new List<Vector2>();
        private int m_Adaptation = 0;

        private float m_Timer = 0;
        private float m_WaitingTime;
        private float m_BaseDistance;
        private Vector2 m_BasePosition;
        private const float FAKE_FRAME_RATE = 60;

        public IA(Vector2 aPosition)
            : base(aPosition)
        {
            m_BasePosition = aPosition;
            m_Color = Microsoft.Xna.Framework.Color.Green;
            m_BaseDistance = m_Texture.Bounds.Width;
            m_WaitingTime = FAKE_FRAME_RATE * 5; // just for testing

            m_Timer = m_WaitingTime;
        }

        public void ResetBasePosition()
        {
            m_Position = m_BasePosition;
            m_HasFinish = false;
            m_MovementIndex = 0;
            m_Timer = m_WaitingTime;
            m_KnowPositions.Clear();
        }

        public void Update(Map aMap)
        {
            m_Timer -= m_MovementSpeed;
            if(m_MovementIndex > m_Movements.Length - 1)
            {
                if (m_Timer < -m_WaitingTime)
                {
                    m_HasFinish = true;
                }

                return;
            }
           
            if (m_Timer < 0)
            {
                Vector2 currentPositionIndexed = aMap.GetPositionToIndex(m_Position);
                Vector2 nextMove = Vector2.Zero;
                switch((EMovement)m_Movements[m_MovementIndex])
                {
                    case EMovement.UP:    nextMove = -Vector2.UnitY; break;
                    case EMovement.DOWN:  nextMove =  Vector2.UnitY; break;
                    case EMovement.LEFT:  nextMove = -Vector2.UnitX; break;
                    case EMovement.RIGHT: nextMove =  Vector2.UnitX; break;
                }

                Vector2 newPositionIndexed = currentPositionIndexed + nextMove;
                if (!aMap.HasElementAtIndex(newPositionIndexed, typeof(Wall)))
                {
                    m_Position = newPositionIndexed * m_BaseDistance;
                }

                if (aMap.HasElementAtIndex(newPositionIndexed, typeof(Parkour)))
                {
                    if (!m_KnowPositions.Contains(newPositionIndexed))
                    {
                        m_KnowPositions.Add(newPositionIndexed);
                        m_Adaptation++;

                        aMap.PassOnElement(newPositionIndexed);
                    }
                }

                m_Timer = FAKE_FRAME_RATE;
                m_MovementIndex++;
            }
        }
    }
}
