using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Population
    {
        public double m_AdaptationSum;
        public int m_PopulationCount = 10;
        public List<Chromosome> m_Individuals = new List<Chromosome>();

        private int m_ChromosomeBitCount = 4;

        public void GeneratePopulation()
        {
            Console.WriteLine("\r\nGeneratePopulation count = " + m_PopulationCount);
            for (int i = 0; i < m_PopulationCount; i++)
            {
                Chromosome individual = new Chromosome(m_ChromosomeBitCount);
                m_Individuals.Add(individual);
            }
        }

        public void ComputeAdaptation()
        {
            m_AdaptationSum = 0;
            for (int i = 0; i < m_PopulationCount; i++)
            {
                /* TO DO  CALCUL*/
                //Fake calcul for now
                double randomAdaptation = Ressources.m_Random.Next(500);
                m_Individuals[i].m_Adaptation = randomAdaptation;
                m_AdaptationSum += randomAdaptation;
            }
        }

        public override string ToString()
        {
            for (int i = 0; i < m_PopulationCount; i++)
            {
                Console.WriteLine(m_Individuals[i].ToString());
            }

            return "";
        }
    }
}
