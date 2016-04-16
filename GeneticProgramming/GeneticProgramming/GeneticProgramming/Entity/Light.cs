using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GeneticProgramming
{
    public class Light:BaseObject
    {
        float m_OpacityFactor;
        int m_Light_Radius;
        public Vector2 Position { get { return m_Position; } set { m_Position = value; } }
        public int Light_Radius { get { return m_Light_Radius; } }

        public Light(Vector2 Position, int Radius)
        {
            m_Position = Position;
            m_Light_Radius = Radius;
        }

        //Determine if the object is in the light
        //if yes [Determine the Opacity factor (if is close the center of the light)]
        private bool ObjectInLight(Map Map, Vector2 ObjectPosition)
        {
            double DeltaX = Math.Pow(ObjectPosition.X - m_Position.X, 2);
            double DeltaY = Math.Pow(ObjectPosition.Y - m_Position.Y, 2);
            double Distance;
            Distance = Math.Sqrt(DeltaY + DeltaX);
            m_OpacityFactor = (float)Math.Abs(Distance - Light_Radius) / Light_Radius;
            //OpacityFactor = (float)Math.Abs(Distance - Map.FlashLight_Radius * 2) / Map.FlashLight_Radius;


            ////Square Light
            if (Map.FlashLight_Mode == FlashLightMode.Square)
                return (ObjectPosition.X < m_Position.X + Light_Radius && ObjectPosition.Y < m_Position.Y + Light_Radius &&
                        ObjectPosition.X > m_Position.X - Light_Radius && ObjectPosition.Y > m_Position.Y - Light_Radius);

            //Circle Light [if the distance is higher of the radius] the object is not in light
            else if (Map.FlashLight_Mode == FlashLightMode.Circle)
                return (Distance < Light_Radius);

            else return true;
        }

        public void Update(Map Map, Enemy[,] Enemy)
        {
            for (int y2 = 0; y2 < Map.Height; y2++)
            {
                for (int x2 = 0; x2 < Map.Width; x2++)
                {
                    // you have spot enemy
                    if (Enemy[y2, x2] != null)
                    {
                        Vector2 EnemyPosition = Enemy[y2, x2].Index_Position;
                        
                        //Add Opacity depend of the proximity with a Light
                        if (ObjectInLight(Map, EnemyPosition)) Enemy[y2, x2].Opacity += Enemy[y2, x2].BaseOpacity * m_OpacityFactor;
                    }

                    //Add Opacity depend of the proximity with a Light
                    if (ObjectInLight(Map, new Vector2(x2, y2))) Map.Grid[y2, x2].Opacity += Map.Grid[y2, x2].BaseOpacity * m_OpacityFactor;
                }
            }
        }

    }
}
