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

        public double[] GetAdaptationOrderedByHighestPerformance()
        {
            int count = m_PopulationCount;
            double[] adaptations = new double[count];
            double[] tempAdaptations = new double[count];

            for (int i = 0; i < count; i++)
            {
                tempAdaptations[i] = m_Chromosomes[i].m_Adaptation;

                if (i == 0)
                {
                    adaptations[i] = tempAdaptations[i];
                    continue;
                }

                for (int j = 0; j < i; j++)
                {
                    if (adaptations[j] >= tempAdaptations[i])
                    {
                        double temp = adaptations[j];
                        adaptations[j] = tempAdaptations[i];
                        adaptations[i] = temp;
                    }
                    else
                    {
                        adaptations[i] = tempAdaptations[i];
                    }
                }
            }

            return adaptations;
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
