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
        private byte[] m_ChromosomeBit;

        public Chromosome(int aBitCount)
        {
            m_ChromosomeBit = new byte[aBitCount];
            byte randomByte;
            string outputDebug = "";
            for (int i = 0; i < aBitCount; i++)
            {
                randomByte = Convert.ToByte(Ressources.m_Random.Next(2));
                outputDebug += randomByte;

                m_ChromosomeBit[i] = randomByte;
            }

            Console.WriteLine(outputDebug);
        }
    }
}
