using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GeneticProgramming
{
    public class Chromosome
    {
        private byte[] m_ChromosomeBit;
        private Random m_Random = Ressources.m_Random;

        public Chromosome(int aBitCount)
        {
            m_ChromosomeBit = new byte[aBitCount];
            byte randomByte;
            for (int i = 0; i < aBitCount; i++)
            {
                randomByte = Convert.ToByte(m_Random.Next(2));
                m_ChromosomeBit[i] = randomByte;
            }
        }
    }
}
