using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GeneticProgramming
{
    public class PopulationContour : Population
    {
        private Map m_Map;
        public PopulationContour(int aPopulationCount, Map aMap) : base(aPopulationCount)
        {
            m_Map = aMap;
        }

        public override void ComputeAdaptation()
        {
            m_AdaptationSum = 0;
            int count = GetCount();
            for (int i = 0; i < count; i++)
            {
                ComputeChromosomeAdaptation(m_Chromosomes[i]);
                m_AdaptationSum += m_Chromosomes[i].m_Adaptation;
            }

            base.ComputeAdaptation();
        }

        private void ComputeChromosomeAdaptation(Chromosome aChromosome)
        {
            Vector2 currentPositionIndexed = m_Map.GetIAStartIndex();
            Vector2 nextMove = Vector2.Zero;
            int adaptation = 0;

            List<Vector2> knowPositions = new List<Vector2>();
            int[] movements = aChromosome.GetGenes();
            for (int i = 0; i < movements.Length; i++)
            {
                switch ((EMovement)movements[i])
                {
                    case EMovement.UP:    nextMove = -Vector2.UnitY; break;
                    case EMovement.DOWN:  nextMove =  Vector2.UnitY; break;
                    case EMovement.LEFT:  nextMove = -Vector2.UnitX; break;
                    case EMovement.RIGHT: nextMove =  Vector2.UnitX; break;
                }

                Vector2 newPositionIndexed = currentPositionIndexed + nextMove;
                if (m_Map.HasElementAtIndex(newPositionIndexed, typeof(Wall)))
                {
                    continue;
                }

                if (m_Map.HasElementAtIndex(newPositionIndexed, typeof(Parkour)))
                {
                    if (!knowPositions.Contains(newPositionIndexed))
                    {
                        knowPositions.Add(newPositionIndexed);
                        adaptation++;
                    }
                }

                currentPositionIndexed = newPositionIndexed;
            }

            aChromosome.m_Adaptation = adaptation;
        }
    }
}
