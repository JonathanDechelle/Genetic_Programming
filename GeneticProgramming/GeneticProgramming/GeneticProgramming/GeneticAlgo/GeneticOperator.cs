using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class GeneticOperator
    {
        private static double m_CrossOverPercent;
        private static double m_MutationPercent;

        public GeneticOperator(double aCrossOverPercent, double aMutationPercent) 
        {
            m_CrossOverPercent = aCrossOverPercent;
            m_MutationPercent = aMutationPercent;
        }

        /*enjambement : Possibilité de chance qu’un bit soit
                        échangé avec son homologue sur l’autre chromosome */
        public void CrossOver(Chromosome aChromosome)
        {
            double crossOverPossibility = Ressources.m_Random.NextDouble();
            if (crossOverPossibility > m_CrossOverPercent)
            {
                return;
            }

        }

        //mutation : Possibilité de chance q’un bit mute (passer de 1 à 0 ou de 0 à 1)
        public void Mutate()
        {
            double mutationPossibility = Ressources.m_Random.NextDouble();
            if (mutationPossibility > m_MutationPercent)
            {
                return;
            }
        }

        public static Chromosome ReproductionByRank(Population aPopulation)
        {
            Chromosome[] orderedChromosomeByRank = aPopulation.GetChromosomesOrderedByHighestPerformance();
            double[] rankScores = new double[aPopulation.m_PopulationCount];
            double sumRank = 0;
            for (int i = 0; i < aPopulation.m_PopulationCount; i++)
            {
                rankScores[i] = i + 1;
                sumRank += i;
            }

            Console.Write("\r\nReproductionByRank\r\n");
            ReproductionByAdaptation(
                orderedChromosomeByRank,
                rankScores, 
                sumRank);

            return null;
        }

        public static Chromosome ReproductionByRoulette(Chromosome[] aChromosomes, double[] aAdaptations, double aAdaptationSum)
        {
            Console.Write("\r\nBest in ReproductionByRoulette\r\n");
            return ReproductionByAdaptation(aChromosomes, aAdaptations, aAdaptationSum);
        }

        private static Chromosome ReproductionByAdaptation(Chromosome[] aChromosomes, double[] aAdaptations, double aAdaptationSum)
        {
            double randomPercent = Ressources.m_Random.NextDouble();
            double currentAdaptationSum = 0;
            double adaptationPercent = 0;
            Chromosome currentChromosome = null;

            for (int i = 0; i < aChromosomes.Length; i++)
            {
                currentAdaptationSum += aAdaptations[i];

                adaptationPercent = currentAdaptationSum / aAdaptationSum;
                if (adaptationPercent > randomPercent)
                {
                    currentChromosome = aChromosomes[i];
                    break;
                }
            }
            
            string debutText = currentChromosome == null ?
                "No chromosome is good" : 
                currentChromosome.ToString();
            Console.WriteLine(debutText);

            return currentChromosome;
        }

        public static void ReproductionByTournamenent(Population aPopulation)
        {
            int randomStartIndex = Ressources.m_Random.Next(aPopulation.m_PopulationCount);
            double chanceToBeSelected = 0.90;

            List<Chromosome> chromosomesWinner = new List<Chromosome>();
            for (int i = randomStartIndex; i < aPopulation.m_PopulationCount; i += 2)
            {
                Chromosome winner =
                    aPopulation.m_Chromosomes[i].m_Adaptation > aPopulation.m_Chromosomes[i + 1].m_Adaptation ?
 
                aPopulation.m_Chromosomes[i] :
                aPopulation.m_Chromosomes[i + 1];

                chromosomesWinner.Add(winner);
            }

            for (int i = 0; i < chromosomesWinner.Count; i++)
            {
                double randomSelection = Ressources.m_Random.NextDouble();
                if (randomSelection < chanceToBeSelected)
                {
                    chromosomesWinner.RemoveAt(i);
                }
            }
        }
    }
}
