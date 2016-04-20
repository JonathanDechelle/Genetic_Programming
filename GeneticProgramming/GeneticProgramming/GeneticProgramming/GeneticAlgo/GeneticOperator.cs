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
        private static float m_ReproductionOnChromosomePercent;
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

        public static void SetReproductionOnChromosomeProbability(float aPercent)
        {
            m_ReproductionOnChromosomePercent = aPercent;
        }

        public static void SetSelectionForReproductionProbability(float aPercent)
        {
            m_SelectionForReproductionPercent = aPercent;
        }

        /*enjambement : Possibilité de chance qu’un bit soit
                        échangé avec son homologue sur l’autre chromosome */
        public static void CrossOver1Point(Population aPopulation)
        {
            Console.WriteLine("CrossOver1Point");
            int affectedChromosomes = (int)(aPopulation.GetCount() * m_CrossOverPercent);
            while (affectedChromosomes > PAIR_GAP - 1)
            {
                Console.WriteLine("\r\nSelect 2 Chromosomes");
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

                Console.WriteLine("\r\nAdded new Chromosomes");
                Console.WriteLine(clonedChromosome1.ToString());
                Console.WriteLine(clonedChromosome2.ToString());

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

            Console.WriteLine("Best Chromosome in Reproduction By Rank");
            return ReproductionByAdaptation(
                orderedChromosomeByRank,
                rankScores, 
                sumRank);
        }

        public static Chromosome ReproductionByRoulette(Population aPopulation, bool aDebug = true)
        {
            if (aDebug)
            {
                Console.WriteLine("Best Chromosome in Reproduction By Roulette");
            }
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
            
            string debutText = currentChromosome == null ?
                "No chromosome is good" : 
                currentChromosome.ToString();
            Console.WriteLine(debutText);

            return currentChromosome;
        }

        public static Chromosome[] ReproductionByTournamenent(Population aPopulation)
        {
            int populationCount = aPopulation.GetCount();
            int minChromosomes = (int)(populationCount * m_ReproductionOnChromosomePercent);
            int randomStartIndex = Ressources.m_Random.Next(minChromosomes);
            
            if (randomStartIndex < PAIR_GAP)
            {
                return null;
            }

            List<Chromosome> chromosomesWinner = new List<Chromosome>();
            for (int i = randomStartIndex; i > PAIR_GAP; i -= PAIR_GAP)
            {
                Chromosome currentChromosome = aPopulation.GetChromosomeAt(i);
                Chromosome nextChromosome = aPopulation.GetChromosomeAt(i + 1);

                Chromosome winner =
                    currentChromosome.m_Adaptation > nextChromosome.m_Adaptation ?
 
                currentChromosome :
                nextChromosome;

                chromosomesWinner.Add(winner);
            }

            Console.Write("\r\nBest Chromosomes in Reproduction By Tournament\r\n");
            for (int i = 0; i < chromosomesWinner.Count; i++)
            {
                double randomSelection = Ressources.m_Random.NextDouble();
                if (randomSelection > m_SelectionForReproductionPercent)
                {
                    chromosomesWinner.RemoveAt(i);
                }
                else
                {
                    Console.WriteLine(chromosomesWinner[i].ToString());
                }
            }

            return chromosomesWinner.ToArray();
        }
    }
}
