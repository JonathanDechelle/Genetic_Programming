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
    public class OutlineMap : Map
    {
        private Vector2 m_IAStartIndex = new Vector2(4, 4);
        private int m_MaxFitness = 0;

        public OutlineMap(string aMapName) : base(aMapName)
        {
            ComputeMaximumFitness();
        }

        protected void ComputeMaximumFitness() 
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

        public Vector2 GetIAStartIndex()
        {
            return m_IAStartIndex;
        }

        public Vector2 GetIAStartPosition()
        {
            return GetIndexToPosition(m_IAStartIndex);
        }
    }
}
