using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Population
    {
        private List<Chromosome> m_Individuals = new List<Chromosome>();
        private int m_PopulationCount = 10;
        private int m_ChromosomeBitCount = 4;

        public void GeneratePopulation()
        {
            for (int i = 0; i < m_PopulationCount; i++)
            {
                Chromosome individual = new Chromosome(m_ChromosomeBitCount);
                m_Individuals.Add(individual);
            }
        }
    }
}
