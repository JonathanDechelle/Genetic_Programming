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
        private int m_NbParameter;

        #region DebugStuff
        private string m_Bytes = "";
        private const string CHROMOSOME_STRING_FORMAT = "Bytes = {0} Adaptation = {1}";
        #endregion

        public Chromosome(int aBitCount, int aNbParameter)
        {
            m_ChromosomeBit = new int[aBitCount];
            for (int i = 0; i < aBitCount; i++)
            {
                m_ChromosomeBit[i] = Convert.ToByte(Ressources.m_Random.Next(aNbParameter));
            }

            m_NbParameter = aNbParameter;
            RebuildDebugText();
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

            m_NbParameter = aChromosome.m_NbParameter;

            RebuildDebugText();
        }

        private void RebuildDebugText()
        {
            m_Bytes = "";
            for (int i = 0; i < m_ChromosomeBit.Length; i++)
            {
                m_Bytes += m_ChromosomeBit[i];
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

        public int GetNumberOfParameter()
        {
            return m_NbParameter;
        }

        public void SetGeneAt(int aIndex, int aGene)
        {
            m_ChromosomeBit[aIndex] = aGene;
            RebuildDebugText();
        }

        public override string ToString()
        {
            return string.Format(CHROMOSOME_STRING_FORMAT, m_Bytes, m_Adaptation);
        }

        public Chromosome Clone()
        {
            return new Chromosome(this);
        }
    }
}
