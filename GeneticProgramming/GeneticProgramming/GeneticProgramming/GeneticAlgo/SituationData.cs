using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class SituationData
    {
        public SituationData(){}

        public int m_ParametersPerChromosomes;
        public int m_ChromosomesPerGeneration;
        public int m_NumbersPairForReproduction;
        public int m_AdditionalTry;
        public int m_MaximumFitness;
        public double m_CurrentMaxAdaptation;
        public double m_BestChromosomeByXGeneration;
        public float m_MutationPercent;
        public float m_CrossOverPercent;
        public float m_ReproductionPercent;

        public int m_MaxTry
        {
            get
            {
                return m_AdditionalTry + m_MaximumFitness;
            }
        }
    }
}
