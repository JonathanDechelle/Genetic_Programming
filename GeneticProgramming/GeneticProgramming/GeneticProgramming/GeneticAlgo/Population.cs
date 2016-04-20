using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Population
    {
        private double m_AdaptationSum;
        private double m_MaxAdaptation;
        private double m_MinAdaptation;
        private int m_ChromosomeBitCount = 4;
        private int m_Count;
        private Chromosome[] m_Chromosomes;

        public Population(int aPopulationCount)
        {
            m_Chromosomes = new Chromosome[aPopulationCount];
        }

        public void GeneratePopulation(int aNbParamaters)
        {
            for (int i = 0; i < m_Chromosomes.Length; i++)
            {
                m_Chromosomes[i] = new Chromosome(m_ChromosomeBitCount, aNbParamaters);
            }
        }

        public Chromosome GetChromosomeAt(int aIndex)
        {
            return m_Chromosomes[aIndex];
        }

        public Chromosome GetRandomChromosome()
        {
            int aIndex = Ressources.m_Random.Next(GetCount());
            return GetChromosomeAt(aIndex);
        }

        public Chromosome[] GetChromosomes()
        {
            return m_Chromosomes;
        }

        public void SetChromosomes(Chromosome[] aChromosomes)
        {
            m_Chromosomes = aChromosomes;
            m_Count = GetCount();
        }

        public void AddChromosomes(Chromosome[] aNewChromosomes)
        {
            int arrayIndex = 0;
            for (int i = 0; i < m_Chromosomes.Length; i++)
            {
                if (m_Chromosomes[i] == null)
                {
                    if (arrayIndex >= aNewChromosomes.Length)
                    {
                        break;
                    }

                    m_Chromosomes[i] = aNewChromosomes[arrayIndex];
                    arrayIndex++;
                }
            }

            m_Count = GetCount();
        }

        public int GetCount()
        {
            int count = 0;
            for (int i = 0; i < m_Chromosomes.Length; i++)
            {
                if (m_Chromosomes[i] != null)
                {
                    count++;
                }
            }

            return count;
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

            m_MinAdaptation = GetMinAdaptation();
            m_MaxAdaptation = GetMaxAdaptation();
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

        public double GetMinAdaptation()
        {
            double min = double.MaxValue;
            for (int i = 0; i < GetCount(); i++)
            {
                if (m_Chromosomes[i].m_Adaptation < min)
                {
                    min = m_Chromosomes[i].m_Adaptation;
                }
            }

            return min;
        }

        public double GetMaxAdaptation()
        {
            double max = double.MinValue;
            for (int i = 0; i < GetCount(); i++)
            {
                if (m_Chromosomes[i].m_Adaptation > max)
                {
                    max = m_Chromosomes[i].m_Adaptation;
                }
            }

            return max;
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

            Console.WriteLine("");
            return "";
        }
    }
}
