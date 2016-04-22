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
    public enum ColorFloor { CornflowerBlue}

    public class MapTile
    {
        public BaseObject m_Object;
        public MapTile() { }
    }

    public class Map
    {
        private int m_MaxFitness = 0;
        private int m_Width;
        private int m_Height;
        private MapTile[,] m_Grid;

        private float m_TileWidth = Ressources.Base_Texture.Width;
        private float m_TileHeight = Ressources.Base_Texture.Height;

        public Map(string MapName)
        {
            #region LoadImage and Grid
            Bitmap MapImage = new Bitmap(Image.FromFile("../../../../GeneticProgrammingContent/" + MapName + ".png"));
            m_Width = MapImage.Width;
            m_Height = MapImage.Height;
            m_Grid = new MapTile[m_Width, m_Height];
            #endregion

            #region Fill the grid
            System.Drawing.Color PixelColor;
            MapTile currentTile;
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    BaseObject tileObject = null;
                    Vector2 tilePosition = new Vector2(j * m_TileWidth, i * m_TileHeight);

                    PixelColor = MapImage.GetPixel(j, i);
                    switch (PixelColor.Name)
                    {
                        case "ff000000": tileObject = new Wall(tilePosition); break;
                        case "ffffffff": tileObject = new Floor(tilePosition); break;
                        case "ff0000ff": tileObject = new Parkour(tilePosition); break;
                        default: break;
                    }

                    currentTile = new MapTile();
                    currentTile.m_Object = tileObject;
                    m_Grid[i, j] = currentTile;
                }
            }
            #endregion

            ComputeMaximumFitness();
        }

        private void ComputeMaximumFitness() 
        {
            m_MaxFitness = 0;
            for (int i = 0; i < m_Grid.GetLength(0); i++)
            {
                for (int j = 0; j < m_Grid.GetLength(1); j++)
                {
                    if (HasElementAtIndex(new Vector2(i, j), typeof(Parkour)))
                    {
                        m_MaxFitness++;
                    }
                }
            }
        }

        public int GetMaximumFitness()
        {
            return m_MaxFitness;
        }
          
        public Vector2 GetPositionToIndex(Vector2 aPosition)
        {
            Vector2 positionIndexed;
            positionIndexed.X = aPosition.X / m_TileWidth;
            positionIndexed.Y = aPosition.Y / m_TileHeight;
            return positionIndexed;
        }

        public Vector2 GetIndexToPosition(Vector2 aIndex)
        {
            Vector2 position;
            position.X = aIndex.X * m_TileWidth;
            position.Y = aIndex.Y * m_TileHeight;
            return position;
        }

        public bool HasElementAtIndex(Vector2 aPositionIndexed, Type aType)
        {
            BaseObject baseObject = m_Grid[(int)aPositionIndexed.Y, (int)aPositionIndexed.X].m_Object;

            return baseObject != null && baseObject.GetType() == aType;
        }

        //Draw each object in the grid
        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < m_Grid.GetLength(0); i++)
            {
                for (int j = 0; j < m_Grid.GetLength(1); j++)
                {
                    m_Grid[i, j].m_Object.Draw(spritebatch);
                }
            }
        }
    }
}
