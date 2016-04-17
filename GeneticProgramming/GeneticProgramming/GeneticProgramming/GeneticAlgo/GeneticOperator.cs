﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class GeneticOperator
    {
        private Random m_Random = new Random();
        private double m_CrossOverPercent;
        private double m_MutationPercent;

        public GeneticOperator(double aCrossOverPercent, double aMutationPercent) 
        {
            m_CrossOverPercent = aCrossOverPercent;
            m_MutationPercent = aMutationPercent;
        }

        /*enjambement : Possibilité de chance qu’un bit soit
                        échangé avec son homologue sur l’autre chromosome */
        public void CrossOver(Chromosome aChromosome)
        {
            double crossOverPossibility = m_Random.NextDouble();
            if (crossOverPossibility > m_CrossOverPercent)
            {
                return;
            }

        }

        //mutation : Possibilité de chance q’un bit mute (passer de 1 à 0 ou de 0 à 1)
        public void Mutate()
        {
            double mutationPossibility = m_Random.NextDouble();
            if (mutationPossibility > m_MutationPercent)
            {
                return;
            }
        }
    }
}