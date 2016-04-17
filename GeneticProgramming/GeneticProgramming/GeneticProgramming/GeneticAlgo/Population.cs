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
        public List<Chromosome> m_Chromosomes = new List<Chromosome>();

        private int m_ChromosomeBitCount = 4;

        public void GeneratePopulation()
        {
            Console.WriteLine("\r\nGeneratePopulation count = " + m_PopulationCount);
            for (int i = 0; i < m_PopulationCount; i++)
            {
                Chromosome chromosome = new Chromosome(m_ChromosomeBitCount);
                m_Chromosomes.Add(chromosome);
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
                m_Chromosomes[i].m_Adaptation = randomAdaptation;
                m_AdaptationSum += randomAdaptation;
            }
        }

        public Chromosome[] GetAdaptationOrderedByHighestPerformance()
        {
            GetAdaptationOrderedByLowestPerformance();
            int count = m_PopulationCount;
            Chromosome[] chromosomes = new Chromosome[count];
            int higherThan;

            for (int i = 0; i < count; i++)
            {
                higherThan = 0;
                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                    {
                        continue; //excluded himself
                    }

                    if (m_Chromosomes[i].m_Adaptation > m_Chromosomes[j].m_Adaptation)
                    {
                        higherThan++;
                    }
                }

                chromosomes[higherThan] = m_Chromosomes[i];
            }

            return chromosomes;
        }

        public Chromosome[] GetAdaptationOrderedByLowestPerformance()
        {
            int count = m_PopulationCount;
            Chromosome[] chromosomes = new Chromosome[count];
            int lowerThan;

            for (int i = 0; i < count; i++)
            {
                lowerThan = 0;
                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                    {
                        continue; //excluded himself
                    }

                    if (m_Chromosomes[i].m_Adaptation < m_Chromosomes[j].m_Adaptation)
                    {
                        lowerThan++;
                    }
                }

                chromosomes[lowerThan] = m_Chromosomes[i];
            }

            return chromosomes;
        }

        public override string ToString()
        {
            for (int i = 0; i < m_PopulationCount; i++)
            {
                Console.WriteLine(m_Chromosomes[i].ToString());
            }

            return "";
        }
    }
}
