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
            #region Debug Stuff
            string outputDebug = "";
            #endregion

            m_ChromosomeBit = new byte[aBitCount];
            byte randomByte;
            for (int i = 0; i < aBitCount; i++)
            {
                randomByte = Convert.ToByte(Ressources.m_Random.Next(2));
                m_ChromosomeBit[i] = randomByte;

                #region Debug Stuff
                outputDebug += randomByte;
                #endregion
            }

            #region Debug Stuff
            Console.WriteLine(outputDebug);
            #endregion
        }
    }
}
