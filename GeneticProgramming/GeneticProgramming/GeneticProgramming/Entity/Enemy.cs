using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GeneticProgramming
{
    public class Enemy: BaseObject
    {
        enum Direction { Left, Up, Right, Down, Any}

        Direction m_Direction;
        float Speed;
        float CptSpeed;
        Vector2 DimensionEnemy;
        public Vector2 Position { get { return base.m_Position; } set { base.m_Position = value; } }
        public Vector2 Index_Position { get { return new Vector2(Position.X / Dimension.X, Position.Y / Dimension.Y); } }

        public Enemy(Vector2 Position, Color Color, float Opacity)
            : base(Position, Color,Opacity)
        {
            //Constructor
            m_Direction = Direction.Any;
            m_BaseOpacity = Opacity;
            m_Opacity = Opacity;
            Speed = 0.25f;
            //Speed = 0.010f;
        }

        public void Initiate(BaseObject[,] Grid)
        {
            DimensionEnemy = new Vector2(Ressources.Base_Texture.Width, Ressources.Base_Texture.Height);
            //Index in grid
            int X = (int)(Position.X / DimensionEnemy.X);
            int Y = (int)(Position.Y / DimensionEnemy.Y);

            //define the first Direction of the enemy
                 if (Grid[Y, X + 1].GetType() == typeof(EnemyParkour) || Grid[Y, X + 1].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Right;
            else if (Grid[Y, X - 1].GetType() == typeof(EnemyParkour) || Grid[Y, X - 1].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Left;
            else if (Grid[Y + 1, X].GetType() == typeof(EnemyParkour) || Grid[Y + 1, X].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Down;
            else if (Grid[Y - 1, X].GetType() == typeof(EnemyParkour) || Grid[Y - 1, X].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Up;
        }

        public void Update(BaseObject[,] Grid)
        {
            base.m_Opacity = Opacity;

            //Look the route
            //Index in grid
            int X = (int)(Position.X / DimensionEnemy.X);
            int Y = (int)(Position.Y / DimensionEnemy.Y);

            //Move depend of the speed
            CptSpeed += Speed;
            if (CptSpeed > 1)
            {
                //Check if we have parkour block near again
                switch (m_Direction)
                {
                    //not complete but this algo work at 60% of all case
                    case Direction.Right:
                        if (Grid[Y, X + 1].GetType() == typeof(Wall) || Grid[Y, X + 1].GetType() == typeof(Floor) || Grid[Y, X + 1].GetType() == typeof(CheckPoint))
                        {
                                 if (Grid[Y + 1, X].GetType() == typeof(EnemyParkour) || Grid[Y + 1, X].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Down;
                            else if (Grid[Y - 1, X].GetType() == typeof(EnemyParkour) || Grid[Y - 1, X].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Up;
                        }
                        else
                        {
                            if (Grid[Y - 1, X + 1].GetType() == typeof(EnemyParkour) || Grid[Y - 1, X + 1].GetType() == typeof(EnemyIndicator))
                            {
                                if (Grid[Y - 1, X].GetType() != typeof(Wall) && Grid[Y - 1, X].GetType() != typeof(Floor) && Grid[Y - 1, X].GetType() != typeof(CheckPoint)) m_Direction = Direction.Up;
                            }
                            else if (Grid[Y + 1, X + 1].GetType() != typeof(EnemyParkour) && Grid[Y + 1, X + 1].GetType() != typeof(EnemyIndicator))
                            {
                                if (Grid[Y + 1, X + 1].GetType() != typeof(EnemyParkour) && Grid[Y + 1, X + 1].GetType() != typeof(EnemyIndicator))
                                {
                                    if (Grid[Y + 1, X].GetType() != typeof(Wall) && 
                                        Grid[Y + 1, X].GetType() != typeof(Floor) &&
                                        Grid[Y + 1, X].GetType() != typeof(CheckPoint) &&
                                        Grid[Y + 1, X - 1].GetType() != typeof(Wall) && 
                                        Grid[Y + 1, X - 1].GetType() != typeof(Floor) &&
                                        Grid[Y + 1, X - 1].GetType() != typeof(CheckPoint)) m_Direction = Direction.Down;
                                }
                            }
                        }
                        break;
                    case Direction.Up:
                        if (Grid[Y - 1, X].GetType() == typeof(Wall) || Grid[Y - 1, X].GetType() == typeof(Floor) || Grid[Y - 1, X].GetType() == typeof(CheckPoint))
                        {
                                 if (Grid[Y, X - 1].GetType() == typeof(EnemyParkour) || Grid[Y, X - 1].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Left;
                            else if (Grid[Y, X + 1].GetType() == typeof(EnemyParkour) || Grid[Y, X + 1].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Right;
                        }
                        break;
                    case Direction.Left:
                        if (Grid[Y, X - 1].GetType() == typeof(Wall) || Grid[Y, X - 1].GetType() == typeof(Floor) || Grid[Y, X - 1].GetType() == typeof(CheckPoint))
                        {
                                 if (Grid[Y + 1, X].GetType() == typeof(EnemyParkour) || Grid[Y + 1, X].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Down;
                            else if (Grid[Y - 1, X].GetType() == typeof(EnemyParkour) || Grid[Y - 1, X].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Up;
                        }
                        else
                        {
                            if (Grid[Y + 1, X - 1].GetType() == typeof(EnemyParkour) || Grid[Y + 1, X - 1].GetType() == typeof(EnemyIndicator))
                            {
                                if (Grid[Y - 1, X - 1].GetType() == typeof(EnemyParkour) || Grid[Y - 1, X - 1].GetType() == typeof(EnemyIndicator))
                                {
                                    if (Grid[Y + 1, X].GetType() != typeof(Wall) && 
                                        Grid[Y + 1, X].GetType() != typeof(Floor) &&
                                        Grid[Y + 1, X].GetType() != typeof(CheckPoint)) m_Direction = Direction.Down;
                                }
                                else
                                {
                                    if (Grid[Y - 1, X].GetType() != typeof(Wall) && 
                                        Grid[Y - 1, X].GetType() != typeof(Floor) &&
                                        Grid[Y - 1, X].GetType() != typeof(CheckPoint)) m_Direction = Direction.Up;
                                }
                            }                               
                        }
                        break;
                    case Direction.Down:
                        if (Grid[Y + 1, X].GetType() == typeof(Wall) || Grid[Y + 1, X].GetType() == typeof(Floor) || Grid[Y + 1, X].GetType() == typeof(CheckPoint))
                        {
                                 if (Grid[Y, X - 1].GetType() == typeof(EnemyParkour) || Grid[Y, X - 1].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Left;
                            else if (Grid[Y, X + 1].GetType() == typeof(EnemyParkour) || Grid[Y, X + 1].GetType() == typeof(EnemyIndicator)) m_Direction = Direction.Right;
                        }
                        else
                        {
                            if ((Grid[Y, X + 1].GetType() == typeof(EnemyParkour) || Grid[Y, X + 1].GetType() == typeof(EnemyIndicator)) && 
                                (Grid[Y + 1, X - 1].GetType() != typeof(Floor) &&  Grid[Y + 1, X - 1].GetType() != typeof(CheckPoint)))
                                m_Direction = Direction.Right;
                        }
                        break;
                }

                //Move depend of the direction
                switch (m_Direction)
                {
                    case Direction.Right: m_Position.X += DimensionEnemy.X ; break;
                    case Direction.Up: m_Position.Y -= DimensionEnemy.Y; break;
                    case Direction.Left: m_Position.X -= DimensionEnemy.X; break;
                    case Direction.Down: m_Position.Y += DimensionEnemy.Y; break;
                }
                CptSpeed = 0;
            }
        }
    }

    
}
