using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GeneticProgramming
{
    public class Player : BaseObject
    {
        Vector2 m_BasePosition;
        float cptVelocity;
        float Velocity;
        public Vector2 StartPosition { get { return m_BasePosition; } set { m_BasePosition = value; } }
        public Vector2 Position { get { return base.m_Position; } set { base.m_Position = value; } }
        bool m_ExitGame;

        public bool ExitGame { get { return m_ExitGame; } }

        public Player(Vector2 Position, Color Color, float Opacity)
            : base(Position, Color,Opacity)
        {
            //Constructor
        }

        public void Initiate(Map Map)
        {
            Velocity = 0.20f;
            cptVelocity = 1;
            bool found = false;
            int i = 0;
            int j = 0;
            while (!found && i < Map.Height)
            {
                while (!found && j < Map.Width)
                {
                    //StartPosition of the player
                    if (Map.Grid[i, j].GetType() == typeof(StartPlayer))
                    {
                        Map.Grid[i, j] = new CheckPoint();
                        m_BasePosition = new Vector2(j * Dimension.X, i * Dimension.Y);
                        Light_Management.List_Light.Add(new Light(new Vector2(i,j),6));
                        Map.Reset_Player(this);
                        found = true;
                    }
                    j++;
                }
                j = 0;
                i++;
            }
        }

        //Check if you collide with a enemy
        private bool Collide_Enemy(Map Map,Enemy[,] Enemy)
        {
            int i = 0;
            int j = 0;

            while (i < Enemy.GetLength(0))
            {
                while (j < Enemy.GetLength(1))
                {
                    if (Enemy[i, j] != null)
                    {
                        if (Enemy[i, j].Position == base.m_Position)
                        {
                            Map.Reset_Player(this);
                            return true;
                        }
                    }
                    j++;
                }
                j = 0;
                i++;
            }

            return false;
        }

        public void Update(Map Map, Enemy[,] Enemy)
        {
            BaseObject[,] Grid = Map.Grid;
            int X = (int)(base.m_Position.X / Dimension.X);
            int Y = (int)(base.m_Position.Y / Dimension.Y);

            #region Second Mode keyboard
            //if (KeyboardHelper.KeyPressed(Keys.D)) base.Position.X += DimensionPlayer.X;
            //if (KeyboardHelper.KeyPressed(Keys.A)) base.Position.X -= DimensionPlayer.X;
            //if (KeyboardHelper.KeyPressed(Keys.S)) base.Position.Y += DimensionPlayer.Y;
            //if (KeyboardHelper.KeyPressed(Keys.W)) base.Position.Y -= DimensionPlayer.Y;
            #endregion

            //KeyboarHelper
            if (KeyboardHelper.KeyHold(Keys.D)) { cptVelocity += Velocity; if (cptVelocity >= 1) { base.m_Position.X += Dimension.X; cptVelocity = 0; } }
            else if (KeyboardHelper.KeyHold(Keys.A)) { cptVelocity += Velocity; if (cptVelocity >= 1) { base.m_Position.X -= Dimension.X; cptVelocity = 0; } }
            else if (KeyboardHelper.KeyHold(Keys.S)) { cptVelocity += Velocity; if (cptVelocity >= 1) { base.m_Position.Y += Dimension.Y; cptVelocity = 0; } }
            else if (KeyboardHelper.KeyHold(Keys.W)) { cptVelocity += Velocity; if (cptVelocity >= 1) { base.m_Position.Y -= Dimension.Y; cptVelocity = 0; } }
            else cptVelocity = 1;

            //FlashLight Mode Activate(The light follow the player)
            if (Map.FlashLight_Mode != FlashLightMode.Any) Light_Management.List_Light[0].Position = new Vector2(X, Y);

            //Collide with a wall
            if (Grid[Y, X].GetType() == typeof(Wall)) Map.Reset_Player(this);

            //Collide with a checkpoint
            if (Grid[Y, X].GetType() == typeof(CheckPoint) && !Grid[Y,X].Activated)
            {
                Grid[Y, X].Color = Color.YellowGreen;
                Grid[Y, X].Activated = true;
                Light_Management.List_Light.Add(new Light(new Vector2(X, Y), 5));
                Map.Reset_CheckPoint(new Vector2(Y, X));
            }

            if (Grid[Y, X].GetType() == typeof(Goal)) m_ExitGame = true;

            //Collide with a enemy
            Collide_Enemy(Map,Enemy);
        }
    }
}
