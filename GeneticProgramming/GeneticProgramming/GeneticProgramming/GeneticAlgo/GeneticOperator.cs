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

        public static void ReproductionByRank(Population aPopulation)
        {

        }

        public static Chromosome ReproductionByRoulette(Population aPopulation)
        {
            double randomPercent = Ressources.m_Random.NextDouble();
            double currentAdaptationSum = 0;
            double adaptationPercent = 0;
            Chromosome currentChromosome = null;
            Console.Write("\r\nBest in ReproductionByRoulette\r\n");

            for (int i = 0; i < aPopulation.m_PopulationCount; i++)
            {
                currentChromosome = aPopulation.m_Individuals[i];
                currentAdaptationSum += currentChromosome.m_Adaptation;

                adaptationPercent = currentAdaptationSum / aPopulation.m_AdaptationSum;
                if (adaptationPercent > randomPercent)
                {
                    break;
                }
            }

            string debutText = currentChromosome == null ?
                "No chromosome is good" : 
                currentChromosome.ToString();
            Console.WriteLine(debutText);
            Console.Write("\r\n\r\n");

            return currentChromosome;
        }

        public static void ReproductionByTournamenent(Population aPopulation)
        {
            
        }
    }
}
