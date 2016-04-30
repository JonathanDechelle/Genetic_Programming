using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Situation
    {
        private SituationData m_SituationData;

        public Situation(SituationData aSituationData) 
        {
            m_SituationData = aSituationData;
        }

        public bool HasSucceeded
        {
            get
            {
                return m_SituationData.m_CurrentMaxAdaptation >= m_SituationData.m_MaximumFitness;
            }
        }

        private List<Chromosome> m_BestChromosomesInGenerations = new List<Chromosome>();
        public List<Chromosome> GetBestChromosomes()
        {
            return m_BestChromosomesInGenerations;
        }

        public void AddABestChromosome(Chromosome aChromosome)
        {
            if (!m_BestChromosomesInGenerations.Contains(aChromosome))
            {
                m_BestChromosomesInGenerations.Add(aChromosome);
            }
        }

        public double GetBestAdaptation()
        {
            double max = double.MinValue;
            int count = m_BestChromosomesInGenerations.Count;
            for (int i = 0; i < count; i++)
            {
                double adaptation = m_BestChromosomesInGenerations[i].m_Adaptation;
                if (adaptation > max)
                {
                    max = adaptation;
                }
            }

            return max;
        }
    }
}
