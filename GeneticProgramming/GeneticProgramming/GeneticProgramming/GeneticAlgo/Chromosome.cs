using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GeneticProgramming
{
    public class Chromosome
    {
        public double m_Adaptation;
        private int[] m_ChromosomeBit;
        private int m_MaximumParameterIndice;

        public Chromosome(int aBitCount, int aNbParameter)
        {
            /* TODO add argument for nb parameter 
             0 = back
             1 = front
             2 = left
             3 = right 
             */
            m_ChromosomeBit = new int[aBitCount];
            byte randomByte;
            for (int i = 0; i < aBitCount; i++)
            {
                randomByte = Convert.ToByte(Ressources.m_Random.Next(aNbParameter));
                m_ChromosomeBit[i] = randomByte;
            }

            m_MaximumParameterIndice = aNbParameter - 1;
        }

        private Chromosome(Chromosome aChromosome)
        {
            m_Adaptation = aChromosome.m_Adaptation;
            int length = aChromosome.GetLenght();
            m_ChromosomeBit = new int[length];

            for (int i = 0; i < length; i++)
            {
                m_ChromosomeBit[i] = aChromosome.m_ChromosomeBit[i];
            }
        }

        public int GetGeneAt(int aIndex)
        {
            return m_ChromosomeBit[aIndex];
        }

        public int[] GetGenes()
        {
            return m_ChromosomeBit;
        }

        public int GetLenght()
        {
            return m_ChromosomeBit.Length;
        }

        public int GetMaximumParameterIndice()
        {
            return m_MaximumParameterIndice;
        }

        public void SetGeneAt(int aIndex, int aGene)
        {
            m_ChromosomeBit[aIndex] = aGene;
        }

        public Chromosome Clone()
        {
            return new Chromosome(this);
        }
    }
}
