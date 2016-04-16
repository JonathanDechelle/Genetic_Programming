using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GeneticProgramming
{
    public class BaseObject
    {
        public Vector2 m_Position;
        protected Color m_Color;
        protected Texture2D m_Texture;
        protected Texture2D m_OutLine;

        public BaseObject(Vector2 aPosition) 
        {
            m_Position = aPosition;
            m_Texture = Ressources.Base_Texture;
            m_OutLine = null;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(m_Texture, m_Position, m_Color);
            if (m_OutLine != null)
            {
                spritebatch.Draw(m_OutLine, m_Position, Color.Black);
            }
        }
    }
}
