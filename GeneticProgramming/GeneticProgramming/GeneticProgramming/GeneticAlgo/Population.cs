﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Population
    {
        protected double m_AdaptationSum;
        protected Chromosome[] m_Chromosomes;
        private double m_MaxAdaptation;
        private double m_MinAdaptation;

        public Population(int aPopulationCount)
        {
            m_Chromosomes = new Chromosome[aPopulationCount];
        }

        public Population(SituationData aSituationData) : this(aSituationData.m_ChromosomesPerGeneration)
        {

        }

        public void GenerateAdditionalPopulation(int aMaxTry, int aParametersPerChromosomes)
        {
            int startIndex = GetCount();
            for (int i = startIndex; i < m_Chromosomes.Length; i++)
            {
                m_Chromosomes[i] = new Chromosome(aMaxTry, aParametersPerChromosomes);
            }
        }

        public void GenerateAdditionalPopulation(SituationData aSituationData)
        {
            GenerateAdditionalPopulation(aSituationData.m_MaxTry, aSituationData.m_ParametersPerChromosomes);
        }

        public Chromosome GetChromosomeAt(int aIndex)
        {
            return m_Chromosomes[aIndex];
        }

        public Chromosome[] GetChromosomes()
        {
            List<Chromosome> finalChromosomes = new List<Chromosome>();
            for (int i = 0; i < m_Chromosomes.Length; i++)
            {
                if (m_Chromosomes[i] != null)
                {
                    finalChromosomes.Add(m_Chromosomes[i]);
                }
            }

            return finalChromosomes.ToArray();
        }

        public void SetChromosomes(Chromosome[] aChromosomes)
        {
            m_Chromosomes = aChromosomes;
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
        }

        public void RemoveChromosomes(Chromosome[] aChromosomesToRemove)
        {
            int arrayIndex = 0;
            for (int i = 0; i < aChromosomesToRemove.Length; i++)
            {
                for (int j = 0; j < m_Chromosomes.Length; j++)
                {
                    if (m_Chromosomes[j] == aChromosomesToRemove[i])
                    {
                        m_Chromosomes[j] = null;
                    }
                }
            }

            ReplaceElements(m_Chromosomes);
        }

        private void ReplaceElements(Chromosome[] aChromosomes)
        {
            int indexToMove = 0;
            for (int i = 0; i < aChromosomes.Length; i++)
            {
                if (aChromosomes[i] != null)
                {
                    Chromosome oldChromosome = aChromosomes[i];
                    aChromosomes[i] = null;
                    aChromosomes[i - indexToMove] = oldChromosome;
                    continue;
                }

                indexToMove++;
            }
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

        private void SetAdaptationExtremum()
        {
            m_MinAdaptation = GetMinAdaptation();
            m_MaxAdaptation = GetMaxAdaptation();
        }

        public virtual void ComputeAdaptation()
        {
            /* redefined by parent */
            SetAdaptationExtremum();
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
            int count = GetCount();
            for (int i = 0; i < count; i++)
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
            int count = GetCount();
            for (int i = 0; i < count; i++)
            {
                if (m_Chromosomes[i].m_Adaptation > max)
                {
                    max = m_Chromosomes[i].m_Adaptation;
                }
            }

            return max;
        }

        public Chromosome GetBestChromosome()
        {
            double max = double.MinValue;
            int maxIndex = 0;
            int count = GetCount();
            for (int i = 0; i < count; i++)
            {
                if (m_Chromosomes[i].m_Adaptation > max)
                {
                    max = m_Chromosomes[i].m_Adaptation;
                    maxIndex = i;
                }
            }

            return m_Chromosomes[maxIndex];
        }

        public Chromosome[] GetChromosomesOrderedByLowestPerformance()
        {
            int populationCount = GetCount();
            Chromosome[] chromosomes = new Chromosome[populationCount];
            int lowerThan;

            for (int i = 0; i < populationCount; i++)
            {
                lowerThan = populationCount - 1;
                for (int j = 0; j < populationCount; j++)
                {
                    if (i == j)
                    {
                        continue; //excluded himself
                    }

                    if (m_Chromosomes[j].m_Adaptation > m_Chromosomes[i].m_Adaptation)
                    {
                        lowerThan--;
                    }
                }

                while (chromosomes[lowerThan] != null)
                {
                    lowerThan--;
                }

                chromosomes[lowerThan] = m_Chromosomes[i];
            }

            return chromosomes;
        }

        public Chromosome[] GetChromosomesOrderedByHighestPerformance()
        {
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

                    if (m_Chromosomes[j].m_Adaptation > m_Chromosomes[i].m_Adaptation)
                    {
                        higherThan++;
                    }
                }

                while (chromosomes[higherThan] != null)
                {
                    higherThan++;
                }

                chromosomes[higherThan] = m_Chromosomes[i];
            }

            return chromosomes;
        }

        public override string ToString()
        {
            Console.WriteLine("fitness max = " + GetMaxAdaptation());
            return "";
        }
    }
}
