using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Population
    {
        private double m_AdaptationSum;
        private int m_ChromosomeBitCount = 4;
        private Chromosome[] m_Chromosomes;
        
        public void GeneratePopulation(int aPopulationCount)
        {
            m_Chromosomes = new Chromosome[aPopulationCount];
            for (int i = 0; i < aPopulationCount; i++)
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

        public void SetChromosomes(Chromosome[] aChromosomes)
        {
            m_Chromosomes = aChromosomes;
        }

        public int GetCount()
        {
            return m_Chromosomes.Length;
        }

        public double GetAdaptationTotal()
        {
            return m_AdaptationSum;
        }

        public void ComputeAdaptation()
        {
            m_AdaptationSum = 0;
            int populationCount = GetCount();
            for (int i = 0; i < populationCount; i++)
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
            int populationCount = GetCount();
            double[] adaptations = new double[populationCount];
            for (int i = 0; i < populationCount; i++)
            {
                adaptations[i] = m_Chromosomes[i].m_Adaptation;
            }

            return adaptations;
        }

        public Chromosome[] GetChromosomesOrderedByHighestPerformance()
        {
            GetChromosomesOrderedByLowestPerformance();
            int populationCount = GetCount();
            Chromosome[] chromosomes = new Chromosome[populationCount];
            int higherThan;

            for (int i = 0; i < populationCount; i++)
            {
                higherThan = 0;
                for (int j = 0; j < populationCount; j++)
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
            int populationCount = GetCount();
            Chromosome[] chromosomes = new Chromosome[populationCount];
            int lowerThan;

            for (int i = 0; i < populationCount; i++)
            {
                lowerThan = 0;
                for (int j = 0; j < populationCount; j++)
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
            int populationCount = GetCount();
            Console.WriteLine("\r\nPopulation count = " + populationCount);
            for (int i = 0; i < populationCount; i++)
            {
                Console.WriteLine(m_Chromosomes[i].ToString());
            }

            return "";
        }
    }
}
