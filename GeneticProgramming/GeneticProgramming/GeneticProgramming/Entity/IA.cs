using Microsoft.Xna.Framework;

namespace GeneticProgramming
{
    public class IA : BaseObject
    {
        public float m_MovementSpeed;

        private int m_MovementIndex = 0;
        private int[] m_Movements = new int[4]
        {
            3,
            3,
            1,
            2,
        };

        private float m_Timer = 0;
        private float m_BaseDistance;
        private float FAKE_FRAME_RATE = 60;

        public IA(Vector2 aPosition)
            : base(aPosition)
        {
            m_Color = Microsoft.Xna.Framework.Color.Green;
            m_BaseDistance = m_Texture.Bounds.Width;
        }

        public void Update()
        {
            if(m_MovementIndex > m_Movements.Length - 1)
            {
                return;
            }

            m_Timer -= m_MovementSpeed;
            if (m_Timer < 0)
            {
                switch(m_Movements[m_MovementIndex])
                {
                    case 0: m_Position.Y -= m_BaseDistance; break;
                    case 1: m_Position.Y += m_BaseDistance; break;
                    case 2: m_Position.X -= m_BaseDistance; break;
                    case 3: m_Position.X += m_BaseDistance; break;
                }

                m_Timer = FAKE_FRAME_RATE;
                m_MovementIndex++;
            }
        }
    }
}
