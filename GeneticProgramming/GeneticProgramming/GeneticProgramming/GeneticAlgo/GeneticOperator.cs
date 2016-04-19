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
        private const int PAIR_GAP = 2;

        public GeneticOperator(double aCrossOverPercent, double aMutationPercent) 
        {
            m_CrossOverPercent = aCrossOverPercent;
            m_MutationPercent = aMutationPercent;
        }

        private void SetMutationProbability(float aPercent)
        {
            m_MutationPercent = aPercent;
        }

        /*enjambement : Possibilité de chance qu’un bit soit
                        échangé avec son homologue sur l’autre chromosome */
        public static void CrossOver1Point(Population aPopulation, float aPercentChromosomeUsed)
        {
            Console.WriteLine("CrossOver1Point");
            int affectedChromosomes = (int)(aPopulation.GetCount() * aPercentChromosomeUsed);
            while (affectedChromosomes > PAIR_GAP - 1)
            {
                Console.WriteLine("\r\nSelect 2 Chromosomes");
                Chromosome chromosome1 = ReproductionByRoulette(aPopulation, false);
                Chromosome chromosome2 = null;

                while (chromosome2 == null || chromosome2 == chromosome1)
                {
                    chromosome2 = ReproductionByRoulette(aPopulation, false);
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
        public void Mutate(Population aPopulation)
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

        public static Chromosome[] ReproductionByTournamenent(Population aPopulation, int aMinChromosomes)
        {
            int populationCount = aPopulation.GetCount();
            int randomStartIndex = Ressources.m_Random.Next(aMinChromosomes - PAIR_GAP);
            if (randomStartIndex < aMinChromosomes)
            {
                randomStartIndex = aMinChromosomes;
            }

            double chanceToBeSelected = 0.90;

            List<Chromosome> chromosomesWinner = new List<Chromosome>();
            for (int i = randomStartIndex; i > 0 + PAIR_GAP; i -= PAIR_GAP)
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
                if (randomSelection > chanceToBeSelected)
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
