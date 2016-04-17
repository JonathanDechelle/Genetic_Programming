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

        #region DebugStuff
        private string m_Bytes = "";
        private const string CHROMOSOME_STRING_FORMAT = "Bytes = {0} Adaptation = {1}";
        #endregion

        public Chromosome(int aBitCount)
        {
            m_ChromosomeBit = new byte[aBitCount];
            byte randomByte;
            for (int i = 0; i < aBitCount; i++)
            {
                randomByte = Convert.ToByte(Ressources.m_Random.Next(2));
                m_ChromosomeBit[i] = randomByte;

                #region Debug Stuff
                m_Bytes += randomByte;
                #endregion
            }
        }

        public override string ToString()
        {
            return string.Format(CHROMOSOME_STRING_FORMAT, m_Bytes, m_Adaptation);
        }
    }
}
