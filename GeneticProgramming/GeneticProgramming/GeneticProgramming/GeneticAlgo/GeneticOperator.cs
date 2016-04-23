using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class GeneticOperator
    {
        private static float m_MutationPercent;
        private static float m_CrossOverPercent;
        private static float m_SelectionForReproductionPercent;
        private const int PAIR_GAP = 2;

        public static void SetMutationProbability(float aPercent)
        {
            m_MutationPercent = aPercent;
        }

        public static void SetCrossOverOnChromosomeProbability(float aPercent)
        {
            m_CrossOverPercent = aPercent;
        }

        public static void SetSelectionForReproductionProbability(float aPercent)
        {
            m_SelectionForReproductionPercent = aPercent;
        }

        /*enjambement : Possibilité de chance qu’un bit soit
                        échangé avec son homologue sur l’autre chromosome */
        public static void CrossOver1Point(Population aPopulation)
        {
            int affectedChromosomes = (int)(aPopulation.GetCount() * m_CrossOverPercent);
            while (affectedChromosomes > PAIR_GAP - 1)
            {
                Chromosome chromosome1 = aPopulation.GetRandomChromosome();
                Chromosome chromosome2 = null;

                while (chromosome2 == null || chromosome2 == chromosome1)
                {
                    chromosome2 = aPopulation.GetRandomChromosome();
                }
                
                //get cross Point
                int maxPoint = chromosome1.GetLenght();
                int crossPoint = Ressources.m_Random.Next(0, maxPoint);
                Chromosome clonedChromosome1 = chromosome1.Clone();
                Chromosome clonedChromosome2 = chromosome2.Clone();

                for (int i = crossPoint; i < maxPoint; i++)
                {
                    int c1Gene = clonedChromosome1.GetGeneAt(i);
                    int c2Gene = clonedChromosome2.GetGeneAt(i);
                    clonedChromosome1.SetGeneAt(i, c2Gene);
                    clonedChromosome2.SetGeneAt(i, c1Gene);
                }

                affectedChromosomes -= PAIR_GAP;
            }
        }

        //mutation : Possibilité de chance q’un bit mute (passer de 1 à 0 ou de 0 à 1)
        public static void Mutate(Population aPopulation)
        {
            for (int i = 0; i < aPopulation.GetCount(); i++)
            {
                Chromosome chromosome = aPopulation.GetChromosomeAt(i);
                for (int j = 0; j < chromosome.GetLenght(); j++)
                {
                    double mutationPossibility = Ressources.m_Random.NextDouble();
                    if (mutationPossibility > m_MutationPercent)
                    {
                        continue;
                    }

                    int gene = chromosome.GetGeneAt(j);
                    gene = Math.Abs(gene - 1);
                    chromosome.SetGeneAt(j, gene);
                }
            }
        }

        public static Chromosome ReproductionByRank(Population aPopulation)
        {
            int populationCount = aPopulation.GetCount();
            Chromosome[] orderedChromosomeByRank = aPopulation.GetChromosomesOrderedByHighestPerformance();
            double[] rankScores = new double[populationCount];
            double sumRank = 0;
            for (int i = 0; i < populationCount; i++)
            {
                rankScores[i] = i + 1;
                sumRank += i;
            }

            return ReproductionByAdaptation(
                orderedChromosomeByRank,
                rankScores, 
                sumRank);
        }

        public static Chromosome ReproductionByRoulette(Population aPopulation)
        {
            return ReproductionByAdaptation(
                aPopulation.GetChromosomes(), 
                aPopulation.GetCurrentPopulationAdaptation(), 
                aPopulation.GetAdaptationTotal());
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

            return currentChromosome;
        }

        public static Chromosome[] ReproductionByTournamenent(Population aPopulation, int aNbPair)
        {
            int minChromomes = aNbPair * PAIR_GAP;
            if (minChromomes > aPopulation.GetCount())
            {
                return null;
            }

            List<Chromosome> chromosomesWinner = new List<Chromosome>();
            List<Chromosome> chromosomesLooser = new List<Chromosome>();

            for (int i = 0; i < minChromomes; i += PAIR_GAP)
            {
                Chromosome currentChromosome = aPopulation.GetChromosomeAt(i);
                Chromosome nextChromosome = aPopulation.GetChromosomeAt(i + 1);

                bool currentWin = currentChromosome.m_Adaptation > nextChromosome.m_Adaptation; 
                Chromosome winner =
                    currentWin ?
                currentChromosome :
                nextChromosome;

                Chromosome looser =
                    currentWin ?
                nextChromosome :
                currentChromosome;
                
                chromosomesWinner.Add(winner);
                chromosomesLooser.Add(looser);
            }

            for (int i = 0; i < chromosomesWinner.Count; i++)
            {
                double randomSelection = Ressources.m_Random.NextDouble();
                if (randomSelection > m_SelectionForReproductionPercent)
                {
                    chromosomesWinner.RemoveAt(i);
                }
                else
                {
                    aPopulation.RemoveChromosomes(chromosomesWinner.ToArray());
                }
            }

            aPopulation.RemoveChromosomes(chromosomesLooser.ToArray());

            return chromosomesWinner.ToArray();
        }
    }
}
