using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Drawing;
using Microsoft.Xna.Framework.Input;

namespace GeneticProgramming
{
    public enum FlashLightMode { Square, Circle, Any }
    public enum ColorFloor { Black, CornflowerBlue}

    /// <summary>
    /// Manage All the object visually presented to the player and update them
    /// </summary>
    public class Map
    {
        BaseObject[,] m_Grid;
        int m_Width;
        int m_Height;
        FlashLightMode m_FlashLight_Mode;
        ColorFloor m_ColorFloor;
        public int Height { get { return m_Height; } }
        public int Width { get { return m_Width; } }
        public BaseObject[,] Grid { get { return m_Grid; } }
        public FlashLightMode FlashLight_Mode { get { return m_FlashLight_Mode; } }
        public ColorFloor ColorFloor { get { return m_ColorFloor; } }

        public Map(string MapName)
        {
            //load image for the dimension
            Bitmap MapImage = new Bitmap(Image.FromFile("../../../../GeneticProgrammingContent/" + MapName + ".png"));
            m_Width = MapImage.Width;
            m_Height = MapImage.Height;
            m_Grid = new BaseObject[m_Width, m_Height];
            m_FlashLight_Mode = FlashLightMode.Any;
            m_ColorFloor = ColorFloor.CornflowerBlue;
           
            //Fill the grid with each pixel of image 
            System.Drawing.Color PixelColor;
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    PixelColor = MapImage.GetPixel(j, i);

                    //Each Pixel Color is a type of BaseObject 
                    switch (PixelColor.Name)
                    {
                        case "ff000000": m_Grid[i, j] = new Wall(); break;
                        case "ffffffff": m_Grid[i, j] = new Floor(); break;
                        case "ff00ff00": m_Grid[i, j] = new StartPlayer(); break;
                        case "ffff0000": m_Grid[i, j] = new EnemyIndicator(); break;
                        case "ffff00ff": m_Grid[i, j] = new EnemyParkour(); break;
                        case "ff0000ff": m_Grid[i, j] = new Goal(); break;
                        case "ff00ffff": m_Grid[i, j] = new CheckPoint(); break;
                        default: break;
                    }
                    //if opacity is not Activate, put the grid object color the same color of the floor and reset his opacity
                    if (m_Grid[i, j].BaseOpacity == 0.00f) { m_Grid[i, j].ResetOpacityAndColor(); } 
                }
            }
        }

        //Generate and initiate a new enemy for each type ENEMY INDICATOR in the Grid
        public void Generate_Enemy(ref Enemy[,] Enemy_Position)
        {
            Vector2 Position;
            Enemy_Position = new Enemy[m_Width, m_Height];
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    Position = new Vector2(j * Ressources.Base_Texture.Width, i * Ressources.Base_Texture.Height);
                    if (Grid[i, j].GetType() == typeof(EnemyIndicator))
                    {
                        Enemy_Position[i, j] = new Enemy(Position, Microsoft.Xna.Framework.Color.Red, 1);
                        Enemy_Position[i, j].Initiate(Grid);
                    }
                }
            }
        }

        //Generate and initiate the player
        public void Generate_Player(ref Player player)
        {
            player = new Player(new Vector2(), Microsoft.Xna.Framework.Color.Green, 1);
            player.Initiate(this);
        }

        public void Update(Enemy[,] Enemy)
        {
            //FlashLight Mode
            int ActualMode;
            if (KeyboardHelper.KeyPressed(Keys.L))
            {
                ActualMode = (int)FlashLight_Mode;
                ActualMode++;
                if (ActualMode > 2) ActualMode = 0;
                m_FlashLight_Mode = (FlashLightMode)ActualMode;
            }

            if (KeyboardHelper.KeyPressed(Keys.M))
            {
                ActualMode = (int)ColorFloor;
                ActualMode++;
                if (ActualMode > 1) ActualMode = 0;
                m_ColorFloor = (ColorFloor)ActualMode;

                Microsoft.Xna.Framework.Color OldFloorColor = BasicFloor.FloorColor;
                switch (m_ColorFloor)
                {
                    case ColorFloor.Black: BasicFloor.FloorColor = Microsoft.Xna.Framework.Color.Black; break;
                    case ColorFloor.CornflowerBlue: BasicFloor.FloorColor = Microsoft.Xna.Framework.Color.CornflowerBlue; break;
                }
                ResetGrid(OldFloorColor);
            }
        }

        //Flash Light Mode OFF 
        //Reset Opacity of all object in Map(grid and Enemy) with the basic opacity foreach one
        public void Reset_Opacity(Enemy[,] Enemy)
        {
            for (int y2 = 0; y2 < Height; y2++)
            {
                for (int x2 = 0; x2 < Width; x2++)
                {
                    //Reset Opacity to basic opcaity(Normal)
                    if (Enemy[y2, x2] != null) Enemy[y2, x2].Opacity = Enemy[y2, x2].BaseOpacity;
                    Grid[y2, x2].Opacity = Grid[y2, x2].BaseOpacity;
                }
            }
        }

        //Reset player with the last checkpoint reached
        public void Reset_Player(Player player)
        {
            int i = 0;
            int j = 0;
            while (i < Height)
            {
                while (j < Width)
                {
                    if (Grid[i, j].GetType() == typeof(CheckPoint))
                    {
                        if (Grid[i, j].Last_Activated)
                        {
                            player.Position = new Vector2(j * player.Dimension.X, i * player.Dimension.Y);
                            return;
                        }
                    }
                    j++;
                }
                j = 0;
                i++;
            }
            player.Position = new Vector2(player.StartPosition.X, player.StartPosition.Y);
        }

        //reset checkpoint for know the lastcheckpoint reached
        public void Reset_CheckPoint(Vector2 LastCheckPointPosition)
        {
            Vector2 CheckPointPosition;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    CheckPointPosition = new Vector2(i, j);
                    if (Grid[i, j].GetType() == typeof(CheckPoint))
                    {
                        if (CheckPointPosition != LastCheckPointPosition) Grid[i, j].Last_Activated = false;
                        else Grid[i, j].Last_Activated = true;
                    }
                }
            }
        }
        
        //Reset Color of the previous floor with the new floor
        private void ResetGrid(Microsoft.Xna.Framework.Color OldFloorColor)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (m_Grid[i, j].Color == OldFloorColor) m_Grid[i, j].Color = BasicFloor.FloorColor;
                }
            }
        }

        //Draw each object in the grid
        public void Draw(SpriteBatch spritebatch)
        {
            Vector2 Position;
            BaseObject Obj;
         
            for (int i = 0; i < m_Grid.GetLength(0); i++)
            {
                for (int j = 0; j < m_Grid.GetLength(1); j++)
                {
                    Position = new Vector2(j * Ressources.Base_Texture.Width, i * Ressources.Base_Texture.Height);
                    Obj = new BaseObject(Position, m_Grid[i,j].Color, m_Grid[i,j].Opacity);
                    Obj.Draw(spritebatch);
                }
            }
        }
    }
}
