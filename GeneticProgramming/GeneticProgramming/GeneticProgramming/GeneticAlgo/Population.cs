using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Population
    {
        private double m_AdaptationSum;
        private int m_PopulationCount = 10;
        private int m_ChromosomeBitCount = 4;
        private Chromosome[] m_Chromosomes;
        
        public void GeneratePopulation()
        {
            Console.WriteLine("\r\nGeneratePopulation count = " + m_PopulationCount);

            m_Chromosomes = new Chromosome[m_PopulationCount];
            for (int i = 0; i < m_PopulationCount; i++)
            {
                m_Chromosomes[i] = new Chromosome(m_ChromosomeBitCount);
            }
        }

        public Chromosome GetChromosomeAt(int aIndex)
        {
            return m_Chromosomes[aIndex];
        }

        public Chromosome[] GetChromosomes()
        {
            return m_Chromosomes;
        }

        public int GetCount()
        {
            return m_PopulationCount;
        }

        public double GetAdaptationTotal()
        {
            return m_AdaptationSum;
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

        public double[] GetCurrentPopulationAdaptation()
        {
            double[] adaptations = new double[m_PopulationCount];
            for (int i = 0; i < m_PopulationCount; i++)
            {
                adaptations[i] = m_Chromosomes[i].m_Adaptation;
            }

            return adaptations;
        }

        public Chromosome[] GetChromosomesOrderedByHighestPerformance()
        {
            GetChromosomesOrderedByLowestPerformance();
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

        public Chromosome[] GetChromosomesOrderedByLowestPerformance()
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
