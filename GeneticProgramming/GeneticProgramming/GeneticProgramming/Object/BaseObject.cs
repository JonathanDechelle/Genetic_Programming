using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GeneticProgramming
{
    /// <summary>
    /// Class Container of all the attributes and field of a Map Object
    /// </summary>
    public class BaseObject
    {
        protected Vector2 m_Position;
        protected Color m_Color;
        protected float m_Opacity;
        protected float m_BaseOpacity;
        protected bool m_Last_Activated; //Checkpoint only
        protected bool m_Activated; //Checkpoint only
        Texture2D m_Texture;
        Texture2D m_Outline_Texture;
        Texture2D m_Gazon_Texture;
        Texture2D m_Rock_Texture;

        public float Opacity { get { return m_Opacity; } set { m_Opacity = value; } }
        public float BaseOpacity { get { return m_BaseOpacity; } }
        public Color Color { get { return m_Color; } set { m_Color = value; } }
        public Vector2 Dimension { get { return new Vector2(m_Texture.Width, m_Texture.Height); } }
        public bool Last_Activated { get { return m_Last_Activated; } set { m_Last_Activated = value; } }
        public bool Activated { get { return m_Activated; } set { m_Activated = value; } }

        public BaseObject(Vector2 Position, Color Color, float Opacity)
        {
            this.m_Position = Position;
            this.m_Color = Color;
            this.m_Opacity = Opacity;
            m_Last_Activated = false;
            m_Texture = Ressources.Base_Texture;
            m_Outline_Texture = Ressources.Outline_Texture;
            m_Gazon_Texture = Ressources.Gazon_Texture;
            m_Rock_Texture = Ressources.Rock_Texture;
        }

        public BaseObject()
        {

        }

        public void ResetOpacityAndColor()
        {
            m_BaseOpacity = 1.00f;
            m_Opacity = m_BaseOpacity;
            m_Color = BasicFloor.FloorColor;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (m_Opacity > 0)
            {
                
                /* draw First Stage of texture */
                spritebatch.Draw(m_Texture, m_Position, BasicFloor.FloorColor);
                //spritebatch.Draw(m_Gazon_Texture, m_Position, m_Color * m_Opacity);

                /* Draw Texture depend of the type */
                if(m_Color == Microsoft.Xna.Framework.Color.Sienna) spritebatch.Draw(m_Rock_Texture, m_Position, m_Color * m_Opacity);
                //else if (m_Color == BasicFloor.FloorColor) spritebatch.Draw(m_Gazon_Texture, m_Position, m_Color * m_Opacity);
                else spritebatch.Draw(m_Texture, m_Position, m_Color * m_Opacity);

                if (m_Color != BasicFloor.FloorColor) spritebatch.Draw(m_Outline_Texture, m_Position, Color.Black);
            }
            else spritebatch.Draw(m_Texture, m_Position, BasicFloor.FloorColor * m_Opacity);
        }
    }
}
